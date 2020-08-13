// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using WInterop.Errors;
using WInterop.Windows;

namespace WInterop.Shell
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/shobjidl_core/nn-shobjidl_core-ishellfolder
    [ComImport,
        Guid(InterfaceIds.IID_IShellFolder),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IShellFolder
    {
        void ParseDisplayName(
            WindowHandle hwnd,
            IBindCtx pbc,
            [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName,
            ref uint pchEaten,
            out ItemIdList ppidl,
            ref uint pdwAttributes);

        IEnumIDList EnumObjects(
            WindowHandle hwnd,
            ShellControlFlags grfFlags);

        [return: MarshalAs(UnmanagedType.Interface)]
        object BindToObject(
            ItemIdList pidl,
            IBindCtx pbc,
            ref Guid riid);

        [return: MarshalAs(UnmanagedType.Interface)]
        object BindToStorage(
            ItemIdList pidl,
            IBindCtx pbc,
            ref Guid riid);

        [PreserveSig]
        HResult CompareIDs(
            LParam lParam,
            ItemIdList pidl1,
            ItemIdList pidl2);

        [return: MarshalAs(UnmanagedType.Interface)]
        object CreateViewObject(
            WindowHandle hwndOwner,
            ref Guid riid);

        void GetAttributesOf(
            uint cidl,
            ItemIdList apidl,
            ref Attributes rgfInOut);

        [return: MarshalAs(UnmanagedType.Interface)]
        object GetUIObjectOf(
            WindowHandle hwndOwner,
            uint cidl,
            ItemIdList apidl,
            ref Guid riid,
            ref uint rgfReserved);

        ReturnString GetDisplayNameOf(
            ItemIdList pidl,
            DisplayNameFlags uFlags);

        ItemIdList SetNameOf(
            WindowHandle hwnd,
            ItemIdList pidl,
            [MarshalAs(UnmanagedType.LPWStr)] string pszName,
            DisplayNameFlags uFlags);
    }
}
