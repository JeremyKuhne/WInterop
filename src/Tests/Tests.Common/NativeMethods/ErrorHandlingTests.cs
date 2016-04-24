// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Tests.NativeMethodTests
{
    using ErrorHandling;
    using FluentAssertions;
    using System;
    using System.IO;
    using Xunit;

    public class ErrorHandlingTests
    {
        [Theory
            InlineData(WinErrors.ERROR_FILE_NOT_FOUND, typeof(FileNotFoundException))
            InlineData(WinErrors.ERROR_PATH_NOT_FOUND, typeof(DirectoryNotFoundException))
            InlineData(WinErrors.ERROR_ACCESS_DENIED, typeof(UnauthorizedAccessException))
            InlineData(WinErrors.ERROR_NETWORK_ACCESS_DENIED, typeof(UnauthorizedAccessException))
            InlineData(WinErrors.ERROR_FILENAME_EXCED_RANGE, typeof(PathTooLongException))
#if PORTABLE
            InlineData(WinErrors.ERROR_INVALID_DRIVE, typeof(IOException))
#else
            InlineData(WinErrors.ERROR_INVALID_DRIVE, typeof(DriveNotFoundException))
#endif
            InlineData(WinErrors.ERROR_OPERATION_ABORTED, typeof(OperationCanceledException))
            InlineData(WinErrors.ERROR_NOT_READY, typeof(DriveNotReadyException))
            InlineData(WinErrors.FVE_E_LOCKED_VOLUME, typeof(DriveLockedException))
            InlineData(WinErrors.ERROR_ALREADY_EXISTS, typeof(IOException))
            InlineData(WinErrors.ERROR_SHARING_VIOLATION, typeof(IOException))
            InlineData(WinErrors.ERROR_FILE_EXISTS, typeof(IOException))
            ]
        public void ErrorsMapToExceptions(uint error, Type exceptionType)
        {
            NativeMethods.ErrorHandling.GetIoExceptionForError(error).Should().BeOfType(exceptionType);
        }

#if DESKTOP
        [Fact]
        public void BasicBeep()
        {
            NativeMethods.ErrorHandling.Beep(frequency: 262, duration: 500);
        }

        [Theory
            InlineData(MessageBeepType.SimpleBeep)
            InlineData(MessageBeepType.MB_OK)
            InlineData(MessageBeepType.MB_ICONQUESTION)
            InlineData(MessageBeepType.MB_ICONWARNING)
            InlineData(MessageBeepType.MB_ICONASTERISK)
            InlineData(MessageBeepType.MB_ICONERROR)
            ]
        public void BasicMessageBeep(MessageBeepType type)
        {
            NativeMethods.ErrorHandling.MessageBeep(type);
        }

        [Fact]
        public void GetProcessErrorMode()
        {
            ErrorMode mode = NativeMethods.ErrorHandling.GetProcessErrorMode();
        }

        [Fact]
        public void BasicThreadErrorMode()
        {
            ErrorMode mode = NativeMethods.ErrorHandling.GetThreadErrorMode();
            ErrorMode newMode = mode ^ ErrorMode.SEM_NOOPENFILEERRORBOX;
            ErrorMode oldMode = NativeMethods.ErrorHandling.SetThreadErrorMode(newMode);
            try
            {
                oldMode.Should().Be(mode);
                NativeMethods.ErrorHandling.GetThreadErrorMode().Should().Be(newMode);
            }
            finally
            {
                NativeMethods.ErrorHandling.SetThreadErrorMode(mode).Should().Be(newMode);
            }
        }
#endif
    }
}
