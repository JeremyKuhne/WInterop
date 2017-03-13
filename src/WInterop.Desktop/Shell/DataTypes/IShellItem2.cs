// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using WInterop.StructuredStorage.DataTypes;

namespace WInterop.Shell.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb761130.aspx
    [ComImport,
        Guid(InterfaceIds.IID_IShellItem2),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IShellItem2 : IShellItem
    {
        [return: MarshalAs(UnmanagedType.Interface)]
        new object BindToHandler(
            IBindCtx pbc,
            [MarshalAs(UnmanagedType.LPStruct)] Guid bhid,
            [MarshalAs(UnmanagedType.LPStruct)] Guid riid);

        new IShellItem GetParent();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        new string GetDisplayName(
            SIGDN sigdnName);

        new SFGAOF GetAttributes(
            SFGAOF sfgaoMask);

        new int Compare(
            IShellItem psi,
            SICHINTF hint);

        [return: MarshalAs(UnmanagedType.Interface)]
        object GetPropertyStore(
            GETPROPERTYSTOREFLAGS flags,
            [In] ref Guid riid);

        [return: MarshalAs(UnmanagedType.Interface)]
        object GetPropertyStoreWithCreateObject(
            GETPROPERTYSTOREFLAGS flags,
            [MarshalAs(UnmanagedType.IUnknown)] object punkCreateObject,
            [In] ref Guid riid);

        [return: MarshalAs(UnmanagedType.Interface)]
        object GetPropertyStoreForKeys(
            SafeHandle rgKeys,
            uint cKeys,
            GETPROPERTYSTOREFLAGS flags,
            [In] ref Guid riid);

        [return: MarshalAs(UnmanagedType.Interface)]
        object GetPropertyDescriptionList(
            [In] ref PROPERTYKEY keyType,
            [In] ref Guid riid);

        void Update(
            IBindCtx pbc);

        PROPVARIANT GetProperty(
            [In] ref PROPERTYKEY key);

        Guid GetCLSID(
            [In] ref PROPERTYKEY key);

        FILETIME GetFileTime(
            [In] ref PROPERTYKEY key);

        int GetInt32(
            [In] ref PROPERTYKEY key);

        [return: MarshalAs(UnmanagedType.LPWStr)]
        void GetString(
            [In] ref PROPERTYKEY key);

        uint GetUInt32(
            [In] ref PROPERTYKEY key);

        ulong GetUInt64(
            [In] ref PROPERTYKEY key);

        [return: MarshalAs(UnmanagedType.Bool)]
        bool GetBool(
            [In] ref PROPERTYKEY key);
    }
}
