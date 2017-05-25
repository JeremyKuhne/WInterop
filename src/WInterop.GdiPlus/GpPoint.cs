// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GdiPlus
{
    // https://msdn.microsoft.com/en-us/library/ms534487.aspx
    public struct GpPoint
    {
        public int X;
        public int Y;

        public GpPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
