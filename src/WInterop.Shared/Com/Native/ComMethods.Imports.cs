// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Security;
using WInterop.Security.Unsafe;
using WInterop.Com;
using WInterop.Errors;
using WInterop.Storage;

namespace WInterop.Com
{
    public static partial class ComMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa380328.aspx
            [DllImport(Libraries.Ole32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern HRESULT StgCreateStorageEx(
                string pwcsName,
                StorageMode grfMode,
                StorageFormat stgfmt,
                FileFlags grfAttrs,
                STGOPTIONS* pStgOptions,
                SECURITY_DESCRIPTOR** pSecurityDescriptor,
                ref Guid riid,
                [MarshalAs(UnmanagedType.IUnknown)] out object ppObjectOpen);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa380342.aspx
            [DllImport(Libraries.Ole32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern HRESULT StgOpenStorageEx(
                string pwcsName,
                StorageMode grfMode,
                StorageFormat stgfmt,
                FileFlags grfAttrs,
                STGOPTIONS* pStgOptions,
                void* reserved2,
                ref Guid riid,
                [MarshalAs(UnmanagedType.IUnknown)] out object ppObjectOpen);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa380334.aspx
            [DllImport(Libraries.Ole32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern HRESULT StgIsStorageFile(
                string pwcsName);

            public static HRESULT VariantClear(IntPtr pvarg) => Support.Internal.Imports.VariantClear(pvarg);
        }
    }
}
