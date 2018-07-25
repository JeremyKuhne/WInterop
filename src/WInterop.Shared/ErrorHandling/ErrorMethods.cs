﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using WInterop.Support;
using WInterop.Support.Buffers;
using Internal = WInterop.Support.Internal;

namespace WInterop.ErrorHandling
{
    public static partial class ErrorMethods
    {
        // All defines referenced in this file are from winerror.h unless otherwise specified

        // Technically WinErrors are uints (GetLastError returns DWORD). Even though .NET uses int for these errors we'll stick with uint.
        // NTSTATUS, however, is int (LONG).

        // Windows Error Codes specification [MS-ERREF]
        // https://msdn.microsoft.com/en-us/library/cc231196.aspx

        // Structure of COM Error Codes
        // https://msdn.microsoft.com/en-us/library/ms690088

        // How do I convert an HRESULT to a Win32 error code?
        // http://blogs.msdn.com/b/oldnewthing/archive/2006/11/03/942851.aspx

        // How to: Map HRESULTs and Exceptions
        // https://msdn.microsoft.com/en-us/library/9ztbc5s1.aspx

        // Using NTSTATUS Values
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff565436.aspx

        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>

        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721800.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public static extern uint LsaNtStatusToWinError(NTSTATUS Status);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679351.aspx
            public static uint FormatMessageW(
                FormatMessageFlags dwFlags,
                IntPtr lpSource,
                uint dwMessageId,
                // LANGID or 0 for auto lookup
                uint dwLanguageId,
                IntPtr lpBuffer,
                // Size is in chars
                uint nSize,
                string[] Arguments) => Internal.Imports.FormatMessageW(dwFlags, lpSource, dwMessageId, dwLanguageId, lpBuffer, nSize, Arguments);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680627.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern void SetLastError(
                WindowsError dwErrCode);
        }

        // .NET's Win32Exception impements the error code lookup on FormatMessage using FORMAT_MESSAGE_FROM_SYSTEM.
        // It won't handle Network Errors (NERR_BASE..MAX_NERR), which come from NETMSG.DLL.

        public static string FormatMessage(
            uint messageId,
            IntPtr source,
            FormatMessageFlags flags,
            params string[] args)
        {
            using (StringBuffer buffer = new StringBuffer())
            {
                // Don't use line breaks
                flags |=  FormatMessageFlags.FORMAT_MESSAGE_MAX_WIDTH_MASK;
                if (args == null || args.Length == 0) flags |= FormatMessageFlags.FORMAT_MESSAGE_IGNORE_INSERTS;

                WindowsError lastError = WindowsError.ERROR_INSUFFICIENT_BUFFER;
                uint capacity = byte.MaxValue;
                uint result = 0;

                while (lastError == WindowsError.ERROR_INSUFFICIENT_BUFFER && capacity <= short.MaxValue)
                {
                    buffer.EnsureCharCapacity(capacity);
                    result = Imports.FormatMessageW(
                        dwFlags: flags,
                        lpSource: source,
                        dwMessageId: messageId,
                        // Do the default language lookup
                        dwLanguageId: 0,
                        lpBuffer: buffer.DangerousGetHandle(),
                        nSize: buffer.CharCapacity,
                        Arguments: args);

                    if (result == 0)
                    {
                        lastError = Errors.GetLastError();
                        capacity = (uint)Math.Min(capacity * 2, short.MaxValue);
                    }
                    else
                    {
                        buffer.Length = result;
                        return buffer.ToString();
                    }
                }

                throw new IOException("Failed to get error string.", (int)ErrorMacros.HRESULT_FROM_WIN32(lastError));
            }
        }

        public static WindowsError NtStatusToWinError(NTSTATUS status)
        {
            return (WindowsError)Imports.LsaNtStatusToWinError(status);
        }

        public static void SetLastError(WindowsError error)
        {
            Imports.SetLastError(error);
        }
    }
}
