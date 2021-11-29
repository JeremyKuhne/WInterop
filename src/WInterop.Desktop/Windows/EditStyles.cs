// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows;

[Flags]
public enum EditStyles : uint
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb775464.aspx

    /// <summary>
    ///  Left align text. (ES_LEFT)
    /// </summary>
    Left = 0x0000,

    /// <summary>
    ///  Center text. (ES_CENTER)
    /// </summary>
    Center = 0x0001,

    /// <summary>
    ///  Right align text. (ES_RIGHT)
    /// </summary>
    Right = 0x0002,

    /// <summary>
    ///  Multiline. (ES_MULTILINE)
    /// </summary>
    Multiline = 0x0004,

    /// <summary>
    ///  Convert all characters to uppercase as typed. (ES_UPPERCASE)
    /// </summary>
    Uppercase = 0x0008,

    /// <summary>
    ///  Convert all characters to lowercase as typed. (ES_LOWERCASE)
    /// </summary>
    Lowercase = 0x0010,

    /// <summary>
    ///  Display asterisks for characters as typed. (ES_PASSWORD)
    /// </summary>
    Password = 0x0020,

    /// <summary>
    ///  (ES_AUTOVSCROLL)
    /// </summary>
    AutoVerticalScroll = 0x0040,

    /// <summary>
    ///  (ES_AUTOHSCROLL)
    /// </summary>
    AutoHorizontalScroll = 0x0080,

    /// <summary>
    ///  Keep selection highlighted when focus is lost. (ES_NOHIDESEL)
    /// </summary>
    NoHideSelection = 0x0100,

    /// <summary>
    ///  Converts to OEM character set. (ES_OEMCONVERT)
    /// </summary>
    OemConvert = 0x0400,

    /// <summary>
    ///  Prevent typing in the control. (ES_READONLY)
    /// </summary>
    ReadOnly = 0x0800,

    /// <summary>
    ///  Enter key insters carriage return rather than being sent to default push button. (ES_WANTRETURN)
    /// </summary>
    WantReturn = 0x1000,

    /// <summary>
    ///  Allow only digits to be typed. (ES_NUMBER)
    /// </summary>
    Number = 0x2000
}