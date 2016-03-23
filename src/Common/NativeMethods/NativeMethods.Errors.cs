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

        public static class Errors
        {
            // Putting private P/Invokes in a subclass to allow exact matching of signatures for perf on initial call and reduce string count
#if DESKTOP
            [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
            private static class Private
            {
                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721800.aspx
                [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
                internal static extern uint LsaNtStatusToWinError(int Status);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679351.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                internal static extern uint FormatMessageW(
                    FormatMessageFlags dwFlags,
                    IntPtr lpSource,
                    uint dwMessageId,
                    // LANGID or 0 for auto lookup
                    uint dwLanguageId,
                    IntPtr lpBuffer,
                    // Size is in chars
                    uint nSize,
                    string[] Arguments);

                [Flags]
                internal enum FormatMessageFlags : uint
                {
                    // This isn't available on Win8 Store apps
                    FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100,
                    FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200,

                    // lpSource is a string
                    FORMAT_MESSAGE_FROM_STRING = 0x00000400,

                    // lpSource is an HMODULE
                    FORMAT_MESSAGE_FROM_HMODULE = 0x00000800,
                    FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000,
                    FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000,

                    // Alternatively < FF means max characters
                    // 00 means use hard coded line breaks (%n)
                    FORMAT_MESSAGE_MAX_WIDTH_MASK = 0x000000FF
                }

                /// <summary>
                /// Extracts the code portion of the specified HRESULT
                /// </summary>
                internal static uint HRESULT_CODE(uint hr)
                {
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679761.aspx
                    // #define HRESULT_CODE(hr)    ((hr) & 0xFFFF)
                    return hr & 0xFFFF;
                }

                /// <summary>
                /// Extracts the facility of the specified HRESULT
                /// </summary>
                internal static uint HRESULT_FACILITY(uint hr)
                {
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680579.aspx
                    // #define HRESULT_FACILITY(hr)  (((hr) >> 16) & 0x1fff)
                    return (hr >> 16) & 0x1fff;
                }

                /// <summary>
                /// Extracts the severity of the specified result
                /// </summary>
                internal static uint HRESULT_SEVERITY(uint hr)
                {
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms693761.aspx
                    // #define HRESULT_SEVERITY(hr)  (((hr) >> 31) & 0x1)  
                    return (((hr) >> 31) & 0x1);
                }

                internal static uint HRESULT_FROM_WIN32(uint error)
                {
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680746(v=vs.85).aspx
                    // return (HRESULT)(x) <= 0 ? (HRESULT)(x) : (HRESULT) (((x) & 0x0000FFFF) | (FACILITY_WIN32 << 16) | 0x80000000);
                    return error <= 0 ? error : ((error & 0x0000FFFF) | ((uint)Facility.WIN32 << 16) | 0x80000000);
                }
            }

            public enum Facility : uint
            {
                XPS = 82,
                XAML = 43,
                USN = 129,
                BLBUI = 128,
                SPP = 256,
                WSB_ONLINE = 133,
                DLS = 153,
                BLB_CLI = 121,
                BLB = 120,
                WSBAPP = 122,
                WPN = 62,
                WMAAECMA = 1996,
                WINRM = 51,
                WINPE = 61,
                WINDOWSUPDATE = 36,
                WINDOWS_STORE = 63,
                WINDOWS_SETUP = 48,
                WINDOWS_DEFENDER = 80,
                WINDOWS_CE = 24,
                WINDOWS = 8,
                WINCODEC_DWRITE_DWM = 2200,
                WIA = 33,
                WER = 27,
                WEP = 2049,
                WEB_SOCKET = 886,
                WEB = 885,
                USERMODE_VOLSNAP = 130,
                USERMODE_VOLMGR = 56,
                VISUALCPP = 109,
                USERMODE_VIRTUALIZATION = 55,
                USERMODE_VHD = 58,
                URT = 19,
                UMI = 22,
                UI = 42,
                TPM_SOFTWARE = 41,
                TPM_SERVICES = 40,
                TIERING = 131,
                SYNCENGINE = 2050,
                SXS = 23,
                STORAGE = 3,
                STATE_MANAGEMENT = 34,
                SSPI = 9,
                USERMODE_SPACES = 231,
                SOS = 160,
                SCARD = 16,
                SHELL = 39,
                SETUPAPI = 15,
                SECURITY = 9,
                SDIAG = 60,
                USERMODE_SDBUS = 2305,
                RPC = 1,
                RESTORE = 256,
                SCRIPT = 112,
                PARSE = 113,
                RAS = 83,
                POWERSHELL = 84,
                PLA = 48,
                PIDGENX = 2561,
                P2P_INT = 98,
                P2P = 99,
                OPC = 81,
                ONLINE_ID = 134,
                WIN32 = 7,
                CONTROL = 10,
                WEBSERVICES = 61,
                NULL = 0,
                NDIS = 52,
                NAP = 39,
                MOBILE = 1793,
                METADIRECTORY = 35,
                MSMQ = 14,
                MEDIASERVER = 13,
                MBN = 84,
                LINGUISTIC_SERVICES = 305,
                LEAP = 2184,
                JSCRIPT = 2306,
                INTERNET = 12,
                ITF = 4,
                INPUT = 64,
                USERMODE_HYPERVISOR = 53,
                ACCELERATOR = 1536,
                HTTP = 25,
                GRAPHICS = 38,
                FWP = 50,
                FVE = 49,
                USERMODE_FILTER_MANAGER = 31,
                EAS = 85,
                EAP = 66,
                DXGI_DDI = 2171,
                DXGI = 2170,
                DPLAY = 21,
                DMSERVER = 256,
                DISPATCH = 2,
                DIRECTORYSERVICE = 37,
                DIRECTMUSIC = 2168,
                DIRECT3D11 = 2172,
                DIRECT3D10 = 2169,
                DIRECT2D = 2201,
                DAF = 100,
                DEPLOYMENT_SERVICES_UTIL = 260,
                DEPLOYMENT_SERVICES_TRANSPORT_MANAGEMENT = 272,
                DEPLOYMENT_SERVICES_TFTP = 264,
                DEPLOYMENT_SERVICES_PXE = 263,
                DEPLOYMENT_SERVICES_MULTICAST_SERVER = 289,
                DEPLOYMENT_SERVICES_MULTICAST_CLIENT = 290,
                DEPLOYMENT_SERVICES_MANAGEMENT = 259,
                DEPLOYMENT_SERVICES_IMAGING = 258,
                DEPLOYMENT_SERVICES_DRIVER_PROVISIONING = 278,
                DEPLOYMENT_SERVICES_SERVER = 257,
                DEPLOYMENT_SERVICES_CONTENT_PROVIDER = 293,
                DEPLOYMENT_SERVICES_BINLSVC = 261,
                DEFRAG = 2304,
                DEBUGGERS = 176,
                CONFIGURATION = 33,
                COMPLUS = 17,
                USERMODE_COMMONLOG = 26,
                CMI = 54,
                CERT = 11,
                BLUETOOTH_ATT = 101,
                BCD = 57,
                BACKGROUNDCOPY = 32,
                AUDIOSTREAMING = 1094,
                AUDCLNT = 2185,
                AUDIO = 102,
                ACTION_QUEUE = 44,
                ACS = 20,
                AAF = 18
            }

            /// <summary>
            /// Convert a Win32 error to an HRESULT
            /// </summary>
            public static uint GetHResultForWindowsError(uint error)
            {
                return Private.HRESULT_FROM_WIN32(error);
            }

            /// <summary>
            /// Get the facility for the given HRESULT
            /// </summary>
            public static Facility GetHResultFacility(uint result)
            {
                return (Facility)Private.HRESULT_FACILITY(result);
            }

            /// <summary>
            /// Get the status code for the given HRESULT
            /// </summary>
            private static uint GetHResultStatusCode(uint result)
            {
                return Private.HRESULT_CODE(result);
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
                        messageId: Private.HRESULT_CODE(result),
                        source: IntPtr.Zero,
                        flags: Private.FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM);
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
                    flags: Private.FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM);

                return string.Format(
                    CultureInfo.CurrentUICulture,
                    "Error {0}: {1}",
                    error,
                    message);
            }

            // .NET's Win32Exception impements the error code lookup on FormatMessage using FORMAT_MESSAGE_FROM_SYSTEM.
            // It won't handle Network Errors (NERR_BASE..MAX_NERR), which come from NETMSG.DLL.

            private static string FormatMessage(
                uint messageId,
                IntPtr source,
                Private.FormatMessageFlags flags,
                params string[] args)
            {
                using (StringBuffer buffer = new StringBuffer())
                {
                    // Don't use line breaks
                    flags |=  Private.FormatMessageFlags.FORMAT_MESSAGE_MAX_WIDTH_MASK;
                    if (args == null && args.Length > 0) flags |= Private.FormatMessageFlags.FORMAT_MESSAGE_IGNORE_INSERTS;

                    uint lastError = WinError.ERROR_INSUFFICIENT_BUFFER;
                    uint capacity = byte.MaxValue;
                    uint result = 0;

                    while (lastError == WinError.ERROR_INSUFFICIENT_BUFFER && capacity <= short.MaxValue)
                    {
                        buffer.EnsureCharCapacity(capacity);
                        result = Private.FormatMessageW(
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

                    throw new IOException("Failed to get error string.", (int)Private.HRESULT_FROM_WIN32(lastError));
                }


            }

            // [MS-ERREF] NTSTATUS
            // https://msdn.microsoft.com/en-us/library/cc231200.aspx
            private const int STATUS_SEVERITY_SUCCESS = 0x0;
            private const int STATUS_SEVERITY_INFORMATIONAL = 0x1;
            private const int STATUS_SEVERITY_WARNING = 0x2;
            private const int STATUS_SEVERITY_ERROR = 0x3;


            public static class WinError
            {
                // From winerror.h
                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms681382.aspx
                internal const uint NO_ERROR = 0;
                internal const uint ERROR_SUCCESS = 0;
                internal const uint NERR_Success = 0;
                internal const uint ERROR_INVALID_FUNCTION = 1;
                internal const uint ERROR_FILE_NOT_FOUND = 2;
                internal const uint ERROR_PATH_NOT_FOUND = 3;
                internal const uint ERROR_ACCESS_DENIED = 5;
                internal const uint ERROR_INVALID_DRIVE = 15;
                internal const uint ERROR_NO_MORE_FILES = 18;
                internal const uint ERROR_NOT_READY = 21;
                internal const uint ERROR_SEEK = 25;
                internal const uint ERROR_SHARING_VIOLATION = 32;
                internal const uint ERROR_BAD_NETPATH = 53;
                internal const uint ERROR_NETNAME_DELETED = 64;
                internal const uint ERROR_NETWORK_ACCESS_DENIED = 65;
                internal const uint ERROR_BAD_NET_NAME = 67;
                internal const uint ERROR_FILE_EXISTS = 80;
                internal const uint ERROR_INVALID_PARAMETER = 87;
                internal const uint ERROR_INSUFFICIENT_BUFFER = 122;
                internal const uint ERROR_INVALID_NAME = 123;
                internal const uint ERROR_BAD_PATHNAME = 161;
                internal const uint ERROR_ALREADY_EXISTS = 183;
                internal const uint ERROR_ENVVAR_NOT_FOUND = 203;
                internal const uint ERROR_FILENAME_EXCED_RANGE = 206;
                internal const uint ERROR_MORE_DATA = 234;
                internal const uint ERROR_OPERATION_ABORTED = 995;
                internal const uint ERROR_NO_TOKEN = 1008;
                internal const uint ERROR_NOT_FOUND = 1168;
                internal const uint ERROR_PRIVILEGE_NOT_HELD = 1314;
                internal const uint ERROR_DISK_CORRUPT = 1393;

                // From lmerr.h
                internal const int NERR_BASE = 2100;
                internal const int NERR_BufTooSmall = NERR_BASE + 23;
                internal const int NERR_InvalidComputer = NERR_BASE + 251;
            }

            public static class NtStatus
            {
                // NTSTATUS values
                // https://msdn.microsoft.com/en-us/library/cc704588.aspx
                internal const int STATUS_SUCCESS = 0x00000000;

                /// <summary>
                /// {Buffer Overflow} The data was too large to fit into the specified buffer.
                /// </summary>
                internal const int STATUS_BUFFER_OVERFLOW = unchecked((int)0x80000005);

                /// <summary>
                /// The specified information record length does not match the length that is required for the specified information class.
                /// </summary>
                internal const int STATUS_INFO_LENGTH_MISMATCH = unchecked((int)0xC0000004);

                /// <summary>
                /// An invalid HANDLE was specified.
                /// </summary>
                internal const int STATUS_INVALID_HANDLE = unchecked((int)0xC0000008);

                /// <summary>
                /// An invalid parameter was passed to a service or function.
                /// </summary>
                internal const int STATUS_INVALID_PARAMETER = unchecked((int)0xC000000D);

                /// <summary>
                /// {Access Denied} A process has requested access to an object but has not been granted those access rights.
                /// </summary>
                internal const int STATUS_ACCESS_DENIED = unchecked((int)0xC0000022);

                /// <summary>
                /// {Buffer Too Small} The buffer is too small to contain the entry. No information has been written to the buffer.
                /// </summary>
                internal const int STATUS_BUFFER_TOO_SMALL = unchecked((int)0xC0000023);

            }

            internal static bool NT_SUCCESS(int NTSTATUS)
            {
                return NTSTATUS >= 0;
            }

            internal static bool NT_INFORMATION(int NTSTATUS)
            {
                return (uint)NTSTATUS >> 30 == STATUS_SEVERITY_INFORMATIONAL;
            }

            internal static bool NT_WARNING(int NTSTATUS)
            {
                return (uint)NTSTATUS >> 30 == STATUS_SEVERITY_WARNING;
            }

            internal static bool NT_ERROR(int NTSTATUS)
            {
                return (uint)NTSTATUS >> 30 == STATUS_SEVERITY_ERROR;
            }

            internal static uint NtStatusToWinError(int status)
            {
                return Private.LsaNtStatusToWinError(status);
            }

            public static Exception GetIoExceptionForError(uint error, string path = null)
            {
                // http://referencesource.microsoft.com/#mscorlib/system/io/__error.cs,142

                string errorText = $"{LastErrorToString(error)} : '{path ?? WInteropStrings.NoValue}'";

                switch (error)
                {
                    case WinError.ERROR_FILE_NOT_FOUND:
                        return new FileNotFoundException(errorText, path);
                    case WinError.ERROR_PATH_NOT_FOUND:
                        return new DirectoryNotFoundException(errorText);
                    case WinError.ERROR_ACCESS_DENIED:
                    // Network access doesn't throw UnauthorizedAccess in .NET
                    case WinError.ERROR_NETWORK_ACCESS_DENIED:
                        return new UnauthorizedAccessException(errorText);
                    case WinError.ERROR_FILENAME_EXCED_RANGE:
                        return new PathTooLongException(errorText);
                    case WinError.ERROR_INVALID_DRIVE:
#if DESKTOP
                    return new DriveNotFoundException(errorText);
#else
                        return new FileNotFoundException(errorText, path);
#endif
                    case WinError.ERROR_OPERATION_ABORTED:
                        return new OperationCanceledException(errorText);
                    case WinError.ERROR_ALREADY_EXISTS:
                    case WinError.ERROR_SHARING_VIOLATION:
                    case WinError.ERROR_FILE_EXISTS:
                    default:
                        return new IOException(errorText, (int)GetHResultForWindowsError(error));
                }
            }
        }
    }
}
