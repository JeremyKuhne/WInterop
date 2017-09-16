// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi.Types;

namespace WInterop.Resources.Types
{
    /// <summary>
    /// Only valid when calling menu APIs (in MenuItemInformation [MENUITEMINFO])
    /// </summary>
    public static class MenuBitmaps
    {
        /// <summary>
        /// [HBMMENU_CALLBACK]
        /// </summary>
        public static BitmapHandle CallBack = new BitmapHandle((IntPtr)(-1));

        /// <summary>
        /// [HBMMENU_MBAR_CLOSE]
        /// </summary>
        public static BitmapHandle Close = new BitmapHandle((IntPtr)5);

        /// <summary>
        /// [HBMMENU_MBAR_CLOSE_D]
        /// </summary>
        public static BitmapHandle DisabledClose = new BitmapHandle((IntPtr)6);

        /// <summary>
        /// [HBMMENU_MBAR_MINIMIZE]
        /// </summary>
        public static BitmapHandle Minimize = new BitmapHandle((IntPtr)3);

        /// <summary>
        /// [HBMMENU_MBAR_MINIMIZE_D]
        /// </summary>
        public static BitmapHandle DisabledMinimize = new BitmapHandle((IntPtr)7);

        /// <summary>
        /// [HBMMENU_MBAR_RESTORE]
        /// </summary>
        public static BitmapHandle Restore = new BitmapHandle((IntPtr)2);

        /// <summary>
        /// [HBMMENU_POPUP_CLOSE]
        /// </summary>
        public static BitmapHandle PopupClose = new BitmapHandle((IntPtr)8);

        /// <summary>
        /// [HBMMENU_POPUP_MAXIMIZE]
        /// </summary>
        public static BitmapHandle PopupMaximize = new BitmapHandle((IntPtr)10);

        /// <summary>
        /// [HBMMENU_POPUP_MINIMIZE]
        /// </summary>
        public static BitmapHandle PopupMinimize = new BitmapHandle((IntPtr)11);

        /// <summary>
        /// [HBMMENU_POPUP_RESTORE]
        /// </summary>
        public static BitmapHandle PopupRestore = new BitmapHandle((IntPtr)9);

        /// <summary>
        /// [HBMMENU_SYSTEM]
        /// </summary>
        public static BitmapHandle System = new BitmapHandle((IntPtr)1);
    }
}
