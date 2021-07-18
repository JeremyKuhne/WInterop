// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security.Native
{
    /// <summary>
    ///  Defines access allowed for a specific trustee identified by the specified SID.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa374847.aspx"/>
    /// </remarks>
    public struct ACCESS_ALLOWED_ACE
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