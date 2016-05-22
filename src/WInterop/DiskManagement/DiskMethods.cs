// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.ErrorHandling;

namespace WInterop.DiskManagement
{
    public static class DiskMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
        {
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            unsafe public static extern bool GetDiskFreeSpaceExW(
                string lpDirectoryName,
                ulong* lpFreeBytesAvailable,
                ulong* lpTotalNumberOfBytes,
                ulong* lpTotalNumberOfFreeBytes);
        }

        public static ExtendedDiskFreeSpace GetDiskFreeSpace(string directory)
        {
            ExtendedDiskFreeSpace freeSpace;

            unsafe
            {
                if (!Direct.GetDiskFreeSpaceExW(
                    lpDirectoryName: directory,
                    lpFreeBytesAvailable: &freeSpace.FreeBytesAvailable,
                    lpTotalNumberOfBytes: &freeSpace.TotalNumberOfBytes,
                    lpTotalNumberOfFreeBytes: &freeSpace.TotalNumberOfFreeBytes))
                {
                    throw ErrorHelper.GetIoExceptionForLastError(directory);
                }
            }

            return freeSpace;
        }
    }
}
