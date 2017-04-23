// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.SystemInformation.Types;

namespace WInterop.SystemInformation
{
    public static partial class SystemInformationMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724432.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetUserNameW(
                SafeHandle lpBuffer,
                ref uint lpnSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724435.aspx
            [DllImport(Libraries.Secur32, SetLastError = true, ExactSpelling = true)]
            public static extern BOOLEAN GetUserNameExW(
                EXTENDED_NAME_FORMAT NameFormat,
                SafeHandle lpNameBuffer,
                ref uint lpnSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724295.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetComputerNameW(
                SafeHandle lpBuffer,
                ref uint lpnSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724301.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetComputerNameExW(
                COMPUTER_NAME_FORMAT NameType,
                SafeHandle lpBuffer,
                ref uint lpnSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/mt668928.aspx
            [DllImport(Libraries.Ntdll, ExactSpelling = true)]
            public static extern SuiteMask RtlGetSuiteMask();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724509.aspx
            [DllImport(Libraries.Ntdll, ExactSpelling = true)]
            public static extern int NtQuerySystemInformation(
                SYSTEM_INFORMATION_CLASS SystemInformationClass,
                IntPtr SystemInformation,
                uint SystemInformationLength,
                out uint ReturnLength);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724265.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint ExpandEnvironmentStringsW(
                string lpSrc,
                SafeHandle lpDst,
                uint nSize);
        }

        /// <summary>
        /// Get the current user name.
        /// </summary>
        public static string GetUserName()
        {
            return BufferHelper.CachedInvoke((StringBuffer buffer) =>
            {
                uint sizeInChars = buffer.CharCapacity;
                while (!Imports.GetUserNameW(buffer, ref sizeInChars))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    buffer.EnsureCharCapacity(sizeInChars);
                }

                buffer.Length = sizeInChars - 1;
                return buffer.ToString();
            });
        }

        /// <summary>
        /// Returns the suite mask for the OS which defines the "edition" of Windows.
        /// </summary>
        public static SuiteMask GetSuiteMask()
        {
            return Imports.RtlGetSuiteMask();
        }

        /// <summary>
        /// Gets the user name in the specified format. Returns null for
        /// formats that aren't mapped.
        /// </summary>
        public static string GetUserName(EXTENDED_NAME_FORMAT format)
        {
            return BufferHelper.CachedInvoke((StringBuffer buffer) =>
            {
                uint size = buffer.CharCapacity;
                while (!Imports.GetUserNameExW(format, buffer, ref size))
                {
                    WindowsError error = Errors.GetLastError();
                    switch (error)
                    {
                        case WindowsError.ERROR_NONE_MAPPED:
                            return null;
                        case WindowsError.ERROR_MORE_DATA:
                            buffer.EnsureCharCapacity(size);
                            break;
                        default:
                            throw Errors.GetIoExceptionForError(error);
                    }
                }

                buffer.Length = size;
                return buffer.ToString();
            });
        }

        /// <summary>
        /// Gets the NetBIOS computer name.
        /// </summary>
        public static string GetComputerName()
        {
            return BufferHelper.CachedInvoke((StringBuffer buffer) =>
            {
                uint size = buffer.CharCapacity;
                while (!Imports.GetComputerNameW(buffer, ref size))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_BUFFER_OVERFLOW);
                    buffer.EnsureCharCapacity(size);
                }
                buffer.Length = size;
                return buffer.ToString();
            });
        }

        /// <summary>
        /// Get the computer name in the specified format.
        /// </summary>
        public static string GetComputerName(COMPUTER_NAME_FORMAT format)
        {
            return BufferHelper.CachedInvoke((StringBuffer buffer) =>
            {
                uint size = buffer.CharCapacity;
                while (!Imports.GetComputerNameExW(format, buffer, ref size))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_MORE_DATA);
                    buffer.EnsureCharCapacity(size);
                }
                buffer.Length = size;
                return buffer.ToString();
            });
        }

        /// <summary>
        /// Expand environment variables in the given string.
        /// </summary>
        public static string ExpandEnvironmentVariables(string value)
        {
            return BufferHelper.CachedInvoke((StringBuffer buffer) =>
            {
                uint size;
                while ((size = Imports.ExpandEnvironmentStringsW(value, buffer, buffer.CharCapacity)) > buffer.CharCapacity)
                {
                    buffer.EnsureCharCapacity(size);
                }

                if (size == 0)
                    throw Errors.GetIoExceptionForLastError();

                buffer.Length = size - 1;
                return buffer.ToString();
            });
        }
    }
}
