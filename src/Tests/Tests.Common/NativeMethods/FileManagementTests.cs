// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Linq;
using WInterop.FileManagement;
using WInterop.Tests.Support;
using Xunit;

namespace WInterop.Tests.NativeMethodTests
{
    public partial class FileManagementTests
    {
        [Fact]
        public void GetTempPathBasic()
        {
            FileManagement.NativeMethods.GetTempPath().Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetFullPathBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            FileManagement.NativeMethods.GetFullPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetFullPathNameForCurrent()
        {
            string fullPath = FileManagement.NativeMethods.GetFullPathName(".");
            fullPath.Length.Should().BeGreaterThan(2);
            fullPath[1].Should().Be(':');
        }

        [Fact]
        public void GetTempFileNameBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = FileManagement.NativeMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                tempFileName.Should().StartWith(tempPath);
            }
            finally
            {
                FileManagement.NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void DeleteFileBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = FileManagement.NativeMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                System.IO.File.Exists(tempFileName).Should().BeTrue();
            }
            finally
            {
                FileManagement.NativeMethods.DeleteFile(tempFileName);
                System.IO.File.Exists(tempFileName).Should().BeFalse();
            }
        }

        [Fact]
        public void CreateFileBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = FileManagement.NativeMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                using (var file = FileManagement.NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            }
            finally
            {
                FileManagement.NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void CreateFileOpenDriveRoot()
        {
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                using (var file = FileManagement.NativeMethods.CreateFile(@"C:\.", 0, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                    FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            });
        }

        [Fact]
        public void CreateFileCreateTempFile()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var file = FileManagement.NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.CREATE_NEW))
                {
                    file.IsInvalid.Should().BeFalse();
                    System.IO.File.Exists(tempFileName).Should().BeTrue();
                }
            }
            finally
            {
                FileManagement.NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetFileAttributesExBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            var info = FileManagement.NativeMethods.GetFileAttributesEx(tempPath);
            info.Attributes.Should().HaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);
        }

        [Fact]
        public void FlushFileBuffersBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var file = FileManagement.NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.ReadWrite, CreationDisposition.CREATE_NEW))
                {
                    FileManagement.NativeMethods.FlushFileBuffers(file);
                }
            }
            finally
            {
                FileManagement.NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetFileNameByHandleBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var file = FileManagement.NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.ReadWrite, CreationDisposition.CREATE_NEW))
                {
                    string fileName = FileManagement.NativeMethods.GetFileNameByHandle(file);
                    tempFileName.Should().EndWith(fileName);
                }
            }
            finally
            {
                FileManagement.NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetStandardInfoByHandleBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var directory = FileManagement.NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    var info = FileManagement.NativeMethods.GetFileStandardInfoByHandle(directory);
                    info.Directory.Should().BeTrue();
                }

                using (var file = FileManagement.NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.ReadWrite, CreationDisposition.CREATE_NEW))
                {
                    var info = FileManagement.NativeMethods.GetFileStandardInfoByHandle(file);
                    info.Directory.Should().BeFalse();
                    info.NumberOfLinks.Should().Be(1);
                    info.DeletePending.Should().BeFalse();
                    info.AllocationSize.Should().Be(0);
                    info.EndOfFile.Should().Be(0);
                }
            }
            finally
            {
                FileManagement.NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetBasicInfoByHandleBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var directory = FileManagement.NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                    FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    var directoryInfo = FileManagement.NativeMethods.GetFileBasicInfoByHandle(directory);
                    directoryInfo.Attributes.Should().HaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);

                    using (var file = FileManagement.NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.ReadWrite, CreationDisposition.CREATE_NEW))
                    {
                        var fileInfo = FileManagement.NativeMethods.GetFileBasicInfoByHandle(file);
                        fileInfo.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);
                        fileInfo.CreationTime.Should().BeAfter(directoryInfo.CreationTime);
                    }
                }
            }
            finally
            {
                FileManagement.NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetStreamInfoByHandleBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var directory = FileManagement.NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                    FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    var directoryInfo = FileManagement.NativeMethods.GetStreamInformationByHandle(directory);
                    directoryInfo.Should().BeEmpty();

                    using (var file = FileManagement.NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.ReadWrite, CreationDisposition.CREATE_NEW))
                    {
                        var fileInfo = FileManagement.NativeMethods.GetStreamInformationByHandle(file);
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
                FileManagement.NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetMultipleStreamInfoByHandle()
        {
            using (var temp = new TestFileCleaner())
            {
                string source = temp.GetTestPath();
                using (var file = FileManagement.NativeMethods.CreateFile(source, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.CREATE_NEW))
                {
                    file.IsInvalid.Should().BeFalse();
                }

                string destination = temp.GetTestPath();
                FileManagement.NativeMethods.CopyFile(source, destination);

                string alternateStream = destination + @":Foo:$DATA";
                FileManagement.NativeMethods.CopyFile(source, alternateStream);

                using (var file = FileManagement.NativeMethods.CreateFile(destination, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING))
                {
                    var fileInfo = FileManagement.NativeMethods.GetStreamInformationByHandle(file);
                    fileInfo.Should().BeEquivalentTo(new FileManagement.StreamInformation[]
                    {
                        new FileManagement.StreamInformation { Name = @"::$DATA" },
                        new FileManagement.StreamInformation { Name = @":Foo:$DATA" }
                    });
                }
            }
        }

        [Fact]
        public void SetFileAttributesBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = FileManagement.NativeMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                var originalInfo = FileManagement.NativeMethods.GetFileAttributesEx(tempFileName);
                originalInfo.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                FileManagement.NativeMethods.SetFileAttributes(tempFileName, originalInfo.Attributes | FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                var newInfo = FileManagement.NativeMethods.GetFileAttributesEx(tempFileName);
                newInfo.Attributes.Should().HaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                FileManagement.NativeMethods.SetFileAttributes(tempFileName, originalInfo.Attributes);
                newInfo = FileManagement.NativeMethods.GetFileAttributesEx(tempFileName);
                newInfo.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
            }
            finally
            {
                FileManagement.NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void CopyFileBasic()
        {
            using (var temp = new TestFileCleaner())
            {
                string source = temp.GetTestPath();
                using (var file = FileManagement.NativeMethods.CreateFile(source, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.CREATE_NEW))
                {
                    file.IsInvalid.Should().BeFalse();
                }

                string destination = temp.GetTestPath();
                FileManagement.NativeMethods.CopyFile(source, destination);

                var info = FileManagement.NativeMethods.GetFileAttributesEx(destination);
                info.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);
            }
        }

        [Fact]
        public void FindFirstFileNoFiles()
        {
            FileManagement.NativeMethods.FindFirstFile(FileManagement.NativeMethods.GetTempPath() + System.IO.Path.GetRandomFileName()).Should().BeNull();
        }

        [Fact]
        public void FindFileEmptyFolder()
        {
            using (var temp = new TestFileCleaner())
            {
                string subdir = System.IO.Path.Combine(temp.TempFolder, "Subdir");
                DirectoryManagement.NativeMethods.CreateDirectory(subdir);
                var foundFile = FileManagement.NativeMethods.FindFirstFile(subdir + @"\*");
                foundFile.Should().NotBeNull();
                foundFile.FileName.Should().Be(".");
                foundFile = FileManagement.NativeMethods.FindNextFile(foundFile);
                foundFile.Should().NotBeNull();
                foundFile.FileName.Should().Be("..");
                foundFile = FileManagement.NativeMethods.FindNextFile(foundFile);
                foundFile.Should().BeNull();
            }
        }

        [Theory
            InlineData(new byte[] { 0xDE })
            ]
        public void WriteFileBasic(byte[] data)
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = FileManagement.NativeMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.None, CreationDisposition.CREATE_NEW))
                {
                    FileManagement.NativeMethods.WriteFile(fileHandle, data).Should().Be((uint)data.Length);
                    FileManagement.NativeMethods.GetFilePointer(fileHandle).Should().Be(data.Length);
                }
            }
        }

        [Fact]
        public void GetFilePositionForEmptyFile()
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = FileManagement.NativeMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.None, CreationDisposition.CREATE_NEW))
                {
                    FileManagement.NativeMethods.SetFilePointer(fileHandle, 0, FileManagement.MoveMethod.FILE_CURRENT).Should().Be(0);
                }
            }
        }

        [Theory
            InlineData(new byte[] { 0xDE })
            InlineData(new byte[] { 0xDE, 0xAD, 0xBE, 0xEF })
            ]
        public void ReadWriteFileBasic(byte[] data)
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = FileManagement.NativeMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.None, CreationDisposition.CREATE_NEW))
                {
                    FileManagement.NativeMethods.WriteFile(fileHandle, data).Should().Be((uint)data.Length);
                    FileManagement.NativeMethods.GetFilePointer(fileHandle).Should().Be(data.Length);
                    FileManagement.NativeMethods.SetFilePointer(fileHandle, 0, FileManagement.MoveMethod.FILE_BEGIN);
                    byte[] outBuffer = new byte[data.Length];
                    FileManagement.NativeMethods.ReadFile(fileHandle, outBuffer, (uint)data.Length).Should().Be((uint)data.Length);
                    outBuffer.ShouldBeEquivalentTo(data);
                }
            }
        }

        [Fact]
        public void GetEmptyFileSize()
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = FileManagement.NativeMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.None, CreationDisposition.CREATE_NEW))
                {
                    FileManagement.NativeMethods.GetFileSize(fileHandle).Should().Be(0);
                }
            }
        }

        [Fact]
        public void GetFileTypeDisk()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            using (var directory = FileManagement.NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                FileManagement.NativeMethods.GetFileType(directory).Should().Be(FileManagement.FileType.FILE_TYPE_DISK);
            }
        }
    }
}
