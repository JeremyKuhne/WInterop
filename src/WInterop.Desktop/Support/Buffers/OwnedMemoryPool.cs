// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Buffers;

namespace WInterop.Support.Buffers
{
    /// <summary>
    ///  Returns IMemoryOwner wrapped buffers from the shared ArrayPool.
    /// </summary>
    /// <remarks>
    ///  Similar to <see cref="MemoryPool{T}.Shared"/>, but returns struct based <see cref="IMemoryOwner{T}"/> wrappers.
    /// </remarks>
    public static class OwnedMemoryPool
    {
        public static IMemoryOwner<T> Rent<T>(int minimumLength)
        {
            return new ArrayPoolOwner<T>(minimumLength);
        }

        private struct ArrayPoolOwner<T> : IMemoryOwner<T>
        {
            private T[]? _array;

            public ArrayPoolOwner(int minimumLength)
            {
                _array = ArrayPool<T>.Shared.Rent(minimumLength);
            }

            public Memory<T> Memory
            {
                get
                {
                    T[]? array = _array;
                    return array is null
                        ? throw new ObjectDisposedException(nameof(IMemoryOwner<T>))
                        : new Memory<T>(array);
                }
            }

            public void Dispose()
            {
                T[]? array = _array;
                if (array != null)
                {
                    _array = null;
                    ArrayPool<T>.Shared.Return(array);
                }
            }
        }
    }
}
