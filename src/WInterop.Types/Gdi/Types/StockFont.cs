// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd183533.aspx
    public enum StockFont : int
    {
        OEM_FIXED_FONT = StockObject.OEM_FIXED_FONT,
        ANSI_FIXED_FONT = StockObject.ANSI_FIXED_FONT,
        ANSI_VAR_FONT = StockObject.ANSI_VAR_FONT,
        SYSTEM_FONT = StockObject.SYSTEM_FONT,
        DEVICE_DEFAULT_FONT = StockObject.DEVICE_DEFAULT_FONT,
        SYSTEM_FIXED_FONT = StockObject.SYSTEM_FIXED_FONT,
        DEFAULT_GUI_FONT = StockObject.DEFAULT_GUI_FONT
    }
}