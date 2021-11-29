// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Native;

// https://msdn.microsoft.com/en-us/library/dd162711.aspx
public struct EXTLOGPEN
{
    public uint elpPenStyle;
    public uint elpWidth;
    public BrushStyle elpBrushStyle;
    public COLORREF elpColor;
    public UIntPtr elpHatch;
    public uint elpNumEntries;
    public uint elpStyleEntry;
}