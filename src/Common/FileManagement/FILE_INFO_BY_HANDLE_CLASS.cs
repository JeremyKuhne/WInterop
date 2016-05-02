// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364228.aspx
    public enum FILE_INFO_BY_HANDLE_CLASS : uint
    {
        // Store apps
        FileBasicInfo = 0,
        // Store apps
        FileStandardInfo = 1,
        // Store apps
        FileNameInfo = 2,
#if !(COMPACT)
        // Desktop only (MoveFileEx is supported)
        FileRenameInfo = 3,
        // Desktop only (DeleteFile is supported)
        FileDispositionInfo = 4,
        // Desktop only
        FileAllocationInfo = 5,
        // Desktop only (SetEndOfFile is supported)
        FileEndOfFileInfo = 6,
#endif
        // Store apps
        FileStreamInfo = 7,
#if !(COMPACT)
        // Store apps
        FileCompressionInfo = 8,
        // Store apps
        FileAttributeTagInfo = 9,
        // Store apps
        FileIdBothDirectoryInfo = 10, // 0xA
        // Store apps
        FileIdBothDirectoryRestartInfo = 11, // 0xB
        // Desktop only
        FileIoPriorityHintInfo = 12, // 0xC
        // Desktop only
        FileRemoteProtocolInfo = 13, // 0xD
        // Desktop only
        FileFullDirectoryInfo = 14, // 0xE
        // Desktop only
        FileFullDirectoryRestartInfo = 15, // 0xF
        // Store apps
        FileStorageInfo = 16, // 0x10
        // Store apps
        FileAlignmentInfo = 17, // 0x11
        // Desktop only
        FileIdInfo = 18, // 0x12
        // Server desktop only
        FileIdExtdDirectoryInfo = 19, // 0x13
        // Server desktop only
        FileIdExtdDirectoryRestartInfo = 20, // 0x14
        MaximumFileInfoByHandlesClass = 21
#endif
    }
}
