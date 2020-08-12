// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WInterop.Support.Buffers
{
    /// <summary>
    ///  Avoid using this directly. Create wrappers that can ensure proper length checks.
    /// </summary>
    public static class BufferHelper
    {
        /// <summary>
        ///  Splits a null terminated unicode string list. The final string is followed by a second null.
        ///  This is a common pattern Windows uses. Usually you should use StringBuffer and split on null. There
        ///  are some cases (such as GetEnvironmentStrings) where Windows returns a handle to a buffer it allocated
        ///  with no length.
        /// </summary>
        public static unsafe IEnumerable<string> SplitNullTerminatedStringList(IntPtr handle)
        {
            if (handle == IntPtr.Zero) throw new ArgumentNullException(nameof(handle));

            var strings = new List<string>();

            char* start = (char*)handle;
            char* current = start;

            for (uint i = 0; ; i++)
            {
                if (*current == '\0')
                {
                    // Split
                    strings.Add(new string(value: start, startIndex: 0, length: checked((int)(current - start))));
                    start = current + 1;

                    if (*start == '\0') break;
                }

                current++;
            }

            return strings;
        }

        public static unsafe string GetNullTerminatedAsciiString(ReadOnlySpan<byte> source)
        {
            if (source.Length == 0)
                return string.Empty;

            int length = source.IndexOf((byte)0x00);
            if (length == 0)
                return string.Empty;

            fixed (byte* start = &MemoryMarshal.GetReference(source))
                return Encoding.ASCII.GetString(start, length == -1 ? source.Length : length);
        }

        /// <summary>
        ///  Invoke the given action on a cached buffer that returns the given type.
        /// </summary>
        /// <example>
        ///  return BufferHelper.BufferInvoke((NativeBuffer buffer) => { return string.Empty; });
        /// </example>
        public static T BufferInvoke<TBuffer, T>(Func<TBuffer, T> func) where TBuffer : HeapBuffer
        {
            var wrapper = new FuncWrapper<TBuffer, T> { Func = func };
            return BufferInvoke<FuncWrapper<TBuffer, T>, TBuffer, T>(ref wrapper);
        }

        /// <summary>
        ///  Invoke the given action on a cached buffer that returns the given type.
        /// </summary>
        public static T BufferInvoke<TBufferFunc, TBuffer, T>(ref TBufferFunc func)
            where TBufferFunc : IBufferFunc<TBuffer, T>
            where TBuffer : HeapBuffer
        {
            var wrapper = new BufferFuncWrapper<TBufferFunc, TBuffer, T> { Func = func };
            BufferInvoke<BufferFuncWrapper<TBufferFunc, TBuffer, T>, TBuffer>(ref wrapper);
            return wrapper.Result;
        }

        /// <summary>
        ///  Invoke the given action on a set of cached buffers.
        /// </summary>
        public static T TwoBufferInvoke<TBuffer, T>(Func<TBuffer, TBuffer, T> func)
            where TBuffer : HeapBuffer
        {
            var wrapper = new TwoBufferFuncWrapper<TBuffer, T> { Func = func };
            TwoBufferInvoke<TwoBufferFuncWrapper<TBuffer, T>, TBuffer>(ref wrapper);
            return wrapper.Result;
        }

        /// <summary>
        ///  Invoke the given action on a set of cached buffers.
        /// </summary>
        public static T TwoBufferInvoke<TBufferFunc, TBuffer, T>(ref TBufferFunc func)
            where TBufferFunc : ITwoBufferFunc<TBuffer, T>
            where TBuffer : HeapBuffer
        {
            var wrapper = new TwoBufferFuncWrapper<TBufferFunc, TBuffer, T> { Func = func };
            TwoBufferInvoke<TwoBufferFuncWrapper<TBufferFunc, TBuffer, T>, TBuffer>(ref wrapper);
            return wrapper.Result;
        }

        /// <summary>
        ///  Invoke the given action on a cached buffer.
        /// </summary>
        public static void BufferInvoke<TBuffer>(Action<TBuffer> action) where TBuffer : HeapBuffer
        {
            var wrapper = new ActionWrapper<TBuffer> { Action = action };
            BufferInvoke<ActionWrapper<TBuffer>, TBuffer>(ref wrapper);
        }

        /// <summary>
        ///  Invoke the given action on a cached buffer.
        /// </summary>
        public static void BufferInvoke<TBufferAction, TBuffer>(ref TBufferAction action)
            where TBufferAction : IBufferAction<TBuffer>
            where TBuffer : HeapBuffer
        {
            // For safer use it's better to ensure we always have at least some capacity in the buffer.
            // This allows consumers not to worry about making sure there is some capacity or trying to
            // multiply a buffer capacity of 0 for recursive invocations.
#if DEBUG
            // Set relatively small in debug to exercise code.
            const uint MinBufferSize = 32;
#else
            const uint MinBufferSize = 128;
#endif

            var buffer = StringBufferCache.Instance.Acquire(minCharCapacity: MinBufferSize);
            try
            {
                action.Action((buffer as TBuffer)!);
            }
            finally
            {
                StringBufferCache.Instance.Release(buffer);
            }
        }

        /// <summary>
        ///  Invoke the given action on a set of cached buffers.
        /// </summary>
        public static void TwoBufferInvoke<TBufferAction, TBuffer>(ref TBufferAction action)
            where TBufferAction : ITwoBufferAction<TBuffer>
            where TBuffer : HeapBuffer
        {
            // For safer use it's better to ensure we always have at least some capacity in the buffer.
            // This allows consumers not to worry about making sure there is some capacity or trying to
            // multiply a buffer capacity of 0 for recursive invocations.
#if DEBUG
            // Set relatively small in debug to exercise code.
            const uint MinBufferSize = 32;
#else
            const uint MinBufferSize = 128;
#endif

            var buffer1 = StringBufferCache.Instance.Acquire(minCharCapacity: MinBufferSize);
            var buffer2 = StringBufferCache.Instance.Acquire(minCharCapacity: MinBufferSize);
            try
            {
                action.Action((buffer1 as TBuffer)!, (buffer2 as TBuffer)!);
            }
            finally
            {
                StringBufferCache.Instance.Release(buffer1);
                StringBufferCache.Instance.Release(buffer2);
            }
        }
    }
}
