// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Native
{
    public readonly struct HBRUSH
    {
        public IntPtr Value { get; }

        public HBRUSH(IntPtr handle)
        {
            Value = handle;
        }

        public bool IsInvalid => Value == IntPtr.Zero;

        public static implicit operator HGDIOBJ(HBRUSH handle) => new HGDIOBJ(handle.Value);
        public static explicit operator HBRUSH(HGDIOBJ handle) => new HBRUSH(handle.Handle);

        public override bool Equals(object? obj) => obj is HBRUSH other ? other.Value == Value : false;
        public bool Equals(HBRUSH other) => other.Value == Value;
        public static bool operator ==(HBRUSH a, HBRUSH b) => a.Value == b.Value;
        public static bool operator !=(HBRUSH a, HBRUSH b) => a.Value != b.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}