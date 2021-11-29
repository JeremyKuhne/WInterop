// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Gdi;

/// <summary>
///  [AXISINFO]
/// </summary>
// https://msdn.microsoft.com/en-us/library/dd183361.aspx
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct AxisInfo
{
    public int MinValue;
    public int MaxValue;

    private FixedString.Size16 _axAxisName;

    public Span<char> AxisName => _axAxisName.Buffer;
}