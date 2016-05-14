// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.ErrorHandling
{
    public static class ErrorHelper
    {
        /// <summary>
        /// Turns the last Windows error into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // Want to try and force the get last error inline
        public static Exception GetIoExceptionForLastError(string path = null)
        {
            uint error = (uint)Marshal.GetLastWin32Error();
            return GetIoExceptionForError(error, path);
        }

        /// <summary>
        /// Try to get the string for an HRESULT
        /// </summary>
        public static string HResultToString(int hr)
        {
            string message;
            if (ErrorMacros.HRESULT_FACILITY(hr) == Facility.WIN32)
            {
                // Win32 Error, extract the code
                message = NativeMethods.FormatMessage(
                    messageId: (uint)ErrorMacros.HRESULT_CODE(hr),
                    source: IntPtr.Zero,
                    flags: FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM);
            }
            else
            {
                // Hope that we get a rational IErrorInfo
                Exception exception = Marshal.GetExceptionForHR(hr);
                message = exception.Message;
            }

            return $"HRESULT {hr:D} [0x{hr:X}]: {message}";
        }

        /// <summary>
        /// Turns HRESULT errors into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        public static Exception GetIoExceptionForHResult(int hr, string path = null)
        {
            string message = $"{HResultToString(hr)} > '{path ?? WInteropStrings.NoValue}'";
            if (ErrorMacros.HRESULT_FACILITY(hr) == Facility.WIN32)
                return WinErrorToException((uint)ErrorMacros.HRESULT_CODE(hr), message, path);
            else
                return new IOException(message, hr);
        }

        /// <summary>
        /// Turns Windows errors into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        public static Exception GetIoExceptionForError(uint error, string path = null)
        {
            // http://referencesource.microsoft.com/#mscorlib/system/io/__error.cs,142

            string message = $"{NativeMethods.LastErrorToString(error)} > '{path ?? WInteropStrings.NoValue}'";
            return WinErrorToException(error, message, path);
        }

        /// <summary>
        /// Turns NTSTATUS errors into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        public static Exception GetIOExceptionForNTStatus(NTSTATUS status, string path = null)
        {
            return GetIoExceptionForError(NativeMethods.NtStatusToWinError(status), path);
        }

        private static Exception WinErrorToException(uint error, string message, string path)
        {
            switch (error)
            {
                case WinErrors.ERROR_FILE_NOT_FOUND:
                    return new FileNotFoundException(message, path);
                case WinErrors.ERROR_PATH_NOT_FOUND:
                    return new DirectoryNotFoundException(message);
                case WinErrors.ERROR_ACCESS_DENIED:
                // Network access doesn't throw UnauthorizedAccess in .NET
                case WinErrors.ERROR_NETWORK_ACCESS_DENIED:
                    return new UnauthorizedAccessException(message);
                case WinErrors.ERROR_FILENAME_EXCED_RANGE:
                    return new PathTooLongException(message);
                case WinErrors.ERROR_INVALID_DRIVE:
                    // Not available in Portable libraries
                    // return new DriveNotFoundException(message);
                    goto default;
                case WinErrors.ERROR_OPERATION_ABORTED:
                    return new OperationCanceledException(message);
                case WinErrors.ERROR_NOT_READY:
                    return new DriveNotReadyException(message);
                case WinErrors.FVE_E_LOCKED_VOLUME:
                    return new DriveLockedException(message);
                case WinErrors.ERROR_INVALID_PARAMETER:
                    return new ArgumentException(message);
                case WinErrors.ERROR_ALREADY_EXISTS:
                case WinErrors.ERROR_SHARING_VIOLATION:
                case WinErrors.ERROR_FILE_EXISTS:
                default:
                    return new IOException(message, ErrorMacros.HRESULT_FROM_WIN32(error));
            }
        }

        /// <summary>
        /// EntryPointNotFoundException isn't available in portable assemblies. Use this to check the type.
        /// </summary>
        public static bool IsEntryPointNotFoundException(Exception e)
        {
            return e.GetType().FullName.Equals("System.EntryPointNotFoundException", StringComparison.Ordinal);
        }
    }
}
