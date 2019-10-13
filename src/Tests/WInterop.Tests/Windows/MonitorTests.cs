// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Gdi;
using WInterop.Windows;
using Xunit;

namespace WindowsTests
{
    public class MonitorTests
    {
        [Fact]
        public void GetDefaultMonitor()
        {
            MonitorHandle handle = Windows.MonitorFromWindow(default, MonitorOption.DefaultToPrimary);
            handle.IsInvalid.Should().BeFalse();
        }

        [Fact]
        public void GetInfoForDefaultMonitor()
        {
            MonitorHandle handle = Windows.MonitorFromWindow(default, MonitorOption.DefaultToPrimary);
            MonitorInfo info = handle.GetMonitorInfo();
            info.IsPrimary.Should().BeTrue();
        }

        [Fact]
        public void GetExtendedInfoForDefaultMonitor()
        {
            MonitorHandle handle = Windows.MonitorFromWindow(default, MonitorOption.DefaultToPrimary);
            ExtendedMonitorInfo info = handle.GetExtendedMonitorInfo();
            info.IsPrimary.Should().BeTrue();
            info.DeviceName.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void GetDeviceContextForDefaultMonitor()
        {
            MonitorHandle handle = Windows.MonitorFromWindow(default, MonitorOption.DefaultToPrimary);
            ExtendedMonitorInfo info = handle.GetExtendedMonitorInfo();
            using DeviceContext dc = Gdi.CreateDeviceContext(info.DeviceName.ToString(), null);
            dc.IsInvalid.Should().BeFalse();
            int bitsPerPixel = dc.GetDeviceCapability(DeviceCapability.BitsPerPixel);
            int planes = dc.GetDeviceCapability(DeviceCapability.Planes);
        }
    }
}
