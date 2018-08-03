// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using WInterop.Devices.Types;
using WInterop.ErrorHandling;
using WInterop.Storage;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Devices
{
    public static partial class DeviceMethods
    {
        /// <summary>
        /// Get the stable Guid for the given device handle.
        /// </summary>
        public unsafe static Guid QueryStableGuid(SafeHandle deviceHandle)
        {
            ControlCode controlCode = ControlCodes.MountDevice.QueryStableGuid;
            MOUNTDEV_STABLE_GUID guid = new MOUNTDEV_STABLE_GUID();

            if (!Imports.DeviceIoControl(
                hDevice: deviceHandle,
                dwIoControlCode: controlCode,
                lpInBuffer: null,
                nInBufferSize: 0,
                lpOutBuffer: &guid,
                nOutBufferSize: (uint)sizeof(MOUNTDEV_STABLE_GUID),
                lpBytesReturned: out _,
                lpOverlapped: null))
            {
                throw Errors.GetIoExceptionForLastError();
            }

            return guid.StableGuid;
        }

        /// <summary>
        /// Get the device name for the given handle.
        /// </summary>
        public unsafe static string QueryDeviceName(SafeHandle deviceHandle)
        {
            return GetMountDevName(deviceHandle, ControlCodes.MountDevice.QueryDeviceName);
        }

        /// <summary>
        /// Get the interface name for the given handle.
        /// </summary>
        public unsafe static string QueryInterfacename(SafeHandle deviceHandle)
        {
            return GetMountDevName(deviceHandle, ControlCodes.MountDevice.QueryInterfaceName);
        }

        private unsafe static string GetMountDevName(SafeHandle deviceHandle, ControlCode controlCode)
        {
            return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                buffer.EnsureByteCapacity((ulong)sizeof(MOUNTDEV_NAME));

                while (!Imports.DeviceIoControl(
                    hDevice: deviceHandle,
                    dwIoControlCode: controlCode,
                    lpInBuffer: null,
                    nInBufferSize: 0,
                    lpOutBuffer: buffer.VoidPointer,
                    nOutBufferSize: checked((uint)buffer.ByteCapacity),
                    lpBytesReturned: out _,
                    lpOverlapped: null))
                {
                    WindowsError error = Errors.GetLastError();
                    switch (error)
                    {
                        case WindowsError.ERROR_MORE_DATA:
                            buffer.EnsureByteCapacity(((MOUNTDEV_NAME*)buffer.VoidPointer)->NameLength + (ulong)sizeof(MOUNTDEV_NAME));
                            break;
                        default:
                            throw Errors.GetIoExceptionForError(error);
                    }
                }

                return ((MOUNTDEV_NAME*)buffer.VoidPointer)->Name.CreateString();
            });
        }

        /// <summary>
        /// Get the suggested link name for the device in the "\DosDevices\" format.
        /// </summary>
        public unsafe static string QuerySuggestedLinkName(SafeHandle deviceHandle)
        {
            return BufferHelper.BufferInvoke(((HeapBuffer buffer) =>
            {
                buffer.EnsureByteCapacity((ulong)sizeof(MOUNTDEV_SUGGESTED_LINK_NAME));
                ControlCode controlCode = ControlCodes.MountDevice.QuerySuggestedLinkName;

                while (!Imports.DeviceIoControl(
                    hDevice: deviceHandle,
                    dwIoControlCode: controlCode,
                    lpInBuffer: null,
                    nInBufferSize: 0,
                    lpOutBuffer: buffer.VoidPointer,
                    nOutBufferSize: checked((uint)buffer.ByteCapacity),
                    lpBytesReturned: out _,
                    lpOverlapped: null))
                {
                    WindowsError error = Errors.GetLastError();
                    switch (error)
                    {
                        case WindowsError.ERROR_MORE_DATA:
                            buffer.EnsureByteCapacity(((MOUNTDEV_SUGGESTED_LINK_NAME*)buffer.VoidPointer)->NameLength + (ulong)sizeof(MOUNTDEV_SUGGESTED_LINK_NAME));
                            break;
                        default:
                            throw Errors.GetIoExceptionForError(error);
                    }
                }

                return (((MOUNTDEV_SUGGESTED_LINK_NAME*)buffer.VoidPointer)->Name).CreateString();
            }));
        }

        /// <summary>
        /// Get the unique ID as presented to the mount manager by a device.
        /// </summary>
        public unsafe static byte[] QueryUniqueId(SafeHandle deviceHandle)
        {
            return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                buffer.EnsureByteCapacity((ulong)sizeof(MOUNTDEV_UNIQUE_ID));
                ControlCode controlCode = ControlCodes.MountDevice.QueryUniqueId;

                while (!Imports.DeviceIoControl(
                    hDevice: deviceHandle,
                    dwIoControlCode: controlCode,
                    lpInBuffer: null,
                    nInBufferSize: 0,
                    lpOutBuffer: buffer.VoidPointer,
                    nOutBufferSize: checked((uint)buffer.ByteCapacity),
                    lpBytesReturned: out _,
                    lpOverlapped: null))
                {
                    WindowsError error = Errors.GetLastError();
                    switch (error)
                    {
                        case WindowsError.ERROR_MORE_DATA:
                            buffer.EnsureByteCapacity(((MOUNTDEV_UNIQUE_ID*)buffer.VoidPointer)->UniqueIdLength + (ulong)sizeof(MOUNTDEV_UNIQUE_ID));
                            break;
                        default:
                            throw Errors.GetIoExceptionForError(error);
                    }
                }

                return ((MOUNTDEV_UNIQUE_ID*)buffer.VoidPointer)->UniqueId.ToArray();
            });
        }

        /// <summary>
        /// Gets the DOS drive letter for the given volume if it has one.
        /// </summary>
        public unsafe static string QueryDosVolumePath(string volume)
        {
            var mountManager = Storage.Storage.CreateFile(
                @"\\?\MountPointManager", CreationDisposition.OpenExisting, 0);

            ControlCode controlCode = ControlCodes.MountManager.QueryDosVolumePath;

            // Read ulong then get string
            string dosVolumePath = null;

            BufferHelper.BufferInvoke((StringBuffer inBuffer) =>
            {
                // The input is MOUNTMGR_TARGET_NAME which is a short length in bytes followed by the unicode string
                // https://msdn.microsoft.com/en-us/library/windows/hardware/ff562289.aspx

                inBuffer.Append((char)(volume.Length * sizeof(char)));
                    inBuffer.Append(volume);

                BufferHelper.BufferInvoke((StringBuffer outBuffer) =>
                {
                    // Give enough for roughly 50 characters for a start
                    outBuffer.EnsureCharCapacity(50);

                    while (!Imports.DeviceIoControl(
                        hDevice: mountManager,
                        dwIoControlCode: controlCode,
                        lpInBuffer: inBuffer.VoidPointer,
                        nInBufferSize: checked((uint)inBuffer.ByteCapacity),
                        lpOutBuffer: outBuffer.VoidPointer,
                        nOutBufferSize: checked((uint)outBuffer.ByteCapacity),
                        lpBytesReturned: out _,
                        lpOverlapped: null))
                    {
                        WindowsError error = Errors.GetLastError();
                        switch (error)
                        {
                            case WindowsError.ERROR_MORE_DATA:
                                outBuffer.EnsureByteCapacity(checked(outBuffer.ByteCapacity * 2));
                                break;
                            default:
                                throw Errors.GetIoExceptionForError(error, volume);
                        }
                    }

                    // MOUNTMGR_VOLUME_PATHS is a uint length followed by a multi string (double null terminated)
                    // we only care about the first string in this case (should only be one?) so we can skip beyond
                    // the 4 bytes and read to null.
                    dosVolumePath = new string(outBuffer.CharPointer + 2);
                });
            });

            return dosVolumePath;
        }

        /// <summary>
        /// Gets reparse point names for symbolic links and mount points.
        /// </summary>
        /// <param name="fileHandle">Handle for the reparse point, must be opened with <see cref="FileAccessRights.ReadExtendedAttributes"/>.</param>
        public unsafe static (string printName, string substituteName, ReparseTag tag) GetReparsePointNames(SafeFileHandle fileHandle)
        {
            return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                ControlCode controlCode = ControlCodes.FileSystem.GetReparsePoint;
                buffer.EnsureByteCapacity(1024);

                while (!Imports.DeviceIoControl(
                    hDevice: fileHandle,
                    dwIoControlCode: controlCode,
                    lpInBuffer: null,
                    nInBufferSize: 0,
                    lpOutBuffer: buffer.VoidPointer,
                    nOutBufferSize: checked((uint)buffer.ByteCapacity),
                    lpBytesReturned: out _,
                    lpOverlapped: null))
                {
                    WindowsError error = Errors.GetLastError();
                    switch (error)
                    {
                        case WindowsError.ERROR_SUCCESS:
                            // This can happen if the handle isn't a reparse point.
                            break;
                        case WindowsError.ERROR_MORE_DATA:
                            buffer.EnsureByteCapacity(buffer.ByteCapacity + 1024);
                            break;
                        default:
                            throw Errors.GetIoExceptionForError(error);
                    }
                }

                ReparseTag reparseTag = ((REPARSE_DATA_BUFFER*)buffer.VoidPointer)->ReparseTag;

                string printName = null;
                string substitutename = null;

                if (reparseTag == ReparseTag.MountPoint)
                {
                    printName = ((REPARSE_DATA_BUFFER*)buffer.VoidPointer)->MountPointData.PrintName.CreateString();
                    substitutename = ((REPARSE_DATA_BUFFER*)buffer.VoidPointer)->MountPointData.SubstituteName.CreateString();
                }
                else if (reparseTag == ReparseTag.SymbolicLink)
                {
                    printName = ((REPARSE_DATA_BUFFER*)buffer.VoidPointer)->SymbolicLinkData.PrintName.CreateString();
                    substitutename = ((REPARSE_DATA_BUFFER*)buffer.VoidPointer)->SymbolicLinkData.SubstituteName.CreateString();
                }

                return (printName, substitutename, reparseTag);
            });
        }
    }
}
