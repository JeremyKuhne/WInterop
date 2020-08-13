// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Windows;

namespace WInterop.Console
{
    /// <summary>
    ///  Console input record structure [INPUT_RECORD]
    /// </summary>
    /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/console/input-record-str"/></msdn>
    public struct InputRecord
    {
        /// <summary>
        ///  The data type in <see cref="Data"/>.
        /// </summary>
        public EventType EventType;

        /// <summary>
        ///  The input data, <see cref="EventType"/> specifies the type of data.
        /// </summary>
        public DataUnion Data;

        [StructLayout(LayoutKind.Explicit)]
        public struct DataUnion
        {
            [FieldOffset(0)]
            public KeyEventRecord KeyEvent;

            [FieldOffset(0)]
            public MouseEventRecord MouseEvent;

            [FieldOffset(0)]
            public WindowBufferSizeRecord WindowBufferSizeEvent;

            [FieldOffset(0)]
            public MenuEventRecord MenuEvent;

            [FieldOffset(0)]
            public FocusEventRecord FocusEvent;

            /// <summary>
            ///  Data for a key event. [KEY_EVENT_RECORD]
            /// </summary>
            /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/console/key-event-record-str"/></msdn>
            public struct KeyEventRecord
            {
                public IntBoolean KeyDown;
                public ushort RepeatCount;
                public VirtualKey VirtualKeyCode;
                public ushort VirtualScanCode;
                public CharUnion Char;
                public ControlKeyState ControlKeyState;

                [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
                public struct CharUnion
                {
                    [FieldOffset(0)]
                    public char UnicodeChar;
                    [FieldOffset(0)]
                    public ushort AsciiChar;
                }
            }

            /// <summary>
            ///  Data for a mouse event. [MOUSE_EVENT_RECORD]
            /// </summary>
            /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/console/mouse-event-record-str"/></msdn>
            public struct MouseEventRecord
            {
                public Coordinate MousePosition;
                public ButtonState ButtonState;
                public ControlKeyState ControlKeyState;
                public MouseEventState EventFlags;
            }

            /// <summary>
            ///  Data for a window buffer size change event. [WINDOW_BUFFER_SIZE_RECORD]
            /// </summary>
            /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/console/window-buffer-size-record-str"/></msdn>
            public struct WindowBufferSizeRecord
            {
                public Coordinate Size;
            }

            /// <summary>
            ///  Data for a menu event. [MENU_EVENT_RECORD]
            /// </summary>
            /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/console/menu-event-record-str"/></msdn>
            public struct MenuEventRecord
            {
                public uint CommandId;
            }

            /// <summary>
            ///  Data for a focus event. [FOCUS_EVENT_RECORD]
            /// </summary>
            /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/console/focus-event-record-str"/></msdn>
            public struct FocusEventRecord
            {
                public IntBoolean SetFocus;
            }
        }
    }
}
