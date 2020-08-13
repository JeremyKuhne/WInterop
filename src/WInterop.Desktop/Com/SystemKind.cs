// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com
{
    /// <summary>
    ///  [SYSKIND]
    /// </summary>
    public enum SystemKind
    {
        /// <summary>
        ///  16 bit Windows. [SYS_WIN16]
        /// </summary>
        Win16 = 0,

        /// <summary>
        ///  32 bit Windows. [SYS_WIN32]
        /// </summary>
        Win32 = Win16 + 1,

        /// <summary>
        ///  Macintosh. [SYS_MAC]
        /// </summary>
        Mac = Win32 + 1,

        /// <summary>
        ///  64 bit Windows. [SYS_WIN64]
        /// </summary>
        Win64 = Mac + 1
    }
}
