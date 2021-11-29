// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage;

[Flags]
public enum OpenFileStyle : ushort
{
    /// <summary>
    ///  Open for reading. [OF_READ]
    /// </summary>
    Read = 0x00000000,

    /// <summary>
    ///  Open for writing. [OF_WRITE]
    /// </summary>
    Write = 0x00000001,

    /// <summary>
    ///  Open for reading and writing. [OF_READWRITE]
    /// </summary>
    ReadWrite = 0x00000002,

    /// <summary>
    ///  In DOS this would open the file with Read/Write sharing and would deny opening
    ///  with other share modes. Now this maps to ShareMode.Read and ShareMode.Write
    ///  (FILE_SHARE_READ | FILE_SHARE_WRITE). [OF_SHARE_COMPAT]
    /// </summary>
    ShareCompat = 0x00000000,

    /// <summary>
    ///  Deny read and write access for others. [OF_SHARE_EXCLUSIVE]
    /// </summary>
    ShareExclusive = 0x00000010,

    /// <summary>
    ///  Deny write access for others. Mapes to ShareMode.Read
    ///  (FILE_SHARE_READ). [OF_SHARE_DENY_WRITE]
    /// </summary>
    ShareDenyWrite = 0x00000020,

    /// <summary>
    ///  Deny read access for others. Maps to ShareMode.Write (FILE_SHARE_WRITE).
    ///  [OF_SHARE_DENY_READ]
    /// </summary>
    ShareDenyRead = 0x00000030,

    /// <summary>
    ///  Allow read and write access for others. Maps to ShareMode.Read and ShareMode.Write
    ///  (FILE_SHARE_READ | FILE_SHARE_WRITE). [OF_SHARE_DENY_NONE]
    /// </summary>
    ShareDenyNone = 0x00000040,

    /// <summary>
    ///  Fills the OFSTRUCT, but carries out no other action. [OF_PARSE]
    /// </summary>
    Parse = 0x00000100,

    /// <summary>
    ///  Deletes the file. [OF_DELETE]
    /// </summary>
    Delete = 0x00000200,

    /// <summary>
    ///  [OF_VERIFY]
    /// </summary>
    Verify = 0x00000400,

    /// <summary>
    ///  Ignored. For compat with 16-bit Windows. [OF_CANCEL]
    /// </summary>
    Cancel = 0x00000800,

    /// <summary>
    ///  Create a new file- if it exists it is truncated. [OF_CREATE]
    /// </summary>
    Create = 0x00001000,

    /// <summary>
    ///  Display a dialog box if the requested file does not exist. [OF_PROMPT]
    /// </summary>
    Prompt = 0x00002000,

    /// <summary>
    ///  Opens the file then closes it to test for a file's existence. [OF_EXIST]
    /// </summary>
    Exist = 0x00004000,

    /// <summary>
    ///  Opens the file using information in the reopen buffer. [OF_REOPEN]
    /// </summary>
    Reopen = 0x00008000
}