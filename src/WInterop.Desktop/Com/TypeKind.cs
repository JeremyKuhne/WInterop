// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com
{
    /// <summary>
    ///  [TYPEKIND]
    /// </summary>
    public enum TypeKind : uint
    {
        /// <summary>
        ///  [TKIND_ENUM]
        /// </summary>
        Enum = 0,

        /// <summary>
        ///  [TKIND_RECORD]
        /// </summary>
        Record = (Enum + 1),

        /// <summary>
        ///  [TKIND_MODULE]
        /// </summary>
        Module = (Record + 1),

        /// <summary>
        ///  [TKIND_INTERFACE]
        /// </summary>
        Interface = (Module + 1),

        /// <summary>
        ///  [TKIND_DISPATCH]
        /// </summary>
        Dispatch = (Interface + 1),

        /// <summary>
        ///  [TKIND_COCLASS]
        /// </summary>
        CoClass = (Dispatch + 1),

        /// <summary>
        ///  [TKIND_ALIAS]
        /// </summary>
        Alias = (CoClass + 1),

        /// <summary>
        ///  [TKIND_UNION]
        /// </summary>
        Union = (Alias + 1),
    }
}
