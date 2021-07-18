// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644940.aspx
    [Flags]
    public enum QueueStatus : uint
    {
        Key             = 0x0001,
        MouseMove       = 0x0002,
        MouseButton     = 0x0004,
        PostMessage     = 0x0008,
        Timer           = 0x0010,
        Paint           = 0x0020,
        SendMessage     = 0x0040,
        HotKey          = 0x0080,
        AllPostMessage  = 0x0100,
        RawInput        = 0x0400,
        Touch           = 0x0800,
        Pointer         = 0x1000,
        Mouse           = MouseMove | MouseButton,
        Input           = Mouse | Key | RawInput | Touch | Pointer,
        AllEvents       = Input | PostMessage | Timer | Paint | HotKey,
        AllInput        = AllEvents | SendMessage
    }
}