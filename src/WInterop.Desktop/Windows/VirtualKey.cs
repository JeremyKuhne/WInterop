// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://docs.microsoft.com/en-us/windows/desktop/inputdev/virtual-key-codes
    public enum VirtualKey : ushort
    {
        /// <summary>
        ///  (VK_LBUTTON)
        /// </summary>
        LeftButton = 0x01,

        /// <summary>
        ///  (VK_RBUTTON)
        /// </summary>
        RightButton = 0x02,

        /// <summary>
        ///  (VK_CANCEL)
        /// </summary>
        Cancel = 0x03,

        /// <summary>
        ///  (VK_MBUTTON)
        /// </summary>
        MiddleButton = 0x04,

        /// <summary>
        ///  (VK_XBUTTON1)
        /// </summary>
        ExtraButton1 = 0x05,

        /// <summary>
        ///  (VK_XBUTTON2)
        /// </summary>
        ExtraButton2 = 0x06,

        /// <summary>
        ///  Xbox button. (VK_NEXUS)
        /// </summary>
        Nexus = 0x07,

        /// <summary>
        ///  (VK_BACK)
        /// </summary>
        Back = 0x08,

        /// <summary>
        ///  (VK_TAB)
        /// </summary>
        Tab = 0x09,

        /// <summary>
        ///  (VK_CLEAR)
        /// </summary>
        Clear = 0x0C,

        /// <summary>
        ///  (VK_RETURN)
        /// </summary>
        Return = 0x0D,

        /// <summary>
        ///  (VK_SHIFT)
        /// </summary>
        Shift = 0x10,

        /// <summary>
        ///  (VK_CONTROL)
        /// </summary>
        Control = 0x11,

        /// <summary>
        ///  (VK_MENU)
        /// </summary>
        Menu = 0x12,

        /// <summary>
        ///  (VK_PAUSE)
        /// </summary>
        Pause = 0x13,

        /// <summary>
        ///  (VK_CAPITAL)
        /// </summary>
        Capital = 0x14,

        /// <summary>
        ///  (VK_KANA)
        /// </summary>
        Kana = 0x15,

        /// <summary>
        ///  (VK_HANGUL)
        /// </summary>
        Hangul = 0x15,

        /// <summary>
        ///  (VK_JUNJA)
        /// </summary>
        Junja = 0x17,

        /// <summary>
        ///  (VK_FINAL)
        /// </summary>
        Final = 0x18,

        /// <summary>
        ///  (VK_HANJA)
        /// </summary>
        Hanja = 0x19,

        /// <summary>
        ///  (VK_KANJI)
        /// </summary>
        Kanji = 0x19,

        /// <summary>
        ///  (VK_ESCAPE)
        /// </summary>
        Escape = 0x1B,

        /// <summary>
        ///  (VK_CONVERT)
        /// </summary>
        Convert = 0x1C,

        /// <summary>
        ///  (VK_NONCONVERT)
        /// </summary>
        NonConvert = 0x1D,

        /// <summary>
        ///  (VK_ACCEPT)
        /// </summary>
        Accept = 0x1E,

        /// <summary>
        ///  (VK_MODECHANGE)
        /// </summary>
        ModeChange = 0x1F,

        /// <summary>
        ///  (VK_SPACE)
        /// </summary>
        Space = 0x20,

        /// <summary>
        ///  Page up. (VK_PRIOR)
        /// </summary>
        Prior = 0x21,

        /// <summary>
        ///  Page down. (VK_NEXT)
        /// </summary>
        Next = 0x22,

        /// <summary>
        ///  (VK_END)
        /// </summary>
        End = 0x23,

        /// <summary>
        ///  (VK_HOME)
        /// </summary>
        Home = 0x24,

        /// <summary>
        ///  (VK_LEFT)
        /// </summary>
        Left = 0x25,

        /// <summary>
        ///  (VK_UP)
        /// </summary>
        Up = 0x26,

        /// <summary>
        ///  (VK_RIGHT)
        /// </summary>
        Right = 0x27,

        /// <summary>
        ///  (VK_DOWN)
        /// </summary>
        Down = 0x28,

        /// <summary>
        ///  (VK_SELECT)
        /// </summary>
        Select = 0x29,

        /// <summary>
        ///  (VK_PRINT)
        /// </summary>
        Print = 0x2A,

        /// <summary>
        ///  (VK_EXECUTE)
        /// </summary>
        Execute = 0x2B,

        /// <summary>
        ///  Print screen. (VK_SNAPSHOT)
        /// </summary>
        Snapshot = 0x2C,

        /// <summary>
        ///  (VK_INSERT)
        /// </summary>
        Insert = 0x2D,

        /// <summary>
        ///  (VK_DELETE)
        /// </summary>
        Delete = 0x2E,

        /// <summary>
        ///  (VK_HELP)
        /// </summary>
        Help = 0x2F,

        // 0-9 and A-Z match ASCII

        One = 0x30,
        Two = 0x31,
        Three = 0x32,
        Four = 0x33,
        Five = 0x34,
        Six = 0x35,
        Seven = 0x36,
        Eight = 0x37,
        Nine = 0x38,
        A = 0x41,
        B = 0x42,
        C = 0x43,
        D = 0x44,
        E = 0x45,
        F = 0x46,
        G = 0x47,
        H = 0x48,
        I = 0x49,
        J = 0x4A,
        K = 0x4B,
        L = 0x4C,
        M = 0x4D,
        N = 0x4E,
        O = 0x4F,
        P = 0x50,
        Q = 0x51,
        R = 0x52,
        S = 0x53,
        T = 0x54,
        U = 0x55,
        V = 0x56,
        W = 0x57,
        X = 0x58,
        Y = 0x59,
        Z = 0x5A,

        /// <summary>
        ///  (VK_LWIN)
        /// </summary>
        LeftWindows = 0x5B,

        /// <summary>
        ///  (VK_RWIN)
        /// </summary>
        RightWindows = 0x5C,

        /// <summary>
        ///  (VK_APPS)
        /// </summary>
        Apps = 0x5D,

        /// <summary>
        ///  (VK_SLEEP)
        /// </summary>
        Sleep = 0x5F,

        /// <summary>
        ///  (VK_NUMPAD0)
        /// </summary>
        NumPad0 = 0x60,

        /// <summary>
        ///  (VK_NUMPAD1)
        /// </summary>
        NumPad1 = 0x61,

        /// <summary>
        ///  (VK_NUMPAD2)
        /// </summary>
        NumPad2 = 0x62,

        /// <summary>
        ///  (VK_NUMPAD3)
        /// </summary>
        NumPad3 = 0x63,

        /// <summary>
        ///  (VK_NUMPAD4)
        /// </summary>
        NumPad4 = 0x64,

        /// <summary>
        ///  (VK_NUMPAD5)
        /// </summary>
        NumPad5 = 0x65,

        /// <summary>
        ///  (VK_NUMPAD6)
        /// </summary>
        NumPad6 = 0x66,

        /// <summary>
        ///  (VK_NUMPAD7)
        /// </summary>
        NumPad7 = 0x67,

        /// <summary>
        ///  (VK_NUMPAD8)
        /// </summary>
        NumPad8 = 0x68,

        /// <summary>
        ///  (VK_NUMPAD9)
        /// </summary>
        NumPad9 = 0x69,

        /// <summary>
        ///  (VK_MULTIPLY)
        /// </summary>
        Multiply = 0x6A,

        /// <summary>
        ///  (VK_ADD)
        /// </summary>
        Add = 0x6B,

        /// <summary>
        ///  (VK_SEPARATOR)
        /// </summary>
        Separator = 0x6C,

        /// <summary>
        ///  (VK_SUBTRACT)
        /// </summary>
        Subtract = 0x6D,

        /// <summary>
        ///  (VK_DECIMAL)
        /// </summary>
        Decimal = 0x6E,

        /// <summary>
        ///  (VK_DIVIDE)
        /// </summary>
        Divide = 0x6F,

        /// <summary>
        ///  (VK_F1)
        /// </summary>
        F1 = 0x70,

        /// <summary>
        ///  (VK_F2)
        /// </summary>
        F2 = 0x71,

        /// <summary>
        ///  (VK_F3)
        /// </summary>
        F3 = 0x72,

        /// <summary>
        ///  (VK_F4)
        /// </summary>
        F4 = 0x73,

        /// <summary>
        ///  (VK_F5)
        /// </summary>
        F5 = 0x74,

        /// <summary>
        ///  (VK_F6)
        /// </summary>
        F6 = 0x75,

        /// <summary>
        ///  (VK_F7)
        /// </summary>
        F7 = 0x76,

        /// <summary>
        ///  (VK_F8)
        /// </summary>
        F8 = 0x77,

        /// <summary>
        ///  (VK_F9)
        /// </summary>
        F9 = 0x78,

        /// <summary>
        ///  (VK_F10)
        /// </summary>
        F10 = 0x79,

        /// <summary>
        ///  (VK_F11)
        /// </summary>
        F11 = 0x7A,

        /// <summary>
        ///  (VK_F12)
        /// </summary>
        F12 = 0x7B,

        /// <summary>
        ///  (VK_F13)
        /// </summary>
        F13 = 0x7C,

        /// <summary>
        ///  (VK_F14)
        /// </summary>
        F14 = 0x7D,

        /// <summary>
        ///  (VK_F15)
        /// </summary>
        F15 = 0x7E,

        /// <summary>
        ///  (VK_F16)
        /// </summary>
        F16 = 0x7F,

        /// <summary>
        ///  (VK_F17)
        /// </summary>
        F17 = 0x80,

        /// <summary>
        ///  (VK_F18)
        /// </summary>
        F18 = 0x81,

        /// <summary>
        ///  (VK_F19)
        /// </summary>
        F19 = 0x82,

        /// <summary>
        ///  (VK_F20)
        /// </summary>
        F20 = 0x83,

        /// <summary>
        ///  (VK_F21)
        /// </summary>
        F21 = 0x84,

        /// <summary>
        ///  (VK_F22)
        /// </summary>
        F22 = 0x85,

        /// <summary>
        ///  (VK_F23)
        /// </summary>
        F23 = 0x86,

        /// <summary>
        ///  (VK_F24)
        /// </summary>
        F24 = 0x87,

        /// <summary>
        ///  Reserved. (VK_NAVIGATION_VIEW)
        /// </summary>
        NavigationView = 0x88,

        /// <summary>
        ///  Reserved. (VK_NAVIGATION_MENU)
        /// </summary>
        NavigationMenu = 0x89,

        /// <summary>
        ///  Reserved. (VK_NAVIGATION_UP)
        /// </summary>
        NavigationUp = 0x8A,

        /// <summary>
        ///  Reserved. (VK_NAVIGATION_DOWN)
        /// </summary>
        NavigationDown = 0x8B,

        /// <summary>
        ///  Reserved. (VK_NAVIGATION_LEFT)
        /// </summary>
        NavigationLeft = 0x8C,

        /// <summary>
        ///  Reserved. (VK_NAVIGATION_RIGHT)
        /// </summary>
        NavigationRight = 0x8D,

        /// <summary>
        ///  Reserved. (VK_NAVIGATION_ACCEPT)
        /// </summary>
        NavigationAccept = 0x8E,

        /// <summary>
        ///  Reserved. (VK_NAVIGATION_CANCEL)
        /// </summary>
        NavigationCancel = 0x8F,

        /// <summary>
        ///  (VK_NUMLOCK)
        /// </summary>
        NumLock = 0x90,

        /// <summary>
        ///  (VK_SCROLL)
        /// </summary>
        Scroll = 0x91,

        /// <summary>
        ///  '=' key on numpad, (VK_OEM_NEC_EQUAL)
        /// </summary>
        OemNecEqual = 0x92,

        /// <summary>
        ///  'Dictionary' key. (VK_OEM_FJ_JISHO)
        /// </summary>
        OemFujitsuJisho = 0x92,

        /// <summary>
        ///  'Unregister word' key. (VK_OEM_FJ_MASSHOU)
        /// </summary>
        OemFujitsuMasshou = 0x93,

        /// <summary>
        ///  'Register word' key. (VK_OEM_FJ_TOUROKU)
        /// </summary>
        OemFujitsuTouroku = 0x94,

        /// <summary>
        ///  'Left OYAYUBI' key. (VK_OEM_FJ_LOYA)
        /// </summary>
        OemFujitsuLoya = 0x95,

        /// <summary>
        ///  'Right OYAYUBI' key. (VK_OEM_FJ_ROYA)
        /// </summary>
        OemFujitsuRoya = 0x96,

        /// <summary>
        ///  Used only as parameter to GetAsyncKeyState() and GetKeyState(). (VK_LSHIFT)
        /// </summary>
        LeftShift = 0xA0,

        /// <summary>
        ///  Used only as parameter to GetAsyncKeyState() and GetKeyState(). (VK_RSHIFT)
        /// </summary>
        RightShift = 0xA1,

        /// <summary>
        ///  Used only as parameter to GetAsyncKeyState() and GetKeyState(). (VK_LCONTROL)
        /// </summary>
        LeftControl = 0xA2,

        /// <summary>
        ///  Used only as parameter to GetAsyncKeyState() and GetKeyState(). (VK_RCONTROL)
        /// </summary>
        RightControl = 0xA3,

        /// <summary>
        ///  Used only as parameter to GetAsyncKeyState() and GetKeyState(). (VK_LMENU)
        /// </summary>
        LeftMenu = 0xA4,

        /// <summary>
        ///  Used only as parameter to GetAsyncKeyState() and GetKeyState(). (VK_RMENU)
        /// </summary>
        RightMenu = 0xA5,

        /// <summary>
        ///  (VK_BROWSER_BACK)
        /// </summary>
        BrowserBack = 0xA6,

        /// <summary>
        ///  (VK_BROWSER_FORWARD)
        /// </summary>
        BrowserForward = 0xA7,

        /// <summary>
        ///  (VK_BROWSER_REFRESH)
        /// </summary>
        BrowserRefresh = 0xA8,

        /// <summary>
        ///  (VK_BROWSER_STOP)
        /// </summary>
        BrowserStop = 0xA9,

        /// <summary>
        ///  (VK_BROWSER_SEARCH)
        /// </summary>
        BrowserSearch = 0xAA,

        /// <summary>
        ///  (VK_BROWSER_FAVORITES)
        /// </summary>
        BrowserFavorites = 0xAB,

        /// <summary>
        ///  (VK_BROWSER_HOME)
        /// </summary>
        BrowserHome = 0xAC,

        /// <summary>
        ///  (VK_VOLUME_MUTE)
        /// </summary>
        VolumeMute = 0xAD,

        /// <summary>
        ///  (VK_VOLUME_DOWN)
        /// </summary>
        VolumeDown = 0xAE,

        /// <summary>
        ///  (VK_VOLUME_UP)
        /// </summary>
        VolumeUp = 0xAF,

        /// <summary>
        ///  (VK_MEDIA_NEXT_TRACK)
        /// </summary>
        MediaNextTrack = 0xB0,

        /// <summary>
        ///  (VK_MEDIA_PREV_TRACK)
        /// </summary>
        MediaPrevTrack = 0xB1,

        /// <summary>
        ///  (VK_MEDIA_STOP)
        /// </summary>
        MediaStop = 0xB2,

        /// <summary>
        ///  (VK_MEDIA_PLAY_PAUSE)
        /// </summary>
        MediaPlayPause = 0xB3,

        /// <summary>
        ///  (VK_LAUNCH_MAIL)
        /// </summary>
        LaunchMail = 0xB4,

        /// <summary>
        ///  (VK_LAUNCH_MEDIA_SELECT)
        /// </summary>
        LaunchMediaSelect = 0xB5,

        /// <summary>
        ///  (VK_LAUNCH_APP1)
        /// </summary>
        LaunchApp1 = 0xB6,

        /// <summary>
        ///  (VK_LAUNCH_APP2)
        /// </summary>
        LaunchApp2 = 0xB7,

        /// <summary>
        ///  ';:' for US. (VK_OEM_1)
        /// </summary>
        Oem1 = 0xBA,

        /// <summary>
        ///  '+' any country. (VK_OEM_PLUS)
        /// </summary>
        OemPlus = 0xBB,

        /// <summary>
        ///  ',' any country. (VK_OEM_COMMA)
        /// </summary>
        OemComma = 0xBC,

        /// <summary>
        ///  '-' any country. (VK_OEM_MINUS)
        /// </summary>
        OemMinus = 0xBD,

        /// <summary>
        ///  '.' any country. (VK_OEM_PERIOD)
        /// </summary>
        OemPeriod = 0xBE,

        /// <summary>
        ///  '/?' for US. (VK_OEM_2)
        /// </summary>
        Oem2 = 0xBF,

        /// <summary>
        ///  '`~' for US. (VK_OEM_3)
        /// </summary>
        Oem3 = 0xC0,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_A)
        /// </summary>
        GamepadA = 0xC3,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_B)
        /// </summary>
        GamepadB = 0xC4,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_X)
        /// </summary>
        GamepadX = 0xC5,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_Y)
        /// </summary>
        GamepadY = 0xC6,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_RIGHT_SHOULDER)
        /// </summary>
        GamepadRightShoulder = 0xC7,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_LEFT_SHOULDER)
        /// </summary>
        GamepadLeftShoulder = 0xC8,

        /// <summary>
        ///  Reserved. VK_GAMEPAD_LEFT_TRIGGER()
        /// </summary>
        GamepadLeftTrigger = 0xC9,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_RIGHT_TRIGGER)
        /// </summary>
        GamepadRightTrigger = 0xCA,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_DPAD_UP)
        /// </summary>
        GamepadDPadUp = 0xCB,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_DPAD_DOWN)
        /// </summary>
        GamepadDPadDown = 0xCC,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_DPAD_LEFT)
        /// </summary>
        GamepadDPadLeft = 0xCD,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_DPAD_RIGHT)
        /// </summary>
        GamepadDPadRight = 0xCE,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_MENU)
        /// </summary>
        GamepadMenu = 0xCF,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_VIEW)
        /// </summary>
        GamepadView = 0xD0,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_LEFT_THUMBSTICK_BUTTON)
        /// </summary>
        GamepadLeftThumbstickButton = 0xD1,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_RIGHT_THUMBSTICK_BUTTON)
        /// </summary>
        GamepadRightThumbstickButton = 0xD2,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_LEFT_THUMBSTICK_UP)
        /// </summary>
        GamepadLeftThumbstickUp = 0xD3,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_LEFT_THUMBSTICK_DOWN)
        /// </summary>
        GamepadLeftThumbstickDown = 0xD4,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_LEFT_THUMBSTICK_RIGHT)
        /// </summary>
        GamepadLeftThumbstickRight = 0xD5,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_LEFT_THUMBSTICK_LEFT)
        /// </summary>
        GamepadLeftThumbstickLeft = 0xD6,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_RIGHT_THUMBSTICK_UP)
        /// </summary>
        GamepadRightThumbstickUp = 0xD7,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_RIGHT_THUMBSTICK_DOWN)
        /// </summary>
        GamepadRightThumbstickDown = 0xD8,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_RIGHT_THUMBSTICK_RIGHT)
        /// </summary>
        GamepadRightThumbstickRight = 0xD9,

        /// <summary>
        ///  Reserved. (VK_GAMEPAD_RIGHT_THUMBSTICK_LEFT)
        /// </summary>
        GamepadRightThumbstickLeft = 0xDA,

        /// <summary>
        ///  '[{' for US. (VK_OEM_4)
        /// </summary>
        Oem4 = 0xDB,

        /// <summary>
        ///  '\|' for US. (VK_OEM_5)
        /// </summary>
        Oem5 = 0xDC,

        /// <summary>
        ///  ']}' for US. (VK_OEM_6)
        /// </summary>
        Oem6 = 0xDD,

        /// <summary>
        ///  ''"' for US. (VK_OEM_7)
        /// </summary>
        Oem7 = 0xDE,

        /// <summary>
        ///  (VK_OEM_8)
        /// </summary>
        Oem8 = 0xDF,

        /// <summary>
        ///  'AX' key on Japanese AX keyboard. (VK_OEM_AX)
        /// </summary>
        OemAX = 0xE1,

        /// <summary>
        ///  "&lt;&gt;" or "\|" on RT 102-key keyboard. (VK_OEM_102)
        /// </summary>
        Oem102 = 0xE2,

        /// <summary>
        ///  Help key on Olivetti M24 "ICO" (102-key) keyboard. (VK_ICO_HELP)
        /// </summary>
        IcoHelp = 0xE3,

        /// <summary>
        ///  00 key on Olivetti M24 "ICO" (102-key) keyboard. (VK_ICO_00)
        /// </summary>
        Ico00 = 0xE4,

        /// <summary>
        ///  (VK_PROCESSKEY)
        /// </summary>
        ProcessKey = 0xE5,

        /// <summary>
        ///  Clear key on Olivetti M24 "ICO" (102-key) keyboard. (VK_ICO_CLEAR)
        /// </summary>
        IcoClear = 0xE6,

        /// <summary>
        ///  Used to pass Unicode characters as if they were keystrokes. (VK_PACKET)
        /// </summary>
        Packet = 0xE7,

        // 0xE9 - 0xF5 are for Ericsson keyboards

        /// <summary>
        ///  Ericsson. (VK_OEM_RESET)
        /// </summary>
        OemReset = 0xE9,

        /// <summary>
        ///  Ericsson. (VK_OEM_JUMP)
        /// </summary>
        OemJump = 0xEA,

        /// <summary>
        ///  Ericsson. (VK_OEM_PA1)
        /// </summary>
        OemPA1 = 0xEB,

        /// <summary>
        ///  Ericsson. (VK_OEM_PA2)
        /// </summary>
        OemPA2 = 0xEC,

        /// <summary>
        ///  Ericsson. (VK_OEM_PA3)
        /// </summary>
        OemPA3 = 0xED,

        /// <summary>
        ///  Ericsson. (VK_OEM_WSCTRL)
        /// </summary>
        OemWSCtrl = 0xEE,

        /// <summary>
        ///  Ericsson. (VK_OEM_CUSEL)
        /// </summary>
        OemCuSel = 0xEF,

        /// <summary>
        ///  Ericsson. (VK_OEM_ATTN)
        /// </summary>
        OemAttn = 0xF0,

        /// <summary>
        ///  Ericsson. (VK_OEM_FINISH)
        /// </summary>
        OemFinish = 0xF1,

        /// <summary>
        ///  Ericsson. (VK_OEM_COPY)
        /// </summary>
        OemCopy = 0xF2,

        /// <summary>
        ///  Ericsson. (VK_OEM_AUTO)
        /// </summary>
        OemAuto = 0xF3,

        /// <summary>
        ///  Ericsson. (VK_OEM_ENLW)
        /// </summary>
        OemEnlw = 0xF4,

        /// <summary>
        ///  Ericsson. (VK_OEM_BACKTAB)
        /// </summary>
        OemBackTab = 0xF5,

        /// <summary>
        ///  (VK_ATTN)
        /// </summary>
        Attn = 0xF6,

        /// <summary>
        ///  (VK_CRSEL)
        /// </summary>
        CrSel = 0xF7,

        /// <summary>
        ///  (VK_EXSEL)
        /// </summary>
        ExSel = 0xF8,

        /// <summary>
        ///  (VK_EREOF)
        /// </summary>
        EraseEOF = 0xF9,

        /// <summary>
        ///  (VK_PLAY)
        /// </summary>
        Play = 0xFA,

        /// <summary>
        ///  (VK_ZOOM)
        /// </summary>
        Zoom = 0xFB,

        /// <summary>
        ///  Reserved. (VK_NONAME)
        /// </summary>
        NoName = 0xFC,

        /// <summary>
        ///  (VK_PA1)
        /// </summary>
        PA1 = 0xFD,

        /// <summary>
        ///  Clear key. (VK_OEM_CLEAR)
        /// </summary>
        OemClear = 0xFE
    }
}