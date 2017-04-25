// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;

namespace WInterop.Windows.Types
{
    public struct WPARAM
    {
        public UIntPtr RawValue;

        public ushort LowWord => Conversion.LowWord(RawValue);
        public ushort HighWord => Conversion.HighWord(RawValue);
    }
}
