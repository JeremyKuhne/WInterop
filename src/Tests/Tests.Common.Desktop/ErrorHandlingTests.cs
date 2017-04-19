// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using Xunit;

namespace DesktopTests
{
    public partial class ErrorHandlingTests
    {
        [Fact]
        public void BasicBeep()
        {
            ErrorMethods.Beep(frequency: 262, duration: 500);
        }

        [Theory,
            InlineData(MessageBeepType.SimpleBeep),
            InlineData(MessageBeepType.MB_OK),
            InlineData(MessageBeepType.MB_ICONQUESTION),
            InlineData(MessageBeepType.MB_ICONWARNING),
            InlineData(MessageBeepType.MB_ICONASTERISK),
            InlineData(MessageBeepType.MB_ICONERROR)
            ]
        public void BasicMessageBeep(MessageBeepType type)
        {
            ErrorMethods.MessageBeep(type);
        }

        [Fact]
        public void GetProcessErrorMode()
        {
            ErrorMode mode = ErrorMethods.GetProcessErrorMode();
        }

        [Fact]
        public void BasicThreadErrorMode()
        {
            ErrorMode mode = ErrorMethods.GetThreadErrorMode();
            ErrorMode newMode = mode ^ ErrorMode.SEM_NOOPENFILEERRORBOX;
            ErrorMode oldMode = ErrorMethods.SetThreadErrorMode(newMode);
            try
            {
                oldMode.Should().Be(mode);
                ErrorMethods.GetThreadErrorMode().Should().Be(newMode);
            }
            finally
            {
                ErrorMethods.SetThreadErrorMode(mode).Should().Be(newMode);
            }
        }
    }
}
