// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ErrorHandling;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.SystemInformation;

namespace WInterop.SystemInformation
{
    public static partial class SystemInformationMethods
    {
        /// <summary>
        /// Get the current user name.
        /// </summary>
        public static string GetUserName()
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                GetUserName(buffer);
                return buffer.ToString();
            });
        }

        public static void GetUserName(StringBuffer buffer)
        {
            uint sizeInChars = buffer.CharCapacity;
            while (!Imports.GetUserNameW(buffer, ref sizeInChars))
            {
                Errors.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                buffer.EnsureCharCapacity(sizeInChars);
            }

            // Returned size includes the null
            buffer.Length = sizeInChars - 1;
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
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
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
        /// Get the computer name in the specified format.
        /// </summary>
        public static string GetComputerName(COMPUTER_NAME_FORMAT format)
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
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
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
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
