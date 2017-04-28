// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;

namespace WInterop.Windows.Types
{
    public struct LPARAM
    {
        public IntPtr RawValue;

        public ushort LowWord => Conversion.LowWord(RawValue);
        public ushort HighWord => Conversion.HighWord(RawValue);

        public LPARAM(IntPtr value)
        {
            RawValue = value;
        }

        public static implicit operator int(LPARAM value)
        {
            return value.RawValue.ToInt32();
        }

        public static implicit operator LPARAM(uint value)
        {
            return new LPARAM((IntPtr)value);
        }
    }
}
