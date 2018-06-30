// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Backup.Types;
using WInterop.Storage;
using WInterop.Storage.Types;

namespace WInterop.Backup
{
    public static partial class BackupMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa362509.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern bool BackupRead(
                SafeFileHandle hFile,
                void* lpBuffer,
                uint nNumberOfBytesToRead,
                out uint lpNumberOfBytesRead,
                bool bAbort,
                bool bProcessSecurity,
                ref IntPtr context);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa362510.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool BackupSeek(
                SafeFileHandle hFile,
                uint dwLowBytesToSeek,
                uint dwHighBytesToSeek,
                out uint lpdwLowByteSeeked,
                out uint lpdwHighByteSeeked,
                ref IntPtr context);
        }

        public static IEnumerable<BackupStreamInformation> GetAlternateStreamInformation(string path)
        {
            List<BackupStreamInformation> streams = new List<BackupStreamInformation>();
            using (var fileHandle = StorageMethods.CreateFile(
                path: path,
                // To look at metadata we don't need read or write access
                desiredAccess: 0,
                shareMode: ShareModes.ReadWrite,
                creationDisposition: CreationDisposition.OpenExisting,
                fileAttributes: FileAttributes.None,
                fileFlags: FileFlags.BackupSemantics))
            {
                using (BackupReader reader = new BackupReader(fileHandle))
                {
                    BackupStreamInformation? info;
                    while ((info = reader.GetNextInfo()).HasValue)
                    {
                        if (info.Value.StreamType == BackupStreamType.BACKUP_ALTERNATE_DATA)
                        {
                            streams.Add(new BackupStreamInformation { Name = info.Value.Name, Size = info.Value.Size });
                        }
                    }
                }
            }

            return streams;
        }
    }
}
