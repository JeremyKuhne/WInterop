// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Linq;
using Tests.Support;
using WInterop.DirectoryManagement;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using WInterop.Support;
using Xunit;

namespace DesktopTests.FileManagement
{
    public class FileManagementTests
    {
        [Fact]
        public void GetShortPathBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            FileMethods.GetShortPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetLongPathBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            FileMethods.GetLongPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void FinalPathNameVolumeNameBehavior()
        {
            // This test is asserting that the original volume name has nothing to do with the volume GetFinalPathNameByHandle returns
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.CreateTestFile("FinalPathNameVolumeNameBehavior");

                using (var handle = FileMethods.CreateFile(filePath.ToLower(), CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
                {
                    handle.IsInvalid.Should().BeFalse();

                    string extendedPath = @"\\?\" + filePath;
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED)
                        .Should().Be(extendedPath);
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_OPENED)
                        .Should().Be(extendedPath);
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_DOS)
                        .Should().Be(extendedPath);
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_GUID)
                        .Should().StartWith(@"\\?\Volume");
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NT)
                        .Should().StartWith(@"\Device\");
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NONE)
                        .Should().Be(filePath.Substring(2));
                }
            }
        }

        [Fact]
        public void FinalPathNameBasic()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.CreateTestFile("FinalPathNameBehavior");

                using (var handle = FileMethods.CreateFile(filePath.ToLower(), CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
                {
                    handle.IsInvalid.Should().BeFalse();

                    string extendedPath = @"\\?\" + filePath;
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED)
                        .Should().Be(extendedPath);
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_OPENED)
                        .Should().Be(extendedPath);
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_DOS)
                        .Should().Be(extendedPath);
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_GUID)
                        .Should().StartWith(@"\\?\Volume");
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NT)
                        .Should().StartWith(@"\Device\");
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NONE)
                        .Should().Be(filePath.Substring(2));
                }
            }
        }

        [Fact]
        public void FinalPathNameLongPath()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                string filePath = Paths.Combine(longPath, System.IO.Path.GetRandomFileName());

                FileHelper.CreateDirectoryRecursive(longPath);
                FileHelper.WriteAllText(filePath, "FinalPathNameLongPathPrefixRoundTripBehavior");

                using (var handle = FileMethods.CreateFile(filePath, CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
                {
                    handle.IsInvalid.Should().BeFalse();

                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED)
                        .Should().Be(filePath);
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_OPENED)
                        .Should().Be(filePath);
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_DOS)
                        .Should().Be(filePath);
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_GUID)
                        .Should().StartWith(@"\\?\Volume");
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NT)
                        .Should().StartWith(@"\Device\");
                    FileMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NONE)
                        .Should().Be(filePath.Substring(6));
                }
            }
        }

        [Fact]
        public void FinalPathNameFromPath()
        {
            string tempPath = FileMethods.GetTempPath();
            string lowerTempPath = tempPath.ToLowerInvariant();
            tempPath.Should().NotBe(lowerTempPath);
            FileMethods.GetFinalPathName(lowerTempPath, 0, false).Should().Be(@"\\?\" + Paths.TrimTrailingSeparators(tempPath));
        }

        [Fact]
        public void GetFileAttributesLongPath()
        {
            string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(@"C:\", 300);
            Action action = () => FileMethods.GetFileAttributes(longPath);
            action.Should().Throw<System.IO.DirectoryNotFoundException>();
        }

        [Fact]
        public void CreateFileRawParition()
        {
            // You can't open with read/write access unless running elevated
            using (var file = FileMethods.CreateFile(@"\\?\GLOBALROOT\Device\Harddisk0\Partition0", CreationDisposition.OpenExisting, 0))
            {
                file.IsInvalid.Should().BeFalse();
                FileMethods.GetFileType(file).Should().Be(FileType.Disk);
            }
        }

        [Fact(Skip = "Need to look up an available path")]
        public void CreateFileHarddiskVolume()
        {
            using (var file = FileMethods.CreateFile(@"\\?\GLOBALROOT\Device\HarddiskVolume1", CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
            {
                file.IsInvalid.Should().BeFalse();
                FileMethods.GetFileType(file).Should().Be(FileType.Disk);
            }
        }

        [Fact(Skip = "Need to add parsing for existing COM ports")]
        public void CreateFileComPort()
        {
            using (var file = FileMethods.CreateFile(@"COM4", CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
            {
                file.IsInvalid.Should().BeFalse();
                FileMethods.GetFileType(file).Should().Be(FileType.Character);
            }
        }

        [Fact]
        public void GetFileNameBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = DirectoryMethods.CreateDirectoryHandle(tempPath))
            {
                // This will give back the local path (minus the device, eg \Users\... or \Server\Share\...)
                string name = FileMethods.GetFileName(directory);

                tempPath.Should().EndWith(Paths.AddTrailingSeparator(name));
            }
        }

        [Fact]
        public void GetVolumeNameBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = DirectoryMethods.CreateDirectoryHandle(tempPath))
            {
                // This will give back the NT volume path (\Device\HarddiskVolumen\)
                try
                {
                    string name = FileMethods.GetVolumeName(directory);
                    name.Should().StartWith(@"\Device\");
                }
                catch (NotImplementedException)
                {
                    // Needs Windows 10
                    System.Environment.OSVersion.Version.Major.Should().BeLessThan(10);
                }
            }
        }

        [Fact]
        public void GetShortNameBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = DirectoryMethods.CreateDirectoryHandle(tempPath))
            {
                // This will give back the NT volume path (\Device\HarddiskVolumen\)
                string directoryName = FileMethods.GetShortName(directory);
                directoryName.Should().Be("Temp");

                string tempFileName = "ExtraLongName" + System.IO.Path.GetRandomFileName();
                string tempFilePath = System.IO.Path.Combine(tempPath, tempFileName);
                try
                {
                    using (var file = FileMethods.CreateFile(tempFilePath, CreationDisposition.CreateNew, DesiredAccess.GenericRead))
                    {
                        string fileName = FileMethods.GetShortName(file);
                        fileName.Length.Should().BeLessOrEqualTo(12);
                    }
                }
                finally
                {
                    FileMethods.DeleteFile(tempFilePath);
                }
            }
        }

        [Fact]
        public void FileModeSynchronousFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                using (var file = FileMethods.CreateFile(filePath, CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0))
                {
                    file.IsInvalid.Should().BeFalse();
                    var mode = FileMethods.GetFileMode(file);
                    mode.Should().HaveFlag(FileAccessModes.SynchronousNotAlertable);
                }
            }
        }

        [Fact]
        public void FileModeAsynchronousFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                using (var file = FileMethods.CreateFile(filePath, CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0,
                    FileAttributes.None, FileFlags.Overlapped))
                {
                    file.IsInvalid.Should().BeFalse();
                    var mode = FileMethods.GetFileMode(file);
                    mode.Should().NotHaveFlag(FileAccessModes.SynchronousAlertable);
                    mode.Should().NotHaveFlag(FileAccessModes.SynchronousNotAlertable);
                }
            }
        }

        [Fact]
        public void GetDirectoryFilenames_SpecialDirectories()
        {
            // The "." and ".." entries returned vary quite a bit
            // (they seem to variant over multiple runs too??)
            using (var handle = DirectoryMethods.CreateDirectoryHandle(@"C:\"))
            {
                string[] names = FileMethods.GetDirectoryFilenames(handle).ToArray();
                names.Should().NotContain(".");
                names.Should().NotContain("..");
            }

            using (var handle = DirectoryMethods.CreateDirectoryHandle(FileMethods.GetTempPath()))
            {
                string[] names = FileMethods.GetDirectoryFilenames(handle).ToArray();
                names.Should().Contain(".");
                names.Should().NotContain("..");
            }

            using (var cleaner = new TestFileCleaner())
            {
                string directory = cleaner.GetTestPath();
                DirectoryMethods.CreateDirectory(directory);
                using (var handle = DirectoryMethods.CreateDirectoryHandle(FileMethods.GetTempPath()))
                {
                    string[] names = FileMethods.GetDirectoryFilenames(handle).ToArray();
                    names.Should().Contain(".");
                    names.Should().Contain("..");
                }
            }
        }
    }
}
