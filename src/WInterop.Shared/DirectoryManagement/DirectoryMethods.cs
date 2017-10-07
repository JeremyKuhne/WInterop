// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using WInterop.DirectoryManagement.BufferWrappers;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.DirectoryManagement
{
    public static partial class DirectoryMethods
    {
        /// <summary>
        /// Remove the given directory.
        /// </summary>
        public static void RemoveDirectory(string path)
        {
            if (!Imports.RemoveDirectoryW(path))
                throw Errors.GetIoExceptionForLastError(path);
        }

        /// <summary>
        /// Create the given directory.
        /// </summary>
        public static void CreateDirectory(string path)
        {
            if (!Imports.CreateDirectoryW(path, IntPtr.Zero))
                throw Errors.GetIoExceptionForLastError(path);
        }

        /// <summary>
        /// Simple wrapper to allow creating a file handle for an existing directory.
        /// </summary>
        public static SafeFileHandle CreateDirectoryHandle(string directoryPath)
        {
            return FileMethods.CreateFile(
                directoryPath,
                CreationDisposition.OpenExisting,
                DesiredAccess.ListDirectory,
                ShareModes.ReadWrite | ShareModes.Delete,
                FileAttributes.None,
                FileFlags.BackupSemantics);
        }

        /// <summary>
        /// Get the current directory.
        /// </summary>
        public static string GetCurrentDirectory()
        {
            var wrapper = new TempPathWrapper();
            return BufferHelper.ApiInvoke(ref wrapper);
        }

        /// <summary>
        /// Set the current directory.
        /// </summary>
        public static void SetCurrentDirectory(string path)
        {
            if (!Imports.SetCurrentDirectoryW(path))
                throw Errors.GetIoExceptionForLastError(path);
        }
    }
}
