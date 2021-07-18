// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public enum ClassLong : int
    {
        /// <summary>
        ///  (GCL_MENUNAME)
        /// </summary>
        MenuName = -8,

        /// <summary>
        ///  (GCL_HBRBACKGROUND)
        /// </summary>
        BackgroundBrush = -10,

        /// <summary>
        ///  (GCL_HCURSOR)
        /// </summary>
        Cursor = -12,

        /// <summary>
        ///  (GCL_HICON)
        /// </summary>
        Icon = -14,

        /// <summary>
        ///  (GCL_HMODULE)
        /// </summary>
        Module = -16,

        /// <summary>
        ///  (GCL_CBWNDEXTRA)
        /// </summary>
        WindowExtra = -18,

        /// <summary>
        ///  (GCL_CBCLSEXTRA)
        /// </summary>
        ClassExtra = -20,

        /// <summary>
        ///  (GCL_WNDPROC)
        /// </summary>
        WindowProcedure = -24,

        /// <summary>
        ///  (GCL_STYLE)
        /// </summary>
        Style = -26,

        /// <summary>
        ///  (GCW_ATOM)
        /// </summary>
        Atom = -32,

        /// <summary>
        ///  (GCL_HICONSM)
        /// </summary>
        SmallIcon = -34
    }
}