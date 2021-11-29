// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

[Flags]
public enum MenuItemInfoMembers : uint
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647578.aspx

    /// <summary>
    ///  (MIIM_STATE)
    /// </summary>
    State = 0x00000001,

    /// <summary>
    ///  (MIIM_ID)
    /// </summary>
    Id = 0x00000002,

    /// <summary>
    ///  (MIIM_SUBMENU)
    /// </summary>
    Submenu = 0x00000004,

    /// <summary>
    ///  (MIIM_CHECKMARKS)
    /// </summary>
    CheckMarks = 0x00000008,

    /// <summary>
    ///  Retrieves or sets the fType and dwTypeData members.
    ///  MIIM_TYPE is replaced by MIIM_BITMAP, MIIM_FTYPE, and MIIM_STRING. (MIIM_TYPE)
    /// </summary>
    Type = 0x00000010,

    /// <summary>
    ///  Item data. (MIIM_DATA)
    /// </summary>
    Data = 0x00000020,

    /// <summary>
    ///  (MIIM_STRING)
    /// </summary>
    String = 0x00000040,

    /// <summary>
    ///  (MIIM_BITMAP)
    /// </summary>
    Bitmap = 0x00000080,

    /// <summary>
    ///  The item type (fType) member. (MIIM_FTYPE)
    /// </summary>
    ItemType = 0x00000100
}