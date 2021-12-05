// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  [VARFLAGS]
/// </summary>
/// <docs>
///  <see cref="https://docs.microsoft.com/windows/win32/api/oaidl/ne-oaidl-varflags"/>
/// </docs>
[Flags]
public enum VariableFlags : ushort
{
    /// <summary>
    ///  [VARFLAG_FREADONLY]
    /// </summary>
    ReadOnly = VARFLAGS.VARFLAG_FREADONLY,

    /// <summary>
    ///  [VARFLAG_FSOURCE]
    /// </summary>
    Source = VARFLAGS.VARFLAG_FSOURCE,

    /// <summary>
    ///  [VARFLAG_FBINDABLE]
    /// </summary>
    Bindable = VARFLAGS.VARFLAG_FBINDABLE,

    /// <summary>
    ///  [VARFLAG_FREQUESTEDIT]
    /// </summary>
    RequestEdit = VARFLAGS.VARFLAG_FREQUESTEDIT,

    /// <summary>
    ///  [VARFLAG_FDISPLAYBIND]
    /// </summary>
    DisplayBindable = VARFLAGS.VARFLAG_FDISPLAYBIND,

    /// <summary>
    ///  [VARFLAG_FDEFAULTBIND]
    /// </summary>
    DefaultBinding = VARFLAGS.VARFLAG_FDEFAULTBIND,

    /// <summary>
    ///  [VARFLAG_FHIDDEN]
    /// </summary>
    Hidden = VARFLAGS.VARFLAG_FHIDDEN,

    /// <summary>
    ///  [VARFLAG_FRESTRICTED]
    /// </summary>
    Restricted = VARFLAGS.VARFLAG_FRESTRICTED,

    /// <summary>
    ///  [VARFLAG_FDEFAULTCOLLELEM]
    /// </summary>
    DefaultCollElem = VARFLAGS.VARFLAG_FDEFAULTCOLLELEM,

    /// <summary>
    ///  [VARFLAG_FUIDEFAULT]
    /// </summary>
    UIDefault = VARFLAGS.VARFLAG_FUIDEFAULT,

    /// <summary>
    ///  [VARFLAG_FNONBROWSABLE]
    /// </summary>
    NonBrowsable = VARFLAGS.VARFLAG_FNONBROWSABLE,

    /// <summary>
    ///  [VARFLAG_FREPLACEABLE]
    /// </summary>
    Replaceable = VARFLAGS.VARFLAG_FREPLACEABLE,

    /// <summary>
    ///  [VARFLAG_FIMMEDIATEBIND]
    /// </summary>
    ImmediateBind = VARFLAGS.VARFLAG_FIMMEDIATEBIND
}