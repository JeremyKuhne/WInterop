﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Com;

namespace ComTests;

/// <summary>
///  Checking that we parse what the runtime puts out. The marshalling code is not available
///  in all runtimes and we want to support additional types (including PROPVARIANT) so we
///  have our own implementation.
/// </summary>
public class ComRuntimeCompat
{
    [Theory,
        InlineData(1.0d),
        InlineData(-1.0d)
        ]
    public unsafe void DecimalRoundTrip(double value)
    {
        decimal d = new(value);

        byte* b = stackalloc byte[24];

        using Variant v = new((IntPtr)b, ownsHandle: true);
        Marshal.GetNativeVariantForObject(d, v.DangerousGetHandle());
        v.VariantType.Should().Be(VariantType.Decimal);
        var data = v.GetData();
        data.Should().NotBeNull();
        data.Should().BeOfType<decimal>();
        data.Should().Be(d);
    }

    [Theory,
        InlineData(0.0d),
        InlineData(1.0d),
        InlineData(-1.0d),
        InlineData(2131.7089891d)
        ]
    public unsafe void DoubleRoundTrip(double value)
    {
        byte* b = stackalloc byte[24];

        using Variant v = new((IntPtr)b, ownsHandle: true);
        Marshal.GetNativeVariantForObject(value, v.DangerousGetHandle());
        v.VariantType.Should().Be(VariantType.Double);
        var data = v.GetData();
        data.Should().NotBeNull();
        data.Should().BeOfType<double>();
        data.Should().Be(value);
    }

    [Theory,
        InlineData("Foo"),
        InlineData("FooBar"),
        InlineData(""),
        ]
    public unsafe void StringRoundTrip(string value)
    {
        byte* b = stackalloc byte[24];

        using Variant v = new((IntPtr)b, ownsHandle: true);
        Marshal.GetNativeVariantForObject(value, v.DangerousGetHandle());
        v.VariantType.Should().Be(VariantType.BasicString);
        var data = v.GetData();
        data.Should().NotBeNull();
        data.Should().BeOfType<string>();
        data.Should().Be(value);
    }
}
