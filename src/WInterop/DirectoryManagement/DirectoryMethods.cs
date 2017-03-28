// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.FileManagement;
using WInterop.FileManagement.DataTypes;
using WInterop.Support.Buffers;

namespace WInterop.DirectoryManagement
{
    public static class DirectoryMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
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
            if (!Direct.RemoveDirectoryW(path))
                throw ErrorHelper.GetIoExceptionForLastError(path);
        }

        /// <summary>
        /// Create the given directory.
        /// </summary>
        public static void CreateDirectory(string path)
        {
            if (!Direct.CreateDirectoryW(path, IntPtr.Zero))
                throw ErrorHelper.GetIoExceptionForLastError(path);
        }

        /// <summary>
        /// Simple wrapper to allow creating a file handle for an existing directory.
        /// </summary>
        public static SafeFileHandle CreateDirectoryHandle(string directoryPath)
        {
            return FileMethods.CreateFile(
                directoryPath,
                DesiredAccess.FILE_GENERIC_READ,
                ShareMode.FILE_SHARE_READWRITE,
                CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE,
                FileFlags.FILE_FLAG_BACKUP_SEMANTICS);
        }

        /// <summary>
        /// Get the current directory.
        /// </summary>
        public static string GetCurrentDirectory()
        {
            return BufferHelper.CachedApiInvoke((buffer) => Direct.GetCurrentDirectoryW(buffer.CharCapacity, buffer));
        }

        /// <summary>
        /// Set the current directory.
        /// </summary>
        public static void SetCurrentDirectory(string path)
        {
            if (!Direct.SetCurrentDirectoryW(path))
                throw ErrorHelper.GetIoExceptionForLastError(path);
        }
    }
}
