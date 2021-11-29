// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Shell;

/// <summary>
///  [PROPDESC_VIEW_FLAGS]
/// </summary>
[Flags]
public enum PropertyViewFlags
{
    Default = 0,
    CenterAlign = 0x1,
    RightAlign = 0x2,
    BeginNewGroup = 0x4,
    FillArea = 0x8,
    SortDescending = 0x10,
    ShowOnlyIfPresent = 0x20,
    ShowByDefault = 0x40,
    ShowInPrimaryList = 0x80,
    ShowInSecondaryList = 0x100,
    HideLabel = 0x200,
    Hidden = 0x800,
    CanWrap = 0x1000,
}