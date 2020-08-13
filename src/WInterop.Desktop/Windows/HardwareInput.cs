// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    /// <summary>
    ///  [HARDWAREINPUT]
    /// </summary>
    /// <docs>https://docs.microsoft.com/windows/win32/api/winuser/ns-winuser-hardwareinput</docs>
    public struct HardwareInput
    {
        public uint Message;
        public ushort WParamL;
        public ushort WParamH;
    }
}
