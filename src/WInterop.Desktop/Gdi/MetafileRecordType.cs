﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

public enum MetafileRecordType : uint
{
    Header = 1,
    PolyBezier = 2,
    Polygon = 3,
    Polyline = 4,
    PolyBezierTo = 5,
    PolylineTo = 6,
    PolyPolyline = 7,
    PolyPolygon = 8,
    SetWindowExtEx = 9,
    SetWindowOrgEx = 10,
    SetViewportExtEx = 11,
    SetViewportOrgEx = 12,
    SetBrushOrgEx = 13,
    Eof = 14,
    SetPixelV = 15,
    SetMapperFlags = 16,
    SetMapMode = 17,
    SetBkMode = 18,
    SetPolyFillMode = 19,
    SetROP2 = 20,
    SetStretchBltMode = 21,
    SetTextAlign = 22,
    SetColorAdjustment = 23,
    SetTextColor = 24,
    SetBkColor = 25,
    OffsetClipRgn = 26,
    MoveToEx = 27,
    SetMetaRgn = 28,
    ExcludeClipRect = 29,
    IntersectClipRect = 30,
    ScaleViewportExtEx = 31,
    ScaleWindowExtEx = 32,
    SaveDC = 33,
    RestoreDC = 34,
    SetWorldTransform = 35,
    ModifyWorldTransform = 36,
    SelectObject = 37,
    CreatePen = 38,
    CreateBrushIndirect = 39,
    DeleteObject = 40,
    AngleArc = 41,
    Ellipse = 42,
    Rectangle = 43,
    RoundRect = 44,
    Arc = 45,
    Chord = 46,
    Pie = 47,
    SelectPalette = 48,
    CreatePalette = 49,
    SetPaletteEntries = 50,
    ResizePalette = 51,
    RealizePalette = 52,
    ExtFloodFill = 53,
    LineTo = 54,
    ArcTo = 55,
    PolyDraw = 56,
    SetArcDirection = 57,
    SetMiterLimit = 58,
    BeginPath = 59,
    EndPath = 60,
    CloseFigure = 61,
    FillPath = 62,
    StrokeAndFillPath = 63,
    StrokePath = 64,
    FlattenPath = 65,
    WidenPath = 66,
    SelectClipPath = 67,
    AbortPath = 68,
    GdiComment = 70,
    FillRgn = 71,
    FrameRgn = 72,
    InvertRgn = 73,
    PaintRgn = 74,
    ExtSelectClipRgn = 75,
    BitBlt = 76,
    StretchBlt = 77,
    MaskBlt = 78,
    PlgBlt = 79,
    SetDIBitsToDevice = 80,
    StretchDIBits = 81,
    ExtCreateFontIndirectW = 82,
    ExtTextOutA = 83,
    ExtTextOutW = 84,
    PolyBezier16 = 85,
    Polygon16 = 86,
    Polyline16 = 87,
    PolyBezierTo16 = 88,
    PolylineTo16 = 89,
    PolyPolyline16 = 90,
    PolyPolygon16 = 91,
    PolyDraw16 = 92,
    CreateMonoBrush = 93,
    CreateDIBPatternBrushPt = 94,
    ExtCreatePen = 95,
    PolyTextOutA = 96,
    PolyTextOutW = 97,
    SetICMMode = 98,
    Createcolorspace = 99,
    SetColorSpace = 100,
    DeleteColorSpace = 101,
    GLSRecord = 102,
    GLSBoundedRecord = 103,
    PixelFormat = 104,
    ColorCorrectPalette = 111,
    SetICMProfileA = 112,
    SetICMProfileW = 113,
    AlphaBlend = 114,
    SetLayout = 115,
    TransparentBlt = 116,
    GradientFill = 118,
    ColorMatchToTargetW = 121,
    CreateColorSpaceW = 122
}