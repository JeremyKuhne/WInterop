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
        public IntPtr Value { get; }

        public HMODULE(IntPtr handle)
        {
            Value = handle;
        }

        public bool IsInvalid => Value == IntPtr.Zero;
    }
}