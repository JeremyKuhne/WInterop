// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security.Native
{
    /// <summary>
    ///  Defines an access control entry (ACE) for the system access control list
    ///  (SACL) that specifies the system resource attributes for a securable object.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/hh448534.aspx"/>
    /// </remarks>
    public struct SYSTEM_RESOURCE_ATTRIBUTE_ACE
    {
        public AceHeader Header;
        public AccessMask Mask;

        /// <summary>
        ///  First uint of the SID, the rest of the bytes follow.
        /// </summary>
        public uint SidStart;

        public unsafe SID* Sid
        {
            get { fixed (uint* u = &SidStart) { return (SID*)u; } }
        }
    }
}