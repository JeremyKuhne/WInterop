// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Support;

namespace WInterop.Gdi.Types
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
        public unsafe fixed char lfFaceName[LF_FACESIZE];

        public unsafe string FaceName
        {
            get
            {
                fixed (char* c = lfFaceName)
                    return new string(c);
            }
            set
            {
                fixed (char* c = lfFaceName)
                    Strings.StringToBuffer(value, c, LF_FACESIZE - 1);
            }
        }

        public static unsafe void Foo (char* c)
        {
        }
    }
}
