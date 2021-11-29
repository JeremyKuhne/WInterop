// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Support.Buffers;

public delegate uint OneBufferApiDelegate(ref ValueBuffer<char> buffer);

[Flags]
public enum ReturnSizeSemantics
{
    /// <summary>
    ///  Buffer truncates if there isn't enough room. Without this flag,
    ///  the return value is expected to give the actual length or
    ///  needed buffer size.
    /// </summary>
    BufferTruncates = 0b0001,

    /// <summary>
    ///  If the buffer is full, it is expected that the last error will
    ///  be set to  <see cref="WindowsError.ERROR_INSUFFICIENT_BUFFER"/>.
    /// </summary>
    LastErrorInsufficientBuffer = 0b0010,

    /// <summary>
    ///  When truncating, some APIs give back the size of the buffer
    ///  (including the null) rather than the size of the string.
    /// </summary>
    SizeIncludesNullWhenTruncated = 0b0100
}

public static class PlatformInvoke
{
    /// <summary>
    ///  A number of APIs in Windows return a string in a passed in buffer and return the
    ///  length. This helper deals with the buffer, resizing if necessary.
    /// </summary>
    /// <param name="invoker">
    ///  Delegate that recieves the buffer and passes back the API result.
    /// </param>
    /// <param name="returnSemantics">
    ///  Details the semantics of the return size. Default is that the size returned
    ///  is the length of the string, or the needed buffer size if the buffer is too
    ///  small.
    /// </param>
    /// <param name="detail">
    ///  Optional detail string for thrown errors, such as the input path, etc.
    /// </param>
    /// <param name="shouldThrow">
    ///  Optional delegate to decide which errors to throw on. Default is always
    ///  throws when failed (outside of growing the buffer).
    /// </param>
    /// <returns>
    ///  The string in the buffer passed to the <paramref name="invoker"/>.
    /// </returns>
    public static string GrowableBufferInvoke(
        OneBufferApiDelegate invoker,
        ReturnSizeSemantics returnSemantics = default,
        string? detail = null,
        Func<WindowsError, bool>? shouldThrow = null)
    {
#if DEBUG
        // Use a small buffer to shake out bugs
        const int StackSize = 8;
#else
            const int StackSize = 128;
#endif

        Span<char> initialBuffer = stackalloc char[StackSize];
        ValueBuffer<char> buffer = new ValueBuffer<char>(initialBuffer);

        static int GrowLength(int length)
        {
            // Windows strings are limited to what will fit in UNICODE_STRING,
            // which is short.MaxValue (32K chars).
            return length < 256
                ? 256
                : length < short.MaxValue / 2
                  ? length * 2
                  : short.MaxValue + 1;
        }

        uint result;

        if (returnSemantics.HasFlag(ReturnSizeSemantics.BufferTruncates))
        {
            while ((result = invoker(ref buffer))
                == buffer.Length - (returnSemantics.HasFlag(ReturnSizeSemantics.SizeIncludesNullWhenTruncated) ? 0 : 1))
            {
                if (buffer.Length > short.MaxValue + 1)
                    throw new InvalidOperationException("Buffer grew beyond largest Windows string size.");

                if (returnSemantics.HasFlag(ReturnSizeSemantics.LastErrorInsufficientBuffer))
                {
                    WindowsError error = (WindowsError)Marshal.GetLastWin32Error();
                    if (error != WindowsError.ERROR_INSUFFICIENT_BUFFER)
                        throw new InvalidOperationException("Last error was not ERROR_INSUFFICIENT_BUFFER.");
                }

                buffer.EnsureCapacity(GrowLength((int)result));
            }
        }
        else
        {
            // Standard pattern where needed buffer length is returned
            while ((result = invoker(ref buffer)) > buffer.Length)
            {
                buffer.EnsureCapacity((int)result);
            }
        }

        if (result == 0)
        {
            WindowsError error = (WindowsError)Marshal.GetLastWin32Error();

            if (shouldThrow == null || shouldThrow(error))
            {
                Error.ThrowLastError(detail);
            }
        }

        return buffer.ToStringAndDispose((int)result);
    }
}