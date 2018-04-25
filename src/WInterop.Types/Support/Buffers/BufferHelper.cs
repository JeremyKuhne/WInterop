// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WInterop.ErrorHandling.Types;

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

        public static unsafe string GetNullTerminatedAsciiString(ReadOnlySpan<byte> source)
        {
            if (source.Length == 0)
                return string.Empty;

            int length = source.IndexOf(0x00);
            if (length == 0)
                return string.Empty;

            fixed (byte* start = &source.DangerousGetPinnableReference())
                return Encoding.ASCII.GetString(start, length == -1 ? source.Length : length);
        }

        /// <summary>
        /// Invoke the given action on a cached buffer that returns the given type.
        /// </summary>
        /// <example>
        /// return BufferHelper.BufferInvoke((NativeBuffer buffer) => { return string.Empty; });
        /// </example>
        public static T BufferInvoke<BufferType, T>(Func<BufferType, T> func) where BufferType : HeapBuffer
        {
            var wrapper = new FuncWrapper<BufferType, T> { Func = func };
            return BufferInvoke<FuncWrapper<BufferType, T>, BufferType, T>(ref wrapper);
        }

        /// <summary>
        /// Invoke the given action on a cached buffer that returns the given type.
        /// </summary>
        public static T BufferInvoke<TBufferFunc, BufferType, T>(ref TBufferFunc func)
            where TBufferFunc : IBufferFunc<BufferType, T>
            where BufferType : HeapBuffer
        {
            var wrapper = new BufferFuncWrapper<TBufferFunc, BufferType, T> { Func = func };
            BufferInvoke<BufferFuncWrapper<TBufferFunc, BufferType, T>, BufferType>(ref wrapper);
            return wrapper.Result;
        }

        /// <summary>
        /// Invoke the given action on a set of cached buffers.
        /// </summary>
        public static T TwoBufferInvoke<BufferType, T>(Func<BufferType, BufferType, T> func)
            where BufferType : HeapBuffer
        {
            var wrapper = new TwoBufferFuncWrapper<BufferType, T> { Func = func };
            TwoBufferInvoke<TwoBufferFuncWrapper<BufferType, T>, BufferType>(ref wrapper);
            return wrapper.Result;
        }

        /// <summary>
        /// Invoke the given action on a set of cached buffers.
        /// </summary>
        public static T TwoBufferInvoke<TBufferFunc, BufferType, T>(ref TBufferFunc func)
            where TBufferFunc : ITwoBufferFunc<BufferType, T>
            where BufferType : HeapBuffer
        {
            var wrapper = new TwoBufferFuncWrapper<TBufferFunc, BufferType, T> { Func = func };
            TwoBufferInvoke<TwoBufferFuncWrapper<TBufferFunc, BufferType, T>, BufferType>(ref wrapper);
            return wrapper.Result;
        }

        /// <summary>
        /// Invoke the given action on a cached buffer.
        /// </summary>
        public static void BufferInvoke<BufferType>(Action<BufferType> action) where BufferType : HeapBuffer
        {
            var wrapper = new ActionWrapper<BufferType> { Action = action };
            BufferInvoke<ActionWrapper<BufferType>, BufferType>(ref wrapper);
        }

        /// <summary>
        /// Invoke the given action on a cached buffer.
        /// </summary>
        public static void BufferInvoke<TBufferAction, BufferType>(ref TBufferAction action)
            where TBufferAction : IBufferAction<BufferType>
            where BufferType : HeapBuffer
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
                action.Action(buffer as BufferType);
            }
            finally
            {
                StringBufferCache.Instance.Release(buffer);
            }
        }

        /// <summary>
        /// Invoke the given action on a set of cached buffers.
        /// </summary>
        public static void TwoBufferInvoke<TBufferAction, BufferType>(ref TBufferAction action)
            where TBufferAction : ITwoBufferAction<BufferType>
            where BufferType : HeapBuffer
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
                action.Action(buffer1 as BufferType, buffer2 as BufferType);
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
        public static string ApiInvoke<TBufferFunc>(
            ref TBufferFunc invoker,
            string detailForError = null,
            Func<WindowsError, bool> shouldThrow = null)
            where TBufferFunc : IBufferFunc<StringBuffer, uint>
        {
            ApiInvoker apiInvoker = new ApiInvoker
            {
                Invoker = new GrowBufferByReturnValue { Invoker = invoker },
                DetailForError = detailForError,
                ShouldThrow = shouldThrow
            };

            return BufferInvoke<ApiInvoker, StringBuffer, string>(ref apiInvoker);
        }

        /// <summary>
        /// Uses the StringBuffer cache and increases the buffer size if needed. This is for APIs that follow the somewhat unfortunate pattern
        /// of truncating the return string to fit the passed in buffer.
        /// </summary>
        /// <param name="detailForError">If an error is returned, this string is used to help construct the exception if present.</param>
        /// <param name="shouldThrow">If provided will pass the error to the delegate to decide whether to throw or return null.</param>
        public static string TruncatingApiInvoke<TBufferFunc>(
            ref TBufferFunc invoker,
            string detailForError = null,
            Func<WindowsError, bool> shouldThrow = null)
            where TBufferFunc : IBufferFunc<StringBuffer, uint>
        {
            ApiInvoker apiInvoker = new ApiInvoker
            {
                Invoker = new GrowBufferIfFull { Invoker = invoker },
                DetailForError = detailForError,
                ShouldThrow = shouldThrow
            };

            return BufferInvoke<ApiInvoker, StringBuffer, string>(ref apiInvoker);
        }

        private struct GrowBufferByReturnValue : IBufferFunc<StringBuffer, uint>
        {
            public IBufferFunc<StringBuffer, uint> Invoker;

            uint IBufferFunc<StringBuffer, uint>.Func(StringBuffer buffer)
            {
                uint returnValue = 0;

                // Ensure enough room for the output string
                while ((returnValue = Invoker.Func(buffer)) > buffer.CharCapacity)
                    buffer.EnsureCharCapacity(returnValue);

                return returnValue;
            }
        }

        private struct GrowBufferIfFull : IBufferFunc<StringBuffer, uint>
        {
            public IBufferFunc<StringBuffer, uint> Invoker;

            uint IBufferFunc<StringBuffer, uint>.Func(StringBuffer buffer)
            {
                uint returnValue = 0;

                // Ensure enough room for the output string- some return the size with the null, some don't.
                // We'll make sure we have enough for both cases.
                while ((returnValue = Invoker.Func(buffer)) + 2 > buffer.CharCapacity)
                {
                    buffer.EnsureCharCapacity(buffer.CharCapacity < 256 ? 256 : checked(buffer.CharCapacity * 2));
                }

                return returnValue;
            }
        }

        private struct ApiInvoker : IBufferFunc<StringBuffer, string>
        {
            public IBufferFunc<StringBuffer, uint> Invoker;
            public string DetailForError;
            public Func<WindowsError, bool> ShouldThrow;

            string IBufferFunc<StringBuffer, string>.Func(StringBuffer buffer)
            {
                uint returnValue = Invoker.Func(buffer);

                if (returnValue == 0)
                {
                    // Failed
                    WindowsError error = (WindowsError)Marshal.GetLastWin32Error();

                    if (ShouldThrow != null && !ShouldThrow(error))
                        return null;

                    throw Errors.GetIoExceptionForError(error, DetailForError);
                }

                buffer.Length = returnValue;
                return buffer.ToString();
            }
        }
    }
}
