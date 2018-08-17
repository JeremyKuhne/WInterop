// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    public enum LogicalColorSpace : uint
    {
        /// <summary>
        /// Endpoints and gamma are given. [LCS_CALIBRATED_RGB]
        /// </summary>
        CalibratedRgb = 0,

        /// <summary>
        /// sRGB. Only valid in V5 header. [LCS_sRGB]
        /// </summary>
        SRgb = ((byte)'s' << 24) + ((byte)'R' << 16) + ((byte)'G' << 8) + (byte)'B', // 1934772034

        /// <summary>
        /// Windows default color space. Only valid in V5 header. [LCS_WINDOWS_COLOR_SPACE]
        /// </summary>
        WindowsColorSpace = ((byte)'W' << 24) + ((byte)'i' << 16) + ((byte)'n' << 8) + (byte)' ', // 1466527264

        /// <summary>
        /// ProfileData points to the file name of the color profile. [PROFILE_LINKED]
        /// </summary>
        ProfileLinked = ((byte)'L' << 24) + ((byte)'I' << 16) + ((byte)'N' << 8) + (byte)'K', // 1279872587

        /// <summary>
        /// ProfileData points to the buffer of the color profile. [PROFILE_EMBEDDED]
        /// </summary>
        ProfileEmbedded = ((byte)'M' << 24) + ((byte)'B' << 16) + ((byte)'E' << 8) + (byte)'D' // 1296188740
    }
}
