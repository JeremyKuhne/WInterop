// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop;

public static class CharSpanExtensions
{
    /// <summary>
    ///  Copy as much into the destination buffer as possible, null terminating if specified.
    /// </summary>
    /// <param name="source">The string to copy from.</param>
    /// <param name="nullTerminate">Add a null to the end of the string.</param>
    public static void CopyFrom(this Span<char> buffer, string source, bool nullTerminate = true)
    {
        if (buffer.Length == 0 || source == null || source.Length == 0)
        {
            if (nullTerminate)
            {
                if (buffer.Length == 0)
                    throw new ArgumentException("Not enough space to null terminate.", nameof(nullTerminate));
                buffer[0] = '\0';
            }

            return;
        }

        int count = buffer.Length - (nullTerminate ? 1 : 0);
        ReadOnlySpan<char> span = source.AsSpan();
        if (span.Length >= count)
            span = span[..count];

        span.CopyTo(buffer);

        if (nullTerminate)
            buffer[count] = '\0';
    }

    /// <summary>
    ///  Return the given buffer as a string, terminating at null if found.
    /// </summary>
    public static unsafe string CreateString(this Span<char> buffer) => CreateString((ReadOnlySpan<char>)buffer);

    /// <summary>
    ///  Return the given buffer as a string, terminating at null if found.
    /// </summary>
    public static unsafe string CreateString(this ReadOnlySpan<char> buffer)
    {
        int nullChar = buffer.IndexOf('\0');
        if (nullChar == 0)
            return string.Empty;
        if (nullChar != -1)
            buffer = buffer[..nullChar];

        string value = new('\0', buffer.Length);
        fixed (char* v = value)
            buffer.CopyTo(new Span<char>(v, value.Length));

        return value;
    }

    /// <summary>
    ///  Returns true if the buffer equals the given value, presuming the buffer contents either
    ///  terminate at null or has an implicit null at the end of the buffer.
    /// </summary>
    public static bool BufferEquals(this Span<char> buffer, string value) => BufferEquals((ReadOnlySpan<char>)buffer, value);

    /// <summary>
    ///  Returns true if the buffer equals the given value, presuming the buffer contents either
    ///  terminate at null or has an implicit null at the end of the buffer.
    /// </summary>
    public static bool BufferEquals(this ReadOnlySpan<char> buffer, string value)
    {
        if (value == null || value.Length > buffer.Length)
            return false;

        int i = 0;
        for (; i < value.Length; i++)
        {
            // Fixed strings are always terminated at null
            // and therefore can never match embedded nulls.
            if (value[i] != buffer[i] || value[i] == '\0')
                return false;
        }

        // If we've maxed out the buffer or reached the
        // null terminator, we're equal.
        return i == buffer.Length || buffer[i] == '\0';
    }
}