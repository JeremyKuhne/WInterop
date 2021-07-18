// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com
{
    /// <summary>
    ///  [VARFLAGS]
    ///  <see cref="https://docs.microsoft.com/en-us/windows/win32/api/oaidl/ne-oaidl-varflags"/>
    /// </summary>
    [Flags]
    public enum VariableFlags : ushort
    {
        /// <summary>
        ///  [VARFLAG_FREADONLY]
        /// </summary>
        ReadOnly = 0x1,

        /// <summary>
        ///  [VARFLAG_FSOURCE]
        /// </summary>
        Source = 0x2,

        /// <summary>
        ///  [VARFLAG_FBINDABLE]
        /// </summary>
        Bindable = 0x4,

        /// <summary>
        ///  [VARFLAG_FREQUESTEDIT]
        /// </summary>
        RequestEdit = 0x8,

        /// <summary>
        ///  [VARFLAG_FDISPLAYBIND]
        /// </summary>
        DisplayBindable = 0x10,

        /// <summary>
        ///  [VARFLAG_FDEFAULTBIND]
        /// </summary>
        DefaultBinding = 0x20,

        /// <summary>
        ///  [VARFLAG_FHIDDEN]
        /// </summary>
        Hidden = 0x40,

        /// <summary>
        ///  [VARFLAG_FRESTRICTED]
        /// </summary>
        Restricted = 0x80,

        /// <summary>
        ///  [VARFLAG_FDEFAULTCOLLELEM]
        /// </summary>
        DefaultCollElem = 0x100,

        /// <summary>
        ///  [VARFLAG_FUIDEFAULT]
        /// </summary>
        UIDefault = 0x200,

        /// <summary>
        ///  [VARFLAG_FNONBROWSABLE]
        /// </summary>
        NonBrowsable = 0x400,

        /// <summary>
        ///  [VARFLAG_FREPLACEABLE]
        /// </summary>
        Replaceable = 0x800,

        /// <summary>
        ///  [VARFLAG_FIMMEDIATEBIND]
        /// </summary>
        ImmediateBind = 0x1000
    }
}