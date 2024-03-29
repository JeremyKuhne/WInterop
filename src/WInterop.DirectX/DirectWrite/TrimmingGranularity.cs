﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  Text granularity used to trim text overflowing the layout box. [DWRITE_TRIMMING_GRANULARITY]
/// </summary>
public enum TrimmingGranularity : uint
{
    /// <summary>
    ///  No trimming occurs. Text flows beyond the layout width.
    /// </summary>
    None = DWRITE_TRIMMING_GRANULARITY.DWRITE_TRIMMING_GRANULARITY_NONE,

    /// <summary>
    ///  Trimming occurs at character cluster boundary.
    /// </summary>
    Character = DWRITE_TRIMMING_GRANULARITY.DWRITE_TRIMMING_GRANULARITY_CHARACTER,

    /// <summary>
    ///  Trimming occurs at word boundary.
    /// </summary>
    Word = DWRITE_TRIMMING_GRANULARITY.DWRITE_TRIMMING_GRANULARITY_WORD
}
