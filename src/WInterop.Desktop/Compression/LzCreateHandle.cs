// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Compression.Native;

namespace WInterop.Compression
{
    public class LzCreateHandle : LzHandle
    {
        public LzCreateHandle(int handle) : base(handle) { }

        protected override void Dispose(bool disposing)
        {
            Imports.LZCloseFile(RawHandle);
        }
    }
}
