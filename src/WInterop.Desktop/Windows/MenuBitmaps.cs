// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

/// <summary>
///  Only valid when calling menu APIs (in MenuItemInformation [MENUITEMINFO])
/// </summary>
public static class MenuBitmaps
{
    /// <summary>
    ///  [HBMMENU_CALLBACK]
    /// </summary>
    public static readonly HBITMAP CallBack = HBMMENU.HBMMENU_CALLBACK;

    /// <summary>
    ///  [HBMMENU_MBAR_CLOSE]
    /// </summary>
    public static readonly HBITMAP Close = HBMMENU.HBMMENU_MBAR_CLOSE;

    /// <summary>
    ///  [HBMMENU_MBAR_CLOSE_D]
    /// </summary>
    public static readonly HBITMAP DisabledClose = HBMMENU.HBMMENU_MBAR_CLOSE_D;

    /// <summary>
    ///  [HBMMENU_MBAR_MINIMIZE]
    /// </summary>
    public static readonly HBITMAP Minimize = HBMMENU.HBMMENU_MBAR_MINIMIZE;

    /// <summary>
    ///  [HBMMENU_MBAR_MINIMIZE_D]
    /// </summary>
    public static readonly HBITMAP DisabledMinimize = HBMMENU.HBMMENU_MBAR_MINIMIZE_D;

    /// <summary>
    ///  [HBMMENU_MBAR_RESTORE]
    /// </summary>
    public static readonly HBITMAP Restore = HBMMENU.HBMMENU_MBAR_RESTORE;

    /// <summary>
    ///  [HBMMENU_POPUP_CLOSE]
    /// </summary>
    public static readonly HBITMAP PopupClose = HBMMENU.HBMMENU_POPUP_CLOSE;

    /// <summary>
    ///  [HBMMENU_POPUP_MAXIMIZE]
    /// </summary>
    public static readonly HBITMAP PopupMaximize = HBMMENU.HBMMENU_POPUP_MAXIMIZE;

    /// <summary>
    ///  [HBMMENU_POPUP_MINIMIZE]
    /// </summary>
    public static readonly HBITMAP PopupMinimize = HBMMENU.HBMMENU_POPUP_MINIMIZE;

    /// <summary>
    ///  [HBMMENU_POPUP_RESTORE]
    /// </summary>
    public static readonly HBITMAP PopupRestore = HBMMENU.HBMMENU_POPUP_RESTORE;

    /// <summary>
    ///  [HBMMENU_SYSTEM]
    /// </summary>
    public static readonly HBITMAP System = HBMMENU.HBMMENU_SYSTEM;
}