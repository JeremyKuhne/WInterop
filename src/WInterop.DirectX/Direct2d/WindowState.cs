// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Describes whether a window is occluded. [D2D1_WINDOW_STATE]
    /// </summary>
    public enum WindowState : uint
    {
        /// <summary>
        ///  [D2D1_WINDOW_STATE_NONE]
        /// </summary>
        None = 0x0000000,

        /// <summary>
        ///  [D2D1_WINDOW_STATE_OCCLUDED]
        /// </summary>
        Occluded = 0x0000001,
    }
}
