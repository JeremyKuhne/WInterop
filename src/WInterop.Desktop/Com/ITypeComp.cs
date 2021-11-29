// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Com.Native;
using WInterop.Errors;

namespace WInterop.Com;

[ComImport,
    Guid("00020403-0000-0000-C000-000000000046"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public unsafe interface ITypeComp
{
    [PreserveSig]
    HResult Bind(
        [MarshalAs(UnmanagedType.LPWStr)]
            string szName,
        uint lHashVal,
        ushort wFlags,
        out ITypeInfo ppTInfo,
        out DescriptionKind pDescKind,
        BINDPTR* pBindPtr);

    [PreserveSig]
    HResult BindType(
        [MarshalAs(UnmanagedType.LPWStr)]
            string szName,
        uint lHashVal,
        out ITypeInfo ppTInfo,
        out ITypeComp ppTComp);
}