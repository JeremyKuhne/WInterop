// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Tests.NativeMethodTests
{
    using FluentAssertions;
    using System;
    using System.IO;
    using Xunit;

    public class ErrorsTests
    {
        [Theory
            InlineData(NativeMethods.Errors.WinError.ERROR_FILE_NOT_FOUND, typeof(FileNotFoundException))
            InlineData(NativeMethods.Errors.WinError.ERROR_PATH_NOT_FOUND, typeof(DirectoryNotFoundException))
            InlineData(NativeMethods.Errors.WinError.ERROR_ACCESS_DENIED, typeof(UnauthorizedAccessException))
            InlineData(NativeMethods.Errors.WinError.ERROR_NETWORK_ACCESS_DENIED, typeof(UnauthorizedAccessException))
            InlineData(NativeMethods.Errors.WinError.ERROR_FILENAME_EXCED_RANGE, typeof(PathTooLongException))
#if PORTABLE
            InlineData(NativeMethods.Errors.WinError.ERROR_INVALID_DRIVE, typeof(IOException))
#else
            InlineData(NativeMethods.Errors.WinError.ERROR_INVALID_DRIVE, typeof(DriveNotFoundException))
#endif
            InlineData(NativeMethods.Errors.WinError.ERROR_OPERATION_ABORTED, typeof(OperationCanceledException))
            InlineData(NativeMethods.Errors.WinError.ERROR_ALREADY_EXISTS, typeof(IOException))
            InlineData(NativeMethods.Errors.WinError.ERROR_SHARING_VIOLATION, typeof(IOException))
            InlineData(NativeMethods.Errors.WinError.ERROR_FILE_EXISTS, typeof(IOException))
            ]
        public void ErrorsMapToExceptions(uint error, Type exceptionType)
        {
            NativeMethods.Errors.GetIoExceptionForError(error).Should().BeOfType(exceptionType);
        }
    }
}
