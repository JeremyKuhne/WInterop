// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Errors;

public class DriveLockedException : WInteropIOException
{
    private const WindowsError DefaultError = WindowsError.FVE_E_LOCKED_VOLUME;

    public DriveLockedException()
        : base(DefaultError) { }

    public DriveLockedException(string? message, Exception? innerException = null)
        : base(message, DefaultError.ToHResult(), innerException) { }

    public DriveLockedException(string? message, HResult hresult, Exception? innerException = null)
        : base(message, hresult, innerException) { }

    public DriveLockedException(HResult result)
        : base(result) { }

    public DriveLockedException(WindowsError error)
        : base(error) { }

    public DriveLockedException(NTStatus status)
        : base(status) { }
}