// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  Contains information about a glyph cluster. [DWRITE_CLUSTER_METRICS]
    /// </summary>
    public readonly struct ClusterMetrics
    {
        /// <summary>
        ///  The total advance width of all glyphs in the cluster.
        /// </summary>
        public readonly float Width;

        /// <summary>
        ///  The number of text positions in the cluster.
        /// </summary>
        public readonly ushort Length;

        public readonly PackBits OtherMetrics;

        [Flags]
        public enum PackBits : ushort
        {
            /// <summary>
            ///  Indicate whether line can be broken right after the cluster.
            /// </summary>
            CanWrapLineAfter = 0b1000_0000_0000_0000,

            /// <summary>
            ///  Indicate whether the cluster corresponds to whitespace character.
            /// </summary>
            IsWhiteSpace =     0b0100_0000_0000_0000,

            /// <summary>
            ///  Indicate whether the cluster corresponds to a newline character.
            /// </summary>
            IsNewLine =        0b0010_0000_0000_0000,

            /// <summary>
            ///  Indicate whether the cluster corresponds to soft hyphen character.
            /// </summary>
            IsSoftHyphen =     0b0001_0000_0000_0000,

            /// <summary>
            ///  Indicate whether the cluster is read from right to left.
            /// </summary>
            IsRightToLeft =    0b0000_1000_0000_0000
        }
    };
}
