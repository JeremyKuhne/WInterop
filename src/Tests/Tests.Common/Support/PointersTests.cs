// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Xunit;
using FluentAssertions;
using WInterop.Support;

namespace Tests.Support
{
    public class PointersTests
    {
        [Fact]
        public unsafe void PointerOffset()
        {
            void* v = (void*)1000;
            v = Pointers.Offset(v, 42);
            ((int)v).Should().Be(1042);
        }

        [Fact]
        public unsafe void PointerOffset_Negative()
        {
            void* v = (void*)1000;
            v = Pointers.Offset(v, -42);
            ((int)v).Should().Be(958);
        }
    }
}
