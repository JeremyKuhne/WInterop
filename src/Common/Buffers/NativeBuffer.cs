// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Buffers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using Handles;

    /// <summary>
    /// Wrapper for access to the native heap. Dispose to free the memory. Try to use with using statements.
    /// Does not allocate zero size buffers, and will free the existing native buffer if capacity is dropped to zero.
    /// 
    /// NativeBuffer utilizes a cache of heap buffers.
    /// </summary>
    /// <remarks>
    /// Suggested use through P/Invoke: define DllImport arguments that take a byte buffer as SafeHandle.
    /// 
    /// Using SafeHandle will ensure that the buffer will not get collected during a P/Invoke but introduces very slight
    /// overhead. (Notably AddRef and ReleaseRef will be called by the interop layer.)
    /// 
    /// This class is not threadsafe, changing the capacity or disposing on multiple threads risks duplicate heap
    /// handles or worse.
    /// </remarks>
    public class NativeBuffer : IDisposable
    {
        private SafeHeapHandle _handle;
        private ulong _byteCapacity;

        /// <summary>
        /// Create a buffer with at least the specified initial capacity in bytes.
        /// </summary>
        public NativeBuffer(ulong initialMinCapacity = 0)
        {
            EnsureByteCapacity(initialMinCapacity);
        }

        protected unsafe void* VoidPointer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return InternalDangerousGetHandle().ToPointer();
            }
        }

        protected unsafe byte* BytePointer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (byte*)VoidPointer;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IntPtr InternalDangerousGetHandle()
        {
            SafeHandle handle = _handle;
            return handle == null ? IntPtr.Zero : handle.DangerousGetHandle();
        }

        public IntPtr DangerousGetHandle()
        {
            return InternalDangerousGetHandle();
        }

        public static implicit operator SafeHandle(NativeBuffer buffer)
        {
            // Marshalling code will throw on null for SafeHandle
            return buffer._handle ?? EmptySafeHandle.Instance;
        }

        /// <summary>
        /// The capacity of the buffer in bytes.
        /// </summary>
        public ulong ByteCapacity
        {
            get { return _byteCapacity; }
        }

        /// <summary>
        /// Ensure capacity in bytes is at least the given minimum.
        /// </summary>
        /// <exception cref="OutOfMemoryException">Thrown if unable to allocate memory when setting.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if attempting to set <paramref name="nameof(minCapacity)"/> to a value that is larger than the maximum addressable memory.</exception>
        public void EnsureByteCapacity(ulong minCapacity)
        {
            if (_byteCapacity < minCapacity)
            {
                Resize(minCapacity);
                _byteCapacity = minCapacity;
            }
        }

        public unsafe byte this[ulong index]
        {
            get
            {
                if (index >= _byteCapacity) throw new ArgumentOutOfRangeException();
                return BytePointer[index];
            }
            set
            {
                if (index >= _byteCapacity) throw new ArgumentOutOfRangeException();
                BytePointer[index] = value;
            }
        }

        private unsafe void Resize(ulong byteLength)
        {
            if (byteLength == 0)
            {
                ReleaseHandle();
                return;
            }

            if (_handle == null)
            {
                _handle = HeapHandleCache.Instance.Acquire(byteLength);
            }
            else
            {
                _handle.Resize(byteLength);
            }
        }

        private void ReleaseHandle()
        {
            if (_handle != null)
            {
                HeapHandleCache.Instance.Release(_handle);
                _handle = null;
                _byteCapacity = 0;
            }
        }

        /// <summary>
        /// Free the space used by the buffer. Sets capacity to 0.
        /// </summary>
        public virtual void Free()
        {
            ReleaseHandle();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Free();
        }
    }
}
