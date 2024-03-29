﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Gdi;

namespace GdiTests;

public class DISPLAY_DEVICE_Tests
{
    [Fact]
    public void MarshalSize()
    {
        Marshal.SizeOf<DisplayDevice>().Should().Be(840);
    }
}
