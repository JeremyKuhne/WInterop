// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support.Internal;

namespace WInterop.ErrorHandling
{
    public class DriveNotReadyException : WInteropIOException
    {
        private const WindowsError DefaultError = WindowsError.ERROR_NOT_READY;

        public DriveNotReadyException()
            : base(DefaultError) { }

        public DriveNotReadyException(string message, Exception innerException = null)
            : base(message, Macros.HRESULT_FROM_WIN32(DefaultError), innerException) { }
    }
}
