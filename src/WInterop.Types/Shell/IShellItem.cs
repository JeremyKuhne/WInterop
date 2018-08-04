// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WInterop.Shell.Types
{
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
            SIGDN sigdnName);

        SFGAOF GetAttributes(
            SFGAOF sfgaoMask);

        int Compare(
            IShellItem psi,
            SICHINTF hint);
    }
}
