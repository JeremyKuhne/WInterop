// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Linq;
using WInterop.DirectoryManagement;
using WInterop.FileManagement;
using WInterop.Tests.Support;
using WInterop.Utility;
using Xunit;

namespace WInterop.Tests.FileManagementTests
{
    public partial class Methods
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
                    var info = FileMethods.GetFileStandardInfoByHandle(file);
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
                    directoryInfo.Attributes.Should().HaveFlag(FileAttributes.FILE_ATTRIBUTE_DIRECTORY);

                    using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                    {
                        var fileInfo = FileMethods.GetFileBasicInfoByHandle(file);
                        fileInfo.Attributes.Should().NotHaveFlag(FileAttributes.FILE_ATTRIBUTE_DIRECTORY);
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
                    var directoryInfo = FileMethods.GetStreamInformationByHandle(directory);
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
            string tempPath = FileMethods.GetTempPath();
            string tempFileName = FileMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                var originalInfo = FileMethods.GetFileAttributesEx(tempFileName);
                originalInfo.Attributes.Should().NotHaveFlag(FileAttributes.FILE_ATTRIBUTE_READONLY);
                FileMethods.SetFileAttributes(tempFileName, originalInfo.Attributes | FileManagement.FileAttributes.FILE_ATTRIBUTE_READONLY);
                var newInfo = FileMethods.GetFileAttributesEx(tempFileName);
                newInfo.Attributes.Should().HaveFlag(FileAttributes.FILE_ATTRIBUTE_READONLY);
                FileMethods.SetFileAttributes(tempFileName, originalInfo.Attributes);
                newInfo = FileMethods.GetFileAttributesEx(tempFileName);
                newInfo.Attributes.Should().NotHaveFlag(FileAttributes.FILE_ATTRIBUTE_READONLY);
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
                DirectoryMethods.CreateDirectory(subdir);

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

        [Theory
            // InlineData(@" "),  // 5 Access is denied (UnauthorizedAccess)
            // InlineData(@"...") // 5 
            InlineData(@"A ")
            InlineData(@"A.")
            ]
        public void CreateFileUnextendedTests(string fileName)
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = Paths.Combine(cleaner.TempFolder, fileName);
                using (var handle = FileMethods.CreateFile(filePath, DesiredAccess.FILE_GENERIC_READWRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    handle.IsInvalid.Should().BeFalse();
                    FileMethods.FileExists(filePath).Should().BeTrue();
                }
            }
        }

        [Theory
            InlineData(@" "),
            InlineData(@"...")
            InlineData(@"A ")
            InlineData(@"A.")
            ]
        public void CreateFileExtendedTests(string fileName)
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = @"\\?\" + Paths.Combine(cleaner.TempFolder, fileName);
                using (var handle = FileMethods.CreateFile(filePath, DesiredAccess.FILE_GENERIC_READWRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                {
                    handle.IsInvalid.Should().BeFalse();
                    FileMethods.FlushFileBuffers(handle);
                    FileMethods.FileExists(filePath).Should().BeTrue();
                }
            }
        }

        [Fact]
        public void FileExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                FileHelper.WriteAllText(filePath, "FileExists");
                FileMethods.FileExists(filePath).Should().BeTrue();
                FileMethods.PathExists(filePath).Should().BeTrue();
                FileMethods.DirectoryExists(filePath).Should().BeFalse();
            }
        }

        [Fact]
        public void FileNotExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                FileMethods.FileExists(filePath).Should().BeFalse();
                FileMethods.PathExists(filePath).Should().BeFalse();
                FileMethods.DirectoryExists(filePath).Should().BeFalse();
            }
        }

        [Fact]
        public void LongPathFileExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                FileHelper.CreateDirectoryRecursive(longPath);

                string filePath = cleaner.CreateTestFile("FileExists", longPath);

                FileMethods.FileExists(filePath).Should().BeTrue();
                FileMethods.PathExists(filePath).Should().BeTrue();
                FileMethods.DirectoryExists(filePath).Should().BeFalse();
            }
        }

        [Fact]
        public void LongPathFileNotExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                string filePath = cleaner.GetTestPath();

                FileMethods.FileExists(filePath).Should().BeFalse();
                FileMethods.PathExists(filePath).Should().BeFalse();
                FileMethods.DirectoryExists(filePath).Should().BeFalse();
            }
        }

        [Fact]
        public void DirectoryExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string directoryPath = cleaner.GetTestPath();
                FileHelper.CreateDirectoryRecursive(directoryPath);

                FileMethods.FileExists(directoryPath).Should().BeFalse();
                FileMethods.PathExists(directoryPath).Should().BeTrue();
                FileMethods.DirectoryExists(directoryPath).Should().BeTrue();
            }
        }

        [Fact]
        public void DirectoryNotExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string directoryPath = cleaner.GetTestPath();

                FileMethods.FileExists(directoryPath).Should().BeFalse();
                FileMethods.PathExists(directoryPath).Should().BeFalse();
                FileMethods.DirectoryExists(directoryPath).Should().BeFalse();
            }
        }

        [Fact]
        public void InfoForNonExistantLongPath()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                FileMethods.TryGetFileInfo(longPath).Should().BeNull();
            }
        }


        [Fact]
        public void FileTypeOfFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                using (var testFile = FileMethods.CreateFile(
                    cleaner.GetTestPath(),
                    System.IO.FileAccess.ReadWrite,
                    System.IO.FileShare.ReadWrite,
                    System.IO.FileMode.Create,
                    0))
                {
                    FileMethods.GetFileType(testFile).Should().Be(FileType.FILE_TYPE_DISK);
                }
            }
        }

        [Theory
            InlineData(@"C:\")
            InlineData(@"\\?\C:\")
            ]
        public void FindFirstFileHandlesRoots(string path)
        {
            using (var find = FileMethods.CreateFindOperation(path))
            {
                find.GetNextResult().Should().BeNull();
            }
        }

        [Fact]
        public void GetFileNameByHandle()
        {
            // Can't open the Users folder in a Store app
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                // @"C:\" -> @"\"
                var fileHandle = FileMethods.CreateFile(
                    @"C:\Users",
                    0,                  // We don't care about read or write, we're just getting metadata with this handle
                    System.IO.FileShare.ReadWrite,
                    System.IO.FileMode.Open,
                    0,
                    FileFlags.FILE_FLAG_OPEN_REPARSE_POINT      // To avoid traversing links
                        | FileFlags.FILE_FLAG_BACKUP_SEMANTICS);    // To open directories

                string name = FileMethods.GetFileNameByHandle(fileHandle);
                name.Should().Be(@"\Users");
            });
        }

        [Fact]
        public void CreateStreamSystemIoDefines()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                using (var stream = FileMethods.CreateFileStream(
                    path: filePath,
                    fileAccess: System.IO.FileAccess.Write,
                    fileShare: System.IO.FileShare.ReadWrite,
                    fileMode: System.IO.FileMode.Append,
                    fileAttributes: 0,
                    securityFlags: Utility.Environment.IsWindowsStoreApplication() ? SecurityQosFlags.NONE : SecurityQosFlags.SECURITY_SQOS_PRESENT | SecurityQosFlags.SECURITY_ANONYMOUS ))
                {
                    stream.Should().NotBeNull();
                }
            }
        }
    }
}
