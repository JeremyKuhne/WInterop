// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Native
{
    public readonly struct HMONITOR
    {
        public IntPtr Value { get; }

        public HMONITOR(IntPtr handle) => Value = handle;

        public bool IsInvalid => Value == IntPtr.Zero || Value == (IntPtr)(-1);

        public override bool Equals(object? obj) => obj is HMONITOR other ? other.Value == Value : false;
        public bool Equals(HMONITOR other) => other.Value == Value;
        public static bool operator ==(HMONITOR a, HMONITOR b) => a.Value == b.Value;
        public static bool operator !=(HMONITOR a, HMONITOR b) => a.Value != b.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}
