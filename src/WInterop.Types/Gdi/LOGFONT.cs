// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd145037.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct LOGFONT
    {
        private const int LF_FACESIZE = 32;

        public int lfHeight;
        public int lfWidth;
        public int lfEscapement;
        public int lfOrientation;
        public int lfWeight;
        public BOOLEAN lfItalic;
        public BOOLEAN lfUnderline;
        public BOOLEAN lfStrikeOut;
        public CharacterSet lfCharSet;
        public OutputPrecision lfOutPrecision;
        public ClippingPrecision lfClipPrecision;
        public Quality lfQuality;
        public PitchAndFamily lfPitchAndFamily;
        private FixedString.Size32 _lfFaceName;
        public Span<char> lfFaceName => _lfFaceName.Buffer;
    }
}
