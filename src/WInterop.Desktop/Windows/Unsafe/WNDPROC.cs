// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Unsafe
{
    public readonly struct WNDPROC
    {
        public readonly IntPtr Value;

        public WNDPROC(IntPtr value)
        {
            Value = value;
        }
    }
}
