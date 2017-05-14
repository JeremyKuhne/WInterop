// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Microsoft.Win32.SafeHandles;
using System;
using Tests.Support;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using WInterop.ProcessAndThreads;
using Xunit;

namespace DesktopTests.FileManagementTests
{
    public class FileManagementBehaviors
    {
        [Theory,
            Trait("Environment", "CurrentDirectory"),
            InlineData(@"C:", @"C:\Users"),
            InlineData(@"C", @"E:\C")
            ]
        public void ValidateKnownRelativeBehaviors(string value, string expected)
        {
            // TODO: Need to modify to work with actually present drives and skip if there
            // isn't more than one.

            // Set the current directory to D: and the hidden env for C:'s last current directory
            ProcessMethods.SetEnvironmentVariable(@"=C:", @"C:\Users");
            using (new TempCurrentDirectory(@"E:\"))
            {
                FileMethods.GetFullPathName(value).Should().Be(expected);
            }
        }

        [Theory,
            InlineData(@"C:\PROGRA~1", @"C:\Program Files"),
            InlineData(@"C:\.\PROGRA~1", @"C:\.\Program Files"),
            ]
        public void ValidateLongPathNameBehaviors(string value, string expected)
        {
            using (new TempCurrentDirectory(@"C:\Users"))
            {
                FileMethods.GetLongPathName(value).Should().Be(expected);
            }
        }

        [Fact]
        public void LongPathNameThrowsFileNotFound()
        {
            string path = System.IO.Path.GetRandomFileName();
            Action action = () => FileMethods.GetLongPathName(path);
            action.ShouldThrow<System.IO.FileNotFoundException>();
        }

        [Theory,
            InlineData(@"C:"),
            InlineData(@"C:\"),
            InlineData(@"C:\."),
            InlineData(@"\\.\C:\"),
            InlineData(@"\\?\C:\"),
            InlineData(@"\\.\C:"),
            InlineData(@"\\?\C:"),
            ]
        public void CreateFileOnDriveRoot(string path)
        {
            SafeFileHandle handle = FileMethods.CreateFile(
                path,
                0,
                ShareMode.ReadWrite,
                CreationDisposition.OpenExisting,
                FileAttributes.NONE,
                FileFlags.FILE_FLAG_BACKUP_SEMANTICS);

            handle.IsInvalid.Should().BeFalse();
        }
    }
}
