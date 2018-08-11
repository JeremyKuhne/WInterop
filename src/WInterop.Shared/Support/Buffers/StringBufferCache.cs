﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support.Collections;

namespace WInterop.Support.Buffers
{
    /// <summary>
    /// Allows caching of StringBuffer objects to ease GC pressure when creating many StringBuffers.
    /// </summary>
    public sealed class StringBufferCache : Cache<StringBuffer>
    {
        private static readonly StringBufferCache s_Instance = new StringBufferCache(0);
        private uint _maxSize;

        public StringBufferCache(int maxBuffers, uint maxSize = 4096) : base(maxBuffers)
        {
            _maxSize = maxSize;
        }

        public StringBuffer Acquire(uint minCharCapacity)
        {
            StringBuffer item;

            // Don't want to end up resizing a buffer we'll throw away
            if (minCharCapacity > _maxSize)
            {
                item = new StringBuffer(minCharCapacity);
                CacheEventSource.Log.ObjectCreated(s_type);
            }
            else
            {
                item = Acquire();
                item.EnsureCharCapacity(minCapacity: minCharCapacity);
            }
            return item;
        }

        public static StringBufferCache Instance
        {
            get { return s_Instance; }
        }

        public override void Release(StringBuffer item)
        {
            if (item.CharCapacity <= _maxSize)
            {
                item.Length = 0;
                base.Release(item);
            }
            else
            {
                CacheEventSource.Log.ObjectDestroyed(s_type, "OverSize");
                item.Dispose();
            }
        }

        public string ToStringAndRelease(StringBuffer item)
        {
            string returnValue = item.ToString();
            Release(item);
            return returnValue;
        }
    }
}
