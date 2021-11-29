// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows;

[Flags]
public enum ButtonStyles : uint
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb775951.aspx

    /// <summary>
    ///  Push button. (BS_PUSHBUTTON)
    /// </summary>
    PushButton = 0x00000000,

    /// <summary>
    ///  Default push button. (BS_DEFPUSHBUTTON)
    /// </summary>
    DefaultPushButton = 0x00000001,

    /// <summary>
    ///  Check box. (BS_CHECKBOX)
    /// </summary>
    CheckBox = 0x00000002,

    /// <summary>
    ///  Check box that automatically toggles state when clicked. (BS_AUTOCHECKBOX)
    /// </summary>
    AutoCheckBox = 0x00000003,

    /// <summary>
    ///  Radio button. (BS_RADIOBUTTON)
    /// </summary>
    RadioButton = 0x00000004,

    /// <summary>
    ///  Check box that can be disabled. (BS_3STATE)
    /// </summary>
    ThreeState = 0x00000005,

    /// <summary>
    ///  Three state checkbox that automatically changes state when clicked. (BS_AUTO3STATE)
    /// </summary>
    AutoThreeState = 0x00000006,

    /// <summary>
    ///  Group box. (BS_GROUPBOX)
    /// </summary>
    GroupBox = 0x00000007,

    /// <summary>
    ///  Obsolete. Use OwnerDraw instead. (BS_USERBUTTON)
    /// </summary>
    UserButton = 0x00000008,

    /// <summary>
    ///  Radio button that automatically changes state when clicked. (BS_AUTORADIOBUTTON)
    /// </summary>
    AutoRadioButton = 0x00000009,

    /// <summary>
    ///  A push button with just text (no frame or face). (BS_PUSHBOX)
    /// </summary>
    PushBox = 0x0000000A,

    /// <summary>
    ///  Sends DrawItem (WM_DRAWITEM) window messages to draw. (BS_OWNERDRAW)
    /// </summary>
    OwnerDrawn = 0x0000000B,

    /// <summary>
    ///  Button with a drop down arrow. (BS_SPLITBUTTON)
    /// </summary>
    SplitButton = 0x0000000C,

    /// <summary>
    ///  Default style split button. (BS_DEFSPLITBUTTON)
    /// </summary>
    DefaultSplitButton = 0x0000000D,

    /// <summary>
    ///  Button with a default green arrow and additional note text. (BS_COMMANDLINK)
    ///  https://msdn.microsoft.com/en-us/library/windows/desktop/dn742403.aspx
    /// </summary>
    CommandLink = 0x0000000E,

    /// <summary>
    ///  Default style command link button. (BS_DEFCOMMANDLINK)
    /// </summary>
    DefaultCommandLink = 0x0000000F,

    // Do not use- does not include all styles.
    // BS_TYPEMASK = 0x0000000F,

    /// <summary>
    ///  Draw text to the left of radio buttons and checkboxes.
    ///  Same as RightButton. (BS_LEFTTEXT)
    /// </summary>
    LeftText = 0x00000020,

    /// <summary>
    ///  Draw text to the left of radio buttons and checkboxes. (BS_RIGHTBUTTON)
    ///  Same as LeftText
    /// </summary>
    RightButton = LeftText,

    // This is the default state- buttons have text
    // BS_TEXT = 0x00000000,

    /// <summary>
    ///  Button only displays an icon (no text) if SetImage (BM_SETIMAGE) is sent. (BS_ICON)
    /// </summary>
    Icon = 0x00000040,

    /// <summary>
    ///  Button only displays a bitmap (no text) if SetImage (BM_SETIMAGE) is sent. (BS_BITMAP)
    /// </summary>
    Bitmap = 0x00000080,

    /// <summary>
    ///  Align text on the left. (BS_LEFT)
    /// </summary>
    Left = 0x00000100,

    /// <summary>
    ///  Align text on the right. (BS_RIGHT)
    /// </summary>
    Right = 0x00000200,

    /// <summary>
    ///  Center text horizontally. (BS_CENTER)
    /// </summary>
    Center = 0x00000300,

    /// <summary>
    ///  Align text at the top. (BS_TOP)
    /// </summary>
    Top = 0x00000400,

    /// <summary>
    ///  Align text at the bottom. (BS_BOTTOM)
    /// </summary>
    Bottom = 0x00000800,

    /// <summary>
    ///  Center text vertically. (BS_VCENTER)
    /// </summary>
    VerticallyCenter = 0x00000C00,

    /// <summary>
    ///  Makes a button look and act like a push button (raised when not pushed, sunken when pushed).(BS_PUSHLIKE)
    /// </summary>
    PushLike = 0x00001000,

    /// <summary>
    ///  Wraps text to multiple lines if needed. (BS_MULTILINE)
    /// </summary>
    Multiline = 0x00002000,

    /// <summary>
    ///  Enables a button to send KillFocus (BN_KILLFOCUS) and SetFocus (BN_SETFOCUS) notifications to the parent window. (BS_NOTIFY)
    /// </summary>
    Notify = 0x00004000,

    /// <summary>
    ///  Button is two-dimensional (doesn't shade). (BS_FLAT)
    /// </summary>
    Flat = 0x00008000
}