// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;

namespace WInterop.Errors
{
    public static class ErrorExtensions
    {
        private const int FACILITY_NT_BIT = 0x10000000;
        private const int STATUS_SEVERITY_SUCCESS = 0x0;
        private const int STATUS_SEVERITY_INFORMATIONAL = 0x1;
        private const int STATUS_SEVERITY_WARNING = 0x2;
        private const int STATUS_SEVERITY_ERROR = 0x3;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Failed(this WindowsError error) => error != WindowsError.NO_ERROR;

        /// <summary>
        /// Throw a relevant exception if <paramref name="error"/> is a failure.
        /// </summary>
        /// <param name="path">Optional path or other input detail.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfFailed(this WindowsError error, string path = null)
        {
            if (error != WindowsError.ERROR_SUCCESS) throw error.GetException(path);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Throw(this WindowsError error, string path = null) => throw error.GetException(path);

        /// <summary>
        /// Turns Windows errors into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        public static Exception GetException(this WindowsError error, string path = null)
        {
            // http://referencesource.microsoft.com/#mscorlib/system/io/__error.cs,142

            string message = path == null
                ? $"{Error.LastErrorToString(error)}"
                : $"{Error.LastErrorToString(error)} '{path}'";

            return Error.WindowsErrorToException(error, message, path);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Succeeded(this HResult hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms687197.aspx
            // #define SUCCEEDED(hr) (((HRESULT)(hr)) >= 0)
            return hr >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Failed(this HResult hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms693474.aspx
            // #define FAILED(hr) (((HRESULT)(hr)) < 0)
            return hr < 0;
        }

        /// <summary>
        /// Extracts the code portion of the specified HRESULT [HRESULT_CODE]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetCode(this HResult hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679761.aspx
            // #define HRESULT_CODE(hr)    ((hr) & 0xFFFF)
            return (int)hr & 0xFFFF;
        }

        /// <summary>
        /// Extracts the facility of the specified HRESULT [HRESULT_FACILITY]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Facility GetFacility(this HResult hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680579.aspx
            // #define HRESULT_FACILITY(hr)  (((hr) >> 16) & 0x1fff)
            return (Facility)(((int)hr >> 16) & 0x1fff);
        }

        /// <summary>
        /// Extracts the severity of the specified result [HRESULT_SEVERITY]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetSeverity(HResult hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms693761.aspx
            // #define HRESULT_SEVERITY(hr)  (((hr) >> 31) & 0x1)
            return ((((int)hr) >> 31) & 0x1);
        }

        /// <summary>
        /// [HRESULT_FROM_WIN32]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HResult ToHResult(this WindowsError error)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680746.aspx
            // return (HRESULT)(x) <= 0 ? (HRESULT)(x) : (HRESULT) (((x) & 0x0000FFFF) | (FACILITY_WIN32 << 16) | 0x80000000);
            return (HResult)((int)error <= 0 ? (int)error : (((int)error & 0x0000FFFF) | ((int)Facility.Win32 << 16) | 0x80000000));
        }

        /// <summary>
        /// Throw a relevant exception if <paramref name="result"/> is a failure.
        /// </summary>
        /// <param name="path">Optional path or other input detail.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfFailed(this HResult result, string path = null)
        {
            if (result.Failed()) throw result.GetException(path);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Throw(this HResult result) => throw result.GetException();

        /// <summary>
        /// Turns HRESULT errors into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        public static Exception GetException(this HResult hr, string path = null)
        {
            string message = path == null
                ? $"{Error.HResultToString(hr)}"
                : $"{Error.HResultToString(hr)} '{path}'";

            switch (hr)
            {
                case HResult.E_ACCESSDENIED:
                    return new UnauthorizedAccessException(message);
                case HResult.E_INVALIDARG:
                    return new ArgumentException(message);
                default:
                    return hr.GetFacility() == Facility.Win32
                        ? Error.WindowsErrorToException((WindowsError)hr.GetCode(), message, path)
                        : new WInteropIOException(message, hr);
            }
        }

        // [MS-ERREF] NTSTATUS
        // https://msdn.microsoft.com/en-us/library/cc231200.aspx

        /// <summary>
        /// [HRESULT_FROM_NT]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HResult ToHResult(this NTStatus status) => (HResult)((int)status | FACILITY_NT_BIT);

        /// <summary>
        /// [NT_SUCCESS]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Success(this NTStatus status) => status >= 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Failed(this NTStatus status) => status < 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfFailed(this NTStatus status, string path = null)
        {
            if (status.Failed())
                throw status.GetException(path);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Throw(this NTStatus status) => throw status.GetException();

        /// <summary>
        /// [NT_INFORMATION]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInformational(this NTStatus status) => (uint)status >> 30 == STATUS_SEVERITY_INFORMATIONAL;

        /// <summary>
        /// [NT_WARNING]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsWarning(this NTStatus status) => (uint)status >> 30 == STATUS_SEVERITY_WARNING;

        /// <summary>
        /// [NT_ERROR]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsError(this NTStatus status) => (uint)status >> 30 == STATUS_SEVERITY_ERROR;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowsError ToWindowsError(this NTStatus status) => Error.NtStatusToWinError(status);

        /// <summary>
        /// Turns NTSTATUS errors into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        public static Exception GetException(this NTStatus status, string path = null)
        {
            switch (status)
            {
                case NTStatus.STATUS_NOT_IMPLEMENTED:
                    return new NotImplementedException(path ?? WInteropStrings.NoValue);
            }

            return status.ToWindowsError().GetException(path);
        }
    }
}
