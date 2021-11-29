// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Xunit;
using FluentAssertions;
using System.Runtime.InteropServices;
using WInterop;
using System;

namespace SupportTests;

public class TrailingArrayTests
{
    [Fact]
    public unsafe void TrailingArrayMutableStruct()
    {
        // Simulate a trailing array
        MutableB b = new()
        {
            A = 'A',
            B = 'B',
            C = 'C'
        };

        MutableA* a = (MutableA*)&b;

        var span = a->GetBuffer();
        span[0].Should().Be('A');
        span[1].Should().Be('B');
        span[2].Should().Be('C');

        b.B = 'G';
        span[1].Should().Be('G');
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MutableA
    {
        public char A;
        public ReadOnlySpan<char> GetBuffer() => TrailingArray<char>.GetBuffer(in A, 3);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MutableB
    {
        public char A;
        public char B;
        public char C;
    }
}
