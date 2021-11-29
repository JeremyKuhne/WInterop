// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Support;

/// <summary>
///  Wrapper for a <see cref="string"/> or <see cref="ReadOnlySpan{char}"/>.
/// </summary>
/// <remarks>
///  Use where you would want to take a <see cref="ReadOnlySpan{char}"/> but
///  also want to call <see cref="ToString()"/> without allocating a string
///  copy when you had a string to begin with.
/// </remarks>
public readonly ref struct StringSpan
{
    private readonly ReadOnlySpan<char> _span;
    private readonly string? _string;

    public StringSpan(ReadOnlySpan<char> span)
    {
        _span = span;
        _string = null;
    }

    public StringSpan(string value)
    {
        _string = value;
        _span = default;
    }

    public bool IsEmpty
        => _span.IsEmpty && string.IsNullOrEmpty(_string);

    public bool IsNullTerminated
        => _string != null || (!_span.IsEmpty && _span[^1] == '\0');

    public ReadOnlySpan<char> GetSpanWithoutTerminator()
        => _string is not null
            ? _string.AsSpan()
            : IsNullTerminated ? _span[0..^1] : _span;

    public static implicit operator StringSpan(string value) => new StringSpan(value);
    public static implicit operator StringSpan(ReadOnlySpan<char> span) => new StringSpan(span);
    public static implicit operator StringSpan(Span<char> span) => new StringSpan(span);
    public static implicit operator StringSpan(char[] buffer) => new StringSpan(buffer);

    public override string ToString() => _string ?? GetSpanWithoutTerminator().ToString();
}