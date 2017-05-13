// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Clipboard.Types;
using WInterop.ErrorHandling.Types;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.Windows.Types;

namespace WInterop.Clipboard
{
    public static partial class ClipboardMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms649048.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool OpenClipboard(
                WindowHandle hWndNewOwner);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms649035.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CloseClipboard();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms649037.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EmptyClipboard();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms649047.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool IsClipboardFormatAvailable(
                uint format);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms649039.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern IntPtr GetClipboardData(
                uint uFormat);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms649036.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int CountClipboardFormats();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms649041.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetClipboardOwner();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms649040.aspx
            [DllImport(Libraries.User32, SetLastError = true,ExactSpelling = true)]
            public static extern int GetClipboardFormatNameW(
                uint format,
                SafeHandle lpszFormatName,
                int cchMaxCount);

            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            unsafe public static extern bool GetUpdatedClipboardFormats(
                uint* lpuiFormats,
                uint cFormats,
                uint* pcFormatsOut);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms649045.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int GetPriorityClipboardFormat(
                uint[] paFormatPriorityList,
                int cFormats);
        }

        /// <summary>
        /// This only works for types that aren't built in (e.g. defined in ClipboardFormat).
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if passing in a built-in format type.</exception>
        public static string GetClipboardFormatName(uint format)
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                int count;
                while ((count = Imports.GetClipboardFormatNameW(format, buffer, (int)buffer.CharCapacity)) == 0)
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    buffer.EnsureCharCapacity(buffer.CharCapacity + 50);
                }

                buffer.Length = (uint)count;
                return buffer.ToString();
            });
        }

        /// <summary>
        /// Gets the formats that are currently available on the clipboard.
        /// </summary>
        public unsafe static uint[] GetAvailableClipboardFormats()
        {
            uint countOut;
            uint countIn = 5;
            uint* array;

            realloc:
            {
                uint* alloc = stackalloc uint[(int)countIn];
                array = alloc;
                if (!Imports.GetUpdatedClipboardFormats(array, countIn, &countOut))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    countIn = countOut;
                    goto realloc;
                }
            }

            uint[] result = new uint[countOut];
            BufferHelper.CopyUintArray(array, result);
            return result;
        }

        /// <summary>
        /// Returns true if the requested format is available.
        /// </summary>
        public static bool IsClipboardFormatAvailable(ClipboardFormat format)
        {
            return Imports.IsClipboardFormatAvailable((uint)format);
        }

        /// <summary>
        /// Returns true if the requested format is available.
        /// </summary>
        public static bool IsClipboardFormatAvailable(uint format)
        {
            return Imports.IsClipboardFormatAvailable(format);
        }

        /// <summary>
        /// Returns the format of the first matching available format.
        /// </summary>
        /// <param name="formats">Formats in priority order.</param>
        /// <returns>
        /// Returns the first matching format. If the clipboard has data, but no matches returns uint.MaxValue.
        /// If the clipboard is empty returns 0.
        /// </returns>
        public static uint GetPriorityClipboardFormat(params uint[] formats)
        {
            return unchecked((uint)Imports.GetPriorityClipboardFormat(formats, formats.Length));
        }
    }
}
