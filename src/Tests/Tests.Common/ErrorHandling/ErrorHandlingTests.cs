// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.Support;
using Xunit;

namespace Tests.ErrorHandling
{
    public partial class ErrorHandlingTests
    {
        [Theory,
            InlineData(WindowsError.ERROR_FILE_NOT_FOUND, typeof(FileNotFoundException)),
            InlineData(WindowsError.ERROR_PATH_NOT_FOUND, typeof(DirectoryNotFoundException)),
            InlineData(WindowsError.ERROR_ACCESS_DENIED, typeof(UnauthorizedAccessException)),
            InlineData(WindowsError.ERROR_NETWORK_ACCESS_DENIED, typeof(UnauthorizedAccessException)),
            InlineData(WindowsError.ERROR_FILENAME_EXCED_RANGE, typeof(PathTooLongException)),
            InlineData(WindowsError.ERROR_INVALID_DRIVE, typeof(IOException)),
            InlineData(WindowsError.ERROR_OPERATION_ABORTED, typeof(OperationCanceledException)),
            InlineData(WindowsError.ERROR_NOT_READY, typeof(DriveNotReadyException)),
            InlineData(WindowsError.FVE_E_LOCKED_VOLUME, typeof(DriveLockedException)),
            InlineData(WindowsError.ERROR_ALREADY_EXISTS, typeof(IOException)),
            InlineData(WindowsError.ERROR_SHARING_VIOLATION, typeof(IOException)),
            InlineData(WindowsError.ERROR_FILE_EXISTS, typeof(IOException))
            ]
        public void ErrorsMapToExceptions(WindowsError error, Type exceptionType)
        {
            Errors.GetIoExceptionForError(error).Should().BeOfType(exceptionType);
        }

        [Theory,
            InlineData(0, @"ERROR_SUCCESS (0): The operation completed successfully. "),
            InlineData(2, @"ERROR_FILE_NOT_FOUND (2): The system cannot find the file specified. "),
            InlineData(3, @"ERROR_PATH_NOT_FOUND (3): The system cannot find the path specified. "),
            InlineData(123, @"ERROR_INVALID_NAME (123): The filename, directory name, or volume label syntax is incorrect. ")
            ]
        public void WindowsErrorTextIsAsExpected(uint error, string expected)
        {
            Errors.LastErrorToString((WindowsError)error).Should().Be(expected);
        }

        [Theory,
            InlineData(WindowsError.ERROR_ACCESS_DENIED, HRESULT.E_ACCESSDENIED),
            // This GetLastError() is already an HRESULT
            InlineData(WindowsError.FVE_E_LOCKED_VOLUME, unchecked((HRESULT)WindowsError.FVE_E_LOCKED_VOLUME))
            ]
        public void WindowsErrorToHresultMappings(WindowsError error, HRESULT expected)
        {
            HRESULT result = ErrorMacros.HRESULT_FROM_WIN32(error);
            result.Should().Be(expected);
        }
    }
}
