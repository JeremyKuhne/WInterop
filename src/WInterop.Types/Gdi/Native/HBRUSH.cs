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
        public IntPtr Handle { get; }

        public HBRUSH(IntPtr handle)
        {
            Handle = handle;
        }

        public bool IsInvalid => Handle == IntPtr.Zero;

        public static implicit operator HGDIOBJ(HBRUSH handle) => new HGDIOBJ(handle.Handle);
        public static explicit operator HBRUSH(HGDIOBJ handle) => new HBRUSH(handle.Handle);
    }
}