// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;

namespace WInterop.Windows.Native
{
    // https://docs.microsoft.com/windows/win32/api/winuser/ns-winuser-windowplacement
    public struct WINDOWPLACEMENT
    {
        public uint length;
        public uint flags;
        public ShowWindowCommand showCmd;
        public Point ptMinPosition;
        public Point ptMaxPosition;
        public Rect rcNormalPosition;
    }
}