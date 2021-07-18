// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Gdi.Native
{
    // https://devblogs.microsoft.com/oldnewthing/20101018-00/?p=12513
    // https://devblogs.microsoft.com/oldnewthing/20120720-00/?p=7083
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct ICONDIR
    {
        /// <summary>
        ///  Reserved. Must be 0.
        /// </summary>
        public ushort idReserved;

        /// <summary>
        ///  Must be 1.
        /// </summary>
        public ushort idType;

        /// <summary>
        ///  Entry count.
        /// </summary>
        public ushort idCount;

        /// <summary>
        ///  First entry in ANYSIZE array of <see cref="idCount" />.
        /// </summary>
        public ICONDIRENTRY idEntries;
    }
}