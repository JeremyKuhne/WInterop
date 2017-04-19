// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support.Internal;

namespace WInterop.ErrorHandling.DataTypes
{
    public class DriveLockedException : WInteropIOException
    {
        private const WindowsError DefaultError = WindowsError.FVE_E_LOCKED_VOLUME;

        public DriveLockedException()
            : base(DefaultError) { }

        public DriveLockedException(string message, Exception innerException = null)
            : base(message, Macros.HRESULT_FROM_WIN32(DefaultError), innerException) { }

        public DriveLockedException(string message, HRESULT hresult, Exception innerException = null)
            : base(message, hresult, innerException) { }

        public DriveLockedException(HRESULT result)
            : base(result) { }

        public DriveLockedException(WindowsError error)
            : base(error) { }

        public DriveLockedException(NTSTATUS status)
            : base(status) { }
    }
}
