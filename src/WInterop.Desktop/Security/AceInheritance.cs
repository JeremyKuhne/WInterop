// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Security
{
    /// <summary>
    ///  Access control entry inheritance flags.
    /// </summary>
    /// <docs>https://docs.microsoft.com/windows/win32/api/securitybaseapi/nf-securitybaseapi-addaccessallowedaceex</docs>
    [Flags]
    public enum AceInheritance : byte
    {
        /// <summary>
        ///  ACE is inherited by noncontainer objects. [OBJECT_INHERIT_ACE]
        /// </summary>
        ObjectInherit = 0x1,

        /// <summary>
        ///  ACE is inherited by container objects. [CONTAINER_INHERIT_ACE]
        /// </summary>
        ContainerInnherit = 0x2,

        /// <summary>
        ///  <see cref="ObjectInherit"/> and <see cref="ContainerInnherit"/> flags are not propogated to an
        ///  inherited ACE. [NO_PROPAGATE_INHERIT_ACE]
        /// </summary>
        NoPropogateInherit = 0x4,

        /// <summary>
        ///  The ACE does not apply to the object to which the access control list (ACL) is assigned, but it can
        ///  be inherited by child objects. [INHERIT_ONLY_ACE]
        /// </summary>
        InheritOnly = 0x8,

        /// <summary>
        ///  Indicates an inherited ACE. [INHERITED_ACE]
        /// </summary>
        Inherited = 0x10
    }
}
