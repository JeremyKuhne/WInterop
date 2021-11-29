// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Errors;
using WInterop.Windows;
using Xunit;

namespace ErrorHandlingTests;

public partial class Basic
{
    [Fact]
    public void BasicBeep()
    {
        Windows.Beep(frequency: 262, duration: 200);
    }

    [Theory,
        InlineData(BeepType.SimpleBeep),
        InlineData(BeepType.Ok),
        InlineData(BeepType.Question),
        InlineData(BeepType.Warning),
        InlineData(BeepType.Information),
        InlineData(BeepType.Error)
        ]
    public void BasicMessageBeep(BeepType type)
    {
        Windows.MessageBeep(type);
    }

    [Fact]
    public void GetProcessErrorMode()
    {
        ErrorMode mode = Error.GetProcessErrorMode();
    }

    [Fact]
    public void BasicThreadErrorMode()
    {
        ErrorMode mode = Error.GetThreadErrorMode();
        ErrorMode newMode = mode ^ ErrorMode.SEM_NOOPENFILEERRORBOX;
        ErrorMode oldMode = Error.SetThreadErrorMode(newMode);
        try
        {
            oldMode.Should().Be(mode);
            Error.GetThreadErrorMode().Should().Be(newMode);
        }
        finally
        {
            Error.SetThreadErrorMode(mode).Should().Be(newMode);
        }
    }

    [Theory,
        InlineData(NTStatus.STATUS_OBJECT_NAME_NOT_FOUND, WindowsError.ERROR_FILE_NOT_FOUND),
        InlineData(NTStatus.STATUS_IO_TIMEOUT, WindowsError.ERROR_SEM_TIMEOUT),
        InlineData(NTStatus.STATUS_NAME_TOO_LONG, WindowsError.ERROR_FILENAME_EXCED_RANGE),
        InlineData(NTStatus.STATUS_OBJECT_NAME_INVALID, WindowsError.ERROR_INVALID_NAME),
        InlineData(NTStatus.STATUS_INVALID_PARAMETER, WindowsError.ERROR_INVALID_PARAMETER)
        ]
    public void ConvertNtStatus(NTStatus status, WindowsError expected)
    {
        Error.NtStatusToWinError(status).Should().Be(expected);
    }
}
