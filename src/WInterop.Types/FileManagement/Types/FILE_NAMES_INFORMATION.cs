// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff540329.aspx

    public struct FILE_NAMES_INFORMATION
    {
        public uint NextEntryOffset;
        public uint FileIndex;
        public TrailingString.SizedInBytes FileName;
    }
}
