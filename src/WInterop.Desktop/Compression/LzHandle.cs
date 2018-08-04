﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Compression.Types
{
    public class LzHandle : IDisposable
    {
        // The handles used by the LZ methods are not IntPtr like most handles.

        public LzHandle(int handle)
        {
            RawHandle = handle;
        }

        public int RawHandle { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            CompressionMethods.Imports.LZClose(RawHandle);
        }

        ~LzHandle()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static implicit operator int(LzHandle handle) => handle.RawHandle;
    }
}
