// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    /// <summary>
    /// [SCROLLINFO]
    /// </summary>
    /// <remarks><see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/bb787537.aspx"/></remarks>
    public struct ScrollInfo
    {
        public uint cbSize;
        public ScrollInfoMask fMask;
        public int nMin;
        public int nMax;
        public uint nPage;
        public int nPos;
        public int nTrackPos;
    }
}
