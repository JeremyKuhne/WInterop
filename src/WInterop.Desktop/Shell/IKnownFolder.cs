// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Shell
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb761768.aspx
    [ComImport,
        Guid(InterfaceIds.IID_IKnownFolder),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IKnownFolder
    {
        Guid GetId();

        KnownFolderCategory GetCategory();

        [return: MarshalAs(UnmanagedType.Interface)]
        IShellItem GetShellItem(
            KnownFolderFlags dwFlags,
            [MarshalAs(UnmanagedType.LPStruct)] Guid riid);

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetPath(
            KnownFolderFlags dwFlags);

        void SetPath(
            KnownFolderFlags dwFlags,
            [MarshalAs(UnmanagedType.LPWStr)] string pszPath);

        ItemIdList GetIDList(
            KnownFolderFlags dwFlags);

        Guid GetFolderType();

        KnownFolderRedirectionCapabilities GetRedirectionCapabilities();

        void GetFolderDefinition(
            KnownFolderDefinition pKFD);
    }
}
