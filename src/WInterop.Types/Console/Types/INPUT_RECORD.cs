// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Windows.Types;

namespace WInterop.Console.Types
{
    /// <summary>
    /// Console input record structure
    /// </summary>
    /// <remarks><see cref="https://docs.microsoft.com/en-us/windows/console/input-record-str"/></remarks>
    public struct INPUT_RECORD
    {
        public EventType EventType;

        public DataUnion Data;

        [StructLayout(LayoutKind.Explicit)]
        public struct DataUnion
        {
            [FieldOffset(0)]
            public KEY_EVENT_RECORD KeyEvent;

            [FieldOffset(0)]
            public MOUSE_EVENT_RECORD MouseEvent;

            [FieldOffset(0)]
            public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;

            [FieldOffset(0)]
            public MENU_EVENT_RECORD MenuEvent;

            [FieldOffset(0)]
            public FOCUS_EVENT_RECORD FocusEvent;


            // https://docs.microsoft.com/en-us/windows/console/key-event-record-str
            public struct KEY_EVENT_RECORD
            {
                public BOOL bKeyDown;
                public ushort wRepeatCount;
                public VirtualKey wVirtualKeyCode;
                public ushort wVirtualScanCode;
                public uCharUnion uChar;
                public ControlKeyState dwControlKeyState;

                [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
                public struct uCharUnion
                {
                    [FieldOffset(0)]
                    public char UnicodeChar;
                    [FieldOffset(0)]
                    public ushort AsciiChar;
                }
            }

            // https://docs.microsoft.com/en-us/windows/console/mouse-event-record-str
            public struct MOUSE_EVENT_RECORD
            {
                public COORD dwMousePosition;
                public ButtonState dwButtonState;
                public ControlKeyState dwControlKeyState;
                public MouseEventState dwEventFlags;
            }

            // https://docs.microsoft.com/en-us/windows/console/window-buffer-size-record-str
            public struct WINDOW_BUFFER_SIZE_RECORD
            {
                public COORD dwSize;
            }

            // https://docs.microsoft.com/en-us/windows/console/menu-event-record-str
            public struct MENU_EVENT_RECORD
            {
                public uint dwCommandId;
            }

            // https://docs.microsoft.com/en-us/windows/console/focus-event-record-str
            public struct FOCUS_EVENT_RECORD
            {
                public BOOL bSetFocus;
            }
        }
    }
}
