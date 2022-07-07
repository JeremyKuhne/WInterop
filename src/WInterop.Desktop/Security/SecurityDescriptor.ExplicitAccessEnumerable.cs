// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using WInterop.Memory;

namespace WInterop.Security;

public unsafe partial class SecurityDescriptor
{
    private class ExplicitAccessEnumerable : LocalHandle, IEnumerable<ExplicitAccess>, IEnumerator<ExplicitAccess>
    {
        private readonly uint _count;
        private int _current;
        private readonly EXPLICIT_ACCESS_W* _entries;

        public ExplicitAccessEnumerable() : base() { }

        public unsafe ExplicitAccessEnumerable(EXPLICIT_ACCESS_W* entries, uint count, bool ownsHandle = true)
            : base((HLOCAL)entries, ownsHandle)
        {
            _count = count;
            _entries = entries;
            Reset();
        }

        public IEnumerator<ExplicitAccess> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public void Reset() => _current = -1;
        object IEnumerator.Current => Current;

        public ExplicitAccess Current
            => (_current >= 0 && _current < _count)
                ? new ExplicitAccess(_entries + _current)
                : throw new InvalidOperationException();

        public bool MoveNext()
        {
            if (_current + 1 < _count)
            {
                _current++;
                return true;
            }

            return false;
        }
    }
}