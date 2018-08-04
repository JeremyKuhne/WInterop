// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Authorization
{
    /// <summary>
    /// Access control list (ACL) header.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa374931.aspx"/>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct ACL
    {
        /// <summary>
        /// Revision level of the ACL. Should be 2 (ACL_REVISION) or 4 (ACL_REVISION_DS)
        /// if it contains an ojbect-specific ACE.
        /// </summary>
        public byte AclRevision;

        // Padding
        private byte Sbz1;

        /// <summary>
        /// Size of the ACL in bytes. Includes the header and all access
        /// control entries (ACEs).
        /// </summary>
        public ushort AclSize;

        /// <summary>
        /// Number of access control entries (ACE) in the ACL.
        /// </summary>
        public ushort AceCount;

        // Padding
        private ushort Sbz2;
    }
}
