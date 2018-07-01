// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Console.Types
{
    [Flags]
    public enum ControlKeyState : uint
    {
        /// <summary>
        /// The right alt key is pressed. [RIGHT_ALT_PRESSED]
        /// </summary>
        RightAltPressed = 0x00000001,

        /// <summary>
        /// The left alt key is pressed. [LEFT_ALT_PRESSED]
        /// </summary>
        LeftAltPressed = 0x00000002,

        /// <summary>
        /// The right ctrl key is pressed. [RIGHT_CTRL_PRESSED]
        /// </summary>
        RightCtrlPressed = 0x00000004,

        /// <summary>
        /// The left ctrl key is pressed. [LEFT_CTRL_PRESSED]
        /// </summary>
        LeftCtrlPressed = 0x00000008,

        /// <summary>
        /// The shift key is pressed. [SHIFT_PRESSED]
        /// </summary>
        ShiftPressed = 0x00000010,

        /// <summary>
        /// The numlock light is on. [NUMLOCK_ON]
        /// </summary>
        NumLockOn = 0x00000020,

        /// <summary>
        /// The scrolllock light is on. [SCROLLLOCK_ON]
        /// </summary>
        ScrollLockOn = 0x00000040,

        /// <summary>
        /// The capslock light is on. [CAPSLOCK_ON]
        /// </summary>
        CapsLockOn = 0x00000080,

        /// <summary>
        /// The key is enhanced. [ENHANCED_KEY]
        /// </summary>
        EnhancedKey = 0x00000100,

        /// <summary>
        /// DBCS for JPN: SBCS/DBCS mode. [NLS_DBCSCHAR]
        /// </summary>
        NlsDbcsChar = 0x00010000,

        // As this is a default value and these are flags, not defining

        // /// <summary>
        // /// DBCS for JPN: Alphanumeric mode. [NLS_ALPHANUMERIC]
        // /// </summary>
        // NlsAlphaNumeric = 0x00000000,

        /// <summary>
        /// DBCS for JPN: Katakana mode. [NLS_KATAKANA]
        /// </summary>
        NlsKatakana = 0x00020000,

        /// <summary>
        /// DBCS for JPN: Hiragana mode. [NLS_HIRAGANA]
        /// </summary>
        NlsHiragana = 0x00040000,

        /// <summary>
        /// DBCS for JPN: Roman/Noroman mode. [NLS_ROMAN]
        /// </summary>
        NlsRoman = 0x00400000,

        /// <summary>
        /// DBCS for JPN: IME conversion. [NLS_IME_CONVERSION]
        /// </summary>
        NlsImeConversion = 0x00800000,

        /// <summary>
        /// AltNumpad OEM char (internal NT). [ALTNUMPAD_BIT]
        /// </summary>
        AltNumPadBit = 0x04000000,

        /// <summary>
        /// DBCS for JPN: IME enable/disable. [NLS_IME_DISABLE]
        /// </summary>
        NlsImeDisable = 0x20000000
    }
}
