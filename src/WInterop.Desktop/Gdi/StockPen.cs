// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd183533.aspx
    public enum StockPen : int
    {
        /// <summary>
        ///  (WHITE_PEN)
        /// </summary>
        White = StockObject.WhitePen,

        /// <summary>
        ///  (BLACK_PEN)
        /// </summary>
        Black = StockObject.BlackPen,

        /// <summary>
        ///  (NULL_PEN)
        /// </summary>
        Null = StockObject.NullPen,

        /// <summary>
        ///  (DC_PEN)
        /// </summary>
        DeviceContext = StockObject.DeviceContextPen
    }
}
