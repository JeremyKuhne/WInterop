// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787593.aspx
    public enum ScrollWindowFlags : uint
    {
        /// <summary>
        ///  [SW_SCROLLCHILDREN]
        /// </summary>
        ScrollChildren = 0x0001,

        /// <summary>
        ///  [SW_INVALIDATE]
        /// </summary>
        Invalidate = 0x0002,

        /// <summary>
        ///  [SW_ERASE]
        /// </summary>
        Erase = 0x0004,

        /// <summary>
        ///  [SW_SMOOTHSCROLL]
        /// </summary>
        SmoothScroll = 0x0010
    }
}