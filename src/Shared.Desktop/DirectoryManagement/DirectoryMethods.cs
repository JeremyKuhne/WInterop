// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;

namespace WInterop.DirectoryManagement
{
    public static partial class DirectoryMethods
    {
        /// <summary>
        /// Wrapper to create a directory within another directory
        /// </summary>
        public static SafeFileHandle CreateDirectory(SafeFileHandle rootDirectory, string name)
        {
            return FileMethods.CreateFileRelative(
                name,
                rootDirectory,
                CreateDisposition.Create,
                DesiredAccess.ListDirectory | DesiredAccess.Synchronize,
                ShareMode.ReadWrite | ShareMode.Delete,
                FileAttributes.None,
                CreateOptions.SynchronousIoNonalert | CreateOptions.DirectoryFile | CreateOptions.OpenForBackupIntent | CreateOptions.OpenReparsePoint);
        }

        /// <summary>
        /// Simple wrapper to allow creating a file handle for an existing directory.
        /// </summary>
        public static SafeFileHandle CreateDirectoryHandle(SafeFileHandle rootDirectory, string subdirectoryPath)
        {
            return FileMethods.CreateFileRelative(
                subdirectoryPath,
                rootDirectory,
                CreateDisposition.Open,
                DesiredAccess.ListDirectory | DesiredAccess.Synchronize,
                ShareMode.ReadWrite | ShareMode.Delete,
                FileAttributes.None,
                CreateOptions.SynchronousIoNonalert | CreateOptions.DirectoryFile | CreateOptions.OpenForBackupIntent | CreateOptions.OpenReparsePoint);
        }
    }
}
