// ------------------------
//    WInterop Framework
// ------------------------

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

namespace StorageTests
{
    public class Behaviors
    {
        [Fact]
        public void OpenFileWithTrailingSeparator()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string testFile = cleaner.CreateTestFile(nameof(OpenFileWithTrailingSeparator));

                string fullName = Storage.GetFullPathName(Paths.AddTrailingSeparator(testFile));

                FindOperation<string> find = new FindOperation<string>(testFile);
                Action action = () => find.FirstOrDefault();
                action.Should().Throw<ArgumentException>().And.HResult.Should().Be((int)WindowsError.ERROR_INVALID_PARAMETER.ToHResult());

                action = () => Storage.CreateFile(Paths.AddTrailingSeparator(testFile), CreationDisposition.OpenExisting, DesiredAccess.ReadAttributes);
                action.Should().Throw<WInteropIOException>().And.HResult.Should().Be((int)WindowsError.ERROR_INVALID_NAME.ToHResult());
            }
        }

        [Theory,
            Trait("Environment", "CurrentDirectory"),
            InlineData(@"C:", @"C:\Users"),
            InlineData(@"C", @"D:\C")
            ]
        public void ValidateKnownRelativeBehaviors(string value, string expected)
        {
            // TODO: Need to modify to work with actually present drives and skip if there
            // isn't more than one.

            // Set the current directory to D: and the hidden env for C:'s last current directory
            Processes.SetEnvironmentVariable(@"=C:", @"C:\Users");
            using (new TempCurrentDirectory(@"D:\"))
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
            SafeFileHandle handle = Storage.CreateFile(
                path,
                CreationDisposition.OpenExisting,
                0,
                ShareModes.ReadWrite,
                FileAttributes.None,
                FileFlags.BackupSemantics);

            handle.IsInvalid.Should().BeFalse();
        }

        [Fact]
        public void CopyFileExNonExistantBehaviors()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string source = cleaner.GetTestPath();
                string destination = cleaner.GetTestPath();

                Action action = () => Storage.CopyFileEx(source, destination);
                action.Should().Throw<System.IO.FileNotFoundException>();

                source = Paths.Combine(source, "file");
                action.Should().Throw<System.IO.DirectoryNotFoundException>();
            }
        }

        [Fact]
        public void CreateFileWithTrailingForwardSlash()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string path = @"\\?\" + cleaner.GetTestPath() + "/";
                Action action = () => Storage.CreateFile(
                    path,
                    CreationDisposition.OpenExisting,
                    0,
                    ShareModes.ReadWrite,
                    FileAttributes.None,
                    FileFlags.BackupSemantics);

                action.Should().Throw<WInteropIOException>().And.HResult.Should().Be(unchecked((int)0x8007007B));
            }
        }

        [Fact]
        public void File_CopyNonExistantBehaviors()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string source = cleaner.GetTestPath();
                string destination = cleaner.GetTestPath();

                Action action = () => System.IO.File.Copy(source, destination);
                action.Should().Throw<System.IO.FileNotFoundException>();

                source = Paths.Combine(source, "file");
                action.Should().Throw<System.IO.DirectoryNotFoundException>();
            }
        }

        [Theory, MemberData(nameof(DosMatchData))]
        public void IsNameInExpression(string expression, string name, bool ignoreCase, bool expected)
        {
            Storage.IsNameInExpression(expression, name, ignoreCase).Should().Be(expected,
                $"'{expression ?? "<null>"}' in '{name ?? "<null>"}' with ignoreCase of {ignoreCase}");
        }

        public static TheoryData<string, string, bool, bool> DosMatchData => Tests.File.DosMatcherTests.DosMatchData;
    }
}
