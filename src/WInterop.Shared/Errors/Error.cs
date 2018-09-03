// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Support.Buffers;
using WInterop.Errors.Unsafe;

namespace WInterop.Errors
{
    public static partial class Error
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
                flags |=  FormatMessageFlags.MaxWidthMask;
                if (args == null || args.Length == 0) flags |= FormatMessageFlags.IgnoreInserts;

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
                        lastError = GetLastError();
                        capacity = (uint)Math.Min(capacity * 2, short.MaxValue);
                    }
                    else
                    {
                        buffer.Length = result;
                        return buffer.ToString();
                    }
                }

                throw new IOException("Failed to get error string.", (int)lastError.ToHResult());
            }
        }

        public static WindowsError NtStatusToWinError(NTStatus status)
        {
            return (WindowsError)Imports.LsaNtStatusToWinError(status);
        }

        public static void SetLastError(WindowsError error)
        {
            Imports.SetLastError(error);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // Want to try and force the get last error inline
        public static WindowsError GetLastError()
        {
            return (WindowsError)Marshal.GetLastWin32Error();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // Want to try and force the get last error inline
        public static void ThrowIfLastErrorNot(WindowsError error, string path = null)
        {
            WindowsError lastError = GetLastError();
            if (lastError != error)
                throw lastError.GetException(path);
        }

        /// <summary>
        /// Turns the last Windows error into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // Want to try and force the get last error inline
        public static Exception GetExceptionForLastError(string path = null)
        {
            WindowsError error = (WindowsError)Marshal.GetLastWin32Error();
            return error.GetException(path);
        }

        /// <summary>
        /// Try to get the string for an HRESULT
        /// </summary>
        public static string HResultToString(HResult hr)
        {
            string message;
            if (hr.GetFacility() == Facility.Win32)
            {
                // Win32 Error, extract the code
                message = FormatMessage(
                    messageId: (uint)hr.GetCode(),
                    source: IntPtr.Zero,
                    flags: FormatMessageFlags.FromSystem);
            }
            else
            {
                // Hope that we get a rational IErrorInfo
                Exception exception = Marshal.GetExceptionForHR((int)hr);
                message = exception.Message;
            }

            return Enum.IsDefined(typeof(HResult), hr)
                ? $"HRESULT {hr} [0x{(int)hr:X} ({(int)hr:D})]: {message}"
                : $"HRESULT [0x{(int)hr:X} ({(int)hr:D})]: {message}";
        }

        /// <summary>
        /// Try to get the error message for GetLastError result
        /// </summary>
        public static string LastErrorToString(WindowsError error)
        {
            string message = FormatMessage(
                messageId: (uint)error,
                source: IntPtr.Zero,
                flags: FormatMessageFlags.FromSystem);

            // There are a few defintions for '0', we'll always use ERROR_SUCCESS
            return error == WindowsError.ERROR_SUCCESS
                ? $"ERROR_SUCCESS ({(uint)error}): {message}"
                : Enum.IsDefined(typeof(WindowsError), error)
                    ? $"{error} ({(uint)error}): {message}"
                    : $"Error {error}: {message}";
        }

        internal static Exception WindowsErrorToException(WindowsError error, string message, string path)
        {
            switch (error)
            {
                case WindowsError.ERROR_FILE_NOT_FOUND:
                    return new FileNotFoundException(message, path);
                case WindowsError.ERROR_PATH_NOT_FOUND:
                    return new DirectoryNotFoundException(message);
                case WindowsError.ERROR_ACCESS_DENIED:
                // Network access doesn't throw UnauthorizedAccess in .NET
                case WindowsError.ERROR_NETWORK_ACCESS_DENIED:
                    return new UnauthorizedAccessException(message);
                case WindowsError.ERROR_FILENAME_EXCED_RANGE:
                    return new PathTooLongException(message);
                case WindowsError.ERROR_INVALID_DRIVE:
                    // Not available in Portable libraries
                    // return new DriveNotFoundException(message);
                    goto default;
                case WindowsError.ERROR_OPERATION_ABORTED:
                case WindowsError.ERROR_CANCELLED:
                    return new OperationCanceledException(message);
                case WindowsError.ERROR_NOT_READY:
                    return new DriveNotReadyException(message);
                case WindowsError.ERROR_FILE_EXISTS:
                case WindowsError.ERROR_ALREADY_EXISTS:
                    return new FileExistsException(message, error);
                case WindowsError.FVE_E_LOCKED_VOLUME:
                    return new DriveLockedException(message);
                case WindowsError.ERROR_INVALID_PARAMETER:
                    return new ArgumentException(message);
                case WindowsError.ERROR_NOT_SUPPORTED:
                case WindowsError.ERROR_NOT_SUPPORTED_IN_APPCONTAINER:
                    return new NotSupportedException(message);
                case WindowsError.ERROR_SHARING_VIOLATION:
                default:
                    return new WInteropIOException(message, error.ToHResult());
            }
        }

        /// <summary>
        /// Throw the last error code from windows if <paramref name="result"/> is false.
        /// </summary>
        /// <param name="path">Optional path or other input detail.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowLastErrorIfFalse(bool result, string path = null)
        {
            if (!result) throw GetExceptionForLastError(path);
        }
    }
}
