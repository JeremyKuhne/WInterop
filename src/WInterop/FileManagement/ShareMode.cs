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
    [Flags]
    public enum ShareMode : uint
    {
        None = 0,
        FILE_SHARE_READ = 0x00000001,
        FILE_SHARE_WRITE = 0x00000002,
        ReadWrite = FILE_SHARE_READ | FILE_SHARE_WRITE,
        FILE_SHARE_DELETE = 0x00000004
    }
}
