// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com;

/// <summary>
///  [LIBFLAGS]
/// </summary>
[Flags]
public enum LibraryFlags : ushort
{
    /// <summary>
    ///  Restricted and should not be displayed to users. [LIBFLAG_FRESTRICTED]
    /// </summary>
    Restricted = 0x1,

    /// <summary>
    ///  Library describes UI controls. [LIBFLAG_FCONTROL]
    /// </summary>
    Control = 0x2,

    /// <summary>
    ///  Hidden and should not be displayed to users. [LIBFLAG_FHIDDEN]
    /// </summary>
    Hidden = 0x4,

    /// <summary>
    ///  Library has a disk image. [LIBFLAG_FHASDISKIMAGE]
    /// </summary>
    HasDiskImage = 0x8
}