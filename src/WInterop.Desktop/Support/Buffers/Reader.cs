// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.SafeString.Native;

namespace WInterop.Support.Buffers;

/// <summary>
///  Unchecked helpers for reading data from a Buffer. Use CheckedReader whenever possible.
/// </summary>
public class Reader
{
    // Windows Data Alignment on IPF, x86, and x64
    // https://msdn.microsoft.com/en-us/library/aa290049.aspx

    private readonly IBuffer _buffer;
    private ulong _byteOffset;

    public Reader(IBuffer buffer)
    {
        _buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
    }

    public unsafe byte* BytePointer => (byte*)_buffer.DangerousGetHandle() + _byteOffset;

    /// <summary>
    ///  Get/set the offset in bytes.
    /// </summary>
    public virtual ulong ByteOffset
    {
        get => _byteOffset;
        set => _byteOffset = value;
    }

    /// <summary>
    ///  Read a string of the given amount of characters from the buffer. Advances the reader offset.
    /// </summary>
    public unsafe virtual string ReadString(int charCount)
    {
        if (charCount == 0) return string.Empty;

        string value = new((char*)BytePointer, startIndex: 0, length: charCount);
        _byteOffset += (ulong)(charCount * sizeof(char));
        return value;
    }

    /// <summary>
    ///  Read a UNICODE_STRING from the buffer. Advances the reader offset.
    /// </summary>
    /// <remarks>
    ///  LSA_UNICODE_STRING has the same definition as UNICODE_STRING.
    /// </remarks>
    public unsafe string ReadUNICODE_STRING()
    {
        UNICODE_STRING us = *(UNICODE_STRING*)BytePointer;
        string value = new(us.Buffer, 0, us.Length / sizeof(char));
        ByteOffset += (ulong)sizeof(UNICODE_STRING);
        return value;
    }

    /// <summary>
    ///  Read a ushort at the current offset. Advances the reader offset.
    /// </summary>
    public virtual ushort ReadUshort() => (ushort)ReadShort();

    /// <summary>
    ///  Read a short at the current offset. Advances the reader offset.
    /// </summary>
    public unsafe virtual short ReadShort()
    {
        byte* address = BytePointer;
        _byteOffset += sizeof(short);

        if (((short)address & (sizeof(short) - 1)) == 0)
        {
            // aligned read
            return *((short*)address);
        }
        else
        {
            // unaligned read
            return (short)(*address | *(address + 1) << 8);
        }
    }

    /// <summary>
    ///  Read a uint at the current offset. Advances the reader offset.
    /// </summary>
    public virtual uint ReadUint() => (uint)ReadInt();

    /// <summary>
    ///  Read an int at the current offset. Advances the reader offset.
    /// </summary>
    public unsafe virtual int ReadInt()
    {
        byte* address = BytePointer;
        _byteOffset += sizeof(int);

        if (((int)address & (sizeof(int) - 1)) == 0)
        {
            // aligned read
            return *((int*)address);
        }
        else
        {
            // unaligned read
            return *address | *(address + 1) << 8 | *(address + 2) << 16 | *(address + 3) << 24;
        }
    }

    /// <summary>
    ///  Read a ulong at the current offset. Advances the reader offset.
    /// </summary>
    public virtual ulong ReadUlong() => (ulong)ReadLong();

    /// <summary>
    ///  Read a long at the current offset. Advances the reader offset.
    /// </summary>
    public unsafe virtual long ReadLong()
    {
        byte* address = BytePointer;
        _byteOffset += sizeof(long);

        if (((long)address & (sizeof(long) - 1)) == 0)
        {
            // aligned read
            return *((long*)address);
        }
        else
        {
            // unaligned read
            int lo = *address | *(address + 1) << 8 | *(address + 2) << 16 | *(address + 3) << 24;
            int hi = *(address + 4) | *(address + 5) << 8 | *(address + 6) << 16 | *(address + 7) << 24;
            return ((long)hi << 32) | (uint)lo;
        }
    }

    /// <summary>
    ///  Get a pointer at the current offset.
    /// </summary>
    public virtual IntPtr ReadIntPtr() => Environment.Is64BitProcess ? (IntPtr)ReadUlong() : (IntPtr)ReadUint();

    /// <summary>
    ///  Read the given struct type at the current offset.
    /// </summary>
    public unsafe virtual T ReadStruct<T>() where T : struct
    {
        ulong sizeOfStruct = (ulong)Marshal.SizeOf<T>();

        T value = Marshal.PtrToStructure<T>(_buffer.DangerousGetHandle() + checked((int)_byteOffset));
        _byteOffset += sizeOfStruct;
        return value;
    }
}