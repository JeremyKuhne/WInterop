// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Unsafe
{
    // https://msdn.microsoft.com/en-us/library/dd145035.aspx
    public struct LOGBRUSH
    {
        public BrushStyle lpStyle;
        public COLORREF lbColor;
        public UIntPtr lbHatch;
    }
}
