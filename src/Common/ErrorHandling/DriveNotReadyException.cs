// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.ErrorHandling
{
    using System;
    using System.IO;

    public class DriveNotReadyException : IOException
    {
        public DriveNotReadyException()
            : base() { HResult = (int)NativeMethods.ErrorHandling.GetHResultForWindowsError(WinErrors.ERROR_NOT_READY); }

        public DriveNotReadyException(string message)
            : base(message) { HResult = (int)NativeMethods.ErrorHandling.GetHResultForWindowsError(WinErrors.ERROR_NOT_READY); }

        public DriveNotReadyException(string message, Exception innerException)
            : base(message, innerException) { HResult = (int)NativeMethods.ErrorHandling.GetHResultForWindowsError(WinErrors.ERROR_NOT_READY); }

        public DriveNotReadyException(string message, int hresult)
            : base(message, hresult) { }
    }
}
