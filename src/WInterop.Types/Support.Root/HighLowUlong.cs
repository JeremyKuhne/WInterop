// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;

namespace WInterop
{
    /// <summary>
    /// For easier access of ulongs where they are defined in native structs as separate
    /// high/low parts.
    /// </summary>
    public struct HighLowUlong
    {
        public uint High;
        public uint Low;

        public static implicit operator HighLowUlong(ulong u) => new HighLowUlong { High = Conversion.HighWord(u), Low = Conversion.LowWord(u) };
        public static implicit operator ulong(HighLowUlong u) => Conversion.HighLowToLong(u.High, u.Low);
    }
}
