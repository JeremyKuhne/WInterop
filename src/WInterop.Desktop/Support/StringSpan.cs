// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Support
{
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
            => _string != null || (!_span.IsEmpty && _span[_span.Length - 1] == '\0');

        public ReadOnlySpan<char> GetSpanWithoutTerminator()
        {
            if (_string != null)
                return _string.AsSpan();

            if (IsNullTerminated)
                return _span.Slice(0, _span.Length - 1);

            return _span;
        }

        public static implicit operator StringSpan(string value) => new StringSpan(value);
        public static implicit operator StringSpan(ReadOnlySpan<char> span) => new StringSpan(span);
        public static implicit operator StringSpan(Span<char> span) => new StringSpan(span);
        public static implicit operator StringSpan(char[] buffer) => new StringSpan(buffer);

        public override string ToString()
        {
            if (_string != null)
                return _string;

            return GetSpanWithoutTerminator().ToString();
        }
    }
}
