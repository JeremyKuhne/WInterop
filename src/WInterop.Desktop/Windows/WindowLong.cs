// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setwindowlongw
    public enum WindowLong : int
    {
        /// <summary>
        ///  Gets/sets the address of the Windows Procedure. [GWL_WNDPROC]
        /// </summary>
        WindowProcedure = -4,

        /// <summary>
        ///  Gets/sets the application instance handle. [GWL_HINSTANCE]
        /// </summary>
        InstanceHandle = -6,

        /// <summary>
        ///  Gets the parent window handle, if any. [GWL_HWNDPARENT]
        /// </summary>
        ParentHandle = -8,

        /// <summary>
        ///  Gets/sets the <see cref="WindowStyles"/>. [GWL_STYLE]
        /// </summary>
        Style = -16,

        /// <summary>
        ///  Gets/sets the <see cref="ExtendedWindowStyles"/>. [GWL_EXSTYLE]
        /// </summary>
        ExtendedStyle = -20,

        /// <summary>
        ///  Gets/sets the user data for the Window. [GWL_USERDATA]
        /// </summary>
        UserData = -21,

        /// <summary>
        ///  Gets/sets the ID of a child window. [GWL_ID]
        /// </summary>
        Id = -12
    }
}
