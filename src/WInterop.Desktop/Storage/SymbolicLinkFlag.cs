// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <native>[SYMBOLIC_LINK_FLAG]</native>
    /// <docs>https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-createsymboliclinkw</docs>
    public enum SymbolicLinkFlag : uint
    {
        /// <native>[SYMBOLIC_LINK_FLAG_FILE]</native>
        File = 0x0,

        /// <native>[SYMBOLIC_LINK_FLAG_DIRECTORY]</native>
        Directory = 0x1
    }
}
