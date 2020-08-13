// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    public struct MetafileRecord
    {
        public MetafileRecordType Type;

        /// <summary>
        ///  Record length in bytes. Must be a multiple of 4.
        /// </summary>
        public uint Size;
    }
}
