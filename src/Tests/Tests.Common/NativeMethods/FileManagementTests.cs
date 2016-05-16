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
            NativeMethods.GetTempPath().Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetFullPathBasic()
        {
            string tempPath = NativeMethods.GetTempPath();
            NativeMethods.GetFullPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetFullPathNameForCurrent()
        {
            string fullPath = NativeMethods.GetFullPathName(".");
            fullPath.Length.Should().BeGreaterThan(2);
            fullPath[1].Should().Be(':');
        }

        [Fact]
        public void GetTempFileNameBasic()
        {
            string tempPath = NativeMethods.GetTempPath();
            string tempFileName = NativeMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                tempFileName.Should().StartWith(tempPath);
            }
            finally
            {
                NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void DeleteFileBasic()
        {
            string tempPath = NativeMethods.GetTempPath();
            string tempFileName = NativeMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                System.IO.File.Exists(tempFileName).Should().BeTrue();
            }
            finally
            {
                NativeMethods.DeleteFile(tempFileName);
                System.IO.File.Exists(tempFileName).Should().BeFalse();
            }
        }

        [Fact]
        public void CreateFileBasic()
        {
            string tempPath = NativeMethods.GetTempPath();
            string tempFileName = NativeMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                using (var file = NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            }
            finally
            {
                NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void CreateFileOpenDriveRoot()
        {
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                using (var file = NativeMethods.CreateFile(@"C:\.", 0, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            });
        }

        [Fact]
        public void CreateFileCreateTempFile()
        {
            string tempPath = NativeMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var file = NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    file.IsInvalid.Should().BeFalse();
                    System.IO.File.Exists(tempFileName).Should().BeTrue();
                }
            }
            finally
            {
                NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetFileAttributesExBasic()
        {
            string tempPath = NativeMethods.GetTempPath();
            var info = NativeMethods.GetFileAttributesEx(tempPath);
            info.Attributes.Should().HaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);
        }

        [Fact]
        public void FlushFileBuffersBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var file = NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    NativeMethods.FlushFileBuffers(file);
                }
            }
            finally
            {
                NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetFileNameByHandleBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var file = NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    string fileName = NativeMethods.GetFileNameByHandle(file);
                    tempFileName.Should().EndWith(fileName);
                }
            }
            finally
            {
                NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetStandardInfoByHandleBasic()
        {
            string tempPath = NativeMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var directory = NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    var info = NativeMethods.GetFileStandardInfoByHandle(directory);
                    info.Directory.Should().BeTrue();
                }

                using (var file = NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
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
                NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetBasicInfoByHandleBasic()
        {
            string tempPath = NativeMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var directory = NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    var directoryInfo = NativeMethods.GetFileBasicInfoByHandle(directory);
                    directoryInfo.Attributes.Should().HaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);

                    using (var file = NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                    {
                        var fileInfo = NativeMethods.GetFileBasicInfoByHandle(file);
                        fileInfo.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);
                        fileInfo.CreationTime.Should().BeAfter(directoryInfo.CreationTime);
                    }
                }
            }
            finally
            {
                NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetStreamInfoByHandleBasic()
        {
            string tempPath = NativeMethods.GetTempPath();
            string tempFileName = System.IO.Path.Combine(tempPath, System.IO.Path.GetRandomFileName());
            try
            {
                using (var directory = NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    var directoryInfo = FileManagement.NativeMethods.GetStreamInformationByHandle(directory);
                    directoryInfo.Should().BeEmpty();

                    using (var file = NativeMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                    {
                        var fileInfo = NativeMethods.GetStreamInformationByHandle(file);
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
                NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void GetMultipleStreamInfoByHandle()
        {
            using (var temp = new TestFileCleaner())
            {
                string source = temp.GetTestPath();
                using (var file = NativeMethods.CreateFile(source, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    file.IsInvalid.Should().BeFalse();
                }

                string destination = temp.GetTestPath();
                FileManagement.NativeMethods.CopyFile(source, destination);

                string alternateStream = destination + @":Foo:$DATA";
                NativeMethods.CopyFile(source, alternateStream);

                using (var file = NativeMethods.CreateFile(destination, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
                {
                    var fileInfo = NativeMethods.GetStreamInformationByHandle(file);
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
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            string tempFileName = FileManagement.NativeMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                var originalInfo = FileManagement.NativeMethods.GetFileAttributesEx(tempFileName);
                originalInfo.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                NativeMethods.SetFileAttributes(tempFileName, originalInfo.Attributes | FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                var newInfo = NativeMethods.GetFileAttributesEx(tempFileName);
                newInfo.Attributes.Should().HaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                NativeMethods.SetFileAttributes(tempFileName, originalInfo.Attributes);
                newInfo = NativeMethods.GetFileAttributesEx(tempFileName);
                newInfo.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
            }
            finally
            {
                NativeMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void CopyFileBasic()
        {
            using (var temp = new TestFileCleaner())
            {
                string source = temp.GetTestPath();
                using (var file = NativeMethods.CreateFile(source, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    file.IsInvalid.Should().BeFalse();
                }

                string destination = temp.GetTestPath();
                NativeMethods.CopyFile(source, destination);

                var info = NativeMethods.GetFileAttributesEx(destination);
                info.Attributes.Should().NotHaveFlag(FileManagement.FileAttributes.FILE_ATTRIBUTE_DIRECTORY);
            }
        }

        [Fact]
        public void FindFirstFileNoFiles()
        {
            NativeMethods.FindFirstFile(FileManagement.NativeMethods.GetTempPath() + System.IO.Path.GetRandomFileName()).Should().BeNull();
        }

        [Fact]
        public void FindFileEmptyFolder()
        {
            using (var temp = new TestFileCleaner())
            {
                string subdir = System.IO.Path.Combine(temp.TempFolder, "Subdir");
                DirectoryManagement.NativeMethods.CreateDirectory(subdir);
                var foundFile = NativeMethods.FindFirstFile(subdir + @"\*");
                foundFile.Should().NotBeNull();
                foundFile.FileName.Should().Be(".");
                foundFile = NativeMethods.FindNextFile(foundFile);
                foundFile.Should().NotBeNull();
                foundFile.FileName.Should().Be("..");
                foundFile = NativeMethods.FindNextFile(foundFile);
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
                using (var fileHandle = NativeMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_NONE, CreationDisposition.CREATE_NEW))
                {
                    NativeMethods.WriteFile(fileHandle, data).Should().Be((uint)data.Length);
                    NativeMethods.GetFilePointer(fileHandle).Should().Be(data.Length);
                }
            }
        }

        [Fact]
        public void GetFilePositionForEmptyFile()
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = NativeMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_NONE, CreationDisposition.CREATE_NEW))
                {
                    NativeMethods.SetFilePointer(fileHandle, 0, FileManagement.MoveMethod.FILE_CURRENT).Should().Be(0);
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
                using (var fileHandle = NativeMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_NONE, CreationDisposition.CREATE_NEW))
                {
                    NativeMethods.WriteFile(fileHandle, data).Should().Be((uint)data.Length);
                    NativeMethods.GetFilePointer(fileHandle).Should().Be(data.Length);
                    NativeMethods.SetFilePointer(fileHandle, 0, FileManagement.MoveMethod.FILE_BEGIN);
                    byte[] outBuffer = new byte[data.Length];
                    NativeMethods.ReadFile(fileHandle, outBuffer, (uint)data.Length).Should().Be((uint)data.Length);
                    outBuffer.ShouldBeEquivalentTo(data);
                }
            }
        }

        [Fact]
        public void GetEmptyFileSize()
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = NativeMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GENERIC_READWRITE, ShareMode.FILE_SHARE_NONE, CreationDisposition.CREATE_NEW))
                {
                    NativeMethods.GetFileSize(fileHandle).Should().Be(0);
                }
            }
        }

        [Fact]
        public void GetFileTypeDisk()
        {
            string tempPath = NativeMethods.GetTempPath();
            using (var directory = NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                NativeMethods.GetFileType(directory).Should().Be(FileManagement.FileType.FILE_TYPE_DISK);
            }
        }
    }
}
