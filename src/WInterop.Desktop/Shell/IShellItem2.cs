// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;
using IBindCtx = System.Runtime.InteropServices.ComTypes.IBindCtx;

namespace WInterop.Shell;

// https://msdn.microsoft.com/en-us/library/windows/desktop/bb761130.aspx
[ComImport,
    Guid(InterfaceIds.IID_IShellItem2),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IShellItem2 : IShellItem
{
    [return: MarshalAs(UnmanagedType.Interface)]
    new object BindToHandler(
        IBindCtx pbc,
        ref Guid bhid,
        ref Guid riid);

    new IShellItem GetParent();

    [return: MarshalAs(UnmanagedType.LPWStr)]
    new string GetDisplayName(
        ShellItemDisplayNames sigdnName);

    new Attributes GetAttributes(
        Attributes sfgaoMask);

    new int Compare(
        IShellItem psi,
        ShellItemCompareFlags hint);

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetPropertyStore(
        GetPropertyStoreFlags flags,
        [In] ref Guid riid);

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetPropertyStoreWithCreateObject(
        GetPropertyStoreFlags flags,
        [MarshalAs(UnmanagedType.Interface)] object punkCreateObject,
        [In] ref Guid riid);

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetPropertyStoreForKeys(
        SafeHandle rgKeys,
        uint cKeys,
        GetPropertyStoreFlags flags,
        [In] ref Guid riid);

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetPropertyDescriptionList(
        [In] ref PropertyKey keyType,
        [In] ref Guid riid);

    void Update(
        IBindCtx pbc);

    PROPVARIANT GetProperty(
        [In] ref PropertyKey key);

    Guid GetCLSID(
        [In] ref PropertyKey key);

    FILETIME GetFileTime(
        [In] ref PropertyKey key);

    int GetInt32(
        [In] ref PropertyKey key);

    [return: MarshalAs(UnmanagedType.LPWStr)]
    string GetString(
        [In] ref PropertyKey key);

    uint GetUInt32(
        [In] ref PropertyKey key);

    ulong GetUInt64(
        [In] ref PropertyKey key);

    [return: MarshalAs(UnmanagedType.Bool)]
    bool GetBool(
        [In] ref PropertyKey key);
}