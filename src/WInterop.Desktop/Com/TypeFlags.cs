// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com
{
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
        AppObject = 0x1,

        /// <summary>
        ///  Can create via <see cref="ITypeInfo.CreateInstance"/>. [TYPEFLAG_FCANCREATE]
        /// </summary>
        CanCreate = 0x2,

        /// <summary>
        ///  [TYPEFLAG_FLICENSED]
        /// </summary>
        Licensed = 0x4,

        /// <summary>
        ///  Predefined. [TYPEFLAG_FPREDECLID]
        /// </summary>
        Predeclared = 0x8,

        /// <summary>
        ///  Should not be displayed in type browsers. [TYPEFLAG_FHIDDEN]
        /// </summary>
        Hidden = 0x10,

        /// <summary>
        ///  [TYPEFLAG_FCONTROL]
        /// </summary>
        Control = 0x20,

        /// <summary>
        ///  Supplies both IDispatch and VTable binding. [TYPEFLAG_FDUAL]
        /// </summary>
        Dual = 0x40,

        /// <summary>
        ///  Cannot add members at run time. [TYPEFLAG_FNONEXTENSIBLE]
        /// </summary>
        NonExtensible = 0x80,

        /// <summary>
        ///  Fully compatible with Automation. [TYPEFLAG_FOLEAUTOMATION]
        /// </summary>
        OleAutomation = 0x100,

        /// <summary>
        ///  Should not be accessible from macro languages. [TYPEFLAG_FRESTRICTED]
        /// </summary>
        Restricted = 0x200,

        /// <summary>
        ///  Supports aggregation. [TYPEFLAG_FAGGREGATABLE]
        /// </summary>
        Aggregatable = 0x400,

        /// <summary>
        ///  [TYPEFLAG_FREPLACEABLE]
        /// </summary>
        Replacable = 0x800,

        /// <summary>
        ///  Derives from IDispatch. [TYPEFLAG_FDISPATCHABLE]
        /// </summary>
        Dispatchable = 0x1000,

        /// <summary>
        ///  [TYPEFLAG_FREVERSEBIND]
        /// </summary>
        ReverseBinding = 0x2000,

        /// <summary>
        ///  [TYPEFLAG_FPROXY]
        /// </summary>
        Proxy = 0x4000
    }
}
