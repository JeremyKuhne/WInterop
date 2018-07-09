// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;

namespace WInterop.Support.Buffers
{
    /// <summary>
    /// Returns IMemoryOwner wrapped buffers from the shared ArrayPool.
    /// </summary>
    /// <remarks>
    /// Similar to <see cref="MemoryPool{T}.Shared"/>, but returns struct based <see cref="IMemoryOwner{T}"/> wrappers.
    /// </remarks>
    public static class OwnedMemoryPool
    {
        public static IMemoryOwner<T> Rent<T>(int minimumLength)
        {
            return new ArrayPoolOwner<T>(minimumLength);
        }

        private struct ArrayPoolOwner<T> : IMemoryOwner<T>
        {
            private T[] _array;

            public ArrayPoolOwner(int minimumLength)
            {
                _array = ArrayPool<T>.Shared.Rent(minimumLength);
            }

            public Memory<T> Memory
            {
                get
                {
                    T[] array = _array;
                    if (array == null)
                        throw new ObjectDisposedException(nameof(IMemoryOwner<T>));

                    return new Memory<T>(array);
                }
            }

            public void Dispose()
            {
                T[] array = _array;
                if (array != null)
                {
                    _array = null;
                    ArrayPool<T>.Shared.Return(array);
                }
            }
        }
    }

    public struct OwnedMemoryPool<T> : IEnumerable<T>, IEnumerator<T>
    {
        private IMemoryOwner<T> _owner;
        private int _index;

        public OwnedMemoryPool(IMemoryOwner<T> owner)
        {
            _owner = owner;
            _index = -1;
        }

        public T Current => throw new NotImplementedException();

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
