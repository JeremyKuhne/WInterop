// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Console
{
    // https://docs.microsoft.com/en-us/windows/console/console-readconsole-control
    public struct CONSOLE_READCONSOLE_CONTROL
    {
        public uint nLength;
        public uint nInitialChars;
        public uint dwCtrlWakeupMask;
        public ControlKeyState dwControlKeyState;
    }
}
