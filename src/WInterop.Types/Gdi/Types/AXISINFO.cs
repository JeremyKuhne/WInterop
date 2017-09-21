// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Gdi.Types
{
#pragma warning disable IDE1006 // Naming Styles
    // https://msdn.microsoft.com/en-us/library/dd183361.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct AXISINFO
    {
        public const int MM_MAX_AXES_NAMELEN = 16;

        // Unfortunately the compiler doesn't allow sizeof(AXISINFO) even though it doesn't change
        public const int AxisInfoSize = sizeof(int) * 2 + MM_MAX_AXES_NAMELEN * sizeof(char);

        public int axMinValue;
        public int axMaxValue;
        private FixedString.Size16 _axAxisName;
        public Span<char> axAxisName => _axAxisName.Buffer;
    }
#pragma warning restore IDE1006 // Naming Styles
}
