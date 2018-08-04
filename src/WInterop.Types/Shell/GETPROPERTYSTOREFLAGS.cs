// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    [Flags]
    public enum GETPROPERTYSTOREFLAGS : uint
    {
        DEFAULT = 0,
        HANDLERPROPERTIESONLY = 0x1,
        READWRITE = 0x2,
        TEMPORARY = 0x4,
        FASTPROPERTIESONLY = 0x8,
        OPENSLOWITEM = 0x10,
        DELAYCREATION = 0x20,
        BESTEFFORT = 0x40,
        NO_OPLOCK = 0x80,
        PREFERQUERYPROPERTIES = 0x100,
        EXTRINSICPROPERTIES = 0x200,
        EXTRINSICPROPERTIESONLY = 0x400,
        MASK_VALID = 0x7ff
    }
}
