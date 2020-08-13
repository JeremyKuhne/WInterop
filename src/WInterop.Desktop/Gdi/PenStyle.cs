// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd183509.aspx
    public enum PenStyle : int
    {
        /// <summary>
        ///  [PS_SOLID]
        /// </summary>
        Solid = 0,

        /// <summary>
        ///  [PS_DASH]
        /// </summary>
        Dash = 1,

        /// <summary>
        ///  [PS_DOT]
        /// </summary>
        Dot = 2,

        /// <summary>
        ///  [PS_DASHDOT]
        /// </summary>
        DashDot = 3,

        /// <summary>
        ///  [PS_DASHDOTDOT]
        /// </summary>
        DashDotDot = 4,

        /// <summary>
        ///  [PS_NULL]
        /// </summary>
        Null = 5,

        /// <summary>
        ///  [PS_INSIDEFRAME]
        /// </summary>
        InsideFrame = 6
    }
}
