// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi;

// https://msdn.microsoft.com/en-us/library/dd162741.aspx
[Flags]
public enum TextMetricFlags : uint
{
    Italic = 0x00000001,
    Bold = 0x00000020,
    Regular = 0x00000040,
    NonNegativeAC = 0x00010000,
    PostScript = 0x00020000,
    TrueType = 0x00040000,
    MultipleMaster = 0x00080000,
    Type1 = 0x00100000,
    DigitalSignature = 0x00200000
}