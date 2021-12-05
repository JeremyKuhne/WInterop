// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Gdi;

/// <summary>
///  [TEXTMETRIC]
/// </summary>
/// <docs>
///  https://docs.microsoft.com/windows/win32/api/wingdi/ns-wingdi-textmetricw
/// </docs>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct TextMetrics
{
    public int Height;
    public int Ascent;
    public int Descent;
    public int InternalLeading;
    public int ExternalLeading;
    public int AverageCharWidth;
    public int MaxCharWidth;
    public int Weight;
    public int Overhang;
    public int DigitizedAspectX;
    public int DigitizedAspectY;
    public char FirstChar;
    public char LastChar;
    public char DefaultChar;
    public char BreakChar;
    private ByteBoolean _italic;

    public bool Italic
    {
        get => _italic;
        set => _italic = value;
    }

    private ByteBoolean _underlined;

    public bool Underlined
    {
        get => _underlined;
        set => _underlined = value;
    }

    private ByteBoolean _struckOut;

    public bool StruckOut
    {
        get => _struckOut;
        set => _struckOut = value;
    }

    public PitchAndFamily PitchAndFamily;
    public CharacterSet CharacterSet;
}