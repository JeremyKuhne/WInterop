// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    /// <summary>
    ///  Commission International de l'Eclairage device independent color space. [CIEXYZ]
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/dd371828.aspx"/>
    /// <see cref="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/wcs/device-independent-color-spaces"/>
    /// </remarks>
    public struct CieXyz
    {
        /// <summary>
        ///  [ciexyzX]
        /// </summary>
        public FixedPoint2_30 X;

        /// <summary>
        ///  [ciexyzY]
        /// </summary>
        public FixedPoint2_30 Y;

        /// <summary>
        ///  [ciexyzZ]
        /// </summary>
        public FixedPoint2_30 Z;
    }
}