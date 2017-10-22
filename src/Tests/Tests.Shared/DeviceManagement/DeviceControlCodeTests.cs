// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using FluentAssertions;
using WInterop.DeviceManagement.Types;
using Xunit;

namespace Tests.DeviceManagement
{
    public class DeviceControlCodeTests
    {
        [Theory,
            // IOCTL_MOUNTMGR_CREATE_POINT                 CTL_CODE(MOUNTMGRCONTROLTYPE, 0, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.CreatePoint, 0, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_DELETE_POINTS                CTL_CODE(MOUNTMGRCONTROLTYPE, 1, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.DeletePoints, 1, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_QUERY_POINTS                 CTL_CODE(MOUNTMGRCONTROLTYPE, 2, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountManager.QueryPoints, 2, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTMGR_DELETE_POINTS_DBONLY         CTL_CODE(MOUNTMGRCONTROLTYPE, 3, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.DeletePointsDatabaseOnly, 3, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_NEXT_DRIVE_LETTER            CTL_CODE(MOUNTMGRCONTROLTYPE, 4, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.NextDriveLetter, 4, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_AUTO_DL_ASSIGNMENTS          CTL_CODE(MOUNTMGRCONTROLTYPE, 5, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.AutoDriveLetterAssignments, 5, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_VOLUME_MOUNT_POINT_CREATED   CTL_CODE(MOUNTMGRCONTROLTYPE, 6, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.VolumeMountPointCreated, 6, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_VOLUME_MOUNT_POINT_DELETED   CTL_CODE(MOUNTMGRCONTROLTYPE, 7, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.VolumeMountPointDeleted, 7, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_CHANGE_NOTIFY                CTL_CODE(MOUNTMGRCONTROLTYPE, 8, METHOD_BUFFERED, FILE_READ_ACCESS)
            InlineData(ControlCodes.MountManager.ChangeNotify, 8, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // IOCTL_MOUNTMGR_KEEP_LINKS_WHEN_OFFLINE      CTL_CODE(MOUNTMGRCONTROLTYPE, 9, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.KeepLinksWhenOffline, 9, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_CHECK_UNPROCESSED_VOLUMES    CTL_CODE(MOUNTMGRCONTROLTYPE, 10, METHOD_BUFFERED, FILE_READ_ACCESS)
            InlineData(ControlCodes.MountManager.CheckUnprocessedVolumes, 10, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // IOCTL_MOUNTMGR_VOLUME_ARRIVAL_NOTIFICATION  CTL_CODE(MOUNTMGRCONTROLTYPE, 11, METHOD_BUFFERED, FILE_READ_ACCESS)
            InlineData(ControlCodes.MountManager.VolumeArrivalNotification, 11, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATH        CTL_CODE(MOUNTMGRCONTROLTYPE, 12, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountManager.QueryDosVolumePath, 12, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATHS       CTL_CODE(MOUNTMGRCONTROLTYPE, 13, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountManager.QueryDosVolumePaths, 13, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTMGR_SCRUB_REGISTRY               CTL_CODE(MOUNTMGRCONTROLTYPE, 14, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.ScrubRegistry, 14, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_QUERY_AUTO_MOUNT             CTL_CODE(MOUNTMGRCONTROLTYPE, 15, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountManager.QueryAutoMount, 15, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTMGR_SET_AUTO_MOUNT               CTL_CODE(MOUNTMGRCONTROLTYPE, 16, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.SetAutoMount, 16, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_BOOT_DL_ASSIGNMENT           CTL_CODE(MOUNTMGRCONTROLTYPE, 17, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.BootDriveLetterAssignment, 17, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_TRACELOG_CACHE               CTL_CODE(MOUNTMGRCONTROLTYPE, 18, METHOD_BUFFERED, FILE_READ_ACCESS)
            InlineData(ControlCodes.MountManager.TracelogCache, 18, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // IOCTL_MOUNTMGR_PREPARE_VOLUME_DELETE        CTL_CODE(MOUNTMGRCONTROLTYPE, 19, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.PrepareVolumeDelete, 19, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_CANCEL_VOLUME_DELETE         CTL_CODE(MOUNTMGRCONTROLTYPE, 20, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.CancelVolumeDelete, 20, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_SILO_ARRIVAL                 CTL_CODE(MOUNTMGRCONTROLTYPE, 21, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.SiloArrival, 21, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite)
            ]
        public void ValidateMountManagerCodes(ControlCodes.MountManager code, uint function, ControlCodeMethod method, ControlCodeAccess access)
        {
            ControlCode generatedCode = new ControlCode(ControlCodeDeviceType.MountManager, function, method, access);
            ((ControlCode)code).Should().Be(generatedCode, $"generated code is 0x{generatedCode.Value:x8}");
        }

        [Theory,
            // IOCTL_MOUNTDEV_QUERY_UNIQUE_ID              CTL_CODE(MOUNTDEVCONTROLTYPE, 0, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountDevice.QueryUniqueId, 0, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTDEV_UNIQUE_ID_CHANGE_NOTIFY      CTL_CODE(MOUNTDEVCONTROLTYPE, 1, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountDevice.UniqueIdChangeNotify, 1, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTDEV_QUERY_DEVICE_NAME            CTL_CODE(MOUNTDEVCONTROLTYPE, 2, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountDevice.QueryDeviceName, 2, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTDEV_QUERY_SUGGESTED_LINK_NAME    CTL_CODE(MOUNTDEVCONTROLTYPE, 3, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountDevice.QuerySuggestedLinkName, 3, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTDEV_LINK_CREATED                 CTL_CODE(MOUNTDEVCONTROLTYPE, 4, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountDevice.LinkCreated, 4, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTDEV_LINK_DELETED                 CTL_CODE(MOUNTDEVCONTROLTYPE, 5, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountDevice.LinkDeleted, 5, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTDEV_QUERY_STABLE_GUID            CTL_CODE(MOUNTDEVCONTROLTYPE, 6, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountDevice.QueryStableGuid, 6, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTDEV_QUERY_INTERFACE_NAME         CTL_CODE(MOUNTDEVCONTROLTYPE, 7, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountDevice.QueryInterfaceName, 7, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            ]
        public void ValidateMountDeviceCodes(ControlCodes.MountDevice code, uint function, ControlCodeMethod method, ControlCodeAccess access)
        {
            ControlCode generatedCode = new ControlCode(ControlCodeDeviceType.MountDevice, function, method, access);
            ((ControlCode)code).Should().Be(generatedCode, $"generated code is 0x{generatedCode.Value:x8}");
        }

    }
}
