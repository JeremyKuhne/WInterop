// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Gdi
{
    /// <summary>
    ///  [DESIGNVECTOR]
    /// </summary>
    /// <remarks><see cref="https://msdn.microsoft.com/en-us/library/dd183551.aspx"/></remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct DesignVector
    {
        private const uint STAMP_DESIGNVECTOR = 0x8000000 + 'd' + ('v' << 8);
        private const int MM_MAX_NUMAXES = 16;

        private readonly uint dvReserved;
        private readonly uint dvNumAxes;
        private FixedInt.Size16 dvValues;

        public DesignVector(ReadOnlySpan<int> values)
        {
            dvReserved = STAMP_DESIGNVECTOR;
            dvNumAxes = (uint)values.Length;
            values.CopyTo(dvValues.Buffer);
        }

        public ReadOnlySpan<int> Values => dvValues.Buffer.Slice(0, (int)dvNumAxes);
    }
}