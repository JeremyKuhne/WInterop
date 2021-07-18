// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi
{
    /// <summary>
    ///  Whether or not the characters in a font have a fixed or variable width (pitch).
    /// </summary>
    [Flags]
    public enum FontPitch : byte
    {
        // https://msdn.microsoft.com/en-us/library/cc250403.aspx

        /// <summary>
        ///  Default pitch. (DEFAULT_PITCH)
        /// </summary>
        Default = 0x00,

        /// <summary>
        ///  The font is fixed. (FIXED_PITCH)
        /// </summary>
        FixedPitch = 0x01,

        /// <summary>
        ///  The width is proportional. (VARIABLE_PITCH)
        /// </summary>
        VariablePitch = 0x02
    }
}