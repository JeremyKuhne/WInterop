// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.DiskManagement.Types;
using WInterop.ErrorHandling;
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
            public unsafe static extern bool GetDiskFreeSpaceW(
                string lpRootPathName,
                uint* lpSectorsPerCluster,
                uint* lpBytesPerSector,
                uint* lpNumberOfFreeClusters,
                uint* lpTotalNumberOfClusters);
        }

        public static DiskFreeSpace GetDiskFreeSpace(string directory)
        {
            DiskFreeSpace freeSpace;

            unsafe
            {
                if (!Imports.GetDiskFreeSpaceW(
                    lpRootPathName: directory,
                    lpSectorsPerCluster: &freeSpace.SectorsPerCluster,
                    lpBytesPerSector: &freeSpace.BytesPerSector,
                    lpNumberOfFreeClusters: &freeSpace.NumberOfFreeClusters,
                    lpTotalNumberOfClusters: &freeSpace.TotalNumberOfClusters))
                {
                    throw Errors.GetIoExceptionForLastError(directory);
                }
            }

            return freeSpace;
        }
    }
}
