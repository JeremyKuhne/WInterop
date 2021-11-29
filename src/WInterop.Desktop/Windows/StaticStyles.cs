// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows;

[Flags]
public enum StaticStyles : uint
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb760773.aspx

    /// <summary>
    ///  Simple rectangle with left aligned text. [SS_LEFT]
    /// </summary>
    Left = 0x00000000,

    /// <summary>
    ///  Simple rectangle with centered text. [SS_CENTER]
    /// </summary>
    Center = 0x00000001,

    /// <summary>
    ///  Simple rectangle with right aligned text. [SS_RIGHT]
    /// </summary>
    Right = 0x00000002,

    /// <summary>
    ///  Icon resource named by text is displayed. [SS_ICON]
    /// </summary>
    Icon = 0x00000003,

    /// <summary>
    ///  Rectangle filled with the window frame color. [SS_BLACKRECT]
    /// </summary>
    BlackRectangle = 0x00000004,

    /// <summary>
    ///  Rectangle filled with the screen background color. [SS_GRAYRECT]
    /// </summary>
    GrayRectangle = 0x00000005,

    /// <summary>
    ///  Rectangle filled with the window background color. [SS_WHITERECT]
    /// </summary>
    WhiteRectangle = 0x00000006,

    /// <summary>
    ///  Box with a frame drawn in the current window frame color. [SS_BLACKFRAME]
    /// </summary>
    BlackFrame = 0x00000007,

    /// <summary>
    ///  Box with a frame drawn in the screen background [desktop] color. [SS_GRAYFRAME]
    /// </summary>
    GrayFrame = 0x00000008,

    /// <summary>
    ///  Box with a frame drawn with the window background color. [SS_WHITEFRAME]
    /// </summary>
    WhiteFrame = 0x00000009,

    /// <summary>
    ///  [SS_USERITEM]
    /// </summary>
    UserItem = 0x0000000A,

    /// <summary>
    ///  Simple rectangle single-line left aligned text.[SS_SIMPLE]
    /// </summary>
    Simple = 0x0000000B,

    /// <summary>
    ///  Left with no word wrapping. [SS_LEFTNOWORDWRAP]
    /// </summary>
    LeftNoWordWrap = 0x0000000C,

    /// <summary>
    ///  Owner of the static control is responsible for drawing. [SS_OWNERDRAW]
    /// </summary>
    OwnerDraw = 0x0000000D,

    /// <summary>
    ///  Bitmap is to be displayed. Text is the name of a bitmap resource.
    ///  Control ignores width and height and sizes to fit the bitmap. [SS_BITMAP]
    /// </summary>
    Bitmap = 0x0000000E,

    /// <summary>
    ///  Enhanced metafile is to be displayed. [SS_ENHMETAFILE]
    /// </summary>
    EnhancedMetafile = 0x0000000F,

    /// <summary>
    ///  Draws the top and bottom edges with the etched style. [SS_ETCHEDHORZ]
    /// </summary>
    EtchedHorizontal = 0x00000010,

    /// <summary>
    ///  Draws the left and right edges with the etched style. [SS_ETCHEDVERT]
    /// </summary>
    EtchedVertical = 0x00000011,

    /// <summary>
    ///  Draws the frame with the etched style. [SS_ETCHEDFRAME]
    /// </summary>
    EtchedFrame = 0x00000012,

    // SS_TYPEMASK          = 0x0000001F,

    /// <summary>
    ///  Adjust the size of bitmaps to fit the control. [SS_REALSIZECONTROL]
    /// </summary>
    RealSizeControl = 0x00000040,

    /// <summary>
    ///  Prevents interpretation of &amp; characters as accelerator prefixes. [SS_NOPREFIX]
    /// </summary>
    NoPrefix = 0x00000080,

    /// <summary>
    ///  Sends parent window click and enable notifications.
    ///  [STN_CLICKED/DBLCLICK, STN_ENABLE/DISABLE] [SS_NOTIFY]
    /// </summary>
    Notify = 0x00000100,

    /// <summary>
    ///  Centers bitmap in the control. [SS_CENTERIMAGE]
    /// </summary>
    CenterImage = 0x00000200,

    /// <summary>
    ///  Lower right corner stays fixed when control is resized. [SS_RIGHTJUST]
    /// </summary>
    RightJustify = 0x00000400,

    /// <summary>
    ///  Uses actual resource widtth for icons. [SS_REALSIZEIMAGE]
    /// </summary>
    RealSizeImage = 0x00000800,

    /// <summary>
    ///  Draws half-sunken border around the control. [SS_SUNKEN]
    /// </summary>
    Sunken = 0x00001000,

    /// <summary>
    ///  Displays text as an edit control would. [SS_EDITCONTROL]
    /// </summary>
    EditControl = 0x00002000,

    /// <summary>
    ///  If the end of the string doesn't fit, ellipsis are added to the end.
    ///  If a word doesn't fit into the control rectangle, it is truncated.
    ///  [SS_ENDELLIPSIS] Forces one line with no word wrap.
    /// </summary>
    EndEllipsis = 0x00004000,

    /// <summary>
    ///  Replaces characters in the middle of the string with ellipses. [SS_PATHELLIPSIS]
    ///  Forces one line with no word wrap.
    /// </summary>
    PathEllipsis = 0x00008000,

    /// <summary>
    ///  Truncates any word that does not fit in the rectangle and adds ellipses. [SS_WORDELLIPSIS]
    ///  Forces one line with no word wrap.
    /// </summary>
    WordEllipsis = 0x0000C000,

    // SS_ELLIPSISMASK      = 0x0000C000
}