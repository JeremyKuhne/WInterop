// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

public enum SystemColor : int
{
    /// <summary>
    ///  Scroll bar gray area. [COLOR_SCROLLBAR]
    /// </summary>
    ScrollBar = COLOR.COLOR_SCROLLBAR,

    /// <summary>
    ///  The Desktop. [COLOR_BACKGROUND]
    /// </summary>
    Background = COLOR.COLOR_BACKGROUND,

    /// <summary>
    ///  [COLOR_ACTIVECAPTION]
    /// </summary>
    ActiveCaption = COLOR.COLOR_ACTIVECAPTION,

    /// <summary>
    ///  [COLOR_INACTIVECAPTION]
    /// </summary>
    InactiveCaption = COLOR.COLOR_INACTIVECAPTION,

    /// <summary>
    ///  [COLOR_MENU]
    /// </summary>
    Menu = COLOR.COLOR_MENU,

    /// <summary>
    ///  Window background. [COLOR_WINDOW]
    /// </summary>
    Window = COLOR.COLOR_WINDOW,

    /// <summary>
    ///  [COLOR_WINDOWFRAME]
    /// </summary>
    WindowFrame = COLOR.COLOR_WINDOWFRAME,

    /// <summary>
    ///  [COLOR_MENUTEXT]
    /// </summary>
    MenuText = COLOR.COLOR_MENUTEXT,

    /// <summary>
    ///  [COLOR_WINDOWTEXT]
    /// </summary>
    WindowText = COLOR.COLOR_WINDOWTEXT,

    /// <summary>
    ///  [COLOR_CAPTIONTEXT]
    /// </summary>
    CaptionText = COLOR.COLOR_CAPTIONTEXT,

    /// <summary>
    ///  [COLOR_ACTIVEBORDER]
    /// </summary>
    ActiveBorder = COLOR.COLOR_ACTIVEBORDER,

    /// <summary>
    ///  [COLOR_INACTIVEBORDER]
    /// </summary>
    InactiveBorder = COLOR.COLOR_INACTIVEBORDER,

    /// <summary>
    ///  [COLOR_APPWORKSPACE]
    /// </summary>
    AppWorkSpace = COLOR.COLOR_APPWORKSPACE,

    /// <summary>
    ///  [COLOR_HIGHLIGHT]
    /// </summary>
    Highlight = COLOR.COLOR_HIGHLIGHT,

    /// <summary>
    ///  [COLOR_HIGHLIGHTTEXT]
    /// </summary>
    HighlightText = COLOR.COLOR_HIGHLIGHTTEXT,

    /// <summary>
    ///  For dialog box backgrounds. [COLOR_BTNFACE]
    /// </summary>
    ButtonFace = COLOR.COLOR_BTNFACE,

    /// <summary>
    ///  [COLOR_BTNSHADOW]
    /// </summary>
    ButtonShadow = COLOR.COLOR_BTNSHADOW,

    /// <summary>
    ///  [COLOR_GRAYTEXT]
    /// </summary>
    GrayText = COLOR.COLOR_GRAYTEXT,

    /// <summary>
    ///  [COLOR_BTNTEXT]
    /// </summary>
    ButtonText = COLOR.COLOR_BTNTEXT,

    /// <summary>
    ///  [COLOR_INACTIVECAPTIONTEXT]
    /// </summary>
    InactiveCaptionText = COLOR.COLOR_INACTIVECAPTIONTEXT,

    /// <summary>
    ///  [COLOR_BTNHIGHLIGHT]
    /// </summary>
    ButtonHighlight = COLOR.COLOR_BTNHIGHLIGHT,

    /// <summary>
    ///  [COLOR_3DDKSHADOW]
    /// </summary>
    DarkShadow3d = COLOR.COLOR_3DDKSHADOW,

    /// <summary>
    ///  [COLOR_3DLIGHT]
    /// </summary>
    Light3d = COLOR.COLOR_3DLIGHT,

    /// <summary>
    ///  [COLOR_INFOTEXT]
    /// </summary>
    InfoText = COLOR.COLOR_INFOTEXT,

    /// <summary>
    ///  [COLOR_INFOBK]
    /// </summary>
    InfoBackground = COLOR.COLOR_INFOBK,

    /// <summary>
    ///  [COLOR_HOTLIGHT]
    /// </summary>
    HotLight = COLOR.COLOR_HOTLIGHT,

    /// <summary>
    ///  [COLOR_GRADIENTACTIVECAPTION]
    /// </summary>
    GradientActiveCaption = COLOR.COLOR_GRADIENTACTIVECAPTION,

    /// <summary>
    ///  [COLOR_GRADIENTINACTIVECAPTION]
    /// </summary>
    GradientInactiveCaption = COLOR.COLOR_GRADIENTINACTIVECAPTION,

    /// <summary>
    ///  [COLOR_MENUHILIGHT]
    /// </summary>
    MenuHighlight = COLOR.COLOR_MENUHILIGHT,

    /// <summary>
    ///  [COLOR_MENUBAR]
    /// </summary>
    MenuBar = COLOR.COLOR_MENUBAR,

#pragma warning disable CA1069 // Enums values should not be duplicated
    /// <summary>
    ///  [COLOR_DESKTOP]
    /// </summary>
    Desktop = COLOR.COLOR_DESKTOP,

    /// <summary>
    ///  [COLOR_3DFACE]
    /// </summary>
    Face3d = COLOR.COLOR_3DFACE,

    /// <summary>
    ///  [COLOR_3DSHADOW]
    /// </summary>
    Shadow3d = COLOR.COLOR_3DSHADOW,

    /// <summary>
    ///  [COLOR_3DHIGHLIGHT]
    /// </summary>
    Highlight3d = COLOR.COLOR_3DHIGHLIGHT
#pragma warning restore CA1069 // Enums values should not be duplicated
}