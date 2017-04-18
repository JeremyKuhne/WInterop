// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.ErrorHandling.DataTypes
{
    public class DriveNotReadyException : WInteropIOException
    {
        private const WindowsError DefaultError = WindowsError.ERROR_NOT_READY;

        public DriveNotReadyException()
            : base(DefaultError) { }

        public DriveNotReadyException(string message, Exception innerException = null)
            : base(message, ErrorMacros.HRESULT_FROM_WIN32(DefaultError), innerException) { }

        public DriveNotReadyException(string message, HRESULT hresult, Exception innerException = null)
            : base(message, hresult, innerException) { }

        public DriveNotReadyException(HRESULT result)
            : base(result) { }

        public DriveNotReadyException(WindowsError error)
            : base(error) { }

        public DriveNotReadyException(NTSTATUS status)
            : base(status) { }
    }
}
