// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    public struct TimerId
    {
        public static TimerId Null => new TimerId();

        public IntPtr RawValue;

        public TimerId(int id)
        {
            RawValue = (IntPtr)id;
        }

        public override bool Equals(object obj)
        {
            return obj is TimerId other
                ? other.RawValue == RawValue
                : false;
        }

        public bool Equals(TimerId other) => other.RawValue == RawValue;

        public override int GetHashCode() => RawValue.GetHashCode();

        public static bool operator ==(TimerId a, TimerId b) => a.RawValue == b.RawValue;

        public static bool operator !=(TimerId a, TimerId b) => a.RawValue != b.RawValue;

        public static implicit operator TimerId(int value) => new TimerId(value);
    }
}
