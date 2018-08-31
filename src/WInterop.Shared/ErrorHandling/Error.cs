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
using Internal = WInterop.Support.Internal;

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
                        lastError = GetLastError();
                        capacity = (uint)Math.Min(capacity * 2, short.MaxValue);
                    }
                    else
                    {
                        buffer.Length = result;
                        return buffer.ToString();
                    }
                }

                throw new IOException("Failed to get error string.", (int)HRESULT_FROM_WIN32(lastError));
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // Want to try and force the get last error inline
        public static WindowsError GetLastError()
        {
            return (WindowsError)Marshal.GetLastWin32Error();
        }

        public static void ThrowIfLastErrorNot(WindowsError error, string path = null)
        {
            WindowsError lastError = GetLastError();
            if (lastError != error)
                throw GetIoExceptionForError(lastError, path);
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
            if (hr.HRESULT_FACILITY() == Facility.WIN32)
            {
                // Win32 Error, extract the code
                message = FormatMessage(
                    messageId: (uint)hr.HRESULT_CODE(),
                    source: IntPtr.Zero,
                    flags: FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM);
            }
            else
            {
                // Hope that we get a rational IErrorInfo
                Exception exception = Marshal.GetExceptionForHR((int)hr);
                message = exception.Message;
            }

            return Enum.IsDefined(typeof(HRESULT), hr)
                ? $"HRESULT {hr} [0x{(int)hr:X} ({(int)hr:D})]: {message}"
                : $"HRESULT [0x{(int)hr:X} ({(int)hr:D})]: {message}";
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
                    return hr.HRESULT_FACILITY() == Facility.WIN32
                        ? WindowsErrorToException((WindowsError)hr.HRESULT_CODE(), message, path)
                        : new WInteropIOException(message, hr);
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

            // There are a few defintions for '0', we'll always use ERROR_SUCCESS
            return error == WindowsError.ERROR_SUCCESS
                ? $"ERROR_SUCCESS ({(uint)error}): {message}"
                : Enum.IsDefined(typeof(WindowsError), error)
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
                    return new WInteropIOException(message, HRESULT_FROM_WIN32(error));
            }
        }

        public static bool Succeeded(this HRESULT result) => SUCCEEDED(result);
        public static bool Failed(this HRESULT result) => FAILED(result);
        public static Exception GetException(this HRESULT result, string path = null) => GetIoExceptionForHResult(result, path);

        /// <summary>
        /// Throw a relevant exception if <paramref name="result"/> is a failure.
        /// </summary>
        /// <param name="path">Optional path or other input detail.</param>
        public static void ThrowIfFailed(this HRESULT result, string path = null)
        {
            if (result.Failed()) throw result.GetException(path);
        }

        /// <summary>
        /// Throw the last error code from windows if <paramref name="result"/> is false.
        /// </summary>
        /// <param name="path">Optional path or other input detail.</param>
        public static void ThrowLastErrorIfFalse(bool result, string path = null)
        {
            if (!result) throw GetIoExceptionForLastError(path);
        }

        /// <summary>
        /// Throw a relevant exception if <paramref name="error"/> is a failure.
        /// </summary>
        /// <param name="path">Optional path or other input detail.</param>
        public static void ThrowIfFailed(this WindowsError error, string path = null)
        {
            if (error != WindowsError.ERROR_SUCCESS) throw GetIoExceptionForError(error, path);
        }

        private const int STATUS_SEVERITY_SUCCESS = 0x0;
        private const int STATUS_SEVERITY_INFORMATIONAL = 0x1;
        private const int STATUS_SEVERITY_WARNING = 0x2;
        private const int STATUS_SEVERITY_ERROR = 0x3;

        public static HRESULT HRESULT_FROM_WIN32(WindowsError error)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680746.aspx
            // return (HRESULT)(x) <= 0 ? (HRESULT)(x) : (HRESULT) (((x) & 0x0000FFFF) | (FACILITY_WIN32 << 16) | 0x80000000);
            return (HRESULT)((int)error <= 0 ? (int)error : (((int)error & 0x0000FFFF) | ((int)Facility.WIN32 << 16) | 0x80000000));
        }

        public static HRESULT HRESULT_FROM_NT(NTSTATUS status)
        {
            return (HRESULT)((int)status | FACILITY_NT_BIT);
        }

        public static bool SUCCEEDED(HRESULT hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms687197.aspx
            // #define SUCCEEDED(hr) (((HRESULT)(hr)) >= 0)
            return hr >= 0;
        }

        public static bool FAILED(HRESULT hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms693474.aspx
            // #define FAILED(hr) (((HRESULT)(hr)) < 0)
            return hr < 0;
        }

        // [MS-ERREF] NTSTATUS
        // https://msdn.microsoft.com/en-us/library/cc231200.aspx

        public static bool NT_SUCCESS(NTSTATUS NTSTATUS)
        {
            return NTSTATUS >= 0;
        }

        public static bool NT_INFORMATION(NTSTATUS NTSTATUS)
        {
            return (uint)NTSTATUS >> 30 == STATUS_SEVERITY_INFORMATIONAL;
        }

        public static bool NT_WARNING(NTSTATUS NTSTATUS)
        {
            return (uint)NTSTATUS >> 30 == STATUS_SEVERITY_WARNING;
        }

        public static bool NT_ERROR(NTSTATUS NTSTATUS)
        {
            return (uint)NTSTATUS >> 30 == STATUS_SEVERITY_ERROR;
        }

        private const int FACILITY_NT_BIT = 0x10000000;
    }
}
