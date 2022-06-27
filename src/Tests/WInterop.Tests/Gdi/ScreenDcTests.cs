// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;

namespace GdiTests;

public class ScreenDcTests
{
    [Fact]
    public void GetCompatibleDC_ScreenDpi()
    {
        using var context = Gdi.CreateCompatibleDeviceContext(default);
        context.IsInvalid.Should().BeFalse();
        int pixeWidth = context.GetDeviceCapability(DeviceCapability.HorzontalResolution);
        int pixelHeight = context.GetDeviceCapability(DeviceCapability.VerticalResolution);
        int dpiX = context.GetDeviceCapability(DeviceCapability.LogicalPixelsX);
        int dpiY = context.GetDeviceCapability(DeviceCapability.LogicalPixelsY);

        dpiX.Should().Be(dpiY);

        using var context2 = Gdi.GetDeviceContext(default);
        int dpiX2 = context2.GetDeviceCapability(DeviceCapability.LogicalPixelsX);
        int dpiY2 = context2.GetDeviceCapability(DeviceCapability.LogicalPixelsY);

        dpiX2.Should().Be(dpiX);
        dpiY2.Should().Be(dpiY);
    }

    [Fact]
    public void GetCompatibleDC_AmbientState()
    {
        using var context = Gdi.CreateCompatibleDeviceContext(default);
        context.IsInvalid.Should().BeFalse();

        context.GetBackgroundMode().Should().Be(BackgroundMode.Opaque);
        context.GetRasterOperation().Should().Be(PenMixMode.CopyPen);
        context.GetMappingMode().Should().Be(MappingMode.Text);

        // Check region defaults
        using var region = context.GetClippingRegion(out bool hasRegion);
        hasRegion.Should().BeFalse();
        using var region2 = context.GetClippingRegion(out hasRegion);
        hasRegion.Should().BeFalse();
        context.SelectClippingRegion(default);
        using var region3 = context.GetClippingRegion(out hasRegion);
        hasRegion.Should().BeFalse();

        Rectangle clip = context.GetClipBox(out RegionType regionType);
        regionType.Should().Be(RegionType.Simple);

        // Check origin defaults
        context.GetViewportOrigin().Should().Be(new Point());
        context.GetWindowOrigin().Should().Be(new Point());
    }


    [Fact]
    public void GetCompatibleDC_SaveState()
    {
        using var context = Gdi.CreateCompatibleDeviceContext(default);
        context.IsInvalid.Should().BeFalse();
    }
}
