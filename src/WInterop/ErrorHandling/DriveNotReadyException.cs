// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;

namespace WInterop.ErrorHandling
{
    public class DriveNotReadyException : IOException
    {
        public DriveNotReadyException()
            : base() { HResult = ErrorMacros.HRESULT_FROM_WIN32(WindowsError.ERROR_NOT_READY); }

        public DriveNotReadyException(string message)
            : base(message) { HResult = ErrorMacros.HRESULT_FROM_WIN32(WindowsError.ERROR_NOT_READY); }

        public DriveNotReadyException(string message, Exception innerException)
            : base(message, innerException) { HResult = ErrorMacros.HRESULT_FROM_WIN32(WindowsError.ERROR_NOT_READY); }

        public DriveNotReadyException(string message, int hresult)
            : base(message, hresult) { }
    }
}
