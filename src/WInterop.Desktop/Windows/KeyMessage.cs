// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    /// <summary>
    /// Helper for translating a Windows Key or Char message (WM_CHAR, WM_KEYDOWN, etc.)
    /// </summary>
    public struct KeyMessage
    {
        private const uint RepeatMask        = 0b00000000_00000000_11111111_11111111;
        private const uint ScanCodeMask      = 0b00000000_11111111_00000000_00000000;
        private const uint ExtendedKeyMask   = 0b00000001_00000000_00000000_00000000;
        // RESERVED                          = 0b00011110_00000000_00000000_00000000;
        private const uint ContextCodeMask   = 0b00100000_00000000_00000000_00000000;
        private const uint PreviousStateMask = 0b01000000_00000000_00000000_00000000;
        private const uint TransitionState   = 0b10000000_00000000_00000000_00000000;

        private uint _wParam;
        private uint _lParam;

        public KeyMessage(WParam wParam, LParam lParam)
        {
            _wParam = wParam;
            _lParam = (uint)lParam;
        }

        /// <summary>
        /// The character for a Char message.
        /// </summary>
        public char CharacterCode => (char)_wParam;

        /// <summary>
        /// Key code for a Key message.
        /// </summary>
        public VirtualKey KeyCode => (VirtualKey)_wParam;

        /// <summary>
        /// Repeat count of the current message.
        /// </summary>
        public ushort RepeatCount => (ushort)(_lParam & RepeatMask);

        /// <summary>
        /// Scan code. For Char messages, this is the most recent KeyDown value.
        /// </summary>
        public byte ScanCode => (byte)((_lParam & ScanCodeMask) >> 16);

        /// <summary>
        /// Right ALT, CTRL, numeric keypad INS, DEL, HOME, END, PAGE UP, PAGE DOWN, arrow key cluster.
        /// Possibly others. For Char messages, this is the most recent KeyDown value.
        /// </summary>
        public bool ExtendedKey => (_lParam & ExtendedKeyMask) != 0;

        /// <summary>
        /// ALT key was down. For Char messages, this is the most recent KeyDown value.
        /// </summary>
        public bool AltKeyDown => (_lParam & ContextCodeMask) != 0;

        /// <summary>
        /// Previous key state when the message was sent. For Char messages, this is the most recent KeyDown value.
        /// </summary>
        public bool KeyDown => (_lParam & PreviousStateMask) != 0;

        /// <summary>
        /// Transition state. For Char messages, this is the most recent KeyDown value.
        /// </summary>
        public bool KeyReleased => (_lParam & TransitionState) != 0;
    }
}
