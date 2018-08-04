// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Errors
{
    // https://msdn.microsoft.com/en-us/library/cc231198.aspx
    public enum HRESULT : int
    {
        S_OK = 0,
        S_FALSE = 1,
        E_NOINTERFACE = unchecked((int)0x80004002),
        E_POINTER = unchecked((int)0x80004003),
        E_FAIL = unchecked((int)0x80004005),
        STG_E_INVALIDFUNCTION = unchecked((int)0x80030001L),
        STG_E_FILENOTFOUND = unchecked((int)0x80030002),
        STG_E_ACCESSDENIED = unchecked((int)0x80030005),
        STG_E_INVALIDPARAMETER = unchecked((int)0x80030057),
        STG_E_INVALIDFLAG = unchecked((int)0x800300FF),
        E_ACCESSDENIED = unchecked((int)0x80070005L),
        E_INVALIDARG = unchecked((int)0x80070057),
    }

    public static class HResultExtensions
    {
        /// <summary>
        /// Extracts the code portion of the specified HRESULT [HRESULT_CODE]
        /// </summary>
        public static int HRESULT_CODE(this HRESULT hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679761.aspx
            // #define HRESULT_CODE(hr)    ((hr) & 0xFFFF)
            return (int)hr & 0xFFFF;
        }

        /// <summary>
        /// Extracts the facility of the specified HRESULT
        /// </summary>
        public static Facility HRESULT_FACILITY(this HRESULT hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680579.aspx
            // #define HRESULT_FACILITY(hr)  (((hr) >> 16) & 0x1fff)
            return (Facility)(((int)hr >> 16) & 0x1fff);
        }

        /// <summary>
        /// Extracts the severity of the specified result
        /// </summary>
        public static int HRESULT_SEVERITY(HRESULT hr)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms693761.aspx
            // #define HRESULT_SEVERITY(hr)  (((hr) >> 31) & 0x1)
            return ((((int)hr) >> 31) & 0x1);
        }
    }
}
