﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Gdi;

/// <summary>
///  [LOGFONT]
/// </summary>
/// <docs>
///  https://docs.microsoft.com/windows/win32/api/wingdi/ns-wingdi-logfontw
/// </docs>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct LogicalFont
{
    private const int LF_FACESIZE = 32;

    public int Height;
    public int Width;
    public int Escapement;
    public int Orientation;
    public FontWeight Weight;
    public ByteBoolean Italic;
    public ByteBoolean Underline;
    public ByteBoolean StrikeOut;
    public CharacterSet CharacterSet;
    public OutputPrecision OutputPrecision;
    public ClippingPrecision ClippingPrecision;
    public FontQuality Quality;
    public PitchAndFamily PitchAndFamily;

    private FixedString.Size32 _lfFaceName;
    public Span<char> FaceName => _lfFaceName.Buffer;
}