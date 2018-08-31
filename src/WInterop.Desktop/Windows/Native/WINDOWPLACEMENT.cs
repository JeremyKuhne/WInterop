// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi.Unsafe;

namespace WInterop.Windows.Unsafe
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms632611.aspx
    public struct WINDOWPLACEMENT
    {
        public uint length;
        public uint flags;
        public ShowWindowCommand showCmd;
        public Point ptMinPosition;
        public Point ptMaxPosition;
        public RECT rcNormalPosition;
    }
}
