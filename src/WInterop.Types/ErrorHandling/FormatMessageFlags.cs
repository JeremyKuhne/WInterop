// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.ErrorHandling.Types
{
    /// <summary>
    /// Flags used when invoking FormatMessage().
    /// </summary>
    [Flags]
    public enum FormatMessageFlags : uint
    {
        // This isn't available on Win8 Store apps
        FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100,
        FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200,

        // lpSource is a string
        FORMAT_MESSAGE_FROM_STRING = 0x00000400,

        // lpSource is an HMODULE
        FORMAT_MESSAGE_FROM_HMODULE = 0x00000800,
        FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000,
        FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000,

        // Alternatively < FF means max characters
        // 00 means use hard coded line breaks (%n)
        FORMAT_MESSAGE_MAX_WIDTH_MASK = 0x000000FF
    }
}
