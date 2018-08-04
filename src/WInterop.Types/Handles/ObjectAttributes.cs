﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Handles.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff564586.aspx
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff547804.aspx
    // 
    [Flags]
    public enum ObjectAttributes : uint
    {
        /// <summary>
        /// This handle can be inherited by child processes of the current process.
        /// [OBJ_INHERIT]
        /// </summary>
        Inherit = 0x00000002,

        /// <summary>
        /// This flag only applies to objects that are named within the object manager.
        /// By default, such objects are deleted when all open handles to them are closed.
        /// If this flag is specified, the object is not deleted when all open handles are closed.
        /// [OBJ_PERMANENT]
        /// </summary>
        Permanent = 0x00000010,

        /// <summary>
        /// Only a single handle can be open for this object. [OBJ_EXCLUSIVE]
        /// </summary>
        Exclusive = 0x00000020,

        /// <summary>
        /// Lookups for this object should be case insensitive. [OBJ_CASE_INSENSITIVE]
        /// </summary>
        CaseInsensitive = 0x00000040,

        /// <summary>
        /// Create on existing object should open, not fail with STATUS_OBJECT_NAME_COLLISION.
        /// [OBJ_OPENIF]
        /// </summary>
        OpenIf = 0x00000080,

        /// <summary>
        /// Open the symbolic link, not its target. [OBJ_OPENLINK]
        /// </summary>
        OpenLink = 0x00000100,

        // Only accessible from kernel mode
        // OBJ_KERNEL_HANDLE

        // Access checks enforced, even in kernel mode
        // OBJ_FORCE_ACCESS_CHECK
        // OBJ_VALID_ATTRIBUTES = 0x000001F2
    }
}
