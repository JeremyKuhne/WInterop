// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ErrorHandling.DataTypes;

namespace WInterop.Support.Internal
{
    /// <summary>
    /// The subset of macros necessary to support data types, notably error conversion.
    /// Not intended for use outside of WInterop, prefer using the wraps outside of the
    /// Internal namespace.
    /// </summary>
    public static class Macros
    {
        private const int FACILITY_NT_BIT = 0x10000000;

        public static int HRESULT_CODE(HRESULT hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679761.aspx
            // #define HRESULT_CODE(hr)    ((hr) & 0xFFFF)
            return (int)hr & 0xFFFF;
        }

        public static Facility HRESULT_FACILITY(HRESULT hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680579.aspx
            // #define HRESULT_FACILITY(hr)  (((hr) >> 16) & 0x1fff)
            return (Facility)(((int)hr >> 16) & 0x1fff);
        }

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
    }
}
