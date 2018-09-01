// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    /// <summary>
    /// [PROPDESC_TYPE_FLAGS]
    /// </summary>
    [Flags]
    public enum PropertyTypeFlags : uint
    {
        Default = 0,
        MultipleValues = 0x1,
        IsInnate = 0x2,
        IsGroup = 0x4,
        CanGroupBy = 0x8,
        CanStackBy = 0x10,
        IsTreeProperty = 0x20,
        IncludeInFullTextQuery = 0x40,
        IsViewable = 0x80,
        IsQueryable = 0x100,
        CanBePurged = 0x200,
        SearchRawValue = 0x400,
        DontCoerceEmptyStrings = 0x800,
        AlwaysInSupplementalStore = 0x1000,
        IsSystemProperty = 0x80000000,
    }
}
