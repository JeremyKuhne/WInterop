// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Com.Native;
using WInterop.Errors;

namespace WInterop.Com
{
    public static partial class ComMethods
    {
        public unsafe static object CreateStorage(
            string path,
            Guid riid,
            StorageMode mode = StorageMode.ReadWrite | StorageMode.Create | StorageMode.ShareExclusive,
            StorageFormat format = StorageFormat.DocFile)
        {
            STGOPTIONS options = new STGOPTIONS
            {
                usVersion = 1,

                // If possible, we want the larger 4096 sector size
                ulSectorSize = (mode & StorageMode.Simple) != 0  ? 512u : 4096
            };

            Imports.StgCreateStorageEx(
                path,
                mode,
                format,
                0,
                format == StorageFormat.DocFile ? &options : null,
                null,
                ref riid,
                out object created).ThrowIfFailed(path);

            return created;
        }

        public unsafe static object OpenStorage(
            string path,
            Guid riid,
            StorageMode mode = StorageMode.ReadWrite | StorageMode.ShareExclusive,
            StorageFormat format = StorageFormat.Any)
        {
            STGOPTIONS options = new STGOPTIONS
            {
                // Must have version set before using
                usVersion = 1
            };

            Imports.StgOpenStorageEx(
                path,
                mode,
                format,
                0,
                format == StorageFormat.DocFile ? &options : null,
                null,
                ref riid,
                out object created).ThrowIfFailed(path);

            return created;
        }

        public static bool IsStorageFile(string path)
        {
            return Imports.StgIsStorageFile(path) == HResult.S_OK;
        }
    }
}
