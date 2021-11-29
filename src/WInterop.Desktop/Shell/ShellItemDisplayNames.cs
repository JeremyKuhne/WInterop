// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Shell;

/// <summary>
///  Type of display name to get for a shell item.
///  https://msdn.microsoft.com/en-us/library/windows/desktop/bb762544.aspx
/// </summary>
public enum ShellItemDisplayNames : int
{
    NormalDisplay = 0x00000000,
    ParentRelativeParsing = unchecked((int)0x80018001),
    DesktopAbsoluteParsing = unchecked((int)0x80028000),
    ParentRelativeEditing = unchecked((int)0x80031001),
    DesktopAbsoluteEditing = unchecked((int)0x8004c000),
    FilesysPath = unchecked((int)0x80058000),
    Url = unchecked((int)0x80068000),
    ParentRelativeForAddressBar = unchecked((int)0x8007c001),
    ParentRelative = unchecked((int)0x80080001),
    ParentRelativeForUI = unchecked((int)0x80094001)
}