// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Gdi;

/// <summary>
///  [AXESLIST]
/// </summary>
/// <docs>https://docs.microsoft.com/windows/win32/api/wingdi/ns-wingdi-axeslistw</docs>
[StructLayout(LayoutKind.Sequential)]
public struct AxesList
{
    public const uint STAMP_AXESLIST = 0x8000000 + 'a' + ('l' << 8);
    private const int MM_MAX_NUMAXES = 16;

    public uint Reserved;
    public uint NumberOfAxes;

    // Unfortunately the compiler doesn't allow sizeof(AXISINFO) even though it doesn't change
    private unsafe fixed byte _axlAxisInfo[MM_MAX_NUMAXES * ((sizeof(int) * 2) + (16 * sizeof(char)))];

    public unsafe Span<AxisInfo> AxisInfo
    {
        get
        {
            fixed (void* v = _axlAxisInfo)
            {
                return new Span<AxisInfo>(v, MM_MAX_NUMAXES);
            }
        }
    }
}