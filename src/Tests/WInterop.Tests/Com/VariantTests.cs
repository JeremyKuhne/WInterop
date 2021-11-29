// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Com.Native;
using Xunit;

namespace ComTests;

public class VariantTests
{
    [Fact]
    public unsafe void VariantTest()
    {
        if (Environment.Is64BitProcess)
        {
            Assert.Equal(24, sizeof(VARIANT));
        }
        else
        {
            Assert.Equal(16, sizeof(VARIANT));
        }
    }
}
