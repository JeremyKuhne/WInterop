// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    /// <summary>
    ///  [GETPROPERTYSTOREFLAGS]
    /// </summary>
    [Flags]
    public enum GetPropertyStoreFlags : uint
    {
        Default = 0,
        HandlePropertiesOnly = 0x1,
        ReadWrite = 0x2,
        Temporary = 0x4,
        FastPropertiesOnly = 0x8,
        OpenSlowItem = 0x10,
        DelayCreation = 0x20,
        BestEffort = 0x40,
        NoOplock = 0x80,
        PreferQueryProperties = 0x100,
        ExtrinsicProperties = 0x200,
        ExtrinsicPropertiesOnly = 0x400
    }
}
