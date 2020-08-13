// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Memory;
using WInterop.Support.Collections;

namespace WInterop.Handles
{
    /// <summary>
    ///  Allows limited reuse of heap buffers to improve memory pressure. This cache does not ensure
    ///  that multiple copies of handles are not released back into the cache.
    /// </summary>
    public sealed class HeapHandleCache : Cache<HeapHandle>
    {
        private readonly ulong _minSize;
        private readonly ulong _maxSize;

        public HeapHandleCache(ulong minSize = 64, ulong maxSize = 1024 * 2, int maxHandles = 0)
            : base(cacheSpace: maxHandles)
        {
            _minSize = minSize;
            _maxSize = maxSize;
        }

        public static HeapHandleCache Instance { get; } = new HeapHandleCache();

        /// <summary>
        ///  Get a HeapHandle
        /// </summary>
        public HeapHandle Acquire(ulong minSize)
        {
            HeapHandle handle = Acquire();
            if (minSize < _minSize) minSize = _minSize;
            if (handle.ByteLength < minSize)
            {
                handle.Resize(minSize);
            }

            return handle;
        }

        public override void Release(HeapHandle item)
        {
            if (item.ByteLength <= _maxSize)
                base.Release(item);
            else
                item.Dispose();
        }
    }
}
