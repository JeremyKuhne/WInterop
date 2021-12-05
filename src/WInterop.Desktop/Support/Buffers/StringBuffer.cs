// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.SafeString.Native;

namespace WInterop.Support.Buffers;

/// <summary>
///  Native buffer that deals in char size increments. Dispose to free memory. Allows buffers larger
///  than a maximum size string to enable working with very large string arrays.
///
///  A more performant replacement for StringBuilder when performing native interop.
/// </summary>
/// <remarks>
///  Suggested use through P/Invoke: define DllImport arguments that take a character buffer as IntPtr.
///  NativeStringBuffer has an implicit conversion to IntPtr.
/// </remarks>
public class StringBuffer : HeapBuffer
{
    // While uint (UInt32) isn't CLS compliant it matches up better with interop scenarios. Windows typically
    // uses DWORD in it's interfaces, which is also an unsigned 32 bit integer.
    //
    // NativeBuffer uses ulong (UInt64) to allow for memory blocks that are much larger than 4GB (Windows uses
    // SIZE_T which is the pointer size ULONG_PTR, e.g. 32 or 64). Allowing string buffers that up to 8GB in size
    // (uint * sizeof(char)) should be way over the size of what one would need. Allowing ulong or long doesn't
    // match up with DWORD and requires bounds checks that are avoidable with uint.
    //
    // Natively Windows strings can never go over ushort bytes (UInt16) which is 64KB or 32K characters. .NET's
    // string can't go over int in size (4GB). While uint chars won't fit in a String, it is, again the native
    // type size used in most Windows APIs that take a size in chars.
    //
    // Note that a number of Windows APIs return null delimited lists of strings. Given the normal maximum Windows
    // string size (32K chars) we could handle around 128K strings in a single StringBuffer.
    private uint _length;

    /// <summary>
    ///  Create and empty StringBuffer.
    /// </summary>
    public StringBuffer()
        : this(initialCharCapacity: 0)
    {
    }

    /// <summary>
    ///  Instantiate the buffer with capacity for at least the specified number of characters. Capacity
    ///  includes the trailing null character.
    /// </summary>
    public StringBuffer(uint initialCharCapacity)
        : base(initialCharCapacity * sizeof(char))
    {
    }

    /// <summary>
    ///  Instantiate the buffer with a copy of the specified string.
    /// </summary>
    public StringBuffer(string initialContents)
        : base(0)
    {
        // We don't pass the count of bytes to the base constructor, appending will
        // initialize to the correct size for the specified initial contents.
        if (initialContents is not null)
        {
            Append(initialContents);
        }
    }

    /// <summary>
    ///  Get/set the character at the given index.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if attempting to index outside of the buffer length.</exception>
    public unsafe char this[uint index]
    {
        // We only need a read lock here to avoid accessing old memory after a resize (as the block may move). The actual read/write is atomic.
        get
        {
            using var readLock = _handleLock.Lock(Locks.Type.Read);

            return index >= _length
                ? throw new ArgumentOutOfRangeException(nameof(index))
                : CharPointer[index];
        }
        set
        {
            using var readLock = _handleLock.Lock(Locks.Type.Read);
            if (index >= _length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            CharPointer[index] = value;
        }
    }

    /// <summary>
    ///  Character capacity of the buffer. Includes the count for the trailing null character.
    /// </summary>
    public uint CharCapacity
    {
        get
        {
            ulong byteCapacity = ByteCapacity;
            ulong charCapacity = byteCapacity == 0 ? 0 : byteCapacity / sizeof(char);
            return charCapacity > uint.MaxValue ? uint.MaxValue : (uint)charCapacity;
        }
    }

    /// <summary>
    ///  Ensure capacity in characters is at least the given minimum.
    /// </summary>
    /// <exception cref="OverflowException">Thrown if trying to allocate more than a int on 32bit.</exception>
    public void EnsureCharCapacity(uint minCapacity) => EnsureByteCapacity((ulong)minCapacity * sizeof(char));

    protected void UnlockedEnsureCharCapacity(uint minCapacity)
        => UnlockedEnsureByteCapacity((ulong)minCapacity * sizeof(char));

    /// <summary>
    ///  The logical length of the buffer in characters. (Does not include the final null.) Will automatically
    ///  attempt to increase capacity. This is where the usable data ends.
    /// </summary>
    public uint Length
    {
        get => _length;
        set
        {
            using var writeLock = _handleLock.Lock(Locks.Type.Write);
            UnlockedSetLength(value);
        }
    }

    protected unsafe void UnlockedSetLength(uint length)
    {
        UnlockedEnsureCharCapacity(length + 1);

        // Null terminate
        _length = length;
        CharPointer[length] = '\0';
    }

    /// <summary>
    ///  For use when the native api null terminates but doesn't return a length.
    ///  If no null is found, the length will not be changed.
    /// </summary>
    public unsafe void SetLengthToFirstNull()
    {
        using var writeLock = _handleLock.Lock(Locks.Type.Write);

        char* buffer = CharPointer;
        uint capacity = CharCapacity;
        for (uint i = 0; i < capacity; i++)
        {
            if (buffer[i] == '\0')
            {
                _length = i;
                break;
            }
        }
    }

    public unsafe char* CharPointer => (char*)VoidPointer;

    public unsafe ushort* UShortPointer => (ushort*)VoidPointer;

    /// <summary>
    ///  Check for the index of a specified character.
    /// </summary>
    /// <param name="value">Character to look for.</param>
    /// <param name="index">
    ///  Index the character was found at if true is returned. Will be >Length if false.
    /// </param>
    /// <param name="skip">Skip the given number of characters before looking.</param>
    /// <returns>True if the given character was found.</returns>
    public unsafe bool IndexOf(char value, out uint index, uint skip = 0)
    {
        using var readLock = _handleLock.Lock(Locks.Type.Read);

        for (index = skip; index < _length; index++)
        {
            if (CharPointer[index] == value)
                return true;
        }

        index = _length + 1;

        return false;
    }

    /// <summary>
    ///  Returns true if the buffer starts with the given string.
    /// </summary>
    public bool StartsWithOrdinal(string value)
    {
        return value is null
            ? throw new ArgumentNullException(nameof(value))
            : _length >= (uint)value.Length
            && SubStringEquals(value, startIndex: 0, count: value.Length);
    }

    /// <summary>
    ///  Returns true if the specified StringBuffer substring equals the given value.
    /// </summary>
    /// <param name="value">The value to compare against the specified substring.</param>
    /// <param name="startIndex">Start index of the sub string.</param>
    /// <param name="count">Length of the substring, or -1 to check all remaining.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  Thrown if <paramref name="startIndex"/> or <paramref name="count"/> are outside the range
    ///  of the buffer's length.
    /// </exception>
    public unsafe bool SubStringEquals(string value, uint startIndex = 0, int count = -1)
    {
        if (value is null) return false;
        if (count < -1) throw new ArgumentOutOfRangeException(nameof(count));

        using var readLock = _handleLock.Lock(Locks.Type.Read);
        uint realCount = count == -1 ? _length - startIndex : (uint)count;
        if (startIndex + realCount > _length) throw new ArgumentOutOfRangeException(nameof(count));

        int length = value.Length;

        // Check the substring length against the input length
        if (realCount != (uint)length) return false;

        fixed (char* valueStart = value)
        {
            char* bufferStart = CharPointer + startIndex;
            for (int i = 0; i < length; i++)
            {
                if (*bufferStart++ != valueStart[i]) return false;
            }
        }

        return true;
    }

    /// <summary>
    ///  Append the given character.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  Thrown if you try to append more characters than the StringBuffer can hold.
    /// </exception>
    public unsafe void Append(char value)
    {
        using var readLock = _handleLock.Lock(Locks.Type.UpgradableRead);

        // Length can't be changed while were in read lock
        uint oldLength = _length;
        if (oldLength == uint.MaxValue) throw new ArgumentOutOfRangeException(nameof(value));

        using var writeLock = _handleLock.Lock(Locks.Type.Write);
        UnlockedSetLength(oldLength + 1);
        CharPointer[oldLength] = value;
    }

    /// <summary>
    ///  Append a specified count of a given character.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  Thrown if you try to append more characters than the StringBuffer can hold.
    /// </exception>
    public unsafe void Append(char value, uint count)
    {
        using var readLock = _handleLock.Lock(Locks.Type.UpgradableRead);

        uint oldLength = _length;
        if (count >= uint.MaxValue - oldLength) throw new ArgumentOutOfRangeException(nameof(count));

        using var writeLock = _handleLock.Lock(Locks.Type.Write);

        UnlockedSetLength(oldLength + count);

        char* current = CharPointer + oldLength;

        while (((uint)current & 3) != 0 && count > 0)
        {
            *current++ = value;
            count--;
        }

        if (count > 1)
        {
            uint twoChars = (uint)(value << 16 | value);

            while (count > 1)
            {
                *((uint*)current) = twoChars;
                current += 2;
                count -= 2;
            }
        }

        if (count == 1)
        {
            *current = value;
        }
    }

    /// <summary>
    ///  Append the given string.
    /// </summary>
    /// <param name="value">The string to append.</param>
    /// <param name="startIndex">The index in the input string to start appending from.</param>
    /// <param name="count">The count of characters to copy from the input string, or -1 for all remaining.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  Thrown if <paramref name="startIndex"/> or <paramref name="count"/> are outside the range
    ///  of <paramref name="value"/> characters.
    /// </exception>
    public void Append(string value, int startIndex = 0, int count = -1)
    {
        using var readLock = _handleLock.Lock(Locks.Type.UpgradableRead);

        CopyFrom(
            bufferIndex: _length,
            source: value,
            sourceIndex: startIndex,
            count: count);
    }

    /// <summary>
    ///  Append the given buffer starting at the given buffer index.
    /// </summary>
    /// <param name="value">The buffer to append.</param>
    /// <param name="startIndex">The index in the input buffer to start appending from.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  Thrown if <paramref name="startIndex"/> is outside the range of <paramref name="value"/> characters.
    /// </exception>
    public void Append(StringBuffer value, uint startIndex = 0)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));

        // We don't want the length of the source to change so we need to read lock
        using var readLock = value._handleLock.Lock(Locks.Type.Read);
        Append(value, startIndex, value.Length - startIndex);
    }

    /// <summary>
    ///  Append the specified count of characters from the given buffer at the given start index.
    /// </summary>
    /// <param name="value">The buffer to append.</param>
    /// <param name="startIndex">The index in the input buffer to start appending from.</param>
    /// <param name="count">The count of characters to copy from the buffer.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  Thrown if <paramref name="startIndex"/> or <paramref name="count"/> are outside the range
    ///  of <paramref name="value"/> characters.
    /// </exception>
    public void Append(StringBuffer value, uint startIndex, uint count)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        if (count == 0) return;

        using var readLock = _handleLock.Lock(Locks.Type.UpgradableRead);

        // Our lock will be upgraded to a write lock in CopyTo
        value.CopyTo(
            bufferIndex: startIndex,
            destination: this,
            destinationIndex: _length,
            count: count);
    }

    /// <summary>
    ///  Copy contents to the specified buffer. Will grow the destination buffer if needed.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="destination"/> is null</exception>
    public unsafe void CopyTo(uint bufferIndex, StringBuffer destination, uint destinationIndex, uint count)
    {
        if (destination is null) throw new ArgumentNullException(nameof(destination));

        using var readLock = _handleLock.Lock(Locks.Type.Read);
        using var writeLock = destination._handleLock.Lock(Locks.Type.Write);

        if (destinationIndex > destination._length) throw new ArgumentOutOfRangeException(nameof(destinationIndex));
        if (_length < bufferIndex + count) throw new ArgumentOutOfRangeException(nameof(count));

        if (count == 0) return;
        uint lastIndex = destinationIndex + count;
        if (destination.Length < lastIndex) destination.UnlockedSetLength(lastIndex);

        Buffer.MemoryCopy(
            source: CharPointer + bufferIndex,
            destination: destination.CharPointer + destinationIndex,
            destinationSizeInBytes: checked((long)(destination.ByteCapacity + (destinationIndex * sizeof(char)))),
            sourceBytesToCopy: checked((long)count * sizeof(char)));
    }

    /// <summary>
    ///  Copy contents from the specified string into the buffer at the given index. Will grow the buffer if neeeded.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null</exception>
    public unsafe void CopyFrom(uint bufferIndex, string source, int sourceIndex = 0, int count = -1)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (sourceIndex < 0 || sourceIndex > source.Length) throw new ArgumentOutOfRangeException(nameof(sourceIndex));
        if (count < 0) count = source.Length - sourceIndex;
        if (count == 0) return;

        if (source.Length < sourceIndex + count) throw new ArgumentOutOfRangeException(nameof(count));

        using var writeLock = _handleLock.Lock(Locks.Type.Write);

        if (bufferIndex > _length) throw new ArgumentOutOfRangeException(nameof(bufferIndex));

        uint lastIndex = bufferIndex + (uint)count;
        if (_length < lastIndex) UnlockedSetLength(lastIndex);

        fixed (char* content = source)
        {
            Buffer.MemoryCopy(
                source: content + sourceIndex,
                destination: CharPointer + bufferIndex,
                destinationSizeInBytes: checked((long)(ByteCapacity + (bufferIndex * sizeof(char)))),
                sourceBytesToCopy: count * sizeof(char));
        }
    }

    /// <summary>
    ///  Split the contents into strings via the given split characters.
    /// </summary>
    /// <exception cref="OverflowException">Thrown if the substring is too big to fit in a string.</exception>
    public unsafe IEnumerable<string> Split(char splitCharacter, bool removeEmptyStrings = false)
    {
        var strings = new List<string>();

        using var readLock = _handleLock.Lock(Locks.Type.Read);
        char* start = CharPointer;
        char* current = start;

        uint length = _length;
        int splitLength;

        for (uint i = 0; i < length; i++)
        {
            if (splitCharacter == *current)
            {
                // Split
                splitLength = checked((int)(current - start));
                if (!removeEmptyStrings || splitLength > 0)
                    strings.Add(new string(value: start, startIndex: 0, length: splitLength));
                start = current + 1;
            }

            current++;
        }

        splitLength = checked((int)(current - start));
        if (!removeEmptyStrings || splitLength > 0)
            strings.Add(new string(value: start, startIndex: 0, length: splitLength));

        return strings;
    }

    /// <summary>
    ///  Split the contents into strings via the given split characters.
    /// </summary>
    /// <param name="splitCharacters">Characters to split on, or null/empty to split on whitespace.</param>
    /// <exception cref="OverflowException">Thrown if the substring is too big to fit in a string.</exception>
    public unsafe IEnumerable<string> Split(params char[] splitCharacters)
    {
        bool splitWhite = splitCharacters is null || splitCharacters.Length == 0;

        var strings = new List<string>();

        using var readLock = _handleLock.Lock(Locks.Type.Read);
        char* start = CharPointer;
        char* current = start;

        uint length = _length;

        for (uint i = 0; i < length; i++)
        {
            if ((splitWhite && char.IsWhiteSpace(*current))
                || (!splitWhite && ContainsChar(splitCharacters!, *current)))
            {
                // Split
                strings.Add(new string(value: start, startIndex: 0, length: checked((int)(current - start))));
                start = current + 1;
            }

            current++;
        }

        strings.Add(new string(value: start, startIndex: 0, length: checked((int)(current - start))));

        return strings;
    }

    /// <summary>
    ///  True if the buffer contains the given character.
    /// </summary>
    public unsafe bool Contains(char value)
    {
        using var readLock = _handleLock.Lock(Locks.Type.Read);
        char* start = CharPointer;
        uint length = _length;

        for (uint i = 0; i < length; i++)
            if (*start++ == value) return true;

        return false;
    }

    /// <summary>
    ///  True if the buffer contains any of the specified characters.
    /// </summary>
    public unsafe bool Contains(params char[] values)
    {
        if (values is null || values.Length == 0) return false;

        using var readLock = _handleLock.Lock(Locks.Type.Read);
        char* start = CharPointer;
        uint length = _length;

        for (uint i = 0; i < length; i++)
        {
            if (ContainsChar(values, *start)) return true;
            start++;
        }

        return false;
    }

    /// <summary>
    ///  Trim the specified values from the end of the buffer.
    /// </summary>
    public unsafe void TrimEnd(params char[] values)
    {
        if (values is null || values.Length == 0 || _length == 0) return;

        using var writeLock = _handleLock.Lock(Locks.Type.Write);
        char* end = CharPointer + _length - 1;

        while (_length > 0 && ContainsChar(values, *end))
        {
            _length--;
            end--;
        }

        CharPointer[_length] = '\0';
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ContainsChar(char[] source, char value)
    {
        for (int i = 0; i < source.Length; i++)
        {
            if (value == source[i]) return true;
        }

        return false;
    }

    /// <summary>
    ///  String representation of the entire buffer.
    /// </summary>
    /// <exception cref="OverflowException">
    ///  Thrown if the length of the buffer is larger than a string's max capacity (int.MaxValue).
    /// </exception>
    public override unsafe string ToString()
    {
        if (_length == 0) return string.Empty;

        using var readLock = _handleLock.Lock(Locks.Type.Read);
        return new string(CharPointer, startIndex: 0, length: checked((int)_length));
    }

    /// <summary>
    ///  Gets a span representing the current value.
    /// </summary>
    public unsafe ReadOnlySpan<char> AsSpan() => new(CharPointer, checked((int)_length));

    /// <summary>
    ///  Gets a reference to the first char.
    /// </summary>
    public ref char GetReference() => ref MemoryMarshal.GetReference(AsSpan());

    /// <summary>
    ///  Get the given substring in the buffer.
    /// </summary>
    /// <param name="count">
    ///  Count of characters to take, or remaining characters from <paramref name="startIndex"/> if -1.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  Thrown if <paramref name="startIndex"/> or <paramref name="count"/> are outside the range
    ///  of the buffer's length or count is greater than the maximum string size (int.MaxValue).
    /// </exception>
    public unsafe string SubString(uint startIndex, int count = -1)
    {
        using var readLock = _handleLock.Lock(Locks.Type.Read);

        if (startIndex > (_length == 0 ? 0 : _length - 1)) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (count < -1) throw new ArgumentOutOfRangeException(nameof(count));

        uint realCount = count == -1 ? _length - startIndex : (uint)count;

        return realCount > int.MaxValue || startIndex + realCount > _length
            ? throw new ArgumentOutOfRangeException(nameof(count))
            : realCount == 0
                ? string.Empty
                : new string(value: CharPointer + startIndex, startIndex: 0, length: checked((int)realCount));
    }

    protected override void Dispose(bool disposing)
    {
        _length = 0;
        base.Dispose(disposing);
    }

    public UNICODE_STRING ToUnicodeString() => new(this);

    public static StringBufferCache Cache => StringBufferCache.Instance;
}