// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Desktop;
using Xunit;

namespace WInterop.DesktopTests.NativeMethodTests
{
    public partial class ErrorHandlingTests
    {
        [Fact]
        public void BasicBeep()
        {
            ErrorHandling.Desktop.NativeMethods.Beep(frequency: 262, duration: 500);
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
            ErrorHandling.Desktop.NativeMethods.MessageBeep(type);
        }

        [Fact]
        public void GetProcessErrorMode()
        {
            ErrorMode mode = ErrorHandling.Desktop.NativeMethods.GetProcessErrorMode();
        }

        [Fact]
        public void BasicThreadErrorMode()
        {
            ErrorMode mode = ErrorHandling.Desktop.NativeMethods.GetThreadErrorMode();
            ErrorMode newMode = mode ^ ErrorMode.SEM_NOOPENFILEERRORBOX;
            ErrorMode oldMode = ErrorHandling.Desktop.NativeMethods.SetThreadErrorMode(newMode);
            try
            {
                oldMode.Should().Be(mode);
                ErrorHandling.Desktop.NativeMethods.GetThreadErrorMode().Should().Be(newMode);
            }
            finally
            {
                ErrorHandling.Desktop.NativeMethods.SetThreadErrorMode(mode).Should().Be(newMode);
            }
        }
    }
}
