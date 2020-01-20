// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Com.Native
{
    public struct ELEMDESC
    {
        public TYPEDESC tdesc;
        public UnionType Union;

        [StructLayout(LayoutKind.Explicit)]
        public unsafe struct UnionType
        {
            [FieldOffset(0)]
            public IdlDescription idldesc;

            [FieldOffset(0)]
            public PARAMDESC paramdesc;
        }
    }
}
