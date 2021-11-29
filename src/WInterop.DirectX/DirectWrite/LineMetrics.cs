// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  The <see cref="LineMetrics"/> structure contains information about a formatted
///  line of text. [DWRITE_LINE_METRICS]
/// </summary>
public readonly struct LineMetrics
{
    /// <summary>
    ///  The number of total text positions in the line.
    ///  This includes any trailing whitespace and newline characters.
    /// </summary>
    public readonly uint Length;

    /// <summary>
    ///  The number of whitespace positions at the end of the line.  Newline
    ///  sequences are considered whitespace.
    /// </summary>
    public readonly uint TrailingWhitespaceLength;

    /// <summary>
    ///  The number of characters in the newline sequence at the end of the line.
    ///  If the count is zero, then the line was either wrapped or it is the
    ///  end of the text.
    /// </summary>
    public readonly uint NewlineLength;

    /// <summary>
    ///  Height of the line as measured from top to bottom.
    /// </summary>
    public readonly float Height;

    /// <summary>
    ///  Distance from the top of the line to its baseline.
    /// </summary>
    public readonly float Baseline;

    /// <summary>
    ///  The line is trimmed.
    /// </summary>
    public readonly IntBoolean IsTrimmed;
};
