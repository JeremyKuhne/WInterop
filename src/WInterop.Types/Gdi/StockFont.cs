// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    public enum StockFont : int
    {
        // https://msdn.microsoft.com/en-us/library/cc231191.aspx
        // https://msdn.microsoft.com/en-us/library/dd183533.aspx

        /// <summary>
        /// (OEM_FIXED_FONT)
        /// </summary>
        OemFixed = StockObject.OEM_FIXED_FONT,

        /// <summary>
        /// (ANSI_FIXED_FONT)
        /// </summary>
        AnsiFixed = StockObject.ANSI_FIXED_FONT,

        /// <summary>
        /// (ANSI_VAR_FONT)
        /// </summary>
        AnsiVariable = StockObject.ANSI_VAR_FONT,

        /// <summary>
        /// (SYSTEM_FONT)
        /// </summary>
        System = StockObject.SYSTEM_FONT,

        /// <summary>
        /// (DEVICE_DEFAULT_FONT)
        /// </summary>
        DeviceDefault = StockObject.DEVICE_DEFAULT_FONT,

        /// <summary>
        /// (SYSTEM_FIXED_FONT)
        /// </summary>
        SystemFixed = StockObject.SYSTEM_FIXED_FONT,

        /// <summary>
        /// Default font used for menus, dialog boxes, etc. (DEFAULT_GUI_FONT)
        /// </summary>
        GuiFont = StockObject.DEFAULT_GUI_FONT
    }
}