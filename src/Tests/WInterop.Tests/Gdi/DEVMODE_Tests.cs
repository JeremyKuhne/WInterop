// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Runtime.InteropServices;
using WInterop;
using WInterop.Gdi;
using Xunit;

namespace GdiTests;

public class DEVMODE_Tests
{
    [Fact]
    public unsafe void ValidateDeviceModeSize()
    {
        // Make sure we're both blittable and the correct size
        GCHandle.Alloc(new DeviceMode(), GCHandleType.Pinned).Free();
        sizeof(DeviceMode).Should().Be(220);
    }

    [Fact]
    public unsafe void DeviceName()
    {
        DeviceMode devMode = new DeviceMode();
        devMode.DeviceName.CreateString().Should().Be(string.Empty);
        devMode.DeviceName.CopyFrom("Foo");
        devMode.DeviceName.CreateString().Should().Be("Foo");
        string tooLong = new string('a', 40);
        devMode.DeviceName.CopyFrom(tooLong);
        devMode.DeviceName.CreateString().Should().Be(new string('a', 31));

        // The next value in the struct- ensuring we didn't write over
        devMode.SpecVersion.Should().Be(0);
    }

    [Fact]
    public unsafe void FormName()
    {
        DeviceMode devMode = new DeviceMode();
        devMode.FormName.CreateString().Should().Be(string.Empty);
        devMode.FormName.CopyFrom("Bar");
        devMode.FormName.CreateString().Should().Be("Bar");
        string tooLong = new string('z', 40);
        devMode.FormName.CopyFrom(tooLong);
        devMode.FormName.CreateString().Should().Be(new string('z', 31));

        // The next value in the struct- ensuring we didn't write over
        devMode.LogicalPixels.Should().Be(0);
    }
}
