// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/cc230542.aspx
    public enum StretchMode
    {
        /// <summary>
        ///  Performs boolean AND with colors. [STRETCH_ANDSCANS]
        /// </summary>
        AndScans = 0x01,

        /// <summary>
        ///  Performs boolean OR with colors. [STRETCH_ORSCANS]
        /// </summary>
        OrScans = 0x02,

        /// <summary>
        ///  Deletes eliminated lines of pixels. [STRETCH_DELETESCANS]
        /// </summary>
        DeleteScans = 0x03,

        /// <summary>
        ///  Takes the average of colors. [STRETCH_HALFTONE]
        /// </summary>
        HalfTone = 0x04
    }
}
