// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.ProcessAndThreads;

namespace WInterop.Handles
{
    public static partial class Handles
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            public static bool CloseHandle(
                IntPtr handle) => Support.Internal.Imports.CloseHandle(handle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724251.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool DuplicateHandle(
                SafeProcessHandle hSourceProcessHandle,
                IntPtr hSourceHandle,
                SafeProcessHandle hTargetProcessHandle,
                SafeHandle lpTargetHandle,
                uint dwDesiredAccess,
                bool bInheritHandle,
                uint dwOptions);
        }

        public static void CloseHandle(IntPtr handle)
        {
            if (!Imports.CloseHandle(handle))
                throw Error.GetIoExceptionForLastError();
        }
    }
}
