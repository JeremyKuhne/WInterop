// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633545.aspx
    [Flags]
    public enum WindowPosition : uint
    {
        /// <summary>
        ///  [SWP_NOSIZE]
        /// </summary>
        NoSize = 0x0001,

        /// <summary>
        ///  [SWP_NOMOVE]
        /// </summary>
        NoMove = 0x0002,

        /// <summary>
        ///  [SWP_NOZORDER]
        /// </summary>
        NoZOrder = 0x0004,

        /// <summary>
        ///  [SWP_NOREDRAW]
        /// </summary>
        NoRedraw = 0x0008,

        /// <summary>
        ///  [SWP_NOACTIVATE]
        /// </summary>
        NoActivate = 0x0010,

        /// <summary>
        ///  [SWP_FRAMECHANGED]
        /// </summary>
        FrameChanged = 0x0020,

        /// <summary>
        ///  [SWP_SHOWWINDOW]
        /// </summary>
        ShowWindow = 0x0040,

        /// <summary>
        ///  [SWP_HIDEWINDOW]
        /// </summary>
        HideWindow = 0x0080,

        /// <summary>
        ///  [SWP_NOCOPYBITS]
        /// </summary>
        NoCopyBits = 0x0100,

        /// <summary>
        ///  [SWP_NOOWNERZORDER]
        /// </summary>
        NoOwnerZOrder = 0x0200,

        /// <summary>
        ///  [SWP_NOSENDCHANGING]
        /// </summary>
        NoSendChanging = 0x0400,

        /// <summary>
        ///  [SWP_DRAWFRAME]
        /// </summary>
        DrawFrame = FrameChanged,

        /// <summary>
        ///  [SWP_NOREPOSITION]
        /// </summary>
        NoReposition = NoOwnerZOrder,

        /// <summary>
        ///  [SWP_DEFERERASE]
        /// </summary>
        DeferErase = 0x2000,

        /// <summary>
        ///  [SWP_ASYNCWINDOWPOS]
        /// </summary>
        AsyncWindowPosition = 0x4000
    }
}
