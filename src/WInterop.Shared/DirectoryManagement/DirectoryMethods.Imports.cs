// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.DirectoryManagement
{
    public static partial class DirectoryMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365488.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool RemoveDirectoryW(
                string lpPathName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363855.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool CreateDirectoryW(
                string lpPathName,
                IntPtr lpSecurityAttributes);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364934.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetCurrentDirectoryW(
                uint nBufferLength,
                SafeHandle lpBuffer);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365530.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetCurrentDirectoryW(
                string lpPathName);
        }
    }
}
