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
    public class StructsTests
    {
        [Fact]
        public unsafe void AddressOf()
        {
            int foo = 6;
            int* directAddress = &foo;
            int* helper = Structs.AddressOf(ref foo);
            (directAddress == helper).Should().BeTrue("Address should be the same");
            helper->Should().Be(*directAddress);
        }

        [Fact]
        public unsafe void SizeOf_Int()
        {
            Structs.SizeInBytes<int>().Should().Be(4);
            int i = 6;
            Structs.SizeInBytes(ref i).Should().Be(4);
        }
    }
}
