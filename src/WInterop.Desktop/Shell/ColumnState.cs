// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell;

/// <summary>
///  [SHCOLSTATE]
/// </summary>
/// <msdn><see cref="https://docs.microsoft.com/en-us/windows/desktop/api/shtypes/ne-shtypes-tagshcolstate"/></msdn>
[Flags]
public enum ColumnState : uint
{
    Default = 0,
    TypeString = 0x1,
    TypeInt = 0x2,
    TypeDate = 0x3,
    TypeMask = 0xf,
    OnByDefault = 0x10,
    Slow = 0x20,
    Extended = 0x40,
    SecondaryUI = 0x80,
    Hidden = 0x100,
    PreferVarCmp = 0x200,
    PreferFmtCmp = 0x400,
    NoSortByFolderness = 0x800,
    ViewOnly = 0x10000,
    BatchRead = 0x20000,
    NoGroupBy = 0x40000,
    FixedWidth = 0x1000,
    NoDpiScale = 0x2000,
    FixedRation = 0x4000,
    DisplayMask = 0xf000
}