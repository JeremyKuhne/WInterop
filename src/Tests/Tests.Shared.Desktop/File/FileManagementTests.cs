// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Linq;
using Tests.Support;
using WInterop.Storage;
using WInterop.Storage.Types;
using WInterop.Support;
using Xunit;

namespace DesktopTests.File
{
    public class FileManagementTests
    {
        [Fact]
        public void GetShortPathBasic()
        {
            string tempPath = StorageMethods.GetTempPath();
            StorageMethods.GetShortPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetLongPathBasic()
        {
            string tempPath = StorageMethods.GetTempPath();
            StorageMethods.GetLongPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void FinalPathNameVolumeNameBehavior()
        {
            // This test is asserting that the original volume name has nothing to do with the volume GetFinalPathNameByHandle returns
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.CreateTestFile("FinalPathNameVolumeNameBehavior");

                using (var handle = StorageMethods.CreateFile(filePath.ToLower(), CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
                {
                    handle.IsInvalid.Should().BeFalse();

                    string extendedPath = @"\\?\" + filePath;
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED)
                        .Should().Be(extendedPath);
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_OPENED)
                        .Should().Be(extendedPath);
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_DOS)
                        .Should().Be(extendedPath);
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_GUID)
                        .Should().StartWith(@"\\?\Volume");
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NT)
                        .Should().StartWith(@"\Device\");
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NONE)
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

                using (var handle = StorageMethods.CreateFile(filePath.ToLower(), CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
                {
                    handle.IsInvalid.Should().BeFalse();

                    string extendedPath = @"\\?\" + filePath;
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED)
                        .Should().Be(extendedPath);
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_OPENED)
                        .Should().Be(extendedPath);
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_DOS)
                        .Should().Be(extendedPath);
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_GUID)
                        .Should().StartWith(@"\\?\Volume");
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NT)
                        .Should().StartWith(@"\Device\");
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NONE)
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

                using (var handle = StorageMethods.CreateFile(filePath, CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
                {
                    handle.IsInvalid.Should().BeFalse();

                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED)
                        .Should().Be(filePath);
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_OPENED)
                        .Should().Be(filePath);
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_DOS)
                        .Should().Be(filePath);
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_GUID)
                        .Should().StartWith(@"\\?\Volume");
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NT)
                        .Should().StartWith(@"\Device\");
                    StorageMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NONE)
                        .Should().Be(filePath.Substring(6));
                }
            }
        }

        [Fact]
        public void FinalPathNameFromPath()
        {
            string tempPath = StorageMethods.GetTempPath();
            string lowerTempPath = tempPath.ToLowerInvariant();
            tempPath.Should().NotBe(lowerTempPath);
            StorageMethods.GetFinalPathName(lowerTempPath, 0, false).Should().Be(@"\\?\" + Paths.TrimTrailingSeparators(tempPath));
        }

        [Fact]
        public void GetFileAttributesLongPath()
        {
            string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(@"C:\", 300);
            Action action = () => StorageMethods.GetFileAttributes(longPath);
            action.Should().Throw<System.IO.DirectoryNotFoundException>();
        }

        [Fact]
        public void CreateFileRawParition()
        {
            // You can't open with read/write access unless running elevated
            using (var file = StorageMethods.CreateFile(@"\\?\GLOBALROOT\Device\Harddisk0\Partition0", CreationDisposition.OpenExisting, 0))
            {
                file.IsInvalid.Should().BeFalse();
                StorageMethods.GetFileType(file).Should().Be(FileType.Disk);
            }
        }

        [Fact(Skip = "Need to look up an available path")]
        public void CreateFileHarddiskVolume()
        {
            using (var file = StorageMethods.CreateFile(@"\\?\GLOBALROOT\Device\HarddiskVolume1", CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
            {
                file.IsInvalid.Should().BeFalse();
                StorageMethods.GetFileType(file).Should().Be(FileType.Disk);
            }
        }

        [Fact(Skip = "Need to add parsing for existing COM ports")]
        public void CreateFileComPort()
        {
            using (var file = StorageMethods.CreateFile(@"COM4", CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
            {
                file.IsInvalid.Should().BeFalse();
                StorageMethods.GetFileType(file).Should().Be(FileType.Character);
            }
        }

        [Fact]
        public void GetFileNameBasic()
        {
            string tempPath = StorageMethods.GetTempPath();
            using (var directory = StorageMethods.CreateDirectoryHandle(tempPath))
            {
                // This will give back the local path (minus the device, eg \Users\... or \Server\Share\...)
                string name = StorageMethods.GetFileName(directory);

                tempPath.Should().EndWith(Paths.AddTrailingSeparator(name));
            }
        }

        [Fact]
        public void GetVolumeNameBasic()
        {
            string tempPath = StorageMethods.GetTempPath();
            using (var directory = StorageMethods.CreateDirectoryHandle(tempPath))
            {
                // This will give back the NT volume path (\Device\HarddiskVolumen\)
                try
                {
                    string name = StorageMethods.GetVolumeName(directory);
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
            string tempPath = StorageMethods.GetTempPath();
            using (var directory = StorageMethods.CreateDirectoryHandle(tempPath))
            {
                // This will give back the NT volume path (\Device\HarddiskVolumen\)
                string directoryName = StorageMethods.GetShortName(directory);
                directoryName.Should().Be("Temp");

                string tempFileName = "ExtraLongName" + System.IO.Path.GetRandomFileName();
                string tempFilePath = System.IO.Path.Combine(tempPath, tempFileName);
                try
                {
                    using (var file = StorageMethods.CreateFile(tempFilePath, CreationDisposition.CreateNew, DesiredAccess.GenericRead))
                    {
                        string fileName = StorageMethods.GetShortName(file);
                        fileName.Length.Should().BeLessOrEqualTo(12);
                    }
                }
                finally
                {
                    StorageMethods.DeleteFile(tempFilePath);
                }
            }
        }

        [Fact]
        public void FileModeSynchronousFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                using (var file = StorageMethods.CreateFile(filePath, CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0))
                {
                    file.IsInvalid.Should().BeFalse();
                    var mode = StorageMethods.GetFileMode(file);
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
                using (var file = StorageMethods.CreateFile(filePath, CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0,
                    FileAttributes.None, FileFlags.Overlapped))
                {
                    file.IsInvalid.Should().BeFalse();
                    var mode = StorageMethods.GetFileMode(file);
                    mode.Should().NotHaveFlag(FileAccessModes.SynchronousAlertable);
                    mode.Should().NotHaveFlag(FileAccessModes.SynchronousNotAlertable);
                }
            }
        }

        [Fact(Skip = "The behavior here is seemingly random.")]
        public void GetDirectoryFilenames_SpecialDirectories()
        {
            // The "." and ".." entries returned vary quite a bit
            // (they seem to variant over multiple runs too??)
            using (var handle = StorageMethods.CreateDirectoryHandle(@"C:\"))
            {
                string[] names = StorageMethods.GetDirectoryFilenames(handle).ToArray();
                names.Should().NotContain(".");
                names.Should().NotContain("..");
            }

            using (var handle = StorageMethods.CreateDirectoryHandle(StorageMethods.GetTempPath()))
            {
                string[] names = StorageMethods.GetDirectoryFilenames(handle).ToArray();
                names.Should().Contain(".");
                names.Should().NotContain("..");
            }

            using (var cleaner = new TestFileCleaner())
            {
                string directory = cleaner.GetTestPath();
                StorageMethods.CreateDirectory(directory);
                using (var handle = StorageMethods.CreateDirectoryHandle(StorageMethods.GetTempPath()))
                {
                    string[] names = StorageMethods.GetDirectoryFilenames(handle).ToArray();
                    names.Should().Contain(".");
                    names.Should().Contain("..");
                }
            }
        }
    }
}
