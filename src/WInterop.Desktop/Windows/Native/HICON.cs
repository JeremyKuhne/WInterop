// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Native
{
    public readonly struct HICON
    {
        public IntPtr Value { get; }

        public HICON(IntPtr handle)
        {
            Value = handle;
        }

        public bool IsInvalid => Value == IntPtr.Zero;

        public override bool Equals(object? obj) => obj is HICON other ? other.Value == Value : false;
        public bool Equals(HICON other) => other.Value == Value;
        public static bool operator ==(HICON a, HICON b) => a.Value == b.Value;
        public static bool operator !=(HICON a, HICON b) => a.Value != b.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}
