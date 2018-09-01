// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;

namespace WInterop.Errors
{
    public class WInteropIOException : IOException
    {
        public WInteropIOException()
            : base() { }

        public WInteropIOException(string message, HRESULT hresult, Exception innerException = null)
            : base(message, innerException) { HResult = (int)hresult; }

        public WInteropIOException(HRESULT result)
            : this(Error.HResultToString(result), result) { }

        public WInteropIOException(WindowsError error)
            : this(Error.LastErrorToString(error), Error.HRESULT_FROM_WIN32(error)) { }

        public WInteropIOException(NTSTATUS status)
            : this(Error.HRESULT_FROM_NT(status)) { }
    }
}
