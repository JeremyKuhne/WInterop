// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  Describes how present should behave. [D2D1_PRESENT_OPTIONS]
/// </summary>
[Flags]
public enum PresentOptions : uint
{
    /// <summary>
    ///  [D2D1_PRESENT_OPTIONS_NONE]
    /// </summary>
    None = 0x00000000,

    /// <summary>
    ///  Keep the target contents intact through present.
    ///  [D2D1_PRESENT_OPTIONS_RETAIN_CONTENTS]
    /// </summary>
    RetainContents = 0x00000001,

    /// <summary>
    ///  Do not wait for display refresh to commit changes to display.
    ///  [D2D1_PRESENT_OPTIONS_IMMEDIATELY]
    /// </summary>
    PresentOptionsImmediately = 0x00000002
}
