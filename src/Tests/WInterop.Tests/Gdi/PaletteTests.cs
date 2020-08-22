// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Gdi;
using WInterop.GdiPlus;
using Xunit;

namespace GdiTests
{
    public class PaletteTests
    {
        [Fact]
        public void GetDisplaySystemPaletteEntries()
        {
            using DeviceContext dc = Gdi.CreateCompatibleDeviceContext(default);
            var paletteEntries = Gdi.GetSystemPaletteEntries(dc);
        }

        [Fact]
        public void GetDisplayCurrentPaletteEntries()
        {
            using DeviceContext dc = Gdi.CreateCompatibleDeviceContext(default);
            var paletteEntries = Gdi.GetPaletteEntries(dc.GetCurrentPalette());
        }

        [Fact]
        public void GetGdiPlusHalftonePaletteEntries()
        {
            using PaletteHandle palette = GdiPlus.GetHalftonePalette();
            var paletteEntries = Gdi.GetPaletteEntries(palette);
        }

        [Fact]
        public void ScreenNumberOfColors()
        {
            using DeviceContext dc = Gdi.CreateCompatibleDeviceContext(default);
            int numberOfColors = dc.GetDeviceCapability(DeviceCapability.NumberOfColors);

            // Docs say 1 for > 8bpp, but we get -1
            numberOfColors.Should().Be(-1);
        }

        [Fact]
        public void ScreenBitsPerBixel()
        {
            using DeviceContext dc = Gdi.CreateCompatibleDeviceContext(default);
            int bpp = dc.GetDeviceCapability(DeviceCapability.BitsPerPixel);

            bpp.Should().Be(32);
        }
    }
}
