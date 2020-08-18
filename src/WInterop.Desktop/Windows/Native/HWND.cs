// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;

namespace WInterop.Windows.Native
{
    [DebuggerDisplay("{Value}")]
    public readonly struct HWND
    {
        public nint Value { get; }

        public HWND(nint handle) => Value = handle;

        public bool IsInvalid => Value == 0 || Value == (-1);

        public override bool Equals(object? obj) => obj is HWND other && other.Value == Value;
        public bool Equals(HWND other) => other.Value == Value;
        public static bool operator ==(HWND a, HWND b) => a.Value == b.Value;
        public static bool operator !=(HWND a, HWND b) => a.Value != b.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}
