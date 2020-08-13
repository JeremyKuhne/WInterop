// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    /// <summary>
    ///  [TRACKMOUSEEVENT]
    /// </summary>
    /// <docs>https://docs.microsoft.com/windows/win32/api/winuser/ns-winuser-trackmouseevent</docs>
    public struct TrackMouseEvent
    {
        public uint Size;
        public TrackMouseEvents Flags;
        public WindowHandle WindowToTrack;
        public uint HoverTimeOut;
    }
}
