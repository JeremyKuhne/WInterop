// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Errors
{
    public class DriveNotReadyException : WInteropIOException
    {
        private const WindowsError DefaultError = WindowsError.ERROR_NOT_READY;

        public DriveNotReadyException()
            : base(DefaultError) { }

        public DriveNotReadyException(string? message, Exception? innerException = null)
            : base(message, DefaultError.ToHResult(), innerException) { }
    }
}
