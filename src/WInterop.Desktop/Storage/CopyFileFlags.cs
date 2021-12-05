// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage;

/// <summary>
///  Flags used by the <a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-copyfileexw">CopyFileEx</a> and
///  <a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-copyfile2">CopyFile2</a> APIs.
/// </summary>
[Flags]
public enum CopyFileFlags : uint
{
    /// <summary>
    ///  The copy operation fails immediately if the target file already exists.
    ///  [COPY_FILE_FAIL_IF_EXISTS]
    /// </summary>
    FailIfExists = COPY.COPY_FILE_FAIL_IF_EXISTS,

    /// <summary>
    ///  Progress of the copy is tracked in the target file in case the copy fails. The failed copy can be restarted at a later time by specifying the same values
    ///  for lpExistingFileName and lpNewFileName as those used in the call that failed. This can significantly slow down the copy operation as the new file may
    ///  be flushed multiple times during the copy operation. [COPY_FILE_RESTARTABLE]
    /// </summary>
    Restartable = COPY.COPY_FILE_RESTARTABLE,

    /// <summary>
    ///  The file is copied and the original file is opened for write access. [COPY_FILE_OPEN_SOURCE_FOR_WRITE]
    /// </summary>
    OpenSourceForWrite = COPY.COPY_FILE_OPEN_SOURCE_FOR_WRITE,

    /// <summary>
    ///  An attempt to copy an encrypted file will succeed even if the destination copy cannot be encrypted. [COPY_FILE_ALLOW_DECRYPTED_DESTINATION]
    /// </summary>
    AllowDecryptedDestination = COPY.COPY_FILE_ALLOW_DECRYPTED_DESTINATION,

    /// <summary>
    ///  If the source file is a symbolic link, the destination file is also a symbolic link pointing to the same file
    ///  that the source symbolic link is pointing to. [COPY_FILE_COPY_SYMLINK]
    /// </summary>
    CopySymbolicLink = COPY.COPY_FILE_COPY_SYMLINK,

    /// <summary>
    ///  The copy operation is performed using unbuffered I/O, bypassing system I/O cache resources. Recommended for very large file transfers.
    ///  [COPY_FILE_NO_BUFFERING]
    /// </summary>
    NoBuffering = COPY.COPY_FILE_NO_BUFFERING,

    /// <summary>
    ///  (CopyFile2 only) The copy is attempted, specifying ACCESS_SYSTEM_SECURITY for the source file and ACCESS_SYSTEM_SECURITY | WRITE_DAC | WRITE_OWNER for the
    ///  destination file. If these requests are denied the access request will be reduced to the highest privilege level for which access is granted. For
    ///  more information see SACL Access Right. This can be used to allow the CopyFile2ProgressRoutine callback to perform operations requiring higher
    ///  privileges, such as copying the security attributes for the file. [COPY_FILE_REQUEST_SECURITY_PRIVILEGES]
    /// </summary>
    RequestSecurityPrivileges = COPY.COPY_FILE_REQUEST_SECURITY_PRIVILEGES,

    /// <summary>
    ///  (CopyFile2 only) The destination file is examined to see if it was copied using COPY_FILE_RESTARTABLE. If so the copy is resumed.
    ///  If not the file will be fully copied. [COPY_FILE_RESUME_FROM_PAUSE]
    /// </summary>
    ResumeFromPause = COPY.COPY_FILE_RESUME_FROM_PAUSE,

    /// <summary>
    ///  (CopyFile2 only) Do not attempt to use the Windows Copy Offload mechanism. This is not generally recommended.
    ///  [COPY_FILE_NO_OFFLOAD]
    /// </summary>
    NoOffload = COPY.COPY_FILE_NO_OFFLOAD,
}