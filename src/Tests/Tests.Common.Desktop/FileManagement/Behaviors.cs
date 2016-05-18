// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using Tests.Support;
using WInterop.FileManagement;
using WInterop.ProcessAndThreads;
using Xunit;

namespace WInterop.DesktopTests.FileManagementTests
{
    public class Behaviors
    {
        [Theory
            Trait("Environment", "CurrentDirectory")
            InlineData(@"C:", @"C:\Users")
            InlineData(@"C", @"D:\C")
            ]
        public void ValidateKnownRelativeBehaviors(string value, string expected)
        {
            // Set the current directory to D: and the hidden env for C:'s last current directory
            ProcessDesktopMethods.SetEnvironmentVariable(@"=C:", @"C:\Users");
            using (new TempCurrentDirectory(@"D:\"))
            {
                FileMethods.GetFullPathName(value).Should().Be(expected);
            }
        }

        [Theory
            InlineData(@"C:\PROGRA~1", @"C:\Program Files")
            InlineData(@"C:\.\PROGRA~1", @"C:\.\Program Files")
            ]
        public void ValidateLongPathNameBehaviors(string value, string expected)
        {
            using (new TempCurrentDirectory(@"C:\Users"))
            {
                FileDesktopMethods.GetLongPathName(value).Should().Be(expected);
            }
        }

        [Fact]
        public void LongPathNameThrowsFileNotFound()
        {
            string path = System.IO.Path.GetRandomFileName();
            Action action = () => FileDesktopMethods.GetLongPathName(path);
            action.ShouldThrow<System.IO.FileNotFoundException>();
        }
    }
}
