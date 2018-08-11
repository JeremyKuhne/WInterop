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
    /// Enumerator for <see cref="IMemoryOwner{T}"/>
    /// </summary>
    public struct OwnedMemoryEnumerable<T> : IEnumerable<T>, IEnumerator<T>
    {
        private IMemoryOwner<T> _owner;
        private Memory<T> _memory;
        private int _index;

        public OwnedMemoryEnumerable(IMemoryOwner<T> owner)
        {
            _owner = owner;
            _memory = owner.Memory;
            _index = -1;
        }

        public OwnedMemoryEnumerable(IMemoryOwner<T> owner, int start, int length)
        {
            _owner = new SlicedMemoryOwner<T>(owner, start, length);
            _memory = owner.Memory;
            _index = -1;
        }

        public T Current
        {
            get
            {
                if (_index == -1)
                    return default;
                if (_index < 0)
                    throw new InvalidOperationException();
                return _memory.Span[_index];
            }
        }

        object IEnumerator.Current => Current;

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool MoveNext()
        {
            if (_index < -1)
                return false;

            int index = ++_index;
            if (index >= _memory.Length)
            {
                _index = -2;
                return false;
            }

            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        public void Dispose()
        {
            _owner.Dispose();
        }
    }
}
