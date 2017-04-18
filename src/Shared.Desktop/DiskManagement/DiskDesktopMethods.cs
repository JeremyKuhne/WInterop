// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.DiskManagement.DataTypes;
using WInterop.ErrorHandling;

namespace WInterop.DiskManagement
{
    public static class DiskDesktopMethods
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
            unsafe public static extern bool GetDiskFreeSpaceW(
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
                if (!Direct.GetDiskFreeSpaceW(
                    lpRootPathName: directory,
                    lpSectorsPerCluster: &freeSpace.SectorsPerCluster,
                    lpBytesPerSector: &freeSpace.BytesPerSector,
                    lpNumberOfFreeClusters: &freeSpace.NumberOfFreeClusters,
                    lpTotalNumberOfClusters: &freeSpace.TotalNumberOfClusters))
                {
                    throw ErrorHelper.GetIoExceptionForLastError(directory);
                }
            }

            return freeSpace;
        }
    }
}
