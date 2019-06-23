// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using System.Linq;
using Tests.Support;
using WInterop.Shell;
using WInterop.Storage;
using WInterop.Support;
using Xunit;

namespace StorageTests
{
    public class Basic
    {
        [Fact]
        public void GetShortPathBasic()
        {
            string tempPath = Storage.GetTempPath();
            Storage.GetShortPathName(tempPath).Should().NotBeNullOrWhiteSpace();

            string programFiles = ShellMethods.GetKnownFolderPath(KnownFolderIds.ProgramFiles);
            Storage.GetShortPathName(programFiles).Should().Contain("~");
        }

        [Fact]
        public void GetLongPathBasic()
        {
            string tempPath = Storage.GetTempPath();
            Storage.GetLongPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void FinalPathNameVolumeNameBehavior()
        {
            // This test is asserting that the original volume name has nothing to do with the volume GetFinalPathNameByHandle returns
            using var cleaner = new TestFileCleaner();
            string filePath = cleaner.CreateTestFile("FinalPathNameVolumeNameBehavior");

            using (var handle = Storage.CreateFile(filePath.ToLower(), CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
            {
                handle.IsInvalid.Should().BeFalse();

                string extendedPath = @"\\?\" + filePath;
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FileNameNormalized)
                    .Should().Be(extendedPath);
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FileNameOpened)
                    .Should().Be(extendedPath);
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VolumeNameDos)
                    .Should().Be(extendedPath);
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VolumeNameGuid)
                    .Should().StartWith(@"\\?\Volume");
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VolumeNameNt)
                    .Should().StartWith(@"\Device\");
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VolumeNameNone)
                    .Should().Be(filePath.Substring(2));
            }
        }

        [Fact]
        public void FinalPathNameBasic()
        {
            using var cleaner = new TestFileCleaner();
            string filePath = cleaner.CreateTestFile("FinalPathNameBehavior");

            using (var handle = Storage.CreateFile(filePath.ToLower(), CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
            {
                handle.IsInvalid.Should().BeFalse();

                string extendedPath = @"\\?\" + filePath;
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FileNameNormalized)
                    .Should().Be(extendedPath);
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FileNameOpened)
                    .Should().Be(extendedPath);
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VolumeNameDos)
                    .Should().Be(extendedPath);
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VolumeNameGuid)
                    .Should().StartWith(@"\\?\Volume");
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VolumeNameNt)
                    .Should().StartWith(@"\Device\");
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VolumeNameNone)
                    .Should().Be(filePath.Substring(2));
            }
        }

        [Fact]
        public void FinalPathNameLongPath()
        {
            using var cleaner = new TestFileCleaner();
            string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
            string filePath = Path.Join(longPath, System.IO.Path.GetRandomFileName());

            FileHelper.CreateDirectoryRecursive(longPath);
            FileHelper.WriteAllText(filePath, "FinalPathNameLongPathPrefixRoundTripBehavior");

            using (var handle = Storage.CreateFile(filePath, CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
            {
                handle.IsInvalid.Should().BeFalse();

                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FileNameNormalized)
                    .Should().Be(filePath);
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FileNameOpened)
                    .Should().Be(filePath);
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VolumeNameDos)
                    .Should().Be(filePath);
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VolumeNameGuid)
                    .Should().StartWith(@"\\?\Volume");
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VolumeNameNt)
                    .Should().StartWith(@"\Device\");
                Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VolumeNameNone)
                    .Should().Be(filePath.Substring(6));
            }
        }

        [Fact]
        public void FinalPathNameFromPath()
        {
            string tempPath = Storage.GetTempPath();
            string lowerTempPath = tempPath.ToLowerInvariant();
            tempPath.Should().NotBe(lowerTempPath);
            Storage.GetFinalPathName(lowerTempPath, 0, false).Should().Be(@"\\?\" + Paths.TrimTrailingSeparators(tempPath));
        }

        [Fact]
        public void GetFileAttributesLongPath()
        {
            string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(@"C:\", 300);
            Action action = () => Storage.GetFileAttributes(longPath);
            action.Should().Throw<DirectoryNotFoundException>();
        }

        [Fact]
        public void CreateFileRawParition()
        {
            // You can't open with read/write access unless running elevated
            using (var file = Storage.CreateFile(@"\\?\GLOBALROOT\Device\Harddisk0\Partition0", CreationDisposition.OpenExisting, 0))
            {
                file.IsInvalid.Should().BeFalse();
                Storage.GetFileType(file).Should().Be(FileType.Disk);
            }
        }

        [Fact(Skip = "Need to look up an available path")]
        public void CreateFileHarddiskVolume()
        {
            using (var file = Storage.CreateFile(@"\\?\GLOBALROOT\Device\HarddiskVolume1", CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
            {
                file.IsInvalid.Should().BeFalse();
                Storage.GetFileType(file).Should().Be(FileType.Disk);
            }
        }

        [Fact(Skip = "Need to add parsing for existing COM ports")]
        public void CreateFileComPort()
        {
            using (var file = Storage.CreateFile(@"COM4", CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
            {
                file.IsInvalid.Should().BeFalse();
                Storage.GetFileType(file).Should().Be(FileType.Character);
            }
        }

        [Fact]
        public void GetFileNameBasic()
        {
            string tempPath = Storage.GetTempPath();
            using (var directory = Storage.CreateDirectoryHandle(tempPath))
            {
                // This will give back the local path (minus the device, eg \Users\... or \Server\Share\...)
                string name = Storage.GetFileName(directory);

                tempPath.Should().EndWith(Paths.AddTrailingSeparator(name));
            }
        }

        [Fact]
        public void GetVolumeNameBasic()
        {
            string tempPath = Storage.GetTempPath();
            using (var directory = Storage.CreateDirectoryHandle(tempPath))
            {
                // This will give back the NT volume path (\Device\HarddiskVolumen\)
                try
                {
                    string name = Storage.GetVolumeName(directory);
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
            string tempPath = Storage.GetTempPath();
            using (var directory = Storage.CreateDirectoryHandle(tempPath))
            {
                // This will give back the NT volume path (\Device\HarddiskVolumen\)
                string directoryName = Storage.GetShortName(directory);
                directoryName.Should().Be("Temp");

                string tempFileName = "ExtraLongName" + System.IO.Path.GetRandomFileName();
                string tempFilePath = System.IO.Path.Combine(tempPath, tempFileName);
                try
                {
                    using (var file = Storage.CreateFile(tempFilePath, CreationDisposition.CreateNew, DesiredAccess.GenericRead))
                    {
                        string fileName = Storage.GetShortName(file);
                        fileName.Length.Should().BeLessOrEqualTo(12);
                    }
                }
                finally
                {
                    Storage.DeleteFile(tempFilePath);
                }
            }
        }

        [Fact]
        public void FileModeSynchronousFile()
        {
            using var cleaner = new TestFileCleaner();
            string filePath = cleaner.GetTestPath();
            using (var file = Storage.CreateFile(filePath, CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0))
            {
                file.IsInvalid.Should().BeFalse();
                var mode = Storage.GetFileMode(file);
                mode.Should().HaveFlag(FileAccessModes.SynchronousNotAlertable);
            }
        }

        [Fact]
        public void FileModeAsynchronousFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                using (var file = Storage.CreateFile(filePath, CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0,
                    AllFileAttributes.None, FileFlags.Overlapped))
                {
                    file.IsInvalid.Should().BeFalse();
                    var mode = Storage.GetFileMode(file);
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
            using (var handle = Storage.CreateDirectoryHandle(@"C:\"))
            {
                string[] names = Storage.GetDirectoryFilenames(handle).ToArray();
                names.Should().NotContain(".");
                names.Should().NotContain("..");
            }

            using (var handle = Storage.CreateDirectoryHandle(Storage.GetTempPath()))
            {
                string[] names = Storage.GetDirectoryFilenames(handle).ToArray();
                names.Should().Contain(".");
                names.Should().NotContain("..");
            }

            using (var cleaner = new TestFileCleaner())
            {
                string directory = cleaner.GetTestPath();
                Storage.CreateDirectory(directory);
                using (var handle = Storage.CreateDirectoryHandle(Storage.GetTempPath()))
                {
                    string[] names = Storage.GetDirectoryFilenames(handle).ToArray();
                    names.Should().Contain(".");
                    names.Should().Contain("..");
                }
            }
        }
    }
}
