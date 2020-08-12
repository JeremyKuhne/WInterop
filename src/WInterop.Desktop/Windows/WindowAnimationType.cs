// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms632669(v=vs.85).aspx
    [Flags]
    public enum WindowAnimationType : uint
    {
        AW_HOR_POSITIVE            = 0x00000001,
        AW_HOR_NEGATIVE            = 0x00000002,
        AW_VER_POSITIVE            = 0x00000004,
        AW_VER_NEGATIVE            = 0x00000008,
        AW_CENTER                  = 0x00000010,
        AW_HIDE                    = 0x00010000,
        AW_ACTIVATE                = 0x00020000,
        AW_SLIDE                   = 0x00040000,
        AW_BLEND                   = 0x00080000
    }
}
