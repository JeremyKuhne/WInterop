// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    /// <summary>
    /// [HARDWAREINPUT]
    /// </summary>
    /// <remarks><see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms646269.aspx"/></remarks>
    public struct HardwareInput
    {
        public uint uMsg;
        public ushort wParamL;
        public ushort wParamH;
    }
}
