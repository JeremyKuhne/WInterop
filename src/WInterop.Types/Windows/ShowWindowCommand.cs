// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633548.aspx
    public enum ShowWindow : int
    {
        /// <summary>
        /// Hides the window, activating another window. (SW_HIDE)
        /// </summary>
        Hide = 0,

        /// <summary>
        /// Activates and displays a window, restoring it to its original size and position.
        /// Use this when displaying a window for the first time. (SW_SHOWNORMAL)
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Activates and displays minimized. (SW_SHOWMINIMIZED)
        /// </summary>
        Minimized = 2,

        /// <summary>
        /// Activates and displays maximized. (SW_SHOWMAXIMIZED)
        /// </summary>
        Maximized = 3,

        /// <summary>
        /// Same as Normal, but doesn't activate. (SW_SHOWNOACTIVATE)
        /// </summary>
        NormalNoActivate = 4,

        /// <summary>
        /// Activates and shows in its current size position. (SW_SHOW)
        /// </summary>
        Show = 5,

        /// <summary>
        /// Minimizes and activates the next window in the Z order. (SW_MINIMIZE)
        /// </summary>
        Minimize = 6,

        /// <summary>
        /// Same as Minimize, but doesn't activate. (SW_SHOWMINNOACTIVE)
        /// </summary>
        MinimizeNoActivate = 7,

        /// <summary>
        /// Same as Show, but doesn't activate. (SW_SHOWNA)
        /// </summary>
        NoActivate = 8,

        /// <summary>
        /// Activates and displays, restoring size and position if minimized or maximized (SW_RESTORE).
        /// </summary>
        Restore = 9,

        /// <summary>
        /// Shows according to the default value used to create the process (in STARTUPINFO). (SW_SHOWDEFAULT)
        /// </summary>
        Default = 10,

        /// <summary>
        /// Minimizes a window, even if the thread is not responding. (SW_FORCEMINIMIZE)
        /// </summary>
        ForceMinimize = 11,
    }
}
