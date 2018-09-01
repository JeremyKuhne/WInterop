// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Errors.Unsafe;

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
            Error.ThrowLastErrorIfFalse(Imports.SetThreadErrorMode(mode, out ErrorMode oldMode));

            return oldMode;
        }

        /// <summary>
        /// Turns NTSTATUS errors into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        public static Exception GetIoExceptionForNTStatus(NTSTATUS status, string path = null)
        {
            switch (status)
            {
                case NTSTATUS.STATUS_NOT_IMPLEMENTED:
                    return new NotImplementedException(path ?? WInteropStrings.NoValue);
            }

            return GetIoExceptionForError(NtStatusToWinError(status), path);
        }
    }
}
