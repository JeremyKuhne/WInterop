// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization.Types
{
    /// <summary>
    /// Defines the type and size of an access control entry (ACE).
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa374919.aspx"/>
    /// </remarks>
    public struct ACE_HEADER
    {
        public AceType AceType;
        public AceInheritance AceFlags;
        public short AceSize;
    }
}
