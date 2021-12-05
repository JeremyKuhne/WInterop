// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  [TYPEFLAGS]
/// </summary>
/// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/oaidl/ne-oaidl-typeflags"/>
[Flags]
public enum TypeFlags : ushort
{
    /// <summary>
    ///  Application object. [TYPEFLAG_FAPPOBJECT]
    /// </summary>
    AppObject = TYPEFLAGS.TYPEFLAG_FAPPOBJECT,

    /// <summary>
    ///  Can create via <see cref="ITypeInfo.CreateInstance"/>. [TYPEFLAG_FCANCREATE]
    /// </summary>
    CanCreate = TYPEFLAGS.TYPEFLAG_FCANCREATE,

    /// <summary>
    ///  [TYPEFLAG_FLICENSED]
    /// </summary>
    Licensed = TYPEFLAGS.TYPEFLAG_FLICENSED,

    /// <summary>
    ///  Predefined. [TYPEFLAG_FPREDECLID]
    /// </summary>
    Predeclared = TYPEFLAGS.TYPEFLAG_FPREDECLID,

    /// <summary>
    ///  Should not be displayed in type browsers. [TYPEFLAG_FHIDDEN]
    /// </summary>
    Hidden = TYPEFLAGS.TYPEFLAG_FHIDDEN,

    /// <summary>
    ///  [TYPEFLAG_FCONTROL]
    /// </summary>
    Control = TYPEFLAGS.TYPEFLAG_FCONTROL,

    /// <summary>
    ///  Supplies both IDispatch and VTable binding. [TYPEFLAG_FDUAL]
    /// </summary>
    Dual = TYPEFLAGS.TYPEFLAG_FDUAL,

    /// <summary>
    ///  Cannot add members at run time. [TYPEFLAG_FNONEXTENSIBLE]
    /// </summary>
    NonExtensible = TYPEFLAGS.TYPEFLAG_FNONEXTENSIBLE,

    /// <summary>
    ///  Fully compatible with Automation. [TYPEFLAG_FOLEAUTOMATION]
    /// </summary>
    OleAutomation = TYPEFLAGS.TYPEFLAG_FOLEAUTOMATION,

    /// <summary>
    ///  Should not be accessible from macro languages. [TYPEFLAG_FRESTRICTED]
    /// </summary>
    Restricted = TYPEFLAGS.TYPEFLAG_FRESTRICTED,

    /// <summary>
    ///  Supports aggregation. [TYPEFLAG_FAGGREGATABLE]
    /// </summary>
    Aggregatable = TYPEFLAGS.TYPEFLAG_FAGGREGATABLE,

    /// <summary>
    ///  [TYPEFLAG_FREPLACEABLE]
    /// </summary>
    Replacable = TYPEFLAGS.TYPEFLAG_FREPLACEABLE,

    /// <summary>
    ///  Derives from IDispatch. [TYPEFLAG_FDISPATCHABLE]
    /// </summary>
    Dispatchable = TYPEFLAGS.TYPEFLAG_FDISPATCHABLE,

    /// <summary>
    ///  [TYPEFLAG_FREVERSEBIND]
    /// </summary>
    ReverseBinding = TYPEFLAGS.TYPEFLAG_FREVERSEBIND,

    /// <summary>
    ///  [TYPEFLAG_FPROXY]
    /// </summary>
    Proxy = TYPEFLAGS.TYPEFLAG_FPROXY
}