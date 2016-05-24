// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using WInterop.Handles;
using WInterop.Handles.DataTypes;

namespace WInterop.Buffers
{
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
    /// The actual buffer handle / memory location can change when resizing.
    /// </remarks>
    public class NativeBuffer : IDisposable
    {
        private SafeHeapHandle _handle;

        // By using a platform specific data type we can be assured of atomic reads/writes.
        // Anything more than a uint isn't addressable on 32bit as well.
        private unsafe void* _byteCapacity;

        protected ReaderWriterLockSlim _handleLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        /// <summary>
        /// Create a buffer with at least the specified initial capacity in bytes.
        /// </summary>
        /// <exception cref="OverflowException">Thrown if trying to allocate more than a uint on 32bit.</exception>
        public NativeBuffer(ulong initialMinCapacity = 0)
        {
            _handle = HeapHandleCache.Instance.Acquire(initialMinCapacity);

            unsafe
            {
                _byteCapacity = (void*)(initialMinCapacity);
            }
        }

        public unsafe void* VoidPointer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return DangerousGetHandle().ToPointer();
            }
        }

        public unsafe byte* BytePointer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (byte*)VoidPointer;
            }
        }

        /// <summary>
        /// Get the handle to the buffer. Prefer using SafeHandle instead of IntPtr for interop (there is an implicit converter).
        /// </summary>
        public IntPtr DangerousGetHandle()
        {
            return _handle?.DangerousGetHandle() ?? IntPtr.Zero;
        }

        public static implicit operator SafeHandle(NativeBuffer buffer)
        {
            // Marshalling code will throw on null for SafeHandle
            return buffer?._handle ?? EmptySafeHandle.Instance;
        }

        /// <summary>
        /// The capacity of the buffer in bytes.
        /// </summary>
        public ulong ByteCapacity
        {
            // Capacity will never decrease, except after disposal. In addition, using the void* allows reads/writes
            // to capacity to be atomic. As such we shouldn't have to worry about returning a size that is too small.
            get { unsafe { return (ulong)_byteCapacity; } }
        }

        /// <summary>
        /// Ensure capacity in bytes is at least the given minimum.
        /// </summary>
        /// <exception cref="OutOfMemoryException">Thrown if unable to allocate memory when setting.</exception>
        /// <exception cref="OverflowException">Thrown if trying to allocate more than a uint on 32bit.</exception>
        public void EnsureByteCapacity(ulong minCapacity)
        {
            // Don't bother trying to get the lock if we're already big enough
            if (ByteCapacity < minCapacity)
            {
                _handleLock.EnterWriteLock();
                try
                {
                    UnlockedEnsureByteCapacity(minCapacity);
                }
                finally
                {
                    _handleLock.ExitWriteLock();
                }
            }
        }

        protected unsafe void UnlockedEnsureByteCapacity(ulong minCapacity)
        {
            if (ByteCapacity < minCapacity)
            {
                if (_handle.ByteLength < minCapacity) _handle.Resize(minCapacity);
                _byteCapacity = (void*)minCapacity;
            }
        }

        /// <summary>
        /// Get the byte at the specified byte index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if attempting to get or set a value that is over the capacity of the buffer.</exception>
        public byte this[ulong index]
        {
            // We only need a read lock here to avoid accessing old memory after a resize (as the block may move). The actual read/write is atomic.
            get
            {
                if (index >= ByteCapacity) throw new ArgumentOutOfRangeException(nameof(index));

                _handleLock.EnterReadLock();
                try
                {
                    unsafe { return BytePointer[index]; }
                }
                finally
                {
                    _handleLock.ExitReadLock();
                }
            }
            set
            {
                if (index >= ByteCapacity) throw new ArgumentOutOfRangeException(nameof(index));

                _handleLock.EnterReadLock();
                try
                {
                    unsafe { BytePointer[index] = value; }
                }
                finally
                {
                    _handleLock.ExitReadLock();
                }
            }
        }

        private unsafe void ReleaseHandle()
        {
            SafeHeapHandle handle;
            _handleLock.EnterWriteLock();
            try
            {
                handle = _handle;
                _byteCapacity = null;
                _handle = null;
            }
            finally
            {
                _handleLock.ExitWriteLock();
            }

            if (handle != null)
            {
                HeapHandleCache.Instance.Release(handle);
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                ReleaseHandle();
        }
    }
}
