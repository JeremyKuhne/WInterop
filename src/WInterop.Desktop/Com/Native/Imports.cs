// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Globalization;
using WInterop.Security.Native;
using WInterop.Storage;

namespace WInterop.Com.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class Imports
{
    // https://docs.microsoft.com/windows/win32/api/coml2api/nf-coml2api-stgcreatestorageex
    [DllImport(Libraries.Ole32, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static unsafe extern HResult StgCreateStorageEx(
        string pwcsName,
        StorageMode grfMode,
        StorageFormat stgfmt,
        FileFlags grfAttrs,
        STGOPTIONS* pStgOptions,
        SECURITY_DESCRIPTOR** pSecurityDescriptor,
        ref Guid riid,
        [MarshalAs(UnmanagedType.IUnknown)] out object ppObjectOpen);

    // https://docs.microsoft.com/windows/win32/api/coml2api/nf-coml2api-stgopenstorageex
    [DllImport(Libraries.Ole32, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static unsafe extern HResult StgOpenStorageEx(
        string pwcsName,
        StorageMode grfMode,
        StorageFormat stgfmt,
        FileFlags grfAttrs,
        STGOPTIONS* pStgOptions,
        void* reserved2,
        ref Guid riid,
        [MarshalAs(UnmanagedType.IUnknown)] out object ppObjectOpen);

    // https://docs.microsoft.com/windows/win32/api/coml2api/nf-coml2api-stgisstoragefile
    [DllImport(Libraries.Ole32, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern HResult StgIsStorageFile(
        string pwcsName);

    // https://docs.microsoft.com/windows/win32/api/combaseapi/nf-combaseapi-propvariantclear
    [DllImport(Libraries.Ole32)]
    public static extern HResult PropVariantClear(
        IntPtr pvar);

    // https://docs.microsoft.com/windows/win32/api/oleauto/nf-oleauto-variantclear
    [DllImport(Libraries.OleAut32)]
    public static extern HResult VariantClear(
        IntPtr pvarg);

    // https://docs.microsoft.com/windows/win32/api/oleauto/nf-oleauto-loadtypelib
    [DllImport(Libraries.OleAut32)]
    public static extern HResult LoadTypeLib(
        string szFile,
        out ITypeLib pptlib);

    // https://docs.microsoft.com/windows/win32/api/oleauto/nf-oleauto-loadtypelibex
    [DllImport(Libraries.OleAut32)]
    public static extern HResult LoadTypeLibEx(
        string szFile,
        RegisterKind regkind,
        out ITypeLib pptlib);

    // https://docs.microsoft.com/windows/win32/api/oleauto/nf-oleauto-loadregtypelib
    [DllImport(Libraries.OleAut32)]
    public static extern HResult LoadRegTypeLib(
        ref Guid rguid,
        ushort wVerMajor,
        ushort wVerMinor,
        LocaleId lcid,
        out ITypeLib pptlib);

    // https://docs.microsoft.com/windows/win32/api/oleauto/nf-oleauto-safearraygetvartype
    [DllImport(Libraries.OleAut32, ExactSpelling = true)]
    public static extern HResult SafeArrayGetVartype(
        ref SAFEARRAY psa,
        out VariantType pvt);

    // https://docs.microsoft.com/windows/win32/api/oleauto/nf-oleauto-safearraylock
    [DllImport(Libraries.OleAut32, ExactSpelling = true)]
    public static extern HResult SafeArrayLock(
        ref SAFEARRAY psa);

    // https://docs.microsoft.com/windows/win32/api/oleauto/nf-oleauto-safearrayunlock
    [DllImport(Libraries.OleAut32)]
    public static extern HResult SafeArrayUnlock(
        ref SAFEARRAY psa);

    // https://docs.microsoft.com/en-us/windows/win32/api/ole2/nf-ole2-releasestgmedium
    [DllImport(Libraries.Ole32)]
    public static extern unsafe void ReleaseStgMedium(STGMEDIUM* Arg1);
}