// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd183465.aspx
    public enum CombineRegionMode : int
    {
        /// <summary>
        /// (RGN_AND)
        /// </summary>
        And = 1,

        /// <summary>
        /// (RGN_OR)
        /// </summary>
        Or = 2,

        /// <summary>
        /// (RGN_XOR)
        /// </summary>
        Xor = 3,

        /// <summary>
        /// (RGN_DIFF)
        /// </summary>
        Diff = 4,

        /// <summary>
        /// (RGN_COPY)
        /// </summary>
        Copy = 5
    }
}
