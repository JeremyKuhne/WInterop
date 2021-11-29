// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

// https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getstockobject
public enum StockObject : int
{
    WhiteBrush = 0,
    LightGrayBrush = 1,
    GrayBrush = 2,
    DarkGrayBrush = 3,
    BlackBrush = 4,
    NullBrush = 5,
    HollowBrush = NullBrush,
    WhitePen = 6,
    BlackPen = 7,
    NullPen = 8,
    OemFixedFont = 10,
    AnsiFixedFont = 11,
    AnsiVariableFont = 12,
    SystemFont = 13,
    DeviceDefaultFont = 14,
    DefaultPalette = 15,
    SystemFixedFont = 16,
    DefaultGuiFont = 17,
    DeviceContextBrush = 18,
    DeviceContextPen = 19
}