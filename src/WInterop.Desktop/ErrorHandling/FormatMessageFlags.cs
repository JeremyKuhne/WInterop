// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Errors;

/// <summary>
///  Flags used when invoking FormatMessage().
/// </summary>
[Flags]
public enum FormatMessageFlags : uint
{
    /// <summary>
    ///  [FORMAT_MESSAGE_ALLOCATE_BUFFER]
    /// </summary>
    AllocateBuffer = 0x00000100,

    /// <summary>
    ///  [FORMAT_MESSAGE_IGNORE_INSERTS]
    /// </summary>
    IgnoreInserts = 0x00000200,

    /// <summary>
    ///  Source is a string. [FORMAT_MESSAGE_FROM_STRING]
    /// </summary>
    FromString = 0x00000400,

    /// <summary>
    ///  Source is a module handle [FORMAT_MESSAGE_FROM_HMODULE]
    /// </summary>
    FromHModule = 0x00000800,

    /// <summary>
    ///  [FORMAT_MESSAGE_FROM_SYSTEM]
    /// </summary>
    FromSystem = 0x00001000,

    /// <summary>
    ///  [FORMAT_MESSAGE_ARGUMENT_ARRAY]
    /// </summary>
    ArgumentArray = 0x00002000,

    /// <summary>
    ///  [FORMAT_MESSAGE_MAX_WIDTH_MASK]
    /// </summary>
    MaxWidthMask = 0x000000FF
}