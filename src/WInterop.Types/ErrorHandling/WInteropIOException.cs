// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using WInterop.Support;
using WInterop.Support.Internal;

namespace WInterop.ErrorHandling
{
    public class WInteropIOException : IOException
    {
        public WInteropIOException()
            : base() { }

        public WInteropIOException(string message, HRESULT hresult, Exception innerException = null)
            : base(message, innerException) { HResult = (int)hresult; }

        public WInteropIOException(HRESULT result)
            : this(Errors.HResultToString(result), result) { }

        public WInteropIOException(WindowsError error)
            : this(Errors.LastErrorToString(error), Macros.HRESULT_FROM_WIN32(error)) { }

        public WInteropIOException(NTSTATUS status)
            : this(Macros.HRESULT_FROM_NT(status)) { }
    }
}
