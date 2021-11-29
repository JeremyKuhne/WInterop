// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Gdi;

/// <summary>
///  [DESIGNVECTOR]
/// </summary>
/// <remarks><see cref="https://msdn.microsoft.com/en-us/library/dd183551.aspx"/></remarks>
[StructLayout(LayoutKind.Sequential)]
public struct DesignVector
{
    private const uint STAMP_DESIGNVECTOR = 0x8000000 + 'd' + ('v' << 8);
    private const int MM_MAX_NUMAXES = 16;

    private readonly uint _dvReserved;
    private readonly uint _dvNumAxes;
    private FixedInt.Size16 _dvValues;

    public DesignVector(ReadOnlySpan<int> values)
    {
        _dvReserved = STAMP_DESIGNVECTOR;
        _dvNumAxes = (uint)values.Length;
        values.CopyTo(_dvValues.Buffer);
    }

    public ReadOnlySpan<int> Values => _dvValues.Buffer[.. (int)_dvNumAxes];
}