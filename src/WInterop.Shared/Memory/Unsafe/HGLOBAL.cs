// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Memory.Native
{
    public readonly struct HGLOBAL
    {
        public IntPtr Value { get; }

        public HGLOBAL(IntPtr handle) => Value = handle;
    }
}
