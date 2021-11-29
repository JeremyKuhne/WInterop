// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi;
using WInterop.Gdi.Native;

namespace WInterop.Windows;

/// <summary>
///  Only valid when calling menu APIs (in MenuItemInformation [MENUITEMINFO])
/// </summary>
public static class MenuBitmaps
{
    /// <summary>
    ///  [HBMMENU_CALLBACK]
    /// </summary>
    public static HBITMAP CallBack = new HBITMAP((IntPtr)(-1));

    /// <summary>
    ///  [HBMMENU_MBAR_CLOSE]
    /// </summary>
    public static HBITMAP Close = new HBITMAP((IntPtr)5);

    /// <summary>
    ///  [HBMMENU_MBAR_CLOSE_D]
    /// </summary>
    public static HBITMAP DisabledClose = new HBITMAP((IntPtr)6);

    /// <summary>
    ///  [HBMMENU_MBAR_MINIMIZE]
    /// </summary>
    public static HBITMAP Minimize = new HBITMAP((IntPtr)3);

    /// <summary>
    ///  [HBMMENU_MBAR_MINIMIZE_D]
    /// </summary>
    public static HBITMAP DisabledMinimize = new HBITMAP((IntPtr)7);

    /// <summary>
    ///  [HBMMENU_MBAR_RESTORE]
    /// </summary>
    public static HBITMAP Restore = new HBITMAP((IntPtr)2);

    /// <summary>
    ///  [HBMMENU_POPUP_CLOSE]
    /// </summary>
    public static HBITMAP PopupClose = new HBITMAP((IntPtr)8);

    /// <summary>
    ///  [HBMMENU_POPUP_MAXIMIZE]
    /// </summary>
    public static HBITMAP PopupMaximize = new HBITMAP((IntPtr)10);

    /// <summary>
    ///  [HBMMENU_POPUP_MINIMIZE]
    /// </summary>
    public static HBITMAP PopupMinimize = new HBITMAP((IntPtr)11);

    /// <summary>
    ///  [HBMMENU_POPUP_RESTORE]
    /// </summary>
    public static HBITMAP PopupRestore = new HBITMAP((IntPtr)9);

    /// <summary>
    ///  [HBMMENU_SYSTEM]
    /// </summary>
    public static HBITMAP System = new HBITMAP((IntPtr)1);
}