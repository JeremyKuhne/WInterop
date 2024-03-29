﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  Storage medium type. [TYMED]
/// </summary>
/// <docs>https://docs.microsoft.com/windows/win32/api/objidl/ne-objidl-tymed</docs>
[Flags]
public enum MediumType : uint
{
    /// <summary>
    ///  Data is a <see cref="HGLOBAL"/> memory handle. [TYMED_HGLOBAL]
    /// </summary>
    /// <remarks>
    ///  The handle should be freed via <see cref="Memory.Memory.GlobalFree"/>.
    /// </remarks>
    Memory = 1,

    /// <summary>
    ///  Data is a disk file path. [TYMED_FILE]
    /// </summary>
    File = 2,

    /// <summary>
    ///  Data is an<see cref="WInterop.Com.Stream"/>. [TYMED_ISTREAM]
    /// </summary>
    Stream = 4,

    /// <summary>
    ///  Data is a <see cref="StructuredStorage"/>. [TYMED_ISTORAGE]
    /// </summary>
    Storage = 8,

    /// <summary>
    ///  Data is an <see cref="HBITMAP"/> [TYMED_GDI]
    /// </summary>
    /// <remarks>
    ///  If the IUnknown handle is null, the handle should be freed via
    ///  <see cref="TerraFXWindows.DeleteObject(HGDIOBJ)"/>.
    /// </remarks>
    Gdi = 16,

    /// <summary>
    ///  [TYMED_MFPICT]
    /// </summary>
    Metafile = 32,

    /// <summary>
    ///  [TYMED_ENHMF]
    /// </summary>
    EnhancedMetafile = 64,

    /// <summary>
    ///  No data. [TYMED_NULL]
    /// </summary>
    Null = 0
}