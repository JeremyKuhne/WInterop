// ----------------------
//    xTask Framework
// ----------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Collections;

namespace WInterop.Handles
{
    /// <summary>
    /// Allows limited reuse of heap buffers to improve memory pressure. This cache does not ensure
    /// that multiple copies of handles are not released back into the cache.
    /// </summary>
    public sealed class HeapHandleCache : Cache<SafeHeapHandle>
    {
        private ulong _minSize;
        private ulong _maxSize;

        private static readonly HeapHandleCache s_Instance = new HeapHandleCache();

        public HeapHandleCache(ulong minSize = 64, ulong maxSize = 1024 * 2, int maxHandles = 0)
            : base(cacheSpace: maxHandles)
        {
            _minSize = minSize;
            _maxSize = maxSize;
        }

        public static HeapHandleCache Instance
        {
            get { return s_Instance; }
        }

        /// <summary>
        /// Get a HeapHandle
        /// </summary>
        public SafeHeapHandle Acquire(ulong minSize)
        {
            SafeHeapHandle handle = Acquire();
            if (minSize < _minSize) minSize = _minSize;
            if (handle.ByteLength < minSize)
            {
                handle.Resize(minSize);
            }

            return handle;
        }

        public override void Release(SafeHeapHandle item)
        {
            if (item.ByteLength <= _maxSize)
                base.Release(item);
        }
    }
}
