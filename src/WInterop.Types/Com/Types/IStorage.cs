// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WInterop.Com.Types
{
    [ComImport,
        Guid("0000000b-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStorage
    {
        IStream CreateStream(
            string pwcsName,
            StorageMode grfMode,
            uint reserved1,
            uint reserved2);

        IStream OpenStream(
            string pwcsName,
            IntPtr reserved1,
            StorageMode grfMode,
            uint reserved2);

        IStorage CreateStorage(
            string pwcsName,
            StorageMode grfMode,
            uint reserved1,
            uint reserved2);

        IStorage OpenStorage(
            string pwcsName,
            IStorage pstgPriority,
            StorageMode grfMode,
            IntPtr snbExclude,
            uint reserved);

        void CopyTo(
            uint ciidExclude,
            in Guid rgiidExclude,
            IntPtr snbExclude,
            IStorage pstgDest);

        void MoveElementTo(
            string pwcsName,
            IStorage pstgDest,
            string pwcsNewName,
            StorageMove grfFlags);

        void Commit(
            StorageCommit grfCommitFlags);

        void Revert();

        IEnumSTATSTG EnumElements(
            uint reserved1,
            IntPtr reserved2,
            uint reserved3);

        void DestroyElement(
            string pwcsName);

        void RenameElement(
            string pwcsOldName,
            string pwcsNewName);

        void SetElementTimes(
            string pwcsName,
            in FILETIME pctime,
            in FILETIME patime,
            in FILETIME pmtime);

        void SetClass(
            ref Guid clsid);

        void SetStateBits(
            uint grfStateBits,
            uint grfMask);

        void Stat(
            out STATSTG pstatstg,
            StatFlag grfStatFlag);
    }
}
