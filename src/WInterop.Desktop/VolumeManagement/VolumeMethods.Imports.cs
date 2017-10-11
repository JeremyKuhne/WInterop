// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.VolumeManagement.Types;

namespace WInterop.VolumeManagement
{
    public static partial class VolumeMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365461.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint QueryDosDeviceW(
                string lpDeviceName,
                SafeHandle lpTargetPath,
                uint ucchMax);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364972.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetLogicalDrives();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364975.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetLogicalDriveStringsW(
                uint nBufferLength,
                SafeHandle lpBuffer);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364996.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetVolumePathNameW(
                string lpszFileName,
                SafeHandle lpszVolumePathName,
                uint cchBufferLength);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364998.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetVolumePathNamesForVolumeNameW(
                string lpszVolumeName,
                SafeHandle lpszVolumePathNames,
                uint cchBuferLength,
                ref uint lpcchReturnLength);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364994.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetVolumeNameForVolumeMountPointW(
                string lpszVolumeMountPoint,
                SafeHandle lpszVolumeName,
                uint cchBufferLength);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364993.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetVolumeInformationW(
                string lpRootPathName,
                SafeHandle lpVolumeNameBuffer,
                uint nVolumeNameSize,
                out uint lpVolumeSerialNumber,
                out uint lpMaximumComponentLength,
                out FileSystemFeature lpFileSystemFlags,
                SafeHandle lpFileSystemNameBuffer,
                uint nFileSystemNameSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364939.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern DriveType GetDriveTypeW(
                string lpRootPathName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364433.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool FindVolumeClose(
                IntPtr hFindVolume);
        }
    }
}
