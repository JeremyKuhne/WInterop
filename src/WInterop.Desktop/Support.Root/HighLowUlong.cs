// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;

namespace WInterop;

/// <summary>
///  For easier access of ulongs where they are defined in native structs as separate
///  high/low parts.
/// </summary>
public readonly struct HighLowUlong
{
    public uint High { get; }
    public uint Low { get; }

    public HighLowUlong(uint high, uint low)
    {
        High = high;
        Low = low;
    }

    public HighLowUlong(ulong value)
    {
        High = Conversion.HighWord(value);
        Low = Conversion.LowWord(value);
    }

    public static implicit operator HighLowUlong(ulong u) => new HighLowUlong(u);
    public static implicit operator ulong(HighLowUlong u) => Conversion.HighLowToLong(u.High, u.Low);
}