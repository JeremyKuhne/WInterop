// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Native
{
    public readonly struct HPALETTE
    {
        public IntPtr Value { get; }

        public HPALETTE(IntPtr handle) => Value = handle;

        public bool IsInvalid => Value == IntPtr.Zero;

        public static implicit operator HGDIOBJ(HPALETTE handle) => new HGDIOBJ(handle.Value);
        public static explicit operator HPALETTE(HGDIOBJ handle) => new HPALETTE(handle.Handle);

        public override bool Equals(object? obj) => obj is HPALETTE other && other.Value == Value;
        public bool Equals(HPALETTE other) => other.Value == Value;
        public static bool operator ==(HPALETTE a, HPALETTE b) => a.Value == b.Value;
        public static bool operator !=(HPALETTE a, HPALETTE b) => a.Value != b.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}