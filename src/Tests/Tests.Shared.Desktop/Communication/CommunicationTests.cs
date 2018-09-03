// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using WInterop.Communications;
using WInterop.Windows;
using Xunit;

namespace CommunicationTests
{
    public class Basic
    {
        [Fact]
        public void DcbIsBlittable()
        {
            // This will throw if the type is not blittable
            GCHandle.Alloc(new DeviceControlBlock(), GCHandleType.Pinned).Should().NotBeNull();
        }

        [Fact]
        public unsafe void DcbIsCorrectSize()
        {
            sizeof(DeviceControlBlock).Should().Be(28);
        }

        [Theory,
            InlineData(DtrControl.Disable),
            InlineData(DtrControl.Enable),
            InlineData(DtrControl.Handshake)
            ]
        public unsafe void DcbDtrControl(DtrControl dtrControl)
        {
            DeviceControlBlock dcb = new DeviceControlBlock()
            {
                DtrControl = dtrControl
            };

            dcb.DtrControl.Should().Be(dtrControl);
        }

        [Fact]
        public unsafe void BuildCommDcb()
        {
            DeviceControlBlock dcb = Communications.BuildDeviceControlBlock(@"baud=1200 parity=N data=8 stop=1");
            dcb.BaudRate.Should().Be(CommBaudRate.Baud_1200);
            dcb.Parity.Should().Be(Parity.None);
            dcb.ByteSize.Should().Be(8);
            dcb.StopBits.Should().Be(StopBits.One);
        }

        [Fact(Skip = "Needs conditioned on specific com port availability")]
        public unsafe void GetCommProperties()
        {
            using (SafeFileHandle handle = Communications.CreateComPortFileHandle(@"\\.\COM4"))
            {
                CommunicationsProperties properties = Communications.GetCommunicationsProperties(handle);
            }
        }

        [Fact(Skip = "Needs conditioned on specific com port availability")]
        public unsafe void GetCommConfig()
        {
            using (SafeFileHandle handle = Communications.CreateComPortFileHandle(@"\\.\COM4"))
            {
                CommunicationsConfig config = Communications.GetCommunicationsConfig(handle);
            }
        }

        [Fact(Skip = "Needs conditioned on specific com port availability")]
        public unsafe void GetDefaultCommConfig()
        {
            CommunicationsConfig config = Communications.GetDefaultCommunicationsConfig(@"COM4");
        }

        [Fact(Skip = "Needs to be run manually or with UI automation")]
        public unsafe void CommConfigDialog()
        {
            CommunicationsConfig config = Communications.CommunicationsConfigDialog(
                @"COM4",
                Windows.GetForegroundWindow());
        }
    }
}
