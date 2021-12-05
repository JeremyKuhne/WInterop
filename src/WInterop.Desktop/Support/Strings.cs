// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WInterop.Support;

public static class Strings
{
    /// <summary>
    ///  Single allocation replacement of a single character in a string.
    /// </summary>
    /// <exception cref="ArgumentNullException">value is null or empty</exception>
    /// <exception cref="ArgumentOutOfRangeException">index is not within the bounds of the string.</exception>
    /// <returns>A copy of the given string with the specified character replaced.</returns>
    public static unsafe string ReplaceChar(string value, int index, char newChar)
    {
        if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
        if (index < 0 || index >= value.Length) throw new ArgumentOutOfRangeException(nameof(index));

        fixed (char* v = value)
        {
            string newString = new(v, 0, value.Length);
            fixed (char* n = newString)
                n[index] = newChar;
            return newString;
        }
    }

    public static ReadOnlyMemory<char> GetChunk(this StringBuilder builder)
    {
        var enumerator = builder.GetChunks();
        enumerator.MoveNext();
        return enumerator.Current;
    }

    public static unsafe string GetNullTerminatedAsciiString(ReadOnlySpan<byte> source)
    {
        if (source.Length == 0)
            return string.Empty;

        int length = source.IndexOf((byte)0x00);
        if (length == 0)
            return string.Empty;

        fixed (byte* start = source)
            return Encoding.ASCII.GetString(start, length == -1 ? source.Length : length);
    }

    /// <summary>
    ///  Splits a null terminated unicode string list. The final string is followed by a second null.
    ///  This is a common pattern Windows uses. Usually you should use StringBuffer and split on null. There
    ///  are some cases (such as GetEnvironmentStrings) where Windows returns a handle to a buffer it allocated
    ///  with no length.
    /// </summary>
    public static unsafe IEnumerable<string> SplitNullTerminatedStringList(IntPtr handle)
    {
        if (handle == IntPtr.Zero) throw new ArgumentNullException(nameof(handle));

        var strings = new List<string>();

        char* start = (char*)handle;
        char* current = start;

        for (uint i = 0; ; i++)
        {
            if (*current == '\0')
            {
                // Split
                strings.Add(new string(value: start, startIndex: 0, length: checked((int)(current - start))));
                start = current + 1;

                if (*start == '\0') break;
            }

            current++;
        }

        return strings;
    }

    /// <summary>
    ///  Gets the span of chars for the given <paramref name="buffer"/>.
    /// </summary>
    /// <param name="includeNull">
    ///  True to include the terminating null in the returned span.
    /// </param>
    public static unsafe Span<char> GetSpanFromNullTerminatedBuffer(char* buffer, bool includeNull = false)
    {
        if (buffer is null)
        {
            return Span<char>.Empty;
        }

        Span<char> span = new(buffer, int.MaxValue);
        int terminator = span.IndexOf('\0');
        return span[..(includeNull ? terminator + 1 : terminator)];
    }

    /// <summary>
    ///  Converts a BSTR to string.
    /// </summary>
    [return: NotNullIfNotNull("bstr")]
    public static unsafe string? FromBSTR(ushort* bstr)
        => bstr is null ? null! : new((char*)bstr, 0, (int)BSTRLength(bstr) / 2);

    /// <summary>
    ///  Converts a BSTR to string and frees the BSTR.
    /// </summary>
    [return: NotNullIfNotNull("bstr")]
    public static unsafe string? FromBSTRAndFree(ushort* bstr)
    {
        string? result = FromBSTR(bstr);
        if (bstr is not null)
        {
            TerraFXWindows.SysFreeString(bstr);
        }

        return result;
    }

    /// <summary>
    ///  Get the length of a BSTR in bytes.
    /// </summary>
    public static unsafe uint BSTRLength(ushort* bstr)
        // BSTRs have the length before the string pointer
        // https://docs.microsoft.com/previous-versions/windows/desktop/automat/bstr
        => bstr is null ? 0 : *(((uint*)bstr) - 1);
}