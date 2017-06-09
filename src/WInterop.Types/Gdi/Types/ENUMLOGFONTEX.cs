// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Support;

namespace WInterop.Gdi.Types
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct ENUMLOGFONTEX
    {
        public const int LF_FULLFACESIZE = 64;
        public const int LF_FACESIZE = 32;

        public LOGFONT elfLogFont;
        public FixedString.Size64 elfFullName;
        public FixedString.Size32 elfStyle;
        public FixedString.Size32 elfScript;
    }
}
