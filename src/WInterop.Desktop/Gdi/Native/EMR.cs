// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Native
{
    /// <summary>
    ///  Enhanced metafile record.
    /// </summary>
    public readonly struct EMR
    {
        /// <summary>
        ///  Record type.
        /// </summary>
        public readonly MetafileRecordType iType;

        /// <summary>
        ///  Length of the record in bytes. Must be in multiples of 4.
        /// </summary>
        public readonly uint nSize;
    }
}
