// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Console
{
    /// <summary>
    /// [CONSOLE_READCONSOLE_CONTROL]
    /// </summary>
    /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/console/console-readconsole-control"/></msdn>
    public struct ReadConsoleControl
    {
        public uint Length;
        public uint InitialChars;
        public uint CtrlWakeupMask;
        public ControlKeyState ControlKeyState;
    }
}
