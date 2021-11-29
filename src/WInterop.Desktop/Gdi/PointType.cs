// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

// https://msdn.microsoft.com/en-us/library/dd162813.aspx
public enum PointType : byte
{
    // This is the actual definition, but the only flag so
    // we'll combine and create our own values for ease of use.
    // PT_CLOSEFIGURE     = 0x01,

    LineTo = 0x02,
    LineToAndClose = 0x03,
    BezierTo = 0x04,
    BezierToAndClose = 0x05,
    MoveTo = 0x06
}