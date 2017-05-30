// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling.Types;
using WInterop.Support.Buffers;
using WInterop.Support.Internal;

namespace WInterop.Support
{
    public static class Errors
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

        public static bool Failed(WindowsError error)
        {
            return error != WindowsError.NO_ERROR;
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
            if (Macros.HRESULT_FACILITY(hr) == Facility.WIN32)
            {
                // Win32 Error, extract the code
                message = FormatMessage(
                    messageId: (uint)Macros.HRESULT_CODE(hr),
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
                    return Macros.HRESULT_FACILITY(hr) == Facility.WIN32
                        ? WindowsErrorToException((WindowsError)Macros.HRESULT_CODE(hr), message, path)
                        : new IOException(message, (int)hr);
            }
        }

        /// <summary>
        /// Try to get the error message for GetLastError result
        /// </summary>
        public static string LastErrorToString(WindowsError error)
        {
            string message = FormatMessage(
                messageId: (uint)error,
                source: IntPtr.Zero,
                flags: FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM);

            return Enum.IsDefined(typeof(WindowsError), error)
                ? $"{error} ({(uint)error}): {message}"
                : $"Error {error}: {message}";
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

        public static string FormatMessage(
            uint messageId,
            IntPtr source,
            FormatMessageFlags flags,
            params string[] args)
        {
            using (StringBuffer buffer = new StringBuffer())
            {
                // Don't use line breaks
                flags |= FormatMessageFlags.FORMAT_MESSAGE_MAX_WIDTH_MASK;
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
                        lastError = (WindowsError)Marshal.GetLastWin32Error();
                        capacity = (uint)Math.Min(capacity * 2, short.MaxValue);
                    }
                    else
                    {
                        buffer.Length = result;
                        return buffer.ToString();
                    }
                }

                throw new IOException("Failed to get error string.", (int)Macros.HRESULT_FROM_WIN32(lastError));
            }
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
                    return new IOException(message, (int)Macros.HRESULT_FROM_WIN32(error));
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
