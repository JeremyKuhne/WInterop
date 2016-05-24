// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ErrorHandling.DataTypes;

namespace WInterop.ErrorHandling
{
    public static class ErrorMacros
    {
        /// <summary>
        /// Extracts the code portion of the specified HRESULT
        /// </summary>
        public static int HRESULT_CODE(int hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679761.aspx
            // #define HRESULT_CODE(hr)    ((hr) & 0xFFFF)
            return hr & 0xFFFF;
        }

        /// <summary>
        /// Extracts the facility of the specified HRESULT
        /// </summary>
        public static Facility HRESULT_FACILITY(int hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680579.aspx
            // #define HRESULT_FACILITY(hr)  (((hr) >> 16) & 0x1fff)
            return (Facility)((hr >> 16) & 0x1fff);
        }

        /// <summary>
        /// Extracts the severity of the specified result
        /// </summary>
        public static int HRESULT_SEVERITY(int hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms693761.aspx
            // #define HRESULT_SEVERITY(hr)  (((hr) >> 31) & 0x1)  
            return (((hr) >> 31) & 0x1);
        }

        public static int HRESULT_FROM_WIN32(WindowsError error)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680746.aspx
            // return (HRESULT)(x) <= 0 ? (HRESULT)(x) : (HRESULT) (((x) & 0x0000FFFF) | (FACILITY_WIN32 << 16) | 0x80000000);
            return (int)((uint)error <= 0 ? (uint)error : (((uint)error & 0x0000FFFF) | ((int)Facility.WIN32 << 16) | 0x80000000));
        }

        public static bool SUCCEEDED(int hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms687197.aspx
            // #define SUCCEEDED(hr) (((HRESULT)(hr)) >= 0)
            return hr >= 0;
        }

        public static bool FAILED(int hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms693474.aspx
            // #define FAILED(hr) (((HRESULT)(hr)) < 0)
            return hr < 0;
        }

        // [MS-ERREF] NTSTATUS
        // https://msdn.microsoft.com/en-us/library/cc231200.aspx
        private const int STATUS_SEVERITY_SUCCESS = 0x0;
        private const int STATUS_SEVERITY_INFORMATIONAL = 0x1;
        private const int STATUS_SEVERITY_WARNING = 0x2;
        private const int STATUS_SEVERITY_ERROR = 0x3;

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
