// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Com
{
    [ComImport,
        Guid("00020402-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public unsafe interface ITypeLib
    {
        [PreserveSig]
        uint GetTypeInfoCount();

        // TYPE_E_ELEMENTNOTFOUND if out of range of GetTypeInfoCount
        [PreserveSig]
        HResult GetTypeInfo(
            uint index,
            out ITypeInfo ppTInfo);

        HResult GetTypeInfoType(
            uint index,
            out TypeKind pTKind);

        HResult GetTypeInfoOfGuid(
            ref Guid guid,
            out ITypeInfo ppTinfo);

        HResult GetLibAttr(
            TypeLibraryAttributes** ppTLibAttr);

        HResult GetTypeComp(
            out ITypeComp ppTComp);

        HResult GetDocumentation(
            int index,
            BasicString* pBstrName,
            BasicString* pBstrDocString,
            uint* pdwHelpContext,
            BasicString* pBstrHelpFile);

        HResult IsName(
            char* szNameBuf,
            uint lHashVal,
            out IntBoolean pfName);

        HResult FindName(
            char* szNameBuf,
            uint lHashVal,
            IntPtr* ppTInfo,
            MemberId* rgMemId,
            ushort* pcFound);

        void ReleaseTLibAttr(
            TypeLibraryAttributes* pTLibAttr);
    }
}
