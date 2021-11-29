// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

// https://msdn.microsoft.com/en-us/library/windows/desktop/ms646270.aspx
public enum InputType : uint
{
    /// <summary>
    ///  [INPUT_MOUSE]
    /// </summary>
    Mouse = 0,

    /// <summary>
    ///  [INPUT_MOUSE]
    /// </summary>
    Keyboard = 1,

    /// <summary>
    ///  [INPUT_MOUSE]
    /// </summary>
    Hardware = 2
}