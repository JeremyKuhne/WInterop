// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Collections;
using WInterop.ErrorHandling;

namespace WInterop.Buffers
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

        /// <summary>
        /// Invoke the given action on a cached buffer.
        /// </summary>
        public static void CachedBufferInvoke(Action<StringBuffer> action)
        {
            var buffer = Instance.Acquire();
            try
            {
                action(buffer);
            }
            finally
            {
                Instance.Release(buffer);
            }
        }

        public static T CachedBufferInvoke<T>(Func<StringBuffer, T> func)
        {
            return CachedBufferInvoke(0, func);
        }

        public static T CachedBufferInvoke<T>(uint minCapacity, Func<StringBuffer, T> func)
        {
            var buffer = Instance.Acquire(minCapacity);
            try
            {
                return func(buffer);
            }
            finally
            {
                Instance.Release(buffer);
            }
        }

        /// <summary>
        /// Uses the stringbuilder cache and increases the buffer size if needed.
        /// </summary>
        public static string BufferInvoke(Func<StringBuffer, uint> invoker, string value = null, Func<WindowsError, bool> shouldThrow = null)
        {
            return CachedBufferInvoke(minCapacity: 260u, func: (buffer) =>
            {
                uint returnValue = 0;

                // Ensure enough room for the output string
                while ((returnValue = invoker(buffer)) > buffer.CharCapacity)
                    buffer.EnsureCharCapacity(returnValue);

                if (returnValue == 0)
                {
                    // Failed
                    WindowsError error = ErrorHelper.GetLastError();

                    if (shouldThrow != null && !shouldThrow(error))
                        return null;

                    throw ErrorHelper.GetIoExceptionForError(error, value);
                }

                buffer.Length = returnValue;
                return buffer.ToString();
            });
        }
    }
}
