// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization
{
    /// <summary>
    /// Defines an access control entry (ACE) for the system access control list
    /// (SACL) that specifies the system resource attributes for a securable object.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/hh448534.aspx"/>
    /// </remarks>
    public struct SYSTEM_RESOURCE_ATTRIBUTE_ACE
    {
        public ACE_HEADER Header;
        public ACCESS_MASK Mask;
        public uint SidStart;
    }
}
