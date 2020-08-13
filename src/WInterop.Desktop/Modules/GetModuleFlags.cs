// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Modules
{
    [Flags]
    public enum GetModuleFlags : uint
    {
        Pin = 0x00000001,
        UnchangedRefCount = 0x00000002,
        FromAddress = 0x00000004
    }
}
