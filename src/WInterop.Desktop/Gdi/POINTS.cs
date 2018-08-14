// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    /// <summary>
    /// [POINTS]
    /// </summary>
    /// <msdn>https://msdn.microsoft.com/en-us/library/dd162808.aspx</msdn>
    public struct PointS
    {
        public short X;
        public short Y;

        public PointS(short x, short y)
        {
            X = x;
            Y = y;
        }
    }
}
