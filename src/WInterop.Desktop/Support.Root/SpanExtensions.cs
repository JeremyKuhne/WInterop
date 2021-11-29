// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop;

public static class SpanExtensions
{
    /// <summary>
    ///  Slice the given <paramref name="span"/> at null, if present.
    /// </summary>
    public static ReadOnlySpan<char> SliceAtNull(this ReadOnlySpan<char> span)
    {
        int index = span.IndexOf('\0');
        return index == -1 ? span : span.Slice(0, index);
    }

    /// <summary>
    ///  Slice the given <paramref name="span"/> at null, if present.
    /// </summary>
    public static Span<char> SliceAtNull(this Span<char> span)
    {
        int index = span.IndexOf('\0');
        return index == -1 ? span : span.Slice(0, index);
    }
}