// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    /// <summary>
    /// <see cref="IShellFolder"/> name styles. [SHGDNF]
    /// </summary>
    /// <remarks>
    /// <see cref="https://docs.microsoft.com/en-us/windows/desktop/api/shobjidl_core/ne-shobjidl_core-_shgdnf"/>
    /// </remarks>
    [Flags]
    public enum DisplayNameFlags : uint
    {
        /// <summary>
        /// Parent relative name for display. [SHGDN_NORMAL]
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Parent relative name without extra disambiguation information. [SHGDN_INFOLDER]
        /// </summary>
        InFolder = 0x1,

        /// <summary>
        /// Name used for editing. [SHGDN_FOREDITING]
        /// </summary>
        ForEditing = 0x1000,

        /// <summary>
        /// Name for address bar combo box. [SHGDN_FORADDRESSBAR]
        /// </summary>
        ForAddressBar = 0x4000,

        /// <summary>
        /// Name for parsing. [SHGDN_FORPARSING]
        /// </summary>
        ForParsing = 0x8000
    }
}
