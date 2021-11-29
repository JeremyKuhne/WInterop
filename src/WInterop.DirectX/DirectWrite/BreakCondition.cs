// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  Condition at the edges of inline object or text used to determine
///  line-breaking behavior. [DWRITE_BREAK_CONDITION]
/// </summary>
public enum BreakCondition : uint
{
    /// <summary>
    ///  Whether a break is allowed is determined by the condition of the
    ///  neighboring text span or inline object.
    /// </summary>
    Neutral,

    /// <summary>
    ///  A break is allowed, unless overruled by the condition of the
    ///  neighboring text span or inline object, either prohibited by a
    ///  May Not or forced by a Must.
    /// </summary>
    CanBreak,

    /// <summary>
    ///  There should be no break, unless overruled by a Must condition from
    ///  the neighboring text span or inline object.
    /// </summary>
    MayNotBreak,

    /// <summary>
    ///  The break must happen, regardless of the condition of the adjacent
    ///  text span or inline object.
    /// </summary>
    MustBreak
}
