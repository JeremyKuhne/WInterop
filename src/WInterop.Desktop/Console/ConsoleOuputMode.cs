// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Console
{
    /// <summary>
    ///  Console output handle modes.
    /// </summary>
    /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/console/getconsolemode"/></msdn>
    [Flags]
    public enum ConsoleOuputMode : uint
    {
        /// <summary>
        ///  [ENABLE_PROCESSED_OUTPUT]
        /// </summary>
        EnableProcessedOutput = 0x0001,

        /// <summary>
        ///  [ENABLE_WRAP_AT_EOL_OUTPUT]
        /// </summary>
        EnalbeWrapAtEndOfLine = 0x0002,

        /// <summary>
        ///  [ENABLE_VIRTUAL_TERMINAL_PROCESSING]
        /// </summary>
        EnableVirtualTerminalProcessing = 0x0004,

        /// <summary>
        ///  [DISABLE_NEWLINE_AUTO_RETURN]
        /// </summary>
        DisableNewlineAutoReturn = 0x0008,

        /// <summary>
        ///  [ENABLE_LVB_GRID_WORLDWIDE]
        /// </summary>
        EnableLvbGridWorldwide = 0x0010
    }
}