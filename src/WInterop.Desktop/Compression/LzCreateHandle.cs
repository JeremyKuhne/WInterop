﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Compression.Types
{
    public class LzCreateHandle : LzHandle
    {
        public LzCreateHandle(int handle) : base(handle) { }

        protected override void Dispose(bool disposing)
        {
            CompressionMethods.Imports.LZCloseFile(RawHandle);
        }
    }
}
