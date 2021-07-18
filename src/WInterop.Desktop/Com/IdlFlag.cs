// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com
{
    [Flags]
    public enum IdlFlag : ushort
    {
        /// <summary>
        ///  [IDLFLAG_NONE]
        /// </summary>
        None = 0x0,

        /// <summary>
        ///  [IDLFLAG_FIN]
        /// </summary>
        In = 0x1,

        /// <summary>
        ///  [IDLFLAG_FOUT]
        /// </summary>
        Out = 0x2,

        /// <summary>
        ///  [IDLFLAG_FLCID]
        /// </summary>
        LocalIdentifer = 0x4,

        /// <summary>
        ///  [IDLFLAG_FRETVAL]
        /// </summary>
        ReturnValue = 0x8,
    }
}