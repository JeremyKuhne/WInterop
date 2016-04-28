// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.IO;
using System.Linq;
using Xunit;

namespace WInterop.Tests.NativeMethodTests
{
    public class FileManagementTests
    {
        [Fact]
        public void GetTempPathBasic()
        {
            NativeMethods.FileManagement.GetTempPath().Should().NotBeNullOrWhiteSpace();
        }

#if DESKTOP
        [Fact]
        public void GetShortPathBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            NativeMethods.FileManagement.Desktop.GetShortPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetLongPathBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            NativeMethods.FileManagement.Desktop.GetLongPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }
#endif

        [Fact]
        public void GetFullPathBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            NativeMethods.FileManagement.GetFullPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetTempFileNameBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = NativeMethods.FileManagement.GetTempFileName(tempPath, "tfn");
            try
            {
                tempFileName.Should().StartWith(tempPath);
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void DeleteFileBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = NativeMethods.FileManagement.GetTempFileName(tempPath, "tfn");
            try
            {
                File.Exists(tempFileName).Should().BeTrue();
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
                File.Exists(tempFileName).Should().BeFalse();
            }
        }

        [Fact]
        public void CreateFileBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = NativeMethods.FileManagement.GetTempFileName(tempPath, "tfn");
            try
            {
                using (var file = NativeMethods.FileManagement.CreateFile(tempFileName, FileAccess.Read, FileShare.ReadWrite, FileMode.Open))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void CreateFileCreateTempFile()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = Path.Combine(tempPath, Path.GetRandomFileName());
            try
            {
                using (var file = NativeMethods.FileManagement.CreateFile(tempFileName, FileAccess.Read, FileShare.ReadWrite, FileMode.Create))
                {
                    file.IsInvalid.Should().BeFalse();
                    File.Exists(tempFileName).Should().BeTrue();
                }
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetFileAttributesExBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            var info = NativeMethods.FileManagement.GetFileAttributesEx(tempPath);
            info.Attributes.Should().HaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);
        }

        [Fact]
        public void FlushFileBuffersBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = Path.Combine(tempPath, Path.GetRandomFileName());
            try
            {
                using (var file = NativeMethods.FileManagement.CreateFile(tempFileName, FileAccess.ReadWrite, FileShare.ReadWrite, FileMode.Create))
                {
                    NativeMethods.FileManagement.FlushFileBuffers(file);
                }
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetFileNameByHandleBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = Path.Combine(tempPath, Path.GetRandomFileName());
            try
            {
                using (var file = NativeMethods.FileManagement.CreateFile(tempFileName, FileAccess.ReadWrite, FileShare.ReadWrite, FileMode.Create))
                {
                    string fileName = NativeMethods.FileManagement.GetFileNameByHandle(file);
                    tempFileName.Should().EndWith(fileName);
                }
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetStandardInfoByHandleBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = Path.Combine(tempPath, Path.GetRandomFileName());
            try
            {
                using (var directory = NativeMethods.FileManagement.CreateFile(tempPath, FileAccess.Read, FileShare.ReadWrite, FileMode.Open,
                    FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    var info = NativeMethods.FileManagement.GetFileStandardInfoByHandle(directory);
                    info.Directory.Should().BeTrue();
                }

                using (var file = NativeMethods.FileManagement.CreateFile(tempFileName, FileAccess.ReadWrite, FileShare.ReadWrite, FileMode.Create))
                {
                    var info = NativeMethods.FileManagement.GetFileStandardInfoByHandle(file);
                    info.Directory.Should().BeFalse();
                    info.NumberOfLinks.Should().Be(1);
                    info.DeletePending.Should().BeFalse();
                    info.AllocationSize.Should().Be(0);
                    info.EndOfFile.Should().Be(0);
                }
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetBasicInfoByHandleBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = Path.Combine(tempPath, Path.GetRandomFileName());
            try
            {
                using (var directory = NativeMethods.FileManagement.CreateFile(tempPath, FileAccess.Read, FileShare.ReadWrite, FileMode.Open,
                    FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    var directoryInfo = NativeMethods.FileManagement.GetFileBasicInfoByHandle(directory);
                    directoryInfo.Attributes.Should().HaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);

                    using (var file = NativeMethods.FileManagement.CreateFile(tempFileName, FileAccess.ReadWrite, FileShare.ReadWrite, FileMode.Create))
                    {
                        var fileInfo = NativeMethods.FileManagement.GetFileBasicInfoByHandle(file);
                        fileInfo.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);
                        fileInfo.CreationTime.Should().BeAfter(directoryInfo.CreationTime);
                    }
                }
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetStreamInfoByHandleBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = Path.Combine(tempPath, Path.GetRandomFileName());
            try
            {
                using (var directory = NativeMethods.FileManagement.CreateFile(tempPath, FileAccess.Read, FileShare.ReadWrite, FileMode.Open,
                    FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    var directoryInfo = NativeMethods.FileManagement.GetStreamInformationByHandle(directory);
                    directoryInfo.Should().BeEmpty();

                    using (var file = NativeMethods.FileManagement.CreateFile(tempFileName, FileAccess.ReadWrite, FileShare.ReadWrite, FileMode.Create))
                    {
                        var fileInfo = NativeMethods.FileManagement.GetStreamInformationByHandle(file);
                        fileInfo.Should().HaveCount(1);
                        var info = fileInfo.First();
                        info.Name.Should().Be(@"::$DATA");
                        info.Size.Should().Be(0);
                        info.AllocationSize.Should().Be(0);
                    }
                }
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void SetFileAttributesBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = NativeMethods.FileManagement.GetTempFileName(tempPath, "tfn");
            try
            {
                var originalInfo = NativeMethods.FileManagement.GetFileAttributesEx(tempFileName);
                originalInfo.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                NativeMethods.FileManagement.SetFileAttributes(tempFileName, originalInfo.Attributes | FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                var newInfo = NativeMethods.FileManagement.GetFileAttributesEx(tempFileName);
                newInfo.Attributes.Should().HaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                NativeMethods.FileManagement.SetFileAttributes(tempFileName, originalInfo.Attributes);
                newInfo = NativeMethods.FileManagement.GetFileAttributesEx(tempFileName);
                newInfo.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
            }
        }
    }
}
