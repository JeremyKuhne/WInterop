// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.DiskManagement.Types;
using WInterop.Support;

namespace WInterop.DiskManagement
{
    public static partial class DiskMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            unsafe public static extern bool GetDiskFreeSpaceExW(
                string lpDirectoryName,
                ulong* lpFreeBytesAvailable,
                ulong* lpTotalNumberOfBytes,
                ulong* lpTotalNumberOfFreeBytes);
        }

        public static ExtendedDiskFreeSpace GetDiskFreeSpaceExtended(string directory)
        {
            ExtendedDiskFreeSpace freeSpace;

            unsafe
            {
                if (!Imports.GetDiskFreeSpaceExW(
                    lpDirectoryName: directory,
                    lpFreeBytesAvailable: &freeSpace.FreeBytesAvailable,
                    lpTotalNumberOfBytes: &freeSpace.TotalNumberOfBytes,
                    lpTotalNumberOfFreeBytes: &freeSpace.TotalNumberOfFreeBytes))
                {
                    throw Errors.GetIoExceptionForLastError(directory);
                }
            }

            return freeSpace;
        }
    }
}
