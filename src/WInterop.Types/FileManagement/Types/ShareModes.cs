// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Equivalent to System.IO.FileShare.
    /// </summary>
    /// <remarks>
    /// System.IO.FileShare contains an additional flag, "Inheritable", that has nothing to do with Windows.
    /// It is used to decide how to create the SECURITY_ATTRIBUTES for CreateFile. SECURITY_ATTRIBUTES.bInheritHandle
    /// will be set to 1 if Inheritable is set.
    /// </remarks>
    [Flags]
    public enum ShareModes : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363874.aspx

        /// <summary>
        /// Allow other read handles to be opened. (FILE_SHARE_READ)
        /// </summary>
        Read = 0x00000001,

        /// <summary>
        /// Allow other write handles to be opened. (FILE_SHARE_WRITE)
        /// </summary>
        Write = 0x00000002,

        /// <summary>
        /// Not actually defined in Windows, for convenience.
        /// </summary>
        ReadWrite = Read | Write,

        /// <summary>
        /// Allow others to delete the file.
        /// </summary>
        Delete = 0x00000004
    }
}
