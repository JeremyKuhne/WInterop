// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;

namespace WInterop.ErrorHandling.DataTypes
{
    public class WInteropIOException : IOException
    {
        public WInteropIOException(string message, HRESULT hresult, Exception innerException = null)
            : base(message, innerException) { HResult = (int)hresult; }

        public WInteropIOException(HRESULT result)
            : this(ErrorHelper.HResultToString(result), result) { }

        public WInteropIOException(WindowsError error)
            : this(ErrorHelper.LastErrorToString(error), ErrorMacros.HRESULT_FROM_WIN32(error)) { }

        public WInteropIOException(NTSTATUS status)
            : this(ErrorMacros.HRESULT_FROM_NT(status)) { }
    }
}
