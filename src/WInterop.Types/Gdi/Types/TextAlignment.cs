// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd144932.aspx
    /// <summary>
    /// Text alignment. Be VERY careful when checking values as 0
    /// is default for three different states. Even worse is that
    /// the other values overlap.
    /// 
    /// To check horizontal alignment, for example, you need to
    /// do the following:
    /// 
    ///   (value & (TA_RIGHT | TA_CENTER)) == desired
    /// </summary>
    [Flags]
    public enum TextAlignment : uint
    {
        TA_NOUPDATECP                = 0,
        TA_UPDATECP                  = 1,
        TA_LEFT                      = 0,
        TA_RIGHT                     = 2,
        TA_CENTER                    = 6,
        TA_TOP                       = 0,
        TA_BOTTOM                    = 8,
        TA_BASELINE                  = 24,
        TA_RTLREADING                = 256,
        GDI_ERROR                    = 0xFFFFFFFF
    }
}
