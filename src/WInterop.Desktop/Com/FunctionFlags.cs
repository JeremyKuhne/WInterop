// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com
{
    /// <summary>
    ///  [FUNCFLAGS]
    ///  <see cref="https://docs.microsoft.com/en-us/windows/win32/api/oaidl/ne-oaidl-funcflags"/>
    /// </summary>
    [Flags]
    public enum FunctionFlags : ushort
    {
        /// <summary>
        ///  [FUNCFLAG_FRESTRICTED]
        /// </summary>
        Restricted = 0x1,

        /// <summary>
        ///  Returns an object that is a source of events. [FUNCFLAG_FSOURCE]
        /// </summary>
        Source = 0x2,

        /// <summary>
        ///  Supports data binding. [FUNCFLAG_FBINDABLE]
        /// </summary>
        Bindable = 0x4,

        /// <summary>
        ///  Calls invoke IPropertySink::OnRequestEdit. [FUNCFLAG_FREQUESTEDIT]
        /// </summary>
        RequestEdit = 0x8,

        /// <summary>
        ///  Display to the user as bindable. [FUNCFLAG_FDISPLAYBIND]
        /// </summary>
        DisplayBindable = 0x10,

        /// <summary>
        ///  [FUNCFLAG_FDEFAULTBIND]
        /// </summary>
        DefaultBinding = 0x20,

        /// <summary>
        ///  [FUNCFLAG_FHIDDEN]
        /// </summary>
        Hidden = 0x40,

        /// <summary>
        ///  Function supports GetLastError. [FUNCFLAG_FUSESGETLASTERROR]
        /// </summary>
        UseGetLastError = 0x80,

        /// <summary>
        ///  VB optimization. [FUNCFLAG_FDEFAULTCOLLELEM]
        ///  <see cref="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/automat/defaultcollelem"/>
        /// </summary>
        DefaultCollElem = 0x100,

        /// <summary>
        ///  Default member for display. [FUNCFLAG_FUIDEFAULT]
        /// </summary>
        UIDefault = 0x200,

        /// <summary>
        ///  [FUNCFLAG_FNONBROWSABLE]
        /// </summary>
        NonBrowsable = 0x400,

        /// <summary>
        ///  [FUNCFLAG_FREPLACEABLE]
        /// </summary>
        Replaceable = 0x800,

        /// <summary>
        ///  [FUNCFLAG_FIMMEDIATEBIND]
        /// </summary>
        ImmediateBind = 0x1000
    }
}
