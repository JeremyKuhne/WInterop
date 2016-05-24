// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;

namespace WInterop.ErrorHandling.DataTypes
{
    public class DriveLockedException : IOException
    {
        public DriveLockedException()
            : base() { HResult = unchecked((int)WindowsError.FVE_E_LOCKED_VOLUME); }

        public DriveLockedException(string message)
            : base(message) { HResult = unchecked((int)WindowsError.FVE_E_LOCKED_VOLUME); }

        public DriveLockedException(string message, Exception innerException)
            : base(message, innerException) { HResult = unchecked((int)WindowsError.FVE_E_LOCKED_VOLUME); }

        public DriveLockedException(string message, int hresult)
            : base(message, hresult) { }
    }
}
