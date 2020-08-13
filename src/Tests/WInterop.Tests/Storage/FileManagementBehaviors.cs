// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Microsoft.Win32.SafeHandles;
using System;
using System.Linq;
using Tests.Support;
using WInterop.Errors;
using WInterop.Storage;
using WInterop.ProcessAndThreads;
using WInterop.Support;
using Xunit;
using System.IO;

namespace StorageTests
{
    public class Behaviors
    {
        [Fact(Skip = "needtoupdate")]
        public void FindFileWithTrailingSeparator()
        {
            using var cleaner = new TestFileCleaner();
            string testFile = cleaner.CreateTestFile(nameof(FindFileWithTrailingSeparator));

            string fullName = Storage.GetFullPathName(Paths.AddTrailingSeparator(testFile));
        }

        [Theory,
            InlineData('\0', (LogicalDrives)0),
            InlineData(' ', (LogicalDrives)0),
            InlineData('[', (LogicalDrives)0),
            InlineData('a', LogicalDrives.A),
            InlineData('A', LogicalDrives.A),
            InlineData('Z', LogicalDrives.Z),
            ]
        public void ConvertDriveLetter(char letter, LogicalDrives expectedDrive)
        {
            Storage.GetLogicalDrive(letter).Should().Be(expectedDrive);
        }

        [Theory,
            InlineData(@"C:", @"C:\Users"),
            InlineData(@"C", @"*:\C")
            ]
        public void ValidateKnownRelativeBehaviors(string value, string expected)
        {
            char testDrive = Storage.GetNextAvailableDrive('C');
            if (testDrive == '\0')
            {
                // No additional drives for testing the behavior here, skip
                return;
            }

            expected = expected.Replace('*', testDrive);

            // Set the current directory to the testdrive and the hidden environment
            // variable for C:'s last current directory
            Processes.SetEnvironmentVariable(@"=C:", @"C:\Users");
            using (new TempCurrentDirectory(testDrive + @":\"))
            {
                Storage.GetFullPathName(value).Should().Be(expected);
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
                Storage.GetLongPathName(value).Should().Be(expected);
            }
        }

        [Fact]
        public void LongPathNameThrowsFileNotFound()
        {
            string path = System.IO.Path.GetRandomFileName();
            Action action = () => Storage.GetLongPathName(path);
            action.Should().Throw<System.IO.FileNotFoundException>();
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
            using SafeFileHandle handle = Storage.CreateFile(
                path,
                CreationDisposition.OpenExisting,
                0,
                ShareModes.ReadWrite,
                AllFileAttributes.None,
                FileFlags.BackupSemantics);
            handle.IsInvalid.Should().BeFalse();
        }

        [Fact]
        public void CopyFileExNonExistantBehaviors()
        {
            using var cleaner = new TestFileCleaner();
            string source = cleaner.GetTestPath();
            string destination = cleaner.GetTestPath();

            Action action = () => Storage.CopyFileEx(source, destination);
            action.Should().Throw<System.IO.FileNotFoundException>();

            source = Path.Join(source, "file");
            action.Should().Throw<System.IO.DirectoryNotFoundException>();
        }

        [Fact]
        public void CreateFileWithTrailingForwardSlash()
        {
            using var cleaner = new TestFileCleaner();
            string path = @"\\?\" + cleaner.GetTestPath() + "/";
            Action action = () => Storage.CreateFile(
                path,
                CreationDisposition.OpenExisting,
                0,
                ShareModes.ReadWrite,
                AllFileAttributes.None,
                FileFlags.BackupSemantics);

            action.Should().Throw<WInteropIOException>().And.HResult.Should().Be(unchecked((int)0x8007007B));
        }

        [Fact]
        public void File_CopyNonExistantBehaviors()
        {
            using var cleaner = new TestFileCleaner();
            string source = cleaner.GetTestPath();
            string destination = cleaner.GetTestPath();

            Action action = () => System.IO.File.Copy(source, destination);
            action.Should().Throw<System.IO.FileNotFoundException>();

            source = Path.Join(source, "file");
            action.Should().Throw<System.IO.DirectoryNotFoundException>();
        }

        [Theory, MemberData(nameof(DosMatchData))]
        public void IsNameInExpression(string expression, string name, bool ignoreCase, bool expected)
        {
            Storage.IsNameInExpression(expression, name, ignoreCase).Should().Be(expected,
                $"'{expression ?? "<null>"}' in '{name ?? "<null>"}' with ignoreCase of {ignoreCase}");
        }

        public static TheoryData<string, string, bool, bool> DosMatchData => DosMatcherTests.DosMatchData;
    }
}
