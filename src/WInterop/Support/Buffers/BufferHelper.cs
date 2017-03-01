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
        public static T CachedInvoke<T, BufferType>(Func<BufferType, T> func) where BufferType : HeapBuffer
        {
            T result = default(T);
            CachedInvoke<BufferType>(buffer => result = func(buffer));
            return result;
        }

        /// <summary>
        /// Invoke the given action on a set of cached buffers that returns the given type.
        /// </summary>
        public static T CachedInvoke<T, BufferType>(Func<BufferType, BufferType, T> func) where BufferType : HeapBuffer
        {
            T result = default(T);
            CachedInvoke<BufferType>((buffer1, buffer2) => result = func(buffer1, buffer2));
            return result;
        }

        /// <summary>
        /// Invoke the given action on a cached buffer.
        /// </summary>
        public static void CachedInvoke<BufferType>(Action<BufferType> action) where BufferType : HeapBuffer
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

            var buffer = StringBufferCache.Instance.Acquire(minCapacity: MinBufferSize);
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
        /// Invoke the given action on a set of cached buffers.
        /// </summary>
        public static void CachedInvoke<BufferType>(Action<BufferType, BufferType> action) where BufferType : HeapBuffer
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

            var buffer1 = StringBufferCache.Instance.Acquire(minCapacity: MinBufferSize);
            var buffer2 = StringBufferCache.Instance.Acquire(minCapacity: MinBufferSize);
            try
            {
                action(buffer1 as BufferType, buffer2 as BufferType);
            }
            finally
            {
                StringBufferCache.Instance.Release(buffer1);
                StringBufferCache.Instance.Release(buffer2);
            }
        }

        /// <summary>
        /// Uses the StringBuffer cache and increases the buffer size if needed. This is for APIs that follow the standard pattern of
        /// returning required capacity + null or actual characters copied, and error with a return value of 0.
        /// </summary>
        /// <param name="detailForError">If an error is returned, this string is used to help construct the exception if present.</param>
        /// <param name="shouldThrow">If provided will pass the error to the delegate to decide whether to throw or return null.</param>
        /// <example>
        /// BufferHelper.CachedApiInvoke((buffer) => Direct.GetCurrentDirectoryW(buffer.CharCapacity, buffer));
        /// </example>
        public static string CachedApiInvoke(Func<StringBuffer, uint> invoker, string detailForError = null, Func<WindowsError, bool> shouldThrow = null)
        {
            return CachedApiInvokeHelper((buffer) =>
            {
                uint returnValue = 0;

                // Ensure enough room for the output string
                while ((returnValue = invoker(buffer)) > buffer.CharCapacity)
                    buffer.EnsureCharCapacity(returnValue);

                return returnValue;
            },
            detailForError,
            shouldThrow);
        }

        /// <summary>
        /// Uses the StringBuffer cache and increases the buffer size if needed. This is for APIs that follow the somewhat unfortunate pattern
        /// of truncating the return string to fit the passed in buffer.
        /// </summary>
        /// <param name="detailForError">If an error is returned, this string is used to help construct the exception if present.</param>
        /// <param name="shouldThrow">If provided will pass the error to the delegate to decide whether to throw or return null.</param>
        /// <example>
        /// BufferHelper.CachedTruncatingApiInvoke((buffer) => Direct.GetModuleFileNameW(module, buffer, buffer.CharCapacity));
        /// </example>
        public static string CachedTruncatingApiInvoke(Func<StringBuffer, uint> invoker, string detailForError = null, Func<WindowsError, bool> shouldThrow = null)
        {
            return CachedApiInvokeHelper((buffer) =>
            {
                uint returnValue = 0;

                // Ensure enough room for the output string- some return the size with the null, some don't.
                // We'll make sure we have enough for both cases.
                while ((returnValue = invoker(buffer)) + 2 > buffer.CharCapacity)
                {
                    buffer.EnsureCharCapacity(buffer.CharCapacity < 256 ? 256 : checked(buffer.CharCapacity * 2));
                }

                return returnValue;
            },
            detailForError,
            shouldThrow);
        }

        private static string CachedApiInvokeHelper(Func<StringBuffer, uint> invoker, string detailForError = null, Func<WindowsError, bool> shouldThrow = null)
        {
            return CachedInvoke<string, StringBuffer>((buffer) =>
            {
                uint returnValue = invoker(buffer);

                if (returnValue == 0)
                {
                    // Failed
                    WindowsError error = ErrorHelper.GetLastError();

                    if (shouldThrow != null && !shouldThrow(error))
                        return null;

                    throw ErrorHelper.GetIoExceptionForError(error, detailForError);
                }

                buffer.Length = returnValue;
                return buffer.ToString();
            });
        }
    }
}
