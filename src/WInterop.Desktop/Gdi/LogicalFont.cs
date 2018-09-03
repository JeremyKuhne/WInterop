// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Gdi
{
    /// <summary>
    /// [LOGFONT]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/dd145037.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct LogicalFont
    {
        private const int LF_FACESIZE = 32;

        public int Height;
        public int Width;
        public int Escapement;
        public int Orientation;
        public int Weight;
        public Boolean8 Italic;
        public Boolean8 Underline;
        public Boolean8 StrikeOut;
        public CharacterSet CharacterSet;
        public OutputPrecision OutputPrecision;
        public ClippingPrecision ClippingPrecision;
        public Quality Quality;
        public PitchAndFamily PitchAndFamily;

        private FixedString.Size32 _lfFaceName;
        public Span<char> lfFaceName => _lfFaceName.Buffer;
    }
}
