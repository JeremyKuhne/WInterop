// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GdiPlus.EmfPlus
{
    /// <summary>
    ///  EMF+ Record.
    /// </summary>
    public struct MetafilePlusRecord
    {
        private TypeAndFlags _typeAndFlags;

        public RecordType Type
        {
            get => (RecordType)_typeAndFlags.Type;
            set => _typeAndFlags.Type = (short)value;
        }

        public ushort Flags
        {
            get => _typeAndFlags.Flags;
            set => _typeAndFlags.Flags = value;
        }

        /// <summary>
        ///  Record length in bytes. Must be a multiple of 4.
        /// </summary>
        public uint Size;

        /// <summary>
        ///  Size of the data following this header in bytes.
        /// </summary>
        public uint DataSize;

        private struct TypeAndFlags
        {
            public short Type;
            public ushort Flags;
        }
    }
}
