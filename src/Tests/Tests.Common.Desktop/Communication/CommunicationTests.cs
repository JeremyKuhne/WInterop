// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.InteropServices;
using WInterop.Desktop.Communications;
using WInterop.Desktop.Communications.DataTypes;
using WInterop.FileManagement;
using WInterop.Windows;
using WInterop.Windows.DataTypes;
using Xunit;

namespace DesktopTests.Communication
{
    public class CommunicationTests
    {
        [Fact]
        public void DcbIsBlittable()
        {
            // This will throw if the type is not blittable
            GCHandle.Alloc(new DCB(), GCHandleType.Pinned).Should().NotBeNull();
        }

        [Fact]
        public unsafe void DcbIsCorrectSize()
        {
            sizeof(DCB).Should().Be(28);
        }

        [Theory,
            InlineData(DtrControl.DTR_CONTROL_DISABLE),
            InlineData(DtrControl.DTR_CONTROL_ENABLE),
            InlineData(DtrControl.DTR_CONTROL_HANDSHAKE)
            ]
        public unsafe void DcbDtrControl(DtrControl dtrControl)
        {
            DCB dcb = new DCB()
            {
                fDtrControl = dtrControl
            };

            dcb.fDtrControl.Should().Be(dtrControl);
        }

        [Fact]
        public unsafe void BuildCommDcb()
        {
            DCB dcb = CommunicationsDesktopMethods.BuildCommDCB(@"baud=1200 parity=N data=8 stop=1");
            dcb.BaudRate.Should().Be(CommBaudRate.CBR_1200);
            dcb.Parity.Should().Be(Parity.NOPARITY);
            dcb.ByteSize.Should().Be(8);
            dcb.StopBits.Should().Be(StopBits.ONESTOPBIT);
        }

        [Fact(Skip = "Needs conditioned on specific com port availability")]
        public unsafe void GetCommProperties()
        {
            using (SafeFileHandle handle = CommunicationsDesktopMethods.CreateComFileHandle(@"\\.\COM4"))
            {
                COMMPROP properties = CommunicationsDesktopMethods.GetCommProperties(handle);
            }
        }

        [Fact(Skip = "Needs conditioned on specific com port availability")]
        public unsafe void GetCommConfig()
        {
            using (SafeFileHandle handle = CommunicationsDesktopMethods.CreateComFileHandle(@"\\.\COM4"))
            {
                COMMCONFIG config = CommunicationsDesktopMethods.GetCommConfig(handle);
            }
        }

        [Fact(Skip = "Needs conditioned on specific com port availability")]
        public unsafe void GetDefaultCommConfig()
        {
            COMMCONFIG config = CommunicationsDesktopMethods.GetDefaultCommConfig(@"COM4");
        }

        [Fact(Skip = "Needs to be run manually or with UI automation")]
        public unsafe void CommConfigDialog()
        {
            COMMCONFIG config = CommunicationsDesktopMethods.CommConfigDialog(
                @"COM4",
                WindowsDesktopMethods.GetForegroundWindow());
        }
    }
}
