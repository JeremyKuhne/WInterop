// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

public enum StockFont : int
{
    // https://docs.microsoft.com/openspecs/windows_protocols/ms-emf/

    /// <summary>
    ///  (OEM_FIXED_FONT)
    /// </summary>
    OemFixed = StockObject.OemFixedFont,

    /// <summary>
    ///  (ANSI_FIXED_FONT)
    /// </summary>
    AnsiFixed = StockObject.AnsiFixedFont,

    /// <summary>
    ///  (ANSI_VAR_FONT)
    /// </summary>
    AnsiVariable = StockObject.AnsiVariableFont,

    /// <summary>
    ///  (SYSTEM_FONT)
    /// </summary>
    System = StockObject.SystemFont,

    /// <summary>
    ///  (DEVICE_DEFAULT_FONT)
    /// </summary>
    DeviceDefault = StockObject.DeviceDefaultFont,

    /// <summary>
    ///  (SYSTEM_FIXED_FONT)
    /// </summary>
    SystemFixed = StockObject.SystemFixedFont,

    /// <summary>
    ///  Default font used for menus, dialog boxes, etc. (DEFAULT_GUI_FONT)
    /// </summary>
    GuiFont = StockObject.DefaultGuiFont
}