// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.DynamicLinkLibrary.DataTypes
{
    [Flags]
    public enum GetModuleFlags : uint
    {
        GET_MODULE_HANDLE_EX_FLAG_PIN = 0x00000001,
        GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT = 0x00000002,
        GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS = 0x00000004
    }
}
