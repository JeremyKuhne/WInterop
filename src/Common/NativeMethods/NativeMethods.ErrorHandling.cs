// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security;
    using Buffers;
    using ErrorHandling;

    public static partial class NativeMethods
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

        public static partial class ErrorHandling
        {
            // Putting private P/Invokes in a subclass to allow exact matching of signatures for perf on initial call and reduce string count
#if DESKTOP
            [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
            public static partial class Direct
            {
                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721800.aspx
                [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
                public static extern uint LsaNtStatusToWinError(int Status);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679351.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                public static extern uint FormatMessageW(
                    FormatMessageFlags dwFlags,
                    IntPtr lpSource,
                    uint dwMessageId,
                    // LANGID or 0 for auto lookup
                    uint dwLanguageId,
                    IntPtr lpBuffer,
                    // Size is in chars
                    uint nSize,
                    string[] Arguments);

                /// <summary>
                /// Extracts the code portion of the specified HRESULT
                /// </summary>
                public static uint HRESULT_CODE(uint hr)
                {
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679761.aspx
                    // #define HRESULT_CODE(hr)    ((hr) & 0xFFFF)
                    return hr & 0xFFFF;
                }

                /// <summary>
                /// Extracts the facility of the specified HRESULT
                /// </summary>
                public static uint HRESULT_FACILITY(uint hr)
                {
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680579.aspx
                    // #define HRESULT_FACILITY(hr)  (((hr) >> 16) & 0x1fff)
                    return (hr >> 16) & 0x1fff;
                }

                /// <summary>
                /// Extracts the severity of the specified result
                /// </summary>
                public static uint HRESULT_SEVERITY(uint hr)
                {
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms693761.aspx
                    // #define HRESULT_SEVERITY(hr)  (((hr) >> 31) & 0x1)  
                    return (((hr) >> 31) & 0x1);
                }

                public static uint HRESULT_FROM_WIN32(uint error)
                {
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680746(v=vs.85).aspx
                    // return (HRESULT)(x) <= 0 ? (HRESULT)(x) : (HRESULT) (((x) & 0x0000FFFF) | (FACILITY_WIN32 << 16) | 0x80000000);
                    return error <= 0 ? error : ((error & 0x0000FFFF) | ((uint)Facility.WIN32 << 16) | 0x80000000);
                }
            }

            /// <summary>
            /// Convert a Win32 error to an HRESULT
            /// </summary>
            public static uint GetHResultForWindowsError(uint error)
            {
                return Direct.HRESULT_FROM_WIN32(error);
            }

            /// <summary>
            /// Get the facility for the given HRESULT
            /// </summary>
            public static Facility GetHResultFacility(uint result)
            {
                return (Facility)Direct.HRESULT_FACILITY(result);
            }

            /// <summary>
            /// Get the status code for the given HRESULT
            /// </summary>
            private static uint GetHResultStatusCode(uint result)
            {
                return Direct.HRESULT_CODE(result);
            }

            /// <summary>
            /// Try to get the string for an HRESULT
            /// </summary>
            public static string HResultToString(uint result)
            {
                string message;
                if (GetHResultFacility(result) == Facility.WIN32)
                {
                    // Win32 Error, extract the code
                    message = FormatMessage(
                        messageId: Direct.HRESULT_CODE(result),
                        source: IntPtr.Zero,
                        flags: FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM);
                }
                else
                {
                    // Hope that we get a rational IErrorInfo
                    Exception exception = Marshal.GetExceptionForHR((int)result);
                    message = exception.Message;
                }

                return string.Format(
                    CultureInfo.CurrentUICulture,
                    "HRESULT {0:D} [0x{0:X}]: {1}",
                    result,
                    message);
            }

            /// <summary>
            /// Try to get the error message for GetLastError result
            /// </summary>
            public static string LastErrorToString(uint error)
            {
                string message = FormatMessage(
                    messageId: error,
                    source: IntPtr.Zero,
                    flags: FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM);

                return string.Format(
                    CultureInfo.CurrentUICulture,
                    "Error {0}: {1}",
                    error,
                    message);
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
                    if (args == null && args.Length > 0) flags |= FormatMessageFlags.FORMAT_MESSAGE_IGNORE_INSERTS;

                    uint lastError = WinErrors.ERROR_INSUFFICIENT_BUFFER;
                    uint capacity = byte.MaxValue;
                    uint result = 0;

                    while (lastError == WinErrors.ERROR_INSUFFICIENT_BUFFER && capacity <= short.MaxValue)
                    {
                        buffer.EnsureCharCapacity(capacity);
                        result = Direct.FormatMessageW(
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
                            lastError = (uint)Marshal.GetLastWin32Error();
                            capacity = (uint)Math.Min(capacity * 2, short.MaxValue);
                        }
                        else
                        {
                            buffer.Length = result;
                            return buffer.ToString();
                        }
                    }

                    throw new IOException("Failed to get error string.", (int)Direct.HRESULT_FROM_WIN32(lastError));
                }
            }

            // [MS-ERREF] NTSTATUS
            // https://msdn.microsoft.com/en-us/library/cc231200.aspx
            private const int STATUS_SEVERITY_SUCCESS = 0x0;
            private const int STATUS_SEVERITY_INFORMATIONAL = 0x1;
            private const int STATUS_SEVERITY_WARNING = 0x2;
            private const int STATUS_SEVERITY_ERROR = 0x3;

            public static bool NT_SUCCESS(int NTSTATUS)
            {
                return NTSTATUS >= 0;
            }

            public static bool NT_INFORMATION(int NTSTATUS)
            {
                return (uint)NTSTATUS >> 30 == STATUS_SEVERITY_INFORMATIONAL;
            }

            public static bool NT_WARNING(int NTSTATUS)
            {
                return (uint)NTSTATUS >> 30 == STATUS_SEVERITY_WARNING;
            }

            public static bool NT_ERROR(int NTSTATUS)
            {
                return (uint)NTSTATUS >> 30 == STATUS_SEVERITY_ERROR;
            }

            public static uint NtStatusToWinError(int status)
            {
                return Direct.LsaNtStatusToWinError(status);
            }
        }
    }
}
