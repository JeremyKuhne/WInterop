// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Gdi.Native
{
    /// <docs>https://docs.microsoft.com/windows/win32/api/wingdi/ns-wingdi-enhmetarecord</docs>
    [StructLayout(LayoutKind.Sequential)]
    public struct ENHMETARECORD
    {
        public uint iType;

        /// <summary>
        ///  Record size, in bytes.
        /// </summary>
        public uint nSize;

        /// <summary>
        ///  Parameters.
        /// </summary>
        private uint _dParm;

        public ReadOnlySpan<uint> dParam
            => TrailingArray<uint>.GetBuffer(
                ref _dParm,
                (nSize - sizeof(uint) - sizeof(uint)) / sizeof(uint));
    }
}
