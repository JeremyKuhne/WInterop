// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;

namespace WInterop.Resources.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648381.aspx
    public struct CURSORINFO
    {
        public uint cbSize;
        public CursorState flags;
        public IntPtr hCursor;
        public Point ptScreenPos;
    }
}
