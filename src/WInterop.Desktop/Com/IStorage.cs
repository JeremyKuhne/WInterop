// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Com.Native;
using WInterop.Storage;

namespace WInterop.Com
{
    [ComImport,
        Guid("0000000b-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStorage
    {
        IStream CreateStream(
            string pwcsName,
            StorageMode grfMode,
            uint reserved1 = 0,
            uint reserved2 = 0);

        IStream OpenStream(
            string pwcsName,
            IntPtr reserved1,
            StorageMode grfMode,
            uint reserved2 = 0);

        IStorage CreateStorage(
            string pwcsName,
            StorageMode grfMode,
            uint reserved1 = 0,
            uint reserved2 = 0);

        IStorage OpenStorage(
            string pwcsName,
            IStorage pstgPriority,
            StorageMode grfMode,
            IntPtr snbExclude,
            uint reserved = 0);

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
            StorageCommit grfCommitFlags = StorageCommit.Default);

        void Revert();

        unsafe IEnumSTATSTG EnumElements(
            uint reserved1 = 0,
            void* reserved2 = null,
            uint reserved3 = 0);

        void DestroyElement(
            string pwcsName);

        void RenameElement(
            string pwcsOldName,
            string pwcsNewName);

        void SetElementTimes(
            string pwcsName,
            in FileTime pctime,
            in FileTime patime,
            in FileTime pmtime);

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