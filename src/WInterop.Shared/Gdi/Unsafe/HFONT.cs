// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Unsafe
{
    public readonly struct HFONT
    {
        public IntPtr Value { get; }

        public HFONT(IntPtr handle)
        {
            Value = handle;
        }

        public bool IsInvalid => Value == IntPtr.Zero;

        public static implicit operator HGDIOBJ(HFONT handle) => new HGDIOBJ(handle.Value);
        public static explicit operator HFONT(HGDIOBJ handle) => new HFONT(handle.Handle);
    }
}