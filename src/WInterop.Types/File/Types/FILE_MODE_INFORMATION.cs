// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.File.Types
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/hardware/ff545809.aspx">FILE_MODE_INFORMATION</a> structure.
    /// Used to get the access mode for a handle through NtQueryInformationFile with FileInformationClass.FileModeInformation.
    /// </summary>
    public struct FILE_MODE_INFORMATION
    {
        public FileAccessModes Mode;
    }
}
