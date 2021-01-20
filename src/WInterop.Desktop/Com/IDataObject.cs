// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Com.Native;
using WInterop.Errors;

namespace WInterop.Com
{
    /// <docs>https://docs.microsoft.com/en-us/windows/win32/api/objidl/nn-objidl-idataobject</docs>
    [ComImport,
        Guid("0000010e-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDataObject
    {
        STGMEDIUM GetData(in FORMATETC pformatetcIn);
        STGMEDIUM GetDataHere(in FORMATETC pformatetc);
        void QueryGetData(in FORMATETC pformatetc);

        [PreserveSig]
        HResult GetCanonicalFormatEtc(
            in FORMATETC pformatectIn,
            ref FORMATETC pformatetcOut);

        void SetData(
            in FORMATETC pformatetc,
            in STGMEDIUM pmedium,
            IntBoolean fRelease);

        [PreserveSig]
        HResult EnumFormatEtc(
            uint dwDirection,
            [MarshalAs(UnmanagedType.IUnknown)]
            out object ppenumFormatEtc);            // IEnumFORMATETC

        // Return E_NOTIMPL here
        [PreserveSig]
        HResult DAdvise(
            in FORMATETC pformatetc,
            uint advf,
            IntPtr pAdvSink,                        // IAdviseSink
            out uint pdwConnection);

        // Return OLE_E_ADVISENOTSUPPORTED
        [PreserveSig]
        HResult DUnadvise(uint dwConnection);

        // Return OLE_E_ADVISENOTSUPPORTED
        [PreserveSig]
        HResult EnumDAdvise(
            out IntPtr ppenumAdvise);               // IEnumSTATDATA
    }
}
