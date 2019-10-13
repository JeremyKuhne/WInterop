// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using WInterop.Console;

namespace WInterop.Console
{
    /// <summary>
    /// Helper to temporarily change the output mode for the console.
    /// </summary>
    public readonly struct TemporaryOutputMode : IDisposable
    {
        readonly ConsoleOuputMode _mode;
        readonly SafeFileHandle _handle;

        /// <summary>
        /// The mode to set or add.
        /// </summary>
        /// <param name="addFlag">Adds the given flags instead of just replacing the existing flags.</param>
        public TemporaryOutputMode(ConsoleOuputMode mode, bool addFlag = false)
        {
            SafeFileHandle? handle = Console.GetStandardHandle(StandardHandleType.Output);
            _handle = handle ?? throw new InvalidOperationException($"Could not get standard output handle.");
            _mode = Console.GetConsoleOutputMode(_handle);
            Console.SetConsoleOutputMode(_handle, addFlag ? mode | _mode : mode);
        }

        public void Dispose()
        {
            Console.SetConsoleOutputMode(_handle, _mode);
        }
    }
}
