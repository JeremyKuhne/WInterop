// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WInterop.ErrorHandling.Types;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.VolumeManagement.Types;

namespace WInterop.VolumeManagement
{
    public static partial class VolumeMethods
    {
        /// <summary>
        /// Returns the mapping for the specified DOS device name or the full list of DOS device names if passed null.
        /// </summary>
        /// <remarks>
        /// This will look up the symbolic link target from the dos device namespace (\??\) when a name is specified.
        /// It performs the equivalent of NtOpenDirectoryObject, NtOpenSymbolicLinkObject, then NtQuerySymbolicLinkObject.
        /// </remarks>
        public static IEnumerable<string> QueryDosDevice(string deviceName)
        {
            if (deviceName != null) deviceName = Paths.TrimTrailingSeparators(deviceName);

            // Null will return everything defined- this list is quite large so set a higher initial allocation

            var buffer = StringBuffer.Cache.Acquire(deviceName == null ? 16384u : 256);

            try
            {
                uint result = 0;

                // QueryDosDevicePrivate takes the buffer count in TCHARs, which is 2 bytes for Unicode (WCHAR)
                while ((result = Imports.QueryDosDeviceW(deviceName, buffer, buffer.CharCapacity)) == 0)
                {
                    WindowsError error = Errors.GetLastError();
                    switch (error)
                    {
                        case WindowsError.ERROR_INSUFFICIENT_BUFFER:
                            buffer.EnsureCharCapacity(buffer.CharCapacity * 2);
                            break;
                        default:
                            throw Errors.GetIoExceptionForError(error, deviceName);
                    }
                }

                // This API returns a szArray, which is terminated by two nulls
                buffer.Length = checked(result - 2);
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
        public static IEnumerable<string> GetLogicalDriveStrings()
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                uint result = 0;

                // GetLogicalDriveStringsPrivate takes the buffer count in TCHARs, which is 2 bytes for Unicode (WCHAR)
                while ((result = Imports.GetLogicalDriveStringsW(buffer.CharCapacity, buffer)) > buffer.CharCapacity)
                {
                    buffer.EnsureCharCapacity(result);
                }

                if (result == 0)
                    throw Errors.GetIoExceptionForLastError();

                buffer.Length = result;
                return buffer.Split('\0', removeEmptyStrings: true);
            });
        }

        /// <summary>
        /// Returns the path of the volume mount point for the specified path.
        /// </summary>
        public static string GetVolumePathName(string path)
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                while (!Imports.GetVolumePathNameW(path, buffer, buffer.CharCapacity))
                {
                    WindowsError error = Errors.GetLastError();
                    switch (error)
                    {
                        case WindowsError.ERROR_FILENAME_EXCED_RANGE:
                            buffer.EnsureCharCapacity(buffer.CharCapacity * 2);
                            break;
                        default:
                            throw Errors.GetIoExceptionForError(error, path);
                    }
                }

                buffer.SetLengthToFirstNull();
                return buffer.ToString();
            });
        }

        /// <summary>
        /// Returns the list of all paths where the given volume name is mounted.
        /// </summary>
        /// <param name="volumeName">A volume GUID path for the volume (\\?\Volume{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}\).</param>
        public static IEnumerable<string> GetVolumePathNamesForVolumeName(string volumeName)
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                uint returnLength = 0;

                // GetLogicalDriveStringsPrivate takes the buffer count in TCHARs, which is 2 bytes for Unicode (WCHAR)
                while (!Imports.GetVolumePathNamesForVolumeNameW(volumeName, buffer, buffer.CharCapacity, ref returnLength))
                {
                    WindowsError error = Errors.GetLastError();
                    switch (error)
                    {
                        case WindowsError.ERROR_MORE_DATA:
                            buffer.EnsureCharCapacity(returnLength);
                            break;
                        default:
                            throw Errors.GetIoExceptionForError(error, volumeName);
                    }
                }

                Debug.Assert(returnLength != 2, "this should never happen can't have a string array without at least 3 chars");

                // If the return length is 1 there were no mount points. The buffer should be '\0'.
                if (returnLength < 3)
                    return Enumerable.Empty<string>();

                // The return length will be the entire length of the buffer, including the final string's
                // null and the string list's second null. Example: "Foo\0Bar\0\0" would be 9.
                buffer.Length = returnLength - 2;
                return buffer.Split('\0');
            });
        }

        /// <summary>
        /// Gets the GUID format (\\?\Volume{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}\) of the given volume mount point path.
        /// </summary>
        public static string GetVolumeNameForVolumeMountPoint(string volumeMountPoint)
        {
            volumeMountPoint = Paths.AddTrailingSeparator(volumeMountPoint);

            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                // MSDN claims 50 is "reasonable", let's go double.
                buffer.EnsureCharCapacity(100);

                if (!Imports.GetVolumeNameForVolumeMountPointW(volumeMountPoint, buffer, buffer.CharCapacity))
                    throw Errors.GetIoExceptionForLastError(volumeMountPoint);

                buffer.SetLengthToFirstNull();
                return buffer.ToString();
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
                if (!Imports.GetVolumeInformationW(
                    rootPath,
                    volumeName,
                    volumeName.CharCapacity,
                    out uint serialNumber,
                    out uint maxComponentLength,
                    out FileSystemFeature flags,
                    fileSystemName,
                    fileSystemName.CharCapacity))
                    throw Errors.GetIoExceptionForLastError(rootPath);

                volumeName.SetLengthToFirstNull();
                fileSystemName.SetLengthToFirstNull();

                return new VolumeInformation
                {
                    RootPathName = rootPath,
                    VolumeName = volumeName.ToString(),
                    VolumeSerialNumber = serialNumber,
                    MaximumComponentLength = maxComponentLength,
                    FileSystemFlags = flags,
                    FileSystemName = fileSystemName.ToString()
                };
            }
        }

        /// <summary>
        /// Get the drive type for the given root path.
        /// </summary>
        public static DriveType GetDriveType(string rootPath)
        {
            if (rootPath != null) rootPath = Paths.AddTrailingSeparator(rootPath);
            return Imports.GetDriveTypeW(rootPath);
        }

        /// <summary>
        /// Get all volume names.
        /// </summary>
        public static IEnumerable<string> GetVolumes()
        {
            return new VolumeNamesEnumerable();
        }

        /// <summary>
        /// Get all of the folder mount points for the given volume. Requires admin access.
        /// </summary>
        /// <remarks>
        /// This API seems busted/flaky. Use GetVolumePathNamesForVolumeName() instead to
        /// get all mount points (folders *and* drive letter mounts) without requiring admin
        /// access.
        /// </remarks>
        /// <param name="volumeName">Volume name in the form "\\?\Volume{guid}\"</param>
        public static IEnumerable<string> GetVolumeMountPoints(string volumeName)
        {
            return new VolumeMountPointsEnumerable(volumeName);
        }
    }
}
