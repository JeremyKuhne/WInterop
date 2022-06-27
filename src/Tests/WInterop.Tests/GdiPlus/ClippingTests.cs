// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Gdi;
using WInterop.GdiPlus;

namespace WInteropTests.GdiPlusTests;

public class ClippingTests
{
    [Fact]
    public unsafe void DeviceContextWithClip()
    {
        GdiPlus.Initialize();

        using DeviceContext deviceContext = Gdi.GetDeviceContext();
        using RegionHandle regionHandle = Gdi.CreateRectangleRegion(new(1, 2, 3, 4));
        deviceContext.SelectClippingRegion(regionHandle);
        using Graphics graphics = new(deviceContext);

        graphics.IsVisibleClipEmpty().Should().BeFalse();

        using Region region = graphics.GetClip();
        region.IsEmpty(graphics).Should().BeFalse();
        region.IsInfinite(graphics).Should().BeTrue();
    }

    [Fact]
    public unsafe void DeviceContextWithFullClip()
    {
        GdiPlus.Initialize();

        using DeviceContext deviceContext = Gdi.GetDeviceContext();
        var rect = deviceContext.GetBoundsRect();
        using RegionHandle regionHandle = Gdi.CreateRectangleRegion(rect);
        deviceContext.SelectClippingRegion(regionHandle);
        using Graphics graphics = new(deviceContext);

        graphics.IsVisibleClipEmpty().Should().BeTrue();

        using Region region = graphics.GetClip();
        region.IsEmpty(graphics).Should().BeFalse();
        region.IsInfinite(graphics).Should().BeTrue();
    }
}
