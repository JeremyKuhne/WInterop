// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement
{
    /// <summary>
    /// Flags used by the <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa363852.aspx">CopyFileEx</a> API.
    /// </summary>
    [Flags]
    public enum CopyFileExFlags : uint
    {
        /// <summary>
        /// The copy operation fails immediately if the target file already exists.
        /// </summary>
        COPY_FILE_FAIL_IF_EXISTS = 0x00000001,

        /// <summary>
        /// Progress of the copy is tracked in the target file in case the copy fails. The failed copy can be restarted at a later time by specifying the same values
        /// for lpExistingFileName and lpNewFileName as those used in the call that failed. This can significantly slow down the copy operation as the new file may
        /// be flushed multiple times during the copy operation.
        /// </summary>
        COPY_FILE_RESTARTABLE = 0x00000002,

        /// <summary>
        /// The file is copied and the original file is opened for write access.
        /// </summary>
        COPY_FILE_OPEN_SOURCE_FOR_WRITE = 0x00000004,

        /// <summary>
        /// An attempt to copy an encrypted file will succeed even if the destination copy cannot be encrypted.
        /// </summary>
        COPY_FILE_ALLOW_DECRYPTED_DESTINATION = 0x00000008,

        /// <summary>
        /// If the source file is a symbolic link, the destination file is also a symbolic link pointing to the same file that the source symbolic link is pointing to. 
        /// </summary>
        COPY_FILE_COPY_SYMLINK = 0x00000800,

        /// <summary>
        /// The copy operation is performed using unbuffered I/O, bypassing system I/O cache resources. Recommended for very large file transfers.
        /// </summary>
        COPY_FILE_NO_BUFFERING = 0x00001000
    }
}
