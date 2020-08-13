﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.SystemInformation
{
    public enum VersionTypeMask : uint
    {
        VER_MINORVERSION               = 0x0000001,
        VER_MAJORVERSION               = 0x0000002,
        VER_BUILDNUMBER                = 0x0000004,
        VER_PLATFORMID                 = 0x0000008,
        VER_SERVICEPACKMINOR           = 0x0000010,
        VER_SERVICEPACKMAJOR           = 0x0000020,
        VER_SUITENAME                  = 0x0000040,
        VER_PRODUCT_TYPE               = 0x0000080,
    }
}
