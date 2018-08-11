// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Gdi.Native
{
    // https://msdn.microsoft.com/en-us/library/dd145041.aspx
    public struct LOGPEN
    {
        public PenStyle lopnStyle;
        public Point lopnWidth;
        public COLORREF lopnColor;
    }
}
