// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// [SYMBOLIC_LINK_FLAG]
    /// </summary>
    /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-createsymboliclinkw"/></msdn>
    public enum SymbolicLinkFlag : uint
    {
        /// <summary>
        /// [SYMBOLIC_LINK_FLAG_FILE]
        /// </summary>
        File = 0x0,

        /// <summary>
        /// [SYMBOLIC_LINK_FLAG_DIRECTORY]
        /// </summary>
        Directory = 0x1
    }
}
