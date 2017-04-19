// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ErrorHandling.DataTypes;
using WInterop.Support.Internal;

namespace WInterop.ErrorHandling
{
    public static class ErrorMacros
    {
        private const int STATUS_SEVERITY_SUCCESS = 0x0;
        private const int STATUS_SEVERITY_INFORMATIONAL = 0x1;
        private const int STATUS_SEVERITY_WARNING = 0x2;
        private const int STATUS_SEVERITY_ERROR = 0x3;

        /// <summary>
        /// Extracts the code portion of the specified HRESULT
        /// </summary>
        public static int HRESULT_CODE(HRESULT hr) => Macros.HRESULT_CODE(hr);

        /// <summary>
        /// Extracts the facility of the specified HRESULT
        /// </summary>
        public static Facility HRESULT_FACILITY(HRESULT hr) => Macros.HRESULT_FACILITY(hr);

        /// <summary>
        /// Extracts the severity of the specified result
        /// </summary>
        public static int HRESULT_SEVERITY(HRESULT hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms693761.aspx
            // #define HRESULT_SEVERITY(hr)  (((hr) >> 31) & 0x1)
            return ((((int)hr) >> 31) & 0x1);
        }

        public static HRESULT HRESULT_FROM_WIN32(WindowsError error) => Macros.HRESULT_FROM_WIN32(error);

        public static HRESULT HRESULT_FROM_NT(NTSTATUS status) => Macros.HRESULT_FROM_NT(status);

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
    }
}
