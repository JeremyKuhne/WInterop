// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

#pragma warning disable CA1069 // Enums values should not be duplicated

[Flags]
public enum TextFormat : uint
{
    /// <summary>
    ///  (DT_TOP)
    /// </summary>
    Top = DT.DT_TOP,

    /// <summary>
    ///  (DT_LEFT)
    /// </summary>
    Left = DT.DT_LEFT,

    /// <summary>
    ///  (DT_CENTER)
    /// </summary>
    Center = DT.DT_CENTER,

    /// <summary>
    ///  (DT_RIGHT)
    /// </summary>
    Right = DT.DT_RIGHT,

    /// <summary>
    ///  (DT_VCENTER)
    /// </summary>
    VerticallyCenter = DT.DT_VCENTER,

    /// <summary>
    ///  (DT_BOTTOM)
    /// </summary>
    Bottom = DT.DT_BOTTOM,

    /// <summary>
    ///  (DT_WORDBREAK)
    /// </summary>
    WordBreak = DT.DT_WORDBREAK,

    /// <summary>
    ///  (DT_SINGLELINE)
    /// </summary>
    SingleLine = DT.DT_SINGLELINE,

    /// <summary>
    ///  (DT_EXPANDTABS)
    /// </summary>
    ExpandTabs = DT.DT_EXPANDTABS,

    /// <summary>
    ///  (DT_TABSTOP)
    /// </summary>
    TabStop = DT.DT_TABSTOP,

    /// <summary>
    ///  (DT_NOCLIP)
    /// </summary>
    NoClip = DT.DT_NOCLIP,

    /// <summary>
    ///  (DT_EXTERNALLEADING)
    /// </summary>
    ExternalLeading = DT.DT_EXTERNALLEADING,

    /// <summary>
    ///  (DT_CALCRECT)
    /// </summary>
    CalculateRectangle = DT.DT_CALCRECT,

    /// <summary>
    ///  (DT_NOPREFIX)
    /// </summary>
    NoPrefix = DT.DT_NOPREFIX,

    /// <summary>
    ///  (DT_INTERNAL)
    /// </summary>
    Internal = DT.DT_INTERNAL,

    /// <summary>
    ///  (DT_EDITCONTROL)
    /// </summary>
    EditControl = DT.DT_EDITCONTROL,

    /// <summary>
    ///  (DT_PATH_ELLIPSIS)
    /// </summary>
    PathEllipsis = DT.DT_PATH_ELLIPSIS,

    /// <summary>
    ///  (DT_END_ELLIPSIS)
    /// </summary>
    EndEllipsis = DT.DT_END_ELLIPSIS,

    /// <summary>
    ///  (DT_MODIFYSTRING)
    /// </summary>
    ModifyString = DT.DT_RIGHT,

    /// <summary>
    ///  (DT_RTLREADING)
    /// </summary>
    RightToLeftReading = DT.DT_RTLREADING,

    /// <summary>
    ///  (DT_WORD_ELLIPSIS)
    /// </summary>
    WordEllipsis = DT.DT_RIGHT,

    /// <summary>
    ///  (DT_NOFULLWIDTHCHARBREAK)
    /// </summary>
    NoFullWidthCharacterBreak = DT.DT_NOFULLWIDTHCHARBREAK,

    /// <summary>
    ///  (DT_HIDEPREFIX)
    /// </summary>
    HidePrefix = DT.DT_HIDEPREFIX,

    /// <summary>
    ///  (DT_PREFIXONLY)
    /// </summary>
    PrefixOnly = DT.DT_PREFIXONLY
}

#pragma warning restore CA1069 // Enums values should not be duplicated