// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    using Buffers;
    using ErrorHandling;
    using Handles;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Security;
    using Utility;
    using VolumeManagement;

    public static partial class NativeMethods
    {
        public static class VolumeManagement
        {
            /// <summary>
            /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
            /// </summary>
            public static class Desktop
            {
                /// <summary>
                /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
                /// </summary>
                /// <remarks>
                /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
                /// </remarks>
#if DESKTOP
                [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
                public static class Direct
                {
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365461.aspx
                    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                    public static extern uint QueryDosDeviceW(
                        string lpDeviceName,
                        SafeHandle lpTargetPath,
                        uint ucchMax);

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
                        SafeFindVolumeHandle hFindVolume);
                }

                /// <summary>
                /// Returns the mapping for the specified DOS device name or the full list of DOS device names if passed null.
                /// </summary>
                [SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke")]
                public static IEnumerable<string> QueryDosDevice(string deviceName)
                {
                    if (deviceName != null) deviceName = Paths.RemoveTrailingSeparators(deviceName);

                    // Null will return everything defined- this list is quite large so set a higher initial allocation

                    var buffer = deviceName == null ? new StringBuffer(initialCharCapacity: 4096) : StringBufferCache.Instance.Acquire(minCapacity: 256);

                    try
                    {
                        uint result = 0;

                        // QueryDosDevicePrivate takes the buffer count in TCHARs, which is 2 bytes for Unicode (WCHAR)
                        while ((result = Direct.QueryDosDeviceW(deviceName, buffer, buffer.CharCapacity)) == 0)
                        {
                            uint lastError = (uint)Marshal.GetLastWin32Error();
                            switch (lastError)
                            {
                                case WinErrors.ERROR_INSUFFICIENT_BUFFER:
                                    buffer.EnsureCharCapacity(buffer.CharCapacity * 2);
                                    break;
                                default:
                                    throw ErrorHelper.GetIoExceptionForError(lastError, deviceName);
                            }
                        }

                        buffer.Length = result;
                        return buffer.Split('\0');
                    }
                    finally
                    {
                        StringBufferCache.Instance.Release(buffer);
                    }
                }

                /// <summary>
                /// Gets a set of strings for the defined logical drives in the system.
                /// </summary>
                [SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke")]
                public static IEnumerable<string> GetLogicalDriveStrings()
                {
                    return StringBufferCache.CachedBufferInvoke((buffer) =>
                    {
                        uint result = 0;

                    // GetLogicalDriveStringsPrivate takes the buffer count in TCHARs, which is 2 bytes for Unicode (WCHAR)
                    while ((result = Direct.GetLogicalDriveStringsW(buffer.CharCapacity, buffer)) > buffer.CharCapacity)
                        {
                            buffer.EnsureCharCapacity(result);
                        }

                        if (result == 0)
                            throw ErrorHelper.GetIoExceptionForLastError();

                        buffer.Length = result;
                        return buffer.Split('\0', removeEmptyStrings: true);
                    });
                }

                /// <summary>
                /// Returns the path of the volume mount point for the specified path.
                /// </summary>
                [SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke")]
                public static string GetVolumePathName(string path)
                {
                    // Most paths are mounted at the root, 50 should handle the canonical (guid) root
                    return StringBufferCache.CachedBufferInvoke(50, (volumePathName) =>
                    {
                        while (!Direct.GetVolumePathNameW(path, volumePathName, volumePathName.CharCapacity))
                        {
                            uint lastError = (uint)Marshal.GetLastWin32Error();
                            switch (lastError)
                            {
                                case WinErrors.ERROR_FILENAME_EXCED_RANGE:
                                    volumePathName.EnsureCharCapacity(volumePathName.CharCapacity * 2);
                                    break;
                                default:
                                    throw ErrorHelper.GetIoExceptionForError(lastError, path);
                            }
                        }

                        volumePathName.SetLengthToFirstNull();
                        return volumePathName.ToString();
                    });
                }

                /// <summary>
                /// Returns the list of all paths where the given volume name is mounted.
                /// </summary>
                /// <param name="volumeName">A volume GUID path for the volume (\\?\Volume{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}\).</param>
                [SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke")]
                public static IEnumerable<string> GetVolumePathNamesForVolumeName(string volumeName)
                {
                    return StringBufferCache.CachedBufferInvoke((buffer) =>
                    {
                        uint returnLength = 0;

                    // GetLogicalDriveStringsPrivate takes the buffer count in TCHARs, which is 2 bytes for Unicode (WCHAR)
                    while (!Direct.GetVolumePathNamesForVolumeNameW(volumeName, buffer, buffer.CharCapacity, ref returnLength))
                        {
                            uint lastError = (uint)Marshal.GetLastWin32Error();
                            switch (lastError)
                            {
                                case WinErrors.ERROR_MORE_DATA:
                                    buffer.EnsureCharCapacity(returnLength);
                                    break;
                                default:
                                    throw ErrorHelper.GetIoExceptionForError(lastError, volumeName);
                            }
                        }

                        buffer.Length = returnLength;
                        return buffer.Split('\0');
                    });
                }

                /// <summary>
                /// Gets the GUID format (\\?\Volume{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}\) of the given volume mount point path.
                /// </summary>
                public static string GetVolumeNameForVolumeMountPoint(string volumeMountPoint)
                {
                    volumeMountPoint = Paths.AddTrailingSeparator(volumeMountPoint);

                    // MSDN claims 50 is "reasonable", let's go double.
                    return StringBufferCache.CachedBufferInvoke(100, (volumeName) =>
                    {
                        if (!Direct.GetVolumeNameForVolumeMountPointW(volumeMountPoint, volumeName, volumeName.CharCapacity))
                            throw ErrorHelper.GetIoExceptionForLastError(volumeMountPoint);

                        volumeName.SetLengthToFirstNull();
                        return volumeName.ToString();
                    });
                }

                /// <summary>
                /// Gets volume information for the given volume root path.
                /// </summary>
                public static VolumeInformation GetVolumeInformation(string rootPath)
                {
                    rootPath = Paths.AddTrailingSeparator(rootPath);

                    using (var volumeName = new StringBuffer(initialCharCapacity: Paths.MaxPath + 1))
                    using (var fileSystemName = new StringBuffer(initialCharCapacity: Paths.MaxPath + 1))
                    {
                        // Documentation claims that removable (floppy/optical) drives will prompt for media when calling this API and say to
                        // set the error mode to prevent it. I can't replicate this behavior or find any documentation on when it might have
                        // changed. I'm guessing this changed in Windows 7 when they added support for setting the thread's error mode (as
                        // opposed to the entire process).
                        uint serialNumber, maxComponentLength;
                        FileSystemFeature flags;
                        if (!Direct.GetVolumeInformationW(rootPath, volumeName, volumeName.CharCapacity, out serialNumber, out maxComponentLength, out flags, fileSystemName, fileSystemName.CharCapacity))
                            throw ErrorHelper.GetIoExceptionForLastError(rootPath);

                        volumeName.SetLengthToFirstNull();
                        fileSystemName.SetLengthToFirstNull();

                        VolumeInformation info = new VolumeInformation
                        {
                            RootPathName = rootPath,
                            VolumeName = volumeName.ToString(),
                            VolumeSerialNumber = serialNumber,
                            MaximumComponentLength = maxComponentLength,
                            FileSystemFlags = flags,
                            FileSystemName = fileSystemName.ToString()
                        };

                        return info;
                    }
                }

                public static DriveType GetDriveType(string rootPath)
                {
                    if (rootPath != null) rootPath = Paths.AddTrailingSeparator(rootPath);
                    return Direct.GetDriveTypeW(rootPath);
                }
            }
        }
    }
}
