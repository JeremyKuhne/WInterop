﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff545848.aspx
    public struct FILE_POSITION_INFORMATION
    {
        public long CurrentByteOffset;
    }
}
