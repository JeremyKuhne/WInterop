// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling.DataTypes;

namespace WInterop.ErrorHandling
{
    public static class ErrorHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // Want to try and force the get last error inline
        public static WindowsError GetLastError()
        {
            return (WindowsError)Marshal.GetLastWin32Error();
        }

        public static void ThrowIfLastErrorNot(WindowsError error)
        {
            WindowsError lastError = GetLastError();
            if (lastError != error)
                throw GetIoExceptionForError(lastError);
        }

        /// <summary>
        /// Turns the last Windows error into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // Want to try and force the get last error inline
        public static Exception GetIoExceptionForLastError(string path = null)
        {
            WindowsError error = (WindowsError)Marshal.GetLastWin32Error();
            return GetIoExceptionForError(error, path);
        }

        /// <summary>
        /// Try to get the string for an HRESULT
        /// </summary>
        public static string HResultToString(HRESULT hr)
        {
            string message;
            if (ErrorMacros.HRESULT_FACILITY(hr) == Facility.WIN32)
            {
                // Win32 Error, extract the code
                message = ErrorMethods.FormatMessage(
                    messageId: (uint)ErrorMacros.HRESULT_CODE(hr),
                    source: IntPtr.Zero,
                    flags: FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM);
            }
            else
            {
                // Hope that we get a rational IErrorInfo
                Exception exception = Marshal.GetExceptionForHR((int)hr);
                message = exception.Message;
            }

            return $"HRESULT {(int)hr:D} [0x{(int)hr:X}]: {message}";
        }

        /// <summary>
        /// Try to get the error message for GetLastError result
        /// </summary>
        public static string LastErrorToString(WindowsError error)
        {
            string message = ErrorMethods.FormatMessage(
                messageId: (uint)error,
                source: IntPtr.Zero,
                flags: FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM);

            
            return Enum.IsDefined(typeof(WindowsError), error)
                ? $"{error} ({(uint)error}): {message}"
                : $"Error {error}: {message}";
        }

        /// <summary>
        /// Turns HRESULT errors into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        public static Exception GetIoExceptionForHResult(HRESULT hr, string path = null)
        {
            string message = path == null
                ? $"{HResultToString(hr)}"
                : $"{HResultToString(hr)} '{path}'";

            switch (hr)
            {
                case HRESULT.E_ACCESSDENIED:
                    return new UnauthorizedAccessException(message);
                case HRESULT.E_INVALIDARG:
                    return new ArgumentException(message);
                default:
                    if (ErrorMacros.HRESULT_FACILITY(hr) == Facility.WIN32)
                    {
                        return WindowsErrorToException((WindowsError)ErrorMacros.HRESULT_CODE(hr), message, path);
                    }
                    else
                    {
                        return new IOException(message, (int)hr);
                    }
            }
        }

        /// <summary>
        /// Turns Windows errors into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        public static Exception GetIoExceptionForError(WindowsError error, string path = null)
        {
            // http://referencesource.microsoft.com/#mscorlib/system/io/__error.cs,142

            string message = path == null
                ? $"{LastErrorToString(error)}"
                : $"{LastErrorToString(error)} '{path}'";

            return WindowsErrorToException(error, message, path);
        }

        /// <summary>
        /// Turns NTSTATUS errors into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        public static Exception GetIoExceptionForNTStatus(NTSTATUS status, string path = null)
        {
            switch (status)
            {
                case NTSTATUS.STATUS_NOT_IMPLEMENTED:
                    return new NotImplementedException(path ?? WInteropStrings.NoValue);
            }

            return GetIoExceptionForError(ErrorMethods.NtStatusToWinError(status), path);
        }

        private static Exception WindowsErrorToException(WindowsError error, string message, string path)
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
                    return new OperationCanceledException(message);
                case WindowsError.ERROR_NOT_READY:
                    return new DriveNotReadyException(message);
                case WindowsError.FVE_E_LOCKED_VOLUME:
                    return new DriveLockedException(message);
                case WindowsError.ERROR_INVALID_PARAMETER:
                    return new ArgumentException(message);
                case WindowsError.ERROR_NOT_SUPPORTED:
                case WindowsError.ERROR_NOT_SUPPORTED_IN_APPCONTAINER:
                    return new NotSupportedException(message);
                case WindowsError.ERROR_ALREADY_EXISTS:
                case WindowsError.ERROR_SHARING_VIOLATION:
                case WindowsError.ERROR_FILE_EXISTS:
                default:
                    return new IOException(message, (int)ErrorMacros.HRESULT_FROM_WIN32(error));
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
