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

        public LPARAM(short high, short low)
        {
            RawValue = (IntPtr)Conversion.HighLowToInt(high, low);
        }

        public LPARAM(int high, int low)
        {
            RawValue = (IntPtr)Conversion.HighLowToInt(checked((short)high), checked((short)low));
        }

        public static implicit operator int(LPARAM value)
        {
            return (int)value.RawValue.ToInt64();
        }

        public static implicit operator LPARAM(int value)
        {
            return new LPARAM((IntPtr)value);
        }

        public static implicit operator LPARAM(IntPtr value)
        {
            return new LPARAM(value);
        }

        public static implicit operator IntPtr(LPARAM value)
        {
            return value.RawValue;
        }

        public override string ToString()
        {
            return RawValue.ToString();
        }
    }
}
