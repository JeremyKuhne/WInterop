// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using WInterop.ErrorHandling;
using Xunit;

namespace Tests
{
    public partial class ErrorHandlingTests
    {
        [Theory
            InlineData(WinErrors.ERROR_FILE_NOT_FOUND, typeof(FileNotFoundException))
            InlineData(WinErrors.ERROR_PATH_NOT_FOUND, typeof(DirectoryNotFoundException))
            InlineData(WinErrors.ERROR_ACCESS_DENIED, typeof(UnauthorizedAccessException))
            InlineData(WinErrors.ERROR_NETWORK_ACCESS_DENIED, typeof(UnauthorizedAccessException))
            InlineData(WinErrors.ERROR_FILENAME_EXCED_RANGE, typeof(PathTooLongException))
            InlineData(WinErrors.ERROR_INVALID_DRIVE, typeof(IOException))
            InlineData(WinErrors.ERROR_OPERATION_ABORTED, typeof(OperationCanceledException))
            InlineData(WinErrors.ERROR_NOT_READY, typeof(DriveNotReadyException))
            InlineData(WinErrors.FVE_E_LOCKED_VOLUME, typeof(DriveLockedException))
            InlineData(WinErrors.ERROR_ALREADY_EXISTS, typeof(IOException))
            InlineData(WinErrors.ERROR_SHARING_VIOLATION, typeof(IOException))
            InlineData(WinErrors.ERROR_FILE_EXISTS, typeof(IOException))
            ]
        public void ErrorsMapToExceptions(uint error, Type exceptionType)
        {
            ErrorHelper.GetIoExceptionForError(error).Should().BeOfType(exceptionType);
        }

        [Theory,
            InlineData(0, @"Error 0: The operation completed successfully. "),
            InlineData(2, @"Error 2: The system cannot find the file specified. "),
            InlineData(3, @"Error 3: The system cannot find the path specified. "),
            InlineData(123, @"Error 123: The filename, directory name, or volume label syntax is incorrect. ")]
        public void WindowsErrorTextIsAsExpected(uint error, string expected)
        {
            ErrorMethods.LastErrorToString(error).Should().Be(expected);
        }
    }
}
