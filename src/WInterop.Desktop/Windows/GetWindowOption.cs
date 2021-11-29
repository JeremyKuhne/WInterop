// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

public enum GetWindowOption : uint
{
    /// <summary>
    ///  The retrieved handle identifies the window of the same type that is highest in the Z order.
    ///  [GW_HWNDFIRST]
    /// </summary>
    /// <remarks>
    ///  If the specified window is a topmost window, the handle identifies a topmost window. If the
    ///  specified window is a top-level window, the handle identifies a top-level window. If the
    ///  specified window is a child window, the handle identifies a sibling window.
    /// </remarks>
    First = 0,

    /// <summary>
    ///  The retrieved handle identifies the window of the same type that is lowest in the Z order.
    ///  [GW_HWNDLAST]
    /// </summary>
    /// <remarks>
    ///  If the specified window is a topmost window, the handle identifies a topmost window. If the
    ///  specified window is a top-level window, the handle identifies a top-level window. If the
    ///  specified window is a child window, the handle identifies a sibling window.
    /// </remarks>
    Last = 1,

    /// <summary>
    ///  The retrieved handle identifies the window below the specified window in the Z order.
    ///  [GW_HWNDNEXT]
    /// </summary>
    /// <remarks>
    ///  If the specified window is a topmost window, the handle identifies a topmost window. If the
    ///  specified window is a top-level window, the handle identifies a top-level window. If the
    ///  specified window is a child window, the handle identifies a sibling window.
    /// </remarks>
    Next = 2,

    /// <summary>
    ///  The retrieved handle identifies the window below the specified window in the Z order.
    ///  [GW_HWNDPREV]
    /// </summary>
    /// <remarks>
    ///  If the specified window is a topmost window, the handle identifies a topmost window. If the
    ///  specified window is a top-level window, the handle identifies a top-level window. If the
    ///  specified window is a child window, the handle identifies a sibling window.
    /// </remarks>
    Previous = 3,

    /// <summary>
    ///  The retrieved handle identifies the specified window's owner window, if any. [GW_OWNER]
    /// </summary>
    Owner = 4,

    /// <summary>
    ///  The retrieved handle identifies the child window at the top of the Z order, if the specified window
    ///  is a parent window; otherwise, the retrieved handle is NULL. The function examines only child windows
    ///  of the specified window. It does not examine descendant windows. [GW_CHILD]
    /// </summary>
    Child = 5,

    /// <summary>
    ///  The retrieved handle identifies the enabled popup window owned by the specified window (the search
    ///  uses the first such window found using GW_HWNDNEXT); otherwise, if there are no enabled popup windows,
    ///  the retrieved handle is that of the specified window. [GW_ENABLEDPOPUP]
    /// </summary>
    EnabledPopup = 6
}