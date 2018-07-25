// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi
{
    public readonly struct HBITMAP
    {
        public IntPtr Handle { get; }

        public HBITMAP(IntPtr handle)
        {
            Handle = handle;
        }

        public bool IsInvalid => Handle == IntPtr.Zero;

        public static implicit operator HGDIOBJ(HBITMAP handle) => new HGDIOBJ(handle.Handle);
    }
}
