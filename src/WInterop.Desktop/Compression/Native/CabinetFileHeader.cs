// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Compression;

/// <summary>
///  Cabinet file header. [CFHEADER]
/// </summary>
/// <docs>https://docs.microsoft.com/en-us/previous-versions/bb417343(v=msdn.10)#cfheader</docs>
public unsafe struct CabinetFileHeader
{
    /// <summary>
    ///  Signature, should be "MSCF" (0x4D, 0x53, 0x43, 0x46)
    /// </summary>
    public fixed byte signature[4];
    public uint reserved1;

    /// <summary>
    ///  Size of this cabinet in bytes.
    /// </summary>
    public uint cbCabinet;

    public uint reserved2;

    /// <summary>
    ///  Offset of the first CFFILE entry.
    /// </summary>
    public uint coffFiles;

    public uint reserved3;

    /// <summary>
    ///  Cabinet file format version, minor.
    /// </summary>
    public byte versionMinor;

    /// <summary>
    ///  Cabinet file format version, major.
    /// </summary>
    public byte versionMajor;

    /// <summary>
    ///  Count of CFFOLDER entries in this cabinet.
    /// </summary>
    public ushort cFolders;

    /// <summary>
    ///  Count of CFFILE entries in this cabinet.
    /// </summary>
    public ushort cFiles;

    /// <summary>
    ///  Cabinet file options.
    /// </summary>
    public Options flags;

    /// <summary>
    ///  Cabinet set ID.
    /// </summary>
    public ushort setID;

    /// <summary>
    ///  Zero based index in the cabinet set.
    /// </summary>
    public ushort iCabinet;

    /// <summary>
    ///  (Optional) Size of per-cabinet reserved area.
    /// </summary>
    public ushort cbCFHeader;

    /// <summary>
    ///  (Optional) Size of per-folder reserved area.
    /// </summary>
    public byte cbCFFolder;

    /// <summary>
    ///  (Optional) Size of per-datablock reserved area.
    /// </summary>
    public byte cbCFData;

    // The following are defined as part of the header, but they are only present if the relevant options are set.
    //
    //    u1 abReserve[cbCFHeader];      /* (optional) per-cabinet reserved area */
    //    u1 szCabinetPrev[];            /* (optional) name of previous cabinet file */
    //    u1 szDiskPrev[];               /* (optional) name of previous disk */
    //    u1 szCabinetNext[];            /* (optional) name of next cabinet file */
    //    u1 szDiskNext[];               /* (optional) name of next disk */

    [Flags]
    public enum Options : ushort
    {
        /// <summary>
        ///  [cfhdrPREV_CABINET]
        /// </summary>
        PreviousCabinet = 0x001,

        /// <summary>
        ///  [cfhdrNEXT_CABINET]
        /// </summary>
        NextCabinet = 0x002,

        /// <summary>
        ///  cfhdrRESERVE_PRESENT
        /// </summary>
        ReservePresent = 0x004
    }
}