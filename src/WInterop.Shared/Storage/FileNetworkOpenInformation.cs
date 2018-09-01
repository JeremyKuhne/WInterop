// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// Basic file information. Used by NtQueryFullAttributesFile and with
    /// <see cref="FileInformationClass.FileNetworkOpenInformation"/>. [FILE_NETWORK_OPEN_INFORMATION]
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff545822.aspx"/>
    /// GetFileAttributesEx calls NtQueryFullAttributesFile.
    /// </remarks>
    public struct FileNetworkOpenInformation
    {
        public LongFileTime CreationTime;
        public LongFileTime LastAccessTime;
        public LongFileTime LastWriteTime;
        public LongFileTime ChangeTime;
        public long AllocationSize;
        public long EndOfFile;
        public FileAttributes FileAttributes;
    }
}
