// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Buffers;

namespace WInterop.Support.Buffers
{
    /// <summary>
    ///  Wrapper to slice an <see cref="IMemoryOwner{T}"/>
    /// </summary>
    public struct SlicedMemoryOwner<T> : IMemoryOwner<T>
    {
        private readonly IMemoryOwner<T> _owner;
        private readonly int _start;
        private readonly int _length;

        public SlicedMemoryOwner(IMemoryOwner<T> owner, int start, int length)
        {
            int originalLength = owner.Memory.Length;
            if (start >= originalLength)
                throw new ArgumentOutOfRangeException(nameof(start));
            if (start + length >= originalLength)
                throw new ArgumentOutOfRangeException(nameof(length));

            _owner = owner;
            _start = start;
            _length = length;
        }

        public Memory<T> Memory => _owner.Memory.Slice(_start, _length);

        public void Dispose() => _owner.Dispose();
    }
}
