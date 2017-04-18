// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Support.Buffers
{
    public abstract class SizedBuffer : ISizedBuffer, IDisposable
    {
        public abstract ulong ByteCapacity { get; }

        public abstract IntPtr DangerousGetHandle();

        public void Dispose()
        {
            Dispose(disposing: true);
        }

        protected abstract void Dispose(bool disposing);
    }
}
