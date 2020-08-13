// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com
{
    /// <summary>
    ///  [PARAMFLAG]
    ///  <see cref="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/automat/paramflags"/>
    /// </summary>
    [Flags]
    public enum ParameterFlags : ushort
    {
        /// <summary>
        ///  [PARAMFLAG_NONE]
        /// </summary>
        None = 0x0,

        /// <summary>
        ///  [PARAMFLAG_FIN]
        /// </summary>
        In = 0x1,

        /// <summary>
        ///  [PARAMFLAG_FOUT]
        /// </summary>
        Out = 0x2,

        /// <summary>
        ///  [PARAMFLAG_FLCID]
        /// </summary>
        LocaleId = 0x4,

        /// <summary>
        ///  [PARAMFLAG_FRETVAL]
        /// </summary>
        ReturnValue = 0x8,

        /// <summary>
        ///  [PARAMFLAG_FOPT]
        /// </summary>
        Optional = 0x10,

        /// <summary>
        ///  [PARAMFLAG_FHASDEFAULT]
        /// </summary>
        HasDefault = 0x20,

        /// <summary>
        ///  [PARAMFLAG_FHASCUSTDATA]
        /// </summary>
        HasCustomData = 0x40
    }
}
