// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Devices
{
    /// <summary>
    ///  [MOUNTMGR_DRIVE_LETTER_INFORMATION]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff562281.aspx
    public readonly struct MountManagerDriveLetterInformation
    {
        public readonly ByteBoolean DriveLetterWasAssigned;
        public readonly Char8 CurrentDriveLetter;
    }
}
