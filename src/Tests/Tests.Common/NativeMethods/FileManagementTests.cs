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
            FileMethods.GetTempPath().Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetFullPathBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            FileMethods.GetFullPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetFullPathNameForCurrent()
        {
            string fullPath = FileMethods.GetFullPathName(".");
            fullPath.Length.Should().BeGreaterThan(2);
            fullPath[1].Should().Be(':');
        }

        [Fact]
        public void GetTempFileNameBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            string tempFileName = FileMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                tempFileName.Should().StartWith(tempPath);
            }
            finally
            {
                FileMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void DeleteFileBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            string tempFileName = FileMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                System.IO.File.Exists(tempFileName).Should().BeTrue();
            }
            finally
            {
                FileMethods.DeleteFile(tempFileName);
                System.IO.File.Exists(tempFileName).Should().BeFalse();
            }
        }

        [Fact]
        public void CreateFileBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            string tempFileName = FileMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            }
            finally
            {
                FileMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void CreateFileOpenDriveRoot()
        {
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                using (var file = FileMethods.CreateFile(@"C:\.", 0, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            });
        }

        [Fact]
        public void CreateFileCreateTempFile()
        {
            string tempPath = FileMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    file.IsInvalid.Should().BeFalse();
                    System.IO.File.Exists(tempFileName).Should().BeTrue();
                }
            }
            finally
            {
                FileMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetFileAttributesExBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            var info = FileMethods.GetFileAttributesEx(tempPath);
            info.Attributes.Should().HaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);
        }

        [Fact]
        public void FlushFileBuffersBasic()
        {
            string tempPath = FileManagement.FileMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    FileMethods.FlushFileBuffers(file);
                }
            }
            finally
            {
                FileMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetFileNameByHandleBasic()
        {
            string tempPath = FileManagement.FileMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    string fileName = FileMethods.GetFileNameByHandle(file);
                    tempFileName.Should().EndWith(fileName);
                }
            }
            finally
            {
                FileMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetStandardInfoByHandleBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    var info = FileMethods.GetFileStandardInfoByHandle(directory);
                    info.Directory.Should().BeTrue();
                }

                using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    var info = FileManagement.FileMethods.GetFileStandardInfoByHandle(file);
                    info.Directory.Should().BeFalse();
                    info.NumberOfLinks.Should().Be(1);
                    info.DeletePending.Should().BeFalse();
                    info.AllocationSize.Should().Be(0);
                    info.EndOfFile.Should().Be(0);
                }
            }
            finally
            {
                FileMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetBasicInfoByHandleBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    var directoryInfo = FileMethods.GetFileBasicInfoByHandle(directory);
                    directoryInfo.Attributes.Should().HaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);

                    using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                    {
                        var fileInfo = FileMethods.GetFileBasicInfoByHandle(file);
                        fileInfo.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);
                        fileInfo.CreationTime.Should().BeAfter(directoryInfo.CreationTime);
                    }
                }
            }
            finally
            {
                FileMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetStreamInfoByHandleBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    var directoryInfo = FileManagement.FileMethods.GetStreamInformationByHandle(directory);
                    directoryInfo.Should().BeEmpty();

                    using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                    {
                        var fileInfo = FileMethods.GetStreamInformationByHandle(file);
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
                FileMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetMultipleStreamInfoByHandle()
        {
            using (var temp = new TestFileCleaner())
            {
                string source = temp.GetTestPath();
                using (var file = FileMethods.CreateFile(source, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    file.IsInvalid.Should().BeFalse();
                }

                string destination = temp.GetTestPath();
                FileManagement.FileMethods.CopyFile(source, destination);

                string alternateStream = destination + @":Foo:$DATA";
                FileMethods.CopyFile(source, alternateStream);

                using (var file = FileMethods.CreateFile(destination, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
                {
                    var fileInfo = FileMethods.GetStreamInformationByHandle(file);
                    fileInfo.Should().BeEquivalentTo(new FileManagement.StreamInformation[]
                    {
                        new StreamInformation { Name = @"::$DATA" },
                        new StreamInformation { Name = @":Foo:$DATA" }
                    });
                }
            }
        }

        [Fact]
        public void SetFileAttributesBasic()
        {
            string tempPath = FileManagement.FileMethods.GetTempPath();
            string tempFileName = FileManagement.FileMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                var originalInfo = FileManagement.FileMethods.GetFileAttributesEx(tempFileName);
                originalInfo.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                FileMethods.SetFileAttributes(tempFileName, originalInfo.Attributes | FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                var newInfo = FileMethods.GetFileAttributesEx(tempFileName);
                newInfo.Attributes.Should().HaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                FileMethods.SetFileAttributes(tempFileName, originalInfo.Attributes);
                newInfo = FileMethods.GetFileAttributesEx(tempFileName);
                newInfo.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
            }
            finally
            {
                FileMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void CopyFileBasic()
        {
            using (var temp = new TestFileCleaner())
            {
                string source = temp.GetTestPath();
                using (var file = FileMethods.CreateFile(source, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    file.IsInvalid.Should().BeFalse();
                }

                string destination = temp.GetTestPath();
                FileMethods.CopyFile(source, destination);

                var info = FileMethods.GetFileAttributesEx(destination);
                info.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);
            }
        }

        [Fact]
        public void FindFirstFileNoFiles()
        {
            using (var find = FileMethods.CreateFindOperation(FileMethods.GetTempPath() + System.IO.Path.GetRandomFileName()))
            {
                find.GetNextResult().Should().BeNull();
            }
        }

        [Fact]
        public void FindFileEmptyFolder()
        {
            using (var temp = new TestFileCleaner())
            {
                string subdir = System.IO.Path.Combine(temp.TempFolder, "Subdir");
                DirectoryManagement.DirectoryMethods.CreateDirectory(subdir);

                using (var find = FileMethods.CreateFindOperation(subdir + @"\*"))
                {
                    var foundFile = find.GetNextResult();
                    foundFile.Should().NotBeNull();
                    foundFile.FileName.Should().Be(".");
                    foundFile = find.GetNextResult();
                    foundFile.Should().NotBeNull();
                    foundFile.FileName.Should().Be("..");
                    foundFile = find.GetNextResult();
                    foundFile.Should().BeNull();
                }
            }
        }

        [Theory
            InlineData(new byte[] { 0xDE })
            ]
        public void WriteFileBasic(byte[] data)
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = FileMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_NONE, CreationDisposition.CREATE_NEW))
                {
                    FileMethods.WriteFile(fileHandle, data).Should().Be((uint)data.Length);
                    FileMethods.GetFilePointer(fileHandle).Should().Be(data.Length);
                }
            }
        }

        [Fact]
        public void GetFilePositionForEmptyFile()
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = FileMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_NONE, CreationDisposition.CREATE_NEW))
                {
                    FileMethods.SetFilePointer(fileHandle, 0, FileManagement.MoveMethod.FILE_CURRENT).Should().Be(0);
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
                using (var fileHandle = FileMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_NONE, CreationDisposition.CREATE_NEW))
                {
                    FileMethods.WriteFile(fileHandle, data).Should().Be((uint)data.Length);
                    FileMethods.GetFilePointer(fileHandle).Should().Be(data.Length);
                    FileMethods.SetFilePointer(fileHandle, 0, FileManagement.MoveMethod.FILE_BEGIN);
                    byte[] outBuffer = new byte[data.Length];
                    FileMethods.ReadFile(fileHandle, outBuffer, (uint)data.Length).Should().Be((uint)data.Length);
                    outBuffer.ShouldBeEquivalentTo(data);
                }
            }
        }

        [Fact]
        public void GetEmptyFileSize()
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = FileMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_NONE, CreationDisposition.CREATE_NEW))
                {
                    FileMethods.GetFileSize(fileHandle).Should().Be(0);
                }
            }
        }

        [Fact]
        public void GetFileTypeDisk()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                FileMethods.GetFileType(directory).Should().Be(FileManagement.FileType.FILE_TYPE_DISK);
            }
        }
    }
}
