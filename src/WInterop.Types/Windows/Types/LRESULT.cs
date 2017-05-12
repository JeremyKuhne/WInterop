// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;

namespace WInterop.Windows.Types
{
    public struct LRESULT
    {
        public IntPtr RawValue;

        public ushort LowWord => Conversion.LowWord(RawValue);
        public ushort HighWord => Conversion.HighWord(RawValue);

        public LRESULT(IntPtr value) => RawValue = value;

        public static implicit operator LRESULT(int value) => new LRESULT((IntPtr)value);
        public static implicit operator LRESULT(IntPtr value) => new LRESULT(value);
        public static implicit operator IntPtr(LRESULT value) => value.RawValue;
    }
}
