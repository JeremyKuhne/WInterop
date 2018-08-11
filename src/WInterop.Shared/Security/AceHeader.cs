// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    /// Defines the type and size of an access control entry (ACE). [ACE_HEADER]
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa374919.aspx"/>
    /// </remarks>
    public readonly struct AceHeader
    {
        public readonly AceType AceType;
        public readonly AceInheritance AceFlags;
        public readonly short AceSize;
    }
}
