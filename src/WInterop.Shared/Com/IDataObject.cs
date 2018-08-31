// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Com.Unsafe;

namespace WInterop.Com
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/objidl/nn-objidl-idataobject
    [ComImport,
        Guid("0000010e-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDataObject
    {
        STGMEDIUM GetData(in FORMATETC pformatetcIn);
        STGMEDIUM GetDataHere(in FORMATETC pformatetc);
        void QueryGetData(in FORMATETC pformatetc);

        [PreserveSig]
        HRESULT GetCanonicalFormatEtc(
            in FORMATETC pformatectIn,
            ref FORMATETC pformatetcOut);

        void SetData(
            in FORMATETC pformatetc,
            in STGMEDIUM pmedium,
            BOOL fRelease);

        [PreserveSig]
        HRESULT EnumFormatEtc(
            uint dwDirection,
            // IEnumFORMATETC
            [MarshalAs(UnmanagedType.IUnknown)]
            out object ppenumFormatEtc);

        // Return E_NOTIMPL here
        [PreserveSig]
        HRESULT DAdvise(
            in FORMATETC pformatetc,
            uint advf,
            // IAdviseSink
            IntPtr pAdvSink,
            out uint pdwConnection);

        // Return OLE_E_ADVISENOTSUPPORTED
        [PreserveSig]
        HRESULT DUnadvise(uint dwConnection);

        // Return OLE_E_ADVISENOTSUPPORTED
        [PreserveSig]
        HRESULT EnumDAdvise(
            // IEnumSTATDATA
            out IntPtr ppenumAdvise);
    }
}
