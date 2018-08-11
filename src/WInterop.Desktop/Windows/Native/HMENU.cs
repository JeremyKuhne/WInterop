// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Native
{
    public readonly struct HMENU
    {
        public IntPtr Value { get; }

        public HMENU(IntPtr handle) => Value = handle;
        public bool IsInvalid => Value == IntPtr.Zero;

        public override bool Equals(object obj) => obj is HMENU other ? other.Value == Value : false;
        public bool Equals(HMENU other) => other.Value == Value;
        public static bool operator ==(HMENU a, HMENU b) => a.Value == b.Value;
        public static bool operator !=(HMENU a, HMENU b) => a.Value != b.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}
