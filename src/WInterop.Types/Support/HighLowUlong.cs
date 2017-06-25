// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Support
{
    /// <summary>
    /// For easier access of ulongs where they are defined in native structs as separate
    /// high/low parts.
    /// </summary>
    public struct HighLowUlong
    {
#pragma warning disable CS0649
        private uint High;
        private uint Low;
#pragma warning restore CS0649

        public static implicit operator ulong(HighLowUlong u) => Conversion.HighLowToLong(u.High, u.Low);
    }
}
