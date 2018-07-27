// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Runtime.InteropServices;
using WInterop;
using WInterop.Gdi;
using Xunit;

namespace GdiTests
{
    public class DEVMODE_Tests
    {
        [Fact]
        public unsafe void ValidateDeviceModeSize()
        {
            // Make sure we're both blittable and the correct size
            GCHandle.Alloc(new DEVMODE(), GCHandleType.Pinned).Free();
            sizeof(DEVMODE).Should().Be(220);
        }

        [Fact]
        public unsafe void DeviceName()
        {
            DEVMODE devMode = new DEVMODE();
            devMode.dmDeviceName.CreateString().Should().Be(string.Empty);
            devMode.dmDeviceName.CopyFrom("Foo");
            devMode.dmDeviceName.CreateString().Should().Be("Foo");
            string tooLong = new string('a', 40);
            devMode.dmDeviceName.CopyFrom(tooLong);
            devMode.dmDeviceName.CreateString().Should().Be(new string('a', 31));

            // The next value in the struct- ensuring we didn't write over
            devMode.dmSpecVersion.Should().Be(0);
        }

        [Fact]
        public unsafe void FormName()
        {
            DEVMODE devMode = new DEVMODE();
            devMode.dmFormName.CreateString().Should().Be(string.Empty);
            devMode.dmFormName.CopyFrom("Bar");
            devMode.dmFormName.CreateString().Should().Be("Bar");
            string tooLong = new string('z', 40);
            devMode.dmFormName.CopyFrom(tooLong);
            devMode.dmFormName.CreateString().Should().Be(new string('z', 31));

            // The next value in the struct- ensuring we didn't write over
            devMode.dmLogPixels.Should().Be(0);
        }
    }
}
