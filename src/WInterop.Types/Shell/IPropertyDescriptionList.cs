// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Shell.Types
{
    [ComImport,
        Guid(InterfaceIds.IID_IPropertyDescriptionList),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyDescriptionList
    {
        uint GetCount();

        [return: MarshalAs(UnmanagedType.Interface)]
        IPropertyDescription GetAt(
            uint iElem,
            [MarshalAs(UnmanagedType.LPStruct)] Guid riid);
    }
}
