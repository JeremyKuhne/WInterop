// ----------------------
//    xTask Framework
// ----------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;

namespace WInterop.Support.Collections
{
    /// <summary>
    /// Light weight multithreaded fixed size cache class.
    /// </summary>
    public class Cache<T> : IDisposable where T : class, new()
    {
        protected static string s_type = typeof(T).ToString();

        // Protected for testing
        protected readonly T?[] _itemsCache;

        /// <summary>
        /// Create a cache with space for the specified number of items.
        /// </summary>
        public Cache(int cacheSpace)
        {
            if (cacheSpace < 1) cacheSpace = System.Environment.ProcessorCount * 4;
            _itemsCache = new T[cacheSpace];
        }

        /// <summary>
        /// Get an item from the cache or create one if none are available.
        /// </summary>
        public virtual T Acquire()
        {
            CacheEventSource.Log.ObjectAquired(s_type);

            T? item;

            for (int i = 0; i < _itemsCache.Length; i++)
            {
                item = Interlocked.Exchange(ref _itemsCache[i], null);
                if (item != null) return item;
            }

            CacheEventSource.Log.ObjectCreated(s_type);
            return new T();
        }

        /// <summary>
        /// Release an item back to the cache, disposing if no room is available.
        /// </summary>
        public virtual void Release(T item)
        {
            CacheEventSource.Log.ObjectReleased(s_type);

            T? temp = item;

            for (int i = 0; i < _itemsCache.Length; i++)
            {
                temp = Interlocked.Exchange(ref _itemsCache[i], temp);
                if (temp is null) return;
            }

            CacheEventSource.Log.ObjectDestroyed(s_type, "NoSlot");
            (temp as IDisposable)?.Dispose();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                for (int i = 0; i < _itemsCache.Length; i++)
                {
                    (_itemsCache[i] as IDisposable)?.Dispose();
                    _itemsCache[i] = null;
                }
            }
        }
    }
}
