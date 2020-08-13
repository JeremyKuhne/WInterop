// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    public enum PenJoin : uint
    {
        /// <summary>
        ///  [PS_JOIN_ROUND]
        /// </summary>
        Round = 0x00000000,

        /// <summary>
        ///  [PS_JOIN_BEVEL]
        /// </summary>
        Bevel = 0x00001000,

        /// <summary>
        ///  [PS_JOIN_MITER]
        /// </summary>
        Miter = 0x00002000
    }
}
