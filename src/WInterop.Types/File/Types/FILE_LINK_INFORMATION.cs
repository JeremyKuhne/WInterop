// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.File.Types
{
    /// <summary>
    /// Used to create an NTFS hard link to an existing file.
    /// </summary>
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff540324.aspx
    /// </remarks>
    public struct FILE_LINK_INFORMATION
    {
        public BOOLEAN ReplaceIfExists;
        public IntPtr RootDirectory;
        public uint FileNameLength;
        public TrailingString FileName;
    }
}
