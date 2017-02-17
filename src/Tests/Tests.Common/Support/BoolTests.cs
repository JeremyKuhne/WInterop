// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Runtime.InteropServices;
using WInterop.Support;
using Xunit;

namespace Tests.Support
{
    public class BoolTests
    {
        private struct BOOLTest
        {
            public int Foo;
            public BOOL Bar;
        }

        private struct BOOLEANTest
        {
            public int Foo;
            public BOOLEAN Bar;
        }

        private struct BoolTest
        {
            public int Foo;
            public bool Bar;
        }

        [Fact]
        public void BOOLIsBlittable()
        {
            // This will throw if the type is not blittable
            GCHandle.Alloc(new BOOL(), GCHandleType.Pinned).Should().NotBeNull();
        }

        [Fact]
        public void EmbeddedBOOLIsBlittable()
        {
            // This will throw if the type is not blittable
            GCHandle.Alloc(new BOOLTest(), GCHandleType.Pinned).Should().NotBeNull();
        }

        [Fact]
        public void EmbeddedBoolIsNotBlittable()
        {
            // Demonstrating that an embedded bool isn't pinnable, and validating the behavior doesn't change
            Action action = () => GCHandle.Alloc(new BoolTest(), GCHandleType.Pinned);
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void BOOLConversion()
        {
            BOOL B = true;
            (B == true).Should().BeTrue("Comparison of TRUE with true is true");
            (B == false).Should().BeFalse("Comparison of TRUE with false is false");
            B.IsTrue.Should().BeTrue("IsTrue should be true when true");
            B.IsFalse.Should().BeFalse("IsFalse should be false when true");
            bool b = B;
            b.Should().BeTrue("assignment is true");
            b = false;
            B = b;
            (B == true).Should().BeFalse("Comparison of FALSE with true is false");
            (B == false).Should().BeTrue("Comparison of FALSE with false is true");
            B.IsTrue.Should().BeFalse("IsTrue should be false when false");
            B.IsFalse.Should().BeTrue("IsFalse should be true when False");
        }

        [Fact]
        public void BOOLEANIsBlittable()
        {
            // This will throw if the type is not blittable
            GCHandle.Alloc(new BOOLEAN(), GCHandleType.Pinned).Should().NotBeNull();
        }

        [Fact]
        public void EmbeddedBOOLEANIsBlittable()
        {
            // This will throw if the type is not blittable
            GCHandle.Alloc(new BOOLEANTest(), GCHandleType.Pinned).Should().NotBeNull();
        }

        [Fact]
        public void BOOLEANConversion()
        {
            BOOLEAN B = true;
            (B == true).Should().BeTrue("Comparison of TRUE with true is true");
            (B == false).Should().BeFalse("Comparison of TRUE with false is false");
            B.IsTrue.Should().BeTrue("IsTrue should be true when true");
            B.IsFalse.Should().BeFalse("IsFalse should be false when true");
            bool b = B;
            b.Should().BeTrue("assignment is true");
            b = false;
            B = b;
            (B == true).Should().BeFalse("Comparison of FALSE with true is false");
            (B == false).Should().BeTrue("Comparison of FALSE with false is true");
            B.IsTrue.Should().BeFalse("IsTrue should be false when false");
            B.IsFalse.Should().BeTrue("IsFalse should be true when False");
        }
    }
}
