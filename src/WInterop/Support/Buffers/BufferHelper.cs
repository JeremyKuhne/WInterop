// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;

namespace WInterop.Support.Buffers
{
    /// <summary>
    /// Avoid using this directly. Create wrappers that can ensure proper length checks.
    /// </summary>
    public static class BufferHelper
    {
        /// <summary>
        /// Splits a null terminated unicode string list. The final string is followed by a second null.
        /// This is a common pattern Windows uses. Usually you should use StringBuffer and split on null. There
        /// are some cases (such as GetEnvironmentStrings) where Windows returns a handle to a buffer it allocated
        /// with no length.
        /// </summary>
        public static unsafe IEnumerable<string> SplitNullTerminatedStringList(IntPtr handle)
        {
            if (handle == IntPtr.Zero) throw new ArgumentNullException(nameof(handle));

            var strings = new List<string>();

            char* start = (char*)handle;
            char* current = start;

            for (uint i = 0; ; i++)
            {
                if ('\0' == *current)
                {
                    // Split
                    strings.Add(new string(value: start, startIndex: 0, length: checked((int)(current - start))));
                    start = current + 1;

                    if ('\0' == *start) break;
                }

                current++;
            }

            return strings;
        }

        public static unsafe void CopyUintArray(uint* source, uint[] destination)
        {
            long bytesToCopy = destination.Length * sizeof(uint);
            fixed (uint* destinationPointer = destination)
            {
                Buffer.MemoryCopy(source, destinationPointer, bytesToCopy, bytesToCopy);
            }
        }

        /// <summary>
        /// Invoke the given action on a cached buffer that returns the given type.
        /// </summary>
        /// <example>
        /// return BufferHelper.CachedInvoke((NativeBuffer buffer) => { return string.Empty; });
        /// </example>
        public static T CachedInvoke<T, BufferType>(Func<BufferType, T> func) where BufferType : NativeBuffer
        {
            T result = default(T);
            CachedInvoke<BufferType>(buffer => result = func(buffer));
            return result;
        }

        /// <summary>
        /// Invoke the given action on a cached buffer.
        /// </summary>
        public static void CachedInvoke<BufferType>(Action<BufferType> action) where BufferType : NativeBuffer
        {
            // For safer use it's better to ensure we always have at least some capacity in the buffer.
            // This allows consumers not to worry about making sure there is some capacity or trying to
            // multiply a buffer capacity of 0 for recursive invocations.
            var buffer = StringBufferCache.Instance.Acquire(minCapacity: 50);
            try
            {
                action(buffer as BufferType);
            }
            finally
            {
                StringBufferCache.Instance.Release(buffer);
            }
        }

        /// <summary>
        /// Uses the stringbuilder cache and increases the buffer size if needed. This is for APIs that follow the standard pattern of
        /// returning required capacity + null or actual characters copied.
        /// </summary>
        /// <example>
        /// BufferHelper.CachedApiInvoke((buffer) => Direct.GetCurrentDirectoryW(buffer.CharCapacity, buffer));
        /// </example>
        public static string CachedApiInvoke(Func<StringBuffer, uint> invoker, string value = null, Func<WindowsError, bool> shouldThrow = null)
        {
            return CachedInvoke<string, StringBuffer>((buffer) =>
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
