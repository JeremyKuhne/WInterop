// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Gdi.Types
{
#pragma warning disable IDE1006
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct ENUMLOGFONTEX
    {
        public const int LF_FULLFACESIZE = 64;
        public const int LF_FACESIZE = 32;

        public LOGFONT elfLogFont;
        private FixedString.Size64 _elfFullName;
        private FixedString.Size32 _elfStyle;
        private FixedString.Size32 _elfScript;
        public Span<char> elfFullName => _elfFullName.Buffer;
        public Span<char> elfStyle => _elfStyle.Buffer;
        public Span<char> elfScript => _elfScript.Buffer;
    }
#pragma warning restore IDE1006
}
