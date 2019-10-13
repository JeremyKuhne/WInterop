// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Native
{
    public readonly struct HWND
    {
        public IntPtr Value { get; }

        public HWND(IntPtr handle) => Value = handle;

        public bool IsInvalid => Value == IntPtr.Zero || Value == (IntPtr)(-1);

        public override bool Equals(object? obj) => obj is HWND other ? other.Value == Value : false;
        public bool Equals(HWND other) => other.Value == Value;
        public static bool operator ==(HWND a, HWND b) => a.Value == b.Value;
        public static bool operator !=(HWND a, HWND b) => a.Value != b.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}
