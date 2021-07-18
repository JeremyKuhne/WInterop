// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Devices
{
    public static partial class ControlCodes
    {
        /// <summary>
        ///  Client codes used by device drivers that support the mount manager.
        /// </summary>
        public enum MountDevice : uint
        {
            /// <summary>
            ///  Mount manager client must return a unique ID. ID is returned in MOUNTDEV_UNIQUE_ID.
            ///  [IOCTL_MOUNTDEV_QUERY_UNIQUE_ID]
            /// </summary>
            /// <remarks>
            ///  https://msdn.microsoft.com/en-us/library/windows/hardware/ff560441.aspx
            ///  CTL_CODE(MOUNTDEVCONTROLTYPE, 0, METHOD_BUFFERED, FILE_ANY_ACCESS)
            /// </remarks>
            QueryUniqueId = 0x004d0000,

            /// <summary>
            ///  Obsolete as of Windows 7. Use the GUID_IO_VOLUME_UNIQUE_ID_CHANGE Plug and Play notification event instead.
            ///  [IOCTL_MOUNTDEV_UNIQUE_ID_CHANGE_NOTIFY]
            /// </summary>
            /// <remarks>
            ///  https://msdn.microsoft.com/en-us/library/windows/hardware/ff560443.aspx
            ///  CTL_CODE(MOUNTDEVCONTROLTYPE, 1, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            UniqueIdChangeNotify = 0x004dc004,

            /// <summary>
            ///  Client driver must return the device name for the volume. (Such as "\Device\HarddiskVolume1".)
            ///  Name is returned in MOUNTDEV_NAME.
            ///  [IOCTL_MOUNTDEV_QUERY_DEVICE_NAME]
            /// </summary>
            /// <remarks>
            ///  https://msdn.microsoft.com/en-us/library/windows/hardware/ff560437.aspx
            ///  CTL_CODE(MOUNTDEVCONTROLTYPE, 2, METHOD_BUFFERED, FILE_ANY_ACCESS)
            /// </remarks>
            QueryDeviceName = 0x004d0008,

            /// <summary>
            ///  Allows the mount manager client to suggest a link name for the device in the "\DosDevices\" format.
            ///  Name is returned in MOUNTDEV_SUGGESTED_LINK_NAME. If the mount manager already has a link it will ignore the suggestion.
            ///  [IOCTL_MOUNTDEV_QUERY_SUGGESTED_LINK_NAME]
            /// </summary>
            /// <remarks>
            ///  https://msdn.microsoft.com/en-us/library/windows/hardware/ff560440.aspx
            ///  CTL_CODE(MOUNTDEVCONTROLTYPE, 3, METHOD_BUFFERED, FILE_ANY_ACCESS)
            /// </remarks>
            QuerySuggestedLinkName = 0x004d000c,

            /// <summary>
            ///  Sent to the device by the mount manager when a link is created. [IOCTL_MOUNTDEV_LINK_CREATED]
            /// </summary>
            /// <remarks>
            ///  https://msdn.microsoft.com/en-us/library/windows/hardware/ff560434.aspx
            ///  CTL_CODE(MOUNTDEVCONTROLTYPE, 4, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            LinkCreated = 0x004dc010,

            /// <summary>
            ///  Sent to the device by the mount manager when a link is deleted. [IOCTL_MOUNTDEV_LINK_DELETED]
            /// </summary>
            /// <remarks>
            ///  https://msdn.microsoft.com/en-us/library/windows/hardware/ff560436.aspx
            ///  CTL_CODE(MOUNTDEVCONTROLTYPE, 5, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            LinkDeleted = 0x004dc014,

            /// <summary>
            ///  Optional to support. Allows a device to give a persistent guid for the device.
            ///  Returns guid in MOUNTDEV_STABLE_GUID.
            ///  [IOCTL_MOUNTDEV_QUERY_STABLE_GUID]
            /// </summary>
            /// <remarks>
            ///  Introduced in Windows XP.
            ///  CTL_CODE(MOUNTDEVCONTROLTYPE, 6, METHOD_BUFFERED, FILE_ANY_ACCESS)
            /// </remarks>
            QueryStableGuid = 0x004d0018,

            /// <summary>
            ///  Passes back the interface path for the volume. Returns the value in MOUNTDEV_NAME.
            ///  [IOCTL_MOUNTDEV_QUERY_INTERFACE_NAME]
            /// </summary>
            /// <remarks>
            ///  Introduced in Windows 10 Anniversary Update (RS1).
            ///  CTL_CODE(MOUNTDEVCONTROLTYPE, 7, METHOD_BUFFERED, FILE_ANY_ACCESS)
            /// </remarks>
            QueryInterfaceName = 0x004d001c
        }
    }
}