// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Errors.Native;

namespace WInterop.Errors
{
    public static partial class Error
    {
        /// <summary>
        /// Gets the error mode for the current process.
        /// </summary>
        public static ErrorMode GetProcessErrorMode() => Imports.GetErrorMode();

        /// <summary>
        /// Gets the error mode for the current thread.
        /// </summary>
        public static ErrorMode GetThreadErrorMode() => Imports.GetThreadErrorMode();

        /// <summary>
        /// Set a new error mode for the current thread.
        /// </summary>
        /// <returns>The old error mode for the thread.</returns>
        public static ErrorMode SetThreadErrorMode(ErrorMode mode)
        {
            ThrowLastErrorIfFalse(Imports.SetThreadErrorMode(mode, out ErrorMode oldMode));

            return oldMode;
        }
    }
}
