// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Security.Native
{
    [Flags]
    public enum AceTypePresent : uint
    {
        /// <summary>
        ///  [ACE_OBJECT_TYPE_PRESENT]
        /// </summary>
        Object = 0x1,

        /// <summary>
        ///  [ACE_INHERITED_OBJECT_TYPE_PRESENT]
        /// </summary>
        InheritedObject = 0x2
    }
}
