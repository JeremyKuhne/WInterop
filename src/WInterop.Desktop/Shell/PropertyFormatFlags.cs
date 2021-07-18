// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    /// <summary>
    ///  [PROPDESC_FORMAT_FLAGS]
    /// </summary>
    [Flags]
    public enum PropertyFormatFlags
    {
        Default = 0,
        PrefixName = 0x1,
        FileName = 0x2,
        AlwaysKb = 0x4,
        ReservedRightToLeft = 0x8,
        ShortTime = 0x10,
        LongTime = 0x20,
        HideTime = 0x40,
        ShortDate = 0x80,
        LongDate = 0x100,
        HideDate = 0x200,
        RelativeDate = 0x400,
        UseEditInvitation = 0x800,
        ReadOnly = 0x1000,
        NoAutoReadingOrder = 0x2000
    }
}