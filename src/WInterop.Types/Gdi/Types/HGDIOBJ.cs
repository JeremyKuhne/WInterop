// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    public readonly struct HGDIOBJ
    {
        public IntPtr Handle { get; }

        public HGDIOBJ(IntPtr handle)
        {
            Handle = handle;
        }

        public bool IsInvalid => Handle == IntPtr.Zero;
    }
}