// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;

namespace WInterop
{
    /// <summary>
    ///  For easier access of ulongs where they are defined in native structs as separate
    ///  low/high parts.
    /// </summary>
    public readonly struct LowHighUlong
    {
        public uint Low { get; }
        public uint High { get; }

        public LowHighUlong(uint low, uint high)
        {
            High = high;
            Low = low;
        }

        public LowHighUlong(ulong value)
        {
            High = Conversion.HighWord(value);
            Low = Conversion.LowWord(value);
        }

        public static implicit operator LowHighUlong(ulong u) => new LowHighUlong(u);
        public static implicit operator ulong(LowHighUlong u) => Conversion.HighLowToLong(u.High, u.Low);
    }
}