// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633591.aspx
    public enum WindowLong : int
    {
        /// <summary>
        /// (GWL_WNDPROC)
        /// </summary>
        WindowProcedure = -4,

        /// <summary>
        /// (GWL_HINSTANCE)
        /// </summary>
        InstanceHandle = -6,

        /// <summary>
        /// (GWL_HWNDPARENT)
        /// </summary>
        ParentHandle = -8,

        /// <summary>
        /// (GWL_STYLE)
        /// </summary>
        Style = -16,

        /// <summary>
        /// (GWL_EXSTYLE)
        /// </summary>
        ExtendedStyle = -20,

        /// <summary>
        /// (GWL_USERDATA)
        /// </summary>
        UserData = -21,

        /// <summary>
        /// (GWL_ID)
        /// </summary>
        Id = -12
    }
}
