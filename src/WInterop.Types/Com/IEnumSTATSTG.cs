// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Com
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379217.aspx
    [ComImport,
        Guid("0000000d-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public unsafe interface IEnumSTATSTG
    {
        uint Next(
            uint celt,
            ref STATSTG rgelt);

        // We want to know the HRESULT in this case as the method does not return the number of
        // skipped values, just whether it skipped them all or not.

        [PreserveSig]
        HRESULT Skip(uint celt);

        void Reset();

        IEnumSTATSTG Clone();
    }
}
