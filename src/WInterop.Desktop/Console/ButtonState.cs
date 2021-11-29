// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Console;

public enum ButtonState : uint
{
    /// <summary>
    ///  [FROM_LEFT_1ST_BUTTON_PRESSED]
    /// </summary>
    FirstFromLeft = 0x0001,

    /// <summary>
    ///  [RIGHTMOST_BUTTON_PRESSED]
    /// </summary>
    Rightmost = 0x0002,

    /// <summary>
    ///  [FROM_LEFT_2ND_BUTTON_PRESSED]
    /// </summary>
    SecondFromLeft = 0x0004,

    /// <summary>
    ///  [FROM_LEFT_3RD_BUTTON_PRESSED]
    /// </summary>
    ThirdFromLeft = 0x0008,

    /// <summary>
    ///  [FROM_LEFT_4TH_BUTTON_PRESSED]
    /// </summary>
    FourthFromLeft = 0x0010
}