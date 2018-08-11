// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Security
{
    [Flags]
    public enum Inheritance : uint
    {
        NoInheritance = 0x0,

        /// <summary>
        /// Non container objects inherit.
        /// [OBJECT_INHERIT_ACE][SUB_OBJECTS_ONLY_INHERIT]
        /// </summary>
        Objects = 0x1,

        /// <summary>
        /// Containers inherit. [CONTAINER_INHERIT_ACE][SUB_CONTAINERS_ONLY_INHERIT]
        /// </summary>
        Containers = 0x2,

        /// <summary>
        /// Rights propogate as specified, but the propogation flags themselves do not.
        /// See <see cref="Objects"/> and <see cref="Containers"/>.
        /// [NO_PROPAGATE_INHERIT_ACE][INHERIT_NO_PROPAGATE]
        /// </summary>
        NoPropogate = 0x4,

        /// <summary>
        /// Rights only apply to descendents. [INHERIT_ONLY_ACE][INHERIT_ONLY]
        /// </summary>
        InheritOnly = 0x8
    }
}
