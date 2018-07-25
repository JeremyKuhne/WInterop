// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    public readonly struct HPEN
    {
        public IntPtr Handle { get; }

        public HPEN(IntPtr handle)
        {
            Handle = handle;
        }

        public bool IsInvalid => Handle == IntPtr.Zero;

        public static implicit operator HGDIOBJ(HPEN handle) => new HGDIOBJ(handle.Handle);
    }
}