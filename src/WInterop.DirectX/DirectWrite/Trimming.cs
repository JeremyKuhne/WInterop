// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  The <see cref="Trimming"/> structure specifies the trimming option for text overflowing the layout box.
///  [DWRITE_TRIMMING]
/// </summary>
public readonly struct Trimming
{
    /// <summary>
    ///  Text granularity of which trimming applies.
    /// </summary>
    public readonly TrimmingGranularity Granularity;

    /// <summary>
    ///  Character code used as the delimiter signaling the beginning of the portion of text to be preserved,
    ///  most useful for path ellipsis, where the delimiter would be a slash. Leave this zero if there is no
    ///  delimiter.
    /// </summary>
    public readonly uint Delimiter;

    /// <summary>
    ///  How many occurrences of the delimiter to step back. Leave this zero if there is no delimiter.
    /// </summary>
    public readonly uint DelimiterCount;
}
