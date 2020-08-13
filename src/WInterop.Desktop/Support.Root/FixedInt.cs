// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;

namespace WInterop
{
    public static unsafe class FixedInt
    {
        public struct Size16
        {
            private const int Size = 16;
            private fixed int _buffer[Size];

            public Span<int> Buffer { [MethodImpl(MethodImplOptions.AggressiveInlining)] get { fixed (int* c = _buffer) return new Span<int>(c, Size); } }
        }
    }
}
