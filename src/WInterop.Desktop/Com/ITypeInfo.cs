// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Com.Native;
using WInterop.Errors;

namespace WInterop.Com;

/// <docs>https://docs.microsoft.com/en-us/windows/win32/api/oaidl/nn-oaidl-itypeinfo</docs>
[ComImport,
    Guid("00020401-0000-0000-C000-000000000046"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public unsafe interface ITypeInfo
{
    [PreserveSig]
    HResult GetTypeAttr(
        out TYPEATTR* ppTypeAttr);

    [PreserveSig]
    HResult GetTypeComp(
        out ITypeComp ppTComp);

    [PreserveSig]
    HResult GetFuncDesc(
        uint index,
        out FUNCDESC* ppFuncDesc);

    [PreserveSig]
    HResult GetVarDesc(
        uint index,
        out VARDESC* ppVarDesc);

    [PreserveSig]
    HResult GetNames(
        MemberId memid,
        BasicString* rgBstrNames,
        uint cMaxNames,
        out uint pcNames);

    [PreserveSig]
    HResult GetRefTypeOfImplType(
        uint index,
        out RefTypeHandle pRefType);

    [PreserveSig]
    HResult GetImplTypeFlags(
        uint index,
        out int pImplTypeFlags);

    [PreserveSig]
    HResult GetIDsOfNames(
        char** rgszNames,
        uint cNames,
        MemberId* pMemId);

    [PreserveSig]
    HResult Invoke(
        void* pvInstance,
        MemberId memid,
        ushort wFlags,
        DISPPARAMS* pDispParams,
        VARIANT* pVarResult,
        EXCEPINFO* pExcepInfo,
        out uint puArgErr);

    [PreserveSig]
    HResult GetDocumentation(
        MemberId memid,
        BasicString* pBstrName = null,
        BasicString* pBstrDocString = null,
        uint* pdwHelpContext = null,
        BasicString* pBstrHelpFile = null);

    [PreserveSig]
    HResult GetDllEntry(
        MemberId memid,
        InvokeKind invKind,
        BasicString* pBstrDllName,
        BasicString* pBstrName,
        out ushort pwOrdinal);

    [PreserveSig]
    HResult GetRefTypeInfo(
        RefTypeHandle hRefType,
        out ITypeInfo ppTInfo);

    [PreserveSig]
    HResult AddressOfMember(
        MemberId memid,
        InvokeKind invKind,
        out void* ppv);

    [PreserveSig]
    HResult CreateInstance(
        IntPtr pUnkOuter,
        Guid riid,
        [MarshalAs(UnmanagedType.Interface)]
            out object ppvObj);

    [PreserveSig]
    HResult GetMops(
        MemberId memid,
        BasicString* pBstrMops);

    [PreserveSig]
    HResult GetContainingTypeLib(
        out ITypeLib ppTLib,
        out uint pIndex);

    [PreserveSig]
    void ReleaseTypeAttr(
        TYPEATTR* pTypeAttr);

    [PreserveSig]
    void ReleaseFuncDesc(
        FUNCDESC* pFuncDesc);

    [PreserveSig]
    void ReleaseVarDesc(
        VARDESC* pVarDesc);
}