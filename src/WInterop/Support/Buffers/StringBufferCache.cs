// ------------------------
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

        public StringBufferCache(int maxBuffers, uint maxSize = 1024) : base(maxBuffers)
        {
            _maxSize = maxSize;
        }

        public StringBuffer Acquire(uint minCapacity)
        {
            StringBuffer item = Acquire();
            item.EnsureCharCapacity(minCapacity: minCapacity);
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
