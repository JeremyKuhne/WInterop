// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Errors;

public class WInteropIOException : IOException
{
    public WInteropIOException()
        : base() { }

    public WInteropIOException(string? message, HResult hresult = default, Exception? innerException = null)
        : base(message, innerException) { HResult = (int)hresult; }

    public WInteropIOException(HResult result)
        : this(Error.HResultToString(result), result) { }

    public WInteropIOException(WindowsError error)
        : this(Error.LastErrorToString(error), error.ToHResult()) { }

    public WInteropIOException(NTStatus status)
        : this(status.ToHResult()) { }
}