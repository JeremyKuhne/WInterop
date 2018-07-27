// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Modules.Native
{
    public readonly struct HMODULE
    {
        public IntPtr Handle { get; }

        public HMODULE(IntPtr handle)
        {
            Handle = handle;
        }

        public bool IsInvalid => Handle == IntPtr.Zero;

        public readonly struct Owned
        {
            public IntPtr Handle { get; }
        }
    }
}