// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.ProcessAndThreads.DataTypes;
using WInterop.Support;

namespace WInterop.Handles
{
    public static partial class HandleMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
        {
            public static bool CloseHandle(
                IntPtr handle) => Support.Internal.Imports.CloseHandle(handle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724251.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DuplicateHandle(
                SafeProcessHandle hSourceProcessHandle,
                IntPtr hSourceHandle,
                SafeProcessHandle hTargetProcessHandle,
                SafeHandle lpTargetHandle,
                uint dwDesiredAccess,
                [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
                uint dwOptions);
        }

        public static void CloseHandle(IntPtr handle)
        {
            if (!Direct.CloseHandle(handle))
                throw Errors.GetIoExceptionForLastError();
        }
    }
}
