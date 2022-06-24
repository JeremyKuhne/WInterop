// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Buffers;
using System.Text;
using WInterop.Errors;
using WInterop.Memory;
using WInterop.Support.Buffers;
using WInterop.Windows;

namespace WInterop.Clipboard;

public static unsafe partial class Clipboard
{
    /// <summary>
    ///  This only works for types that aren't built in (e.g. defined in ClipboardFormat).
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if passing in a built-in format type.</exception>
    public static string GetClipboardFormatName(uint format)
    {
        return BufferHelper.BufferInvoke((StringBuffer buffer) =>
        {
            uint count;
            while ((count = (uint)TerraFXWindows.GetClipboardFormatNameW(format, buffer.UShortPointer, (int)buffer.CharCapacity)) == 0u)
            {
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                buffer.EnsureCharCapacity(buffer.CharCapacity + 50);
            }

            buffer.Length = count;
            return buffer.ToString();
        });
    }

    /// <summary>
    ///  Gets the formats that are currently available on the clipboard.
    /// </summary>
    public static unsafe uint[] GetAvailableClipboardFormats()
    {
        uint count;

        Span<uint> initialBuffer = stackalloc uint[10];
        ValueBuffer<uint> buffer = new(initialBuffer);

        do
        {
            fixed (uint* b = buffer)
            {
                if (TerraFXWindows.GetUpdatedClipboardFormats(b, buffer.Length, &count))
                {
                    break;
                }

                Error.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                buffer.EnsureCapacity((int)count);
            }
        } while (true);

        return buffer.Span[..(int)count].ToArray();
    }

    /// <summary>
    ///  Returns true if the requested format is available.
    /// </summary>
    public static bool IsClipboardFormatAvailable(ClipboardFormat format)
        => TerraFXWindows.IsClipboardFormatAvailable((uint)format);

    /// <summary>
    ///  Returns true if the requested format is available.
    /// </summary>
    public static bool IsClipboardFormatAvailable(uint format)
        => TerraFXWindows.IsClipboardFormatAvailable(format);

    /// <summary>
    ///  Returns the format of the first matching available format.
    /// </summary>
    /// <param name="formats">Formats in priority order.</param>
    /// <returns>
    ///  Returns the first matching format. If the clipboard has data, but no matches returns uint.MaxValue.
    ///  If the clipboard is empty returns 0.
    /// </returns>
    public static uint GetPriorityClipboardFormat(params uint[] formats)
    {
        fixed (uint* f = formats)
        {
            return (uint)TerraFXWindows.GetPriorityClipboardFormat(f, formats.Length);
        }
    }

    public static void OpenClipboard(WindowHandle window = default)
        => Error.ThrowLastErrorIfFalse(TerraFXWindows.OpenClipboard(window));

    public static void EmptyClipboard() => Error.ThrowLastErrorIfFalse(TerraFXWindows.EmptyClipboard());

    public static void CloseClipboard() => Error.ThrowLastErrorIfFalse(TerraFXWindows.CloseClipboard());

    public static WindowHandle GetOpenClipboardWindow()
    {
        // This API claims it sets last error, but I can get spurious errors when checking if
        // null is an error. It clearly either never does or only does sometimes. Clearing last
        // error before calling makes spurious errors go away.

        Error.SetLastError(WindowsError.NO_ERROR);
        WindowHandle handle = TerraFXWindows.GetOpenClipboardWindow();
        if (handle.IsInvalid)
            Error.ThrowIfLastErrorNot(WindowsError.NO_ERROR);

        return handle;
    }

    public static WindowHandle GetClipboardOwner()
    {
        // This API claims it sets last error, but I can get spurious errors when checking if
        // null is an error. It clearly either never does or only does sometimes. Clearing last
        // error before calling makes spurious errors go away.

        Error.SetLastError(WindowsError.NO_ERROR);
        WindowHandle handle = TerraFXWindows.GetClipboardOwner();
        if (handle.IsInvalid)
            Error.ThrowIfLastErrorNot(WindowsError.NO_ERROR);

        return handle;
    }

    /// <summary>
    ///  Set Unicode text in the clipboard under the given format.
    /// </summary>
    public static unsafe void SetClipboardUnicodeText(ReadOnlySpan<char> span, ClipboardFormat format = ClipboardFormat.UnicodeText)
    {
        using GlobalHandle global = Memory.Memory.GlobalAlloc((ulong)((span.Length + 1) * sizeof(char)), GlobalMemoryFlags.Moveable);
        using GlobalLock globalLock = global.Lock;
        Span<char> buffer = globalLock.GetSpan<char>();
        span.CopyTo(buffer);
        buffer[^1] = '\0';

        TerraFXWindows.SetClipboardData((uint)format, (HANDLE)globalLock.Pointer);
    }

    /// <summary>
    ///  Set ASCII text in the clipboard under the given format.
    /// </summary>
    public static unsafe void SetClipboardAsciiText(ReadOnlySpan<char> span, ClipboardFormat format = ClipboardFormat.Text)
    {
        Encoding ascii = Encoding.ASCII;
        fixed (char* c = span)
        {
            byte[] buffer = ArrayPool<byte>.Shared.Rent(ascii.GetByteCount(c, span.Length));
            fixed (byte* b = buffer)
            {
                int length = ascii.GetBytes(c, span.Length, b, buffer.Length);
                SetClipboardBinaryData(buffer.AsSpan(0, length), format);
            }
        }
    }

    /// <summary>
    ///  Set ASCII text in the clipboard under the given format.
    /// </summary>
    public static unsafe void SetClipboardAsciiText(ReadOnlySpan<char> span, string format)
        => SetClipboardAsciiText(span, (ClipboardFormat)RegisterClipboardFormat(format));

    /// <summary>
    ///  Set binary data in the clipboard under the given format.
    /// </summary>
    public static unsafe void SetClipboardBinaryData(ReadOnlySpan<byte> span, ClipboardFormat format)
    {
        using GlobalHandle global = Memory.Memory.GlobalAlloc((ulong)(span.Length + 1), GlobalMemoryFlags.Moveable);
        using GlobalLock globalLock = global.Lock;
        Span<byte> buffer = globalLock.GetSpan<byte>();
        span.CopyTo(buffer);
        buffer[^1] = 0;

        TerraFXWindows.SetClipboardData((uint)format, (HANDLE)globalLock.Pointer);
    }

    /// <summary>
    ///  Set binary data in the clipboard under the given format.
    /// </summary>
    public static void SetClipboardBinaryData(ReadOnlySpan<byte> span, string format)
        => SetClipboardBinaryData(span, (ClipboardFormat)RegisterClipboardFormat(format));

    /// <summary>
    ///  Registers the given format if not already registered. Returns the format id.
    /// </summary>
    public static uint RegisterClipboardFormat(string format)
    {
        fixed (void* f = format)
        {
            uint id = TerraFXWindows.RegisterClipboardFormatW((ushort*)f);
            if (id == 0)
                Error.ThrowLastError(format);
            return id;
        }
    }
}