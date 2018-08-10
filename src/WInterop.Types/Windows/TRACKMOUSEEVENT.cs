// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    /// <summary>
    /// [TRACKMOUSEEVENT]
    /// </summary>
    /// <remarks><see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms645604.aspx"/></remarks>
    public struct TrackMouseEvent
    {
        public uint cbSize;
        public TrackMouseEvents dwFlags;
        public WindowHandle hwndTrack;
        public uint dwHoverTime;
    }
}
