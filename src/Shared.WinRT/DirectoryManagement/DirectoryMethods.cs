// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using WInterop.DirectoryManagement.BufferWrappers;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.DirectoryManagement
{
    public static class DirectoryMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365488.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool RemoveDirectoryW(
                string lpPathName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363855.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CreateDirectoryW(
                string lpPathName,
                IntPtr lpSecurityAttributes);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364934.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetCurrentDirectoryW(
                uint nBufferLength,
                SafeHandle lpBuffer);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365530.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetCurrentDirectoryW(
                string lpPathName);
        }

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
                DesiredAccess.GenericRead,
                ShareMode.ReadWrite,
                CreationDisposition.OpenExisting,
                FileAttributes.NONE,
                FileFlags.FILE_FLAG_BACKUP_SEMANTICS);
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
