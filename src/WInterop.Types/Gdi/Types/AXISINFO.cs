// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd183361.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct AXISINFO
    {
        public const int MM_MAX_AXES_NAMELEN = 16;

        // Unfortunately the compiler doesn't allow sizeof(AXISINFO) even though it doesn't change
        public const int AxisInfoSize = sizeof(int) * 2 + MM_MAX_AXES_NAMELEN * sizeof(char);

        public int axMinValue;
        public int axMaxValue;
        public FixedString.Size16 axAxisName;
    }
}
