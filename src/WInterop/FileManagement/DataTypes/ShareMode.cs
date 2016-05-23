// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363874.aspx
    /// <summary>
    /// Equivalent to System.IO.FileShare.
    /// </summary>
    /// <remarks>
    /// System.IO.FileShare contains an additional flag, "Inheritable", that has nothing to do with Windows.
    /// It is used to decide how to create the SECURITY_ATTRIBUTES for CreateFile. SECURITY_ATTRIBUTES.bInheritHandle
    /// will be set to 1 if Inheritable is set.
    /// </remarks>
    [Flags]
    public enum ShareMode : uint
    {
        // Not actually defined in Windows, for convenience.
        FILE_SHARE_NONE = 0,
        FILE_SHARE_READ = 0x00000001,
        FILE_SHARE_WRITE = 0x00000002,

        // Not actually defined in Windows, for convenience.
        FILE_SHARE_READWRITE = FILE_SHARE_READ | FILE_SHARE_WRITE,
        FILE_SHARE_DELETE = 0x00000004
    }
}
