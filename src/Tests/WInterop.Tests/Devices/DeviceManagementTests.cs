﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Devices;

namespace DeviceTests;

public class ControlCodeDecoding
{
    [Fact]
    public void DecodeControlCode()
    {
        ControlCode code = new() { Value = 0x0f60401a };
        code.DeviceType.Should().Be(ControlCodeDeviceType.Infrared);
        code.RequiredAccess.Should().Be(ControlCodeAccess.Read);
        code.TransferType.Should().Be(ControlCodeMethod.OutDirect);
        code.FunctionCode.Should().Be(6);
    }
}
