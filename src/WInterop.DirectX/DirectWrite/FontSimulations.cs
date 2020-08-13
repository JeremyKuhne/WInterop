// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  Specifies algorithmic style simulations to be applied to the font face.
    ///  Bold and oblique simulations can be combined via bitwise OR operation.
    ///  [DWRITE_FONT_SIMULATIONS]
    /// </summary>
    [Flags]
    public enum FontSimulations : uint
    {
        /// <summary>
        ///  No simulations are performed.
        /// </summary>
        None = 0x0000,

        /// <summary>
        ///  Algorithmic emboldening is performed.
        /// </summary>
        Bold = 0x0001,

        /// <summary>
        ///  Algorithmic italicization is performed.
        /// </summary>
        Oblique = 0x0002
    }
}
