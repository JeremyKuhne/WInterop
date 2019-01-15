// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using WInterop.Errors;
using Xunit;

namespace ErrorHandlingTests
{
    public partial class BasicTests
    {
        [Theory,
            InlineData(WindowsError.ERROR_FILE_NOT_FOUND, typeof(FileNotFoundException)),
            InlineData(WindowsError.ERROR_PATH_NOT_FOUND, typeof(DirectoryNotFoundException)),
            InlineData(WindowsError.ERROR_ACCESS_DENIED, typeof(UnauthorizedAccessException)),
            InlineData(WindowsError.ERROR_NETWORK_ACCESS_DENIED, typeof(UnauthorizedAccessException)),
            InlineData(WindowsError.ERROR_FILENAME_EXCED_RANGE, typeof(PathTooLongException)),
            InlineData(WindowsError.ERROR_INVALID_DRIVE, typeof(WInteropIOException)),
            InlineData(WindowsError.ERROR_OPERATION_ABORTED, typeof(OperationCanceledException)),
            InlineData(WindowsError.ERROR_NOT_READY, typeof(DriveNotReadyException)),
            InlineData(WindowsError.FVE_E_LOCKED_VOLUME, typeof(DriveLockedException)),
            InlineData(WindowsError.ERROR_ALREADY_EXISTS, typeof(FileExistsException)),
            InlineData(WindowsError.ERROR_SHARING_VIOLATION, typeof(WInteropIOException)),
            InlineData(WindowsError.ERROR_FILE_EXISTS, typeof(FileExistsException))
            ]
        public void ErrorsMapToExceptions(WindowsError error, Type exceptionType)
        {
            error.GetException().Should().BeOfType(exceptionType);
        }

        [Theory,
            InlineData(0, @"ERROR_SUCCESS (0): The operation completed successfully. "),
            InlineData(2, @"ERROR_FILE_NOT_FOUND (2): The system cannot find the file specified. "),
            InlineData(3, @"ERROR_PATH_NOT_FOUND (3): The system cannot find the path specified. "),
            InlineData(123, @"ERROR_INVALID_NAME (123): The filename, directory name, or volume label syntax is incorrect. ")
            ]
        public void WindowsErrorTextIsAsExpected(uint error, string expected)
        {
            Error.LastErrorToString((WindowsError)error).Should().Be(expected);
        }

        [Theory,
            InlineData(WindowsError.ERROR_ACCESS_DENIED, HResult.E_ACCESSDENIED),
            // This GetLastError() is already an HRESULT
            InlineData(WindowsError.FVE_E_LOCKED_VOLUME, unchecked((HResult)WindowsError.FVE_E_LOCKED_VOLUME))
            ]
        public void WindowsErrorToHresultMappings(WindowsError error, HResult expected)
        {
            HResult result = error.ToHResult();
            result.Should().Be(expected);
        }
    }
}
