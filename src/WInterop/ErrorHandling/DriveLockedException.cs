// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;

namespace WInterop.ErrorHandling
{
    public class DriveLockedException : IOException
    {
        public DriveLockedException()
            : base() { HResult = unchecked((int)WinErrors.FVE_E_LOCKED_VOLUME); }

        public DriveLockedException(string message)
            : base(message) { HResult = unchecked((int)WinErrors.FVE_E_LOCKED_VOLUME); }

        public DriveLockedException(string message, Exception innerException)
            : base(message, innerException) { HResult = unchecked((int)WinErrors.FVE_E_LOCKED_VOLUME); }

        public DriveLockedException(string message, int hresult)
            : base(message, hresult) { }
    }
}
