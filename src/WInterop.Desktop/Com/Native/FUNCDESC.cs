// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;

namespace WInterop.Com.Native
{
    /// <summary>
    ///  <see cref="https://docs.microsoft.com/en-us/windows/win32/api/oaidl/ns-oaidl-funcdesc"/>
    /// </summary>
    public unsafe struct FUNCDESC
    {
        public MemberId memid;

        /// <summary>
        ///  This is an [SCODE], which is equivalent to an HRESULT.
        /// </summary>
        public HResult* lprgscode;
        public ELEMDESC* lprgelemdescParam;
        public FunctionKind funckind;
        public InvokeKind invkind;
        public CallConvention callconv;
        public short cParams;
        public short cParamsOpt;
        public short oVft;
        public short cScodes;
        public ELEMDESC elemdescFunc;
        public FunctionFlags wFuncFlags;
    }
}
