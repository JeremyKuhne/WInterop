// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Com;
using WInterop.Windows;

namespace WInterop.Shell
{
    [ComImport,
        Guid(InterfaceIds.IID_IKnownFolderManager),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IKnownFolderManager
    {
        Guid FolderIdFromCsidl(
            int nCsidl);

        int FolderIdToCsidl(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rfid);

        uint GetFolderIds(
            out SafeComHandle ppKFId);

        [return: MarshalAs(UnmanagedType.Interface)]
        IKnownFolder GetFolder(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rfid);

        IKnownFolder GetFolderByName(
            [MarshalAs(UnmanagedType.LPWStr)] string pszCanonicalName);

        void RegisterFolder(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
            KnownFolderDefinition pKFD);

        void UnregisterFolder(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rfid);

        IKnownFolder FindFolderFromPath(
            [MarshalAs(UnmanagedType.LPWStr)] string pszPath,
            FindFolderFromPathMode mode);

        IKnownFolder FindFolderFromIDList(
            ItemIdList pidl);

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string Redirect(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
            WindowHandle hwnd,
            KnownFolderRedirectFlags flags,
            [MarshalAs(UnmanagedType.LPWStr)] string pszTargetPath,
            uint cFolders,
            SafeHandle pExclusion);
    }
}
