﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;

namespace WInterop
{
    /// <summary>
    /// For easier access of ulongs where they are defined in native structs as separate
    /// low/high parts.
    /// </summary>
    public struct LowHighUlong
    {
        public uint Low;
        public uint High;

        public static implicit operator LowHighUlong(ulong u) => new LowHighUlong { High = Conversion.HighWord(u), Low = Conversion.LowWord(u) };
        public static implicit operator ulong(LowHighUlong u) => Conversion.HighLowToLong(u.High, u.Low);
    }
}
