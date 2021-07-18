// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;
using WInterop.Clipboard.Native;
using WInterop.Errors;
using WInterop.Memory;
using WInterop.Support.Buffers;
using WInterop.Windows;

namespace WInterop.Clipboard
{
    public static partial class Clipboard
    {
        /// <summary>
        ///  This only works for types that aren't built in (e.g. defined in ClipboardFormat).
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if passing in a built-in format type.</exception>
        public static string GetClipboardFormatName(uint format)
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                int count;
                while ((count = Imports.GetClipboardFormatNameW(format, buffer, (int)buffer.CharCapacity)) == 0)
                {
                    Error.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    buffer.EnsureCharCapacity(buffer.CharCapacity + 50);
                }

                buffer.Length = (uint)count;
                return buffer.ToString();
            });
        }

        /// <summary>
        ///  Gets the formats that are currently available on the clipboard.
        /// </summary>
        public static unsafe uint[] GetAvailableClipboardFormats()
        {
            uint count;

            Span<uint> initialBuffer = stackalloc uint[5];
            ValueBuffer<uint> buffer = new ValueBuffer<uint>(initialBuffer);

            while (!Imports.GetUpdatedClipboardFormats(ref MemoryMarshal.GetReference(buffer.Span), (uint)buffer.Length, out count))
            {
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                buffer.EnsureCapacity((int)count);
            }

            return buffer.Span.Slice(0, (int)count).ToArray();
        }

        /// <summary>
        ///  Returns true if the requested format is available.
        /// </summary>
        public static bool IsClipboardFormatAvailable(ClipboardFormat format)
        {
            return Imports.IsClipboardFormatAvailable((uint)format);
        }

        /// <summary>
        ///  Returns true if the requested format is available.
        /// </summary>
        public static bool IsClipboardFormatAvailable(uint format)
        {
            return Imports.IsClipboardFormatAvailable(format);
        }

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
            return unchecked((uint)Imports.GetPriorityClipboardFormat(formats, formats.Length));
        }

        public static void OpenClipboard(WindowHandle window = default) => Error.ThrowLastErrorIfFalse(Imports.OpenClipboard(window));

        public static void EmptyClipboard() => Error.ThrowLastErrorIfFalse(Imports.EmptyClipboard());

        public static void CloseClipboard() => Error.ThrowLastErrorIfFalse(Imports.CloseClipboard());

        public static WindowHandle GetOpenClipboardWindow()
        {
            // This API claims it sets last error, but I can get spurious errors when checking if
            // null is an error. It clearly either never does or only does sometimes. Clearing last
            // error before calling makes spurious errors go away.

            Error.SetLastError(WindowsError.NO_ERROR);
            WindowHandle handle = Imports.GetOpenClipboardWindow();
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
            WindowHandle handle = Imports.GetClipboardOwner();
            if (handle.IsInvalid)
                Error.ThrowIfLastErrorNot(WindowsError.NO_ERROR);

            return handle;
        }

        /// <summary>
        ///  Set Unicode text in the clipboard under the given format.
        /// </summary>
        public static void SetClipboardUnicodeText(ReadOnlySpan<char> span, ClipboardFormat format = ClipboardFormat.UnicodeText)
        {
            using GlobalHandle global = Memory.Memory.GlobalAlloc((ulong)((span.Length + 1) * sizeof(char)), GlobalMemoryFlags.Moveable);
            using GlobalLock globalLock = global.Lock;
            Span<char> buffer = globalLock.GetSpan<char>();
            span.CopyTo(buffer);
            buffer[buffer.Length - 1] = '\0';

            Imports.SetClipboardData((uint)format, globalLock.Pointer);
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
        {
            SetClipboardAsciiText(span, (ClipboardFormat)RegisterClipboardFormat(format));
        }

        /// <summary>
        ///  Set binary data in the clipboard under the given format.
        /// </summary>
        public static void SetClipboardBinaryData(ReadOnlySpan<byte> span, ClipboardFormat format)
        {
            using GlobalHandle global = Memory.Memory.GlobalAlloc((ulong)(span.Length + 1), GlobalMemoryFlags.Moveable);
            using GlobalLock globalLock = global.Lock;
            Span<byte> buffer = globalLock.GetSpan<byte>();
            span.CopyTo(buffer);
            buffer[buffer.Length - 1] = 0;

            Imports.SetClipboardData((uint)format, globalLock.Pointer);
        }

        /// <summary>
        ///  Set binary data in the clipboard under the given format.
        /// </summary>
        public static void SetClipboardBinaryData(ReadOnlySpan<byte> span, string format)
        {
            SetClipboardBinaryData(span, (ClipboardFormat)RegisterClipboardFormat(format));
        }

        /// <summary>
        ///  Registers the given format if not already registered. Returns the format id.
        /// </summary>
        public static uint RegisterClipboardFormat(string format)
        {
            uint id = Imports.RegisterClipboardFormatW(format);
            if (id == 0)
                Error.ThrowLastError(format);
            return id;
        }
    }
}