// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Security;

namespace WInterop.Handles
{
    [Flags]
    public enum SymbolicLinkObjectRights : uint
    {
        SYMBOLIC_LINK_QUERY = 0x0001,
        SYMBOLIC_LINK_SET = 0x0002,
        SYMBOLIC_LINK_ALL_ACCESS = StandardAccessRights.Required | SYMBOLIC_LINK_QUERY,

        /// <summary>
        /// Maps to STANDARD_RIGHTS_READ | SYMBOLIC_LINK_QUERY.
        /// </summary>
        GENERIC_READ = GenericAccessRights.Read,

        /// <summary>
        /// Maps to STANDARD_RIGHTS_WRITE.
        /// </summary>
        GENERIC_WRITE = GenericAccessRights.Write,

        /// <summary>
        /// Maps to STANDARD_RIGHTS_EXECUTE | SYMBOLIC_LINK_QUERY.
        /// </summary>
        GENERIC_EXECUTE = GenericAccessRights.Execute,

        /// <summary>
        /// Maps to SYMBOLIC_LINK_ALL_ACCESS.
        /// </summary>
        GENERIC_ALL = GenericAccessRights.All
    }
}
