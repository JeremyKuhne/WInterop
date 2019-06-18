// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// This describes how the individual mapping operation should be performed. [D2D1_MAP_OPTIONS]
    /// </summary>
    public enum MapOptions : uint
    {

        /// <summary>
        /// The mapped pointer has undefined behavior.
        /// </summary>
        None = 0,

        /// <summary>
        /// The mapped pointer can be read from.
        /// </summary>
        Read = 1,

        /// <summary>
        /// The mapped pointer can be written to.
        /// </summary>
        Write = 2,

        /// <summary>
        /// The previous contents of the bitmap are discarded when it is mapped.
        /// </summary>
        Discard = 4,
    }
}
