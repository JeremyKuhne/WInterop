// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Desktop;
using Xunit;

namespace WInterop.DesktopTests
{
    public partial class ErrorHandlingTests
    {
        [Fact]
        public void BasicBeep()
        {
            ErrorDesktopMethods.Beep(frequency: 262, duration: 500);
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
            ErrorDesktopMethods.MessageBeep(type);
        }

        [Fact]
        public void GetProcessErrorMode()
        {
            ErrorMode mode = ErrorDesktopMethods.GetProcessErrorMode();
        }

        [Fact]
        public void BasicThreadErrorMode()
        {
            ErrorMode mode = ErrorDesktopMethods.GetThreadErrorMode();
            ErrorMode newMode = mode ^ ErrorMode.SEM_NOOPENFILEERRORBOX;
            ErrorMode oldMode = ErrorDesktopMethods.SetThreadErrorMode(newMode);
            try
            {
                oldMode.Should().Be(mode);
                ErrorDesktopMethods.GetThreadErrorMode().Should().Be(newMode);
            }
            finally
            {
                ErrorDesktopMethods.SetThreadErrorMode(mode).Should().Be(newMode);
            }
        }
    }
}
