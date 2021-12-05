// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;
using WInterop.Gdi;

namespace WInterop.GdiPlus.EmfPlus;

/// <summary>
///  EMF+ Record.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly ref struct MetafilePlusRecord
{
    private readonly TypeAndFlags _typeAndFlags;

    public RecordType Type => (RecordType)_typeAndFlags.Type;

    public ushort Flags => _typeAndFlags.Flags;

    /// <summary>
    ///  Record length in bytes. Must be a multiple of 4.
    /// </summary>
    public readonly uint Size;

    /// <summary>
    ///  Size of the data following this header in bytes.
    /// </summary>
    public readonly uint DataSize;

    [StructLayout(LayoutKind.Sequential)]
    private readonly struct TypeAndFlags
    {
        public readonly short Type;
        public readonly ushort Flags;
    }

    public static unsafe MetafilePlusRecord* GetFromMetafileComment(EMRGDICOMMENT* comment)
    {
        if (comment->emr.iType != (uint)MetafileRecordType.GdiComment)
        {
            throw new ArgumentException("Does not have the proper record type.", nameof(comment));
        }

        uint recordBytes = comment->emr.nSize;
        uint dataBytes = comment->cbData;

        // 4 for the uint signature below

        return dataBytes < sizeof(MetafilePlusRecord) + 4 || *(uint*)&comment->Data != Metafile.EMFPlusSignature
            ? (MetafilePlusRecord*)null
            : (MetafilePlusRecord*)(&comment->Data + sizeof(uint));
    }
}
