// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Shell.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb761768.aspx
    [ComImport,
        Guid(InterfaceIds.IID_IKnownFolder),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IKnownFolder
    {
        Guid GetId();

        KF_CATEGORY GetCategory();

        [return: MarshalAs(UnmanagedType.Interface)]
        IShellItem GetShellItem(
            KNOWN_FOLDER_FLAG dwFlags,
            [MarshalAs(UnmanagedType.LPStruct)] Guid riid);

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetPath(
            KNOWN_FOLDER_FLAG dwFlags);

        void SetPath(
            KNOWN_FOLDER_FLAG dwFlags,
            [MarshalAs(UnmanagedType.LPWStr)] string pszPath);

        ItemIdList GetIDList(
            KNOWN_FOLDER_FLAG dwFlags);

        Guid GetFolderType();

        KF_REDIRECTION_CAPABILITIES GetRedirectionCapabilities();

        void GetFolderDefinition(
            KNOWNFOLDER_DEFINITION pKFD);
    }
}
