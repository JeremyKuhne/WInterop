// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using IBindCtx = System.Runtime.InteropServices.ComTypes.IBindCtx;

namespace WInterop.Shell;

// https://msdn.microsoft.com/en-us/library/windows/desktop/bb761144.aspx
[ComImport,
    Guid(InterfaceIds.IID_IShellItem),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IShellItem
{
    [return: MarshalAs(UnmanagedType.Interface)]
    object BindToHandler(
        IBindCtx pbc,
        ref Guid bhid,
        ref Guid riid);

    IShellItem GetParent();

    [return: MarshalAs(UnmanagedType.LPWStr)]
    string GetDisplayName(
        ShellItemDisplayNames sigdnName);

    Attributes GetAttributes(
        Attributes sfgaoMask);

    int Compare(
        IShellItem psi,
        ShellItemCompareFlags hint);
}