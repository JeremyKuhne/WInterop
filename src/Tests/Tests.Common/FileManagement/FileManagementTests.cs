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
using Tests.Support;
using WInterop.Support;
using Xunit;
using WInterop.FileManagement.Types;
using WInterop.Support.Buffers;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using System.Collections.Generic;

namespace Tests.FileManagementTests
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
                using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GenericRead, ShareMode.ReadWrite, CreationDisposition.OpenExisting))
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
                using (var file = FileMethods.CreateFile(@"C:\.", 0, ShareMode.ReadWrite, CreationDisposition.OpenExisting,
                    FileAttributes.None, FileFlags.BackupSemantics))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            });
        }

        [Fact]
        public void CreateFileCreateTempFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempPath = cleaner.TempFolder;
                string tempFileName = cleaner.GetTestPath();

                using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GenericRead, ShareMode.ReadWrite, CreationDisposition.CreateNew))
                {
                    file.IsInvalid.Should().BeFalse();
                    System.IO.File.Exists(tempFileName).Should().BeTrue();
                }
            }
        }

        [Fact]
        public void GetFileAttributesExBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            var info = FileMethods.GetFileAttributesEx(tempPath);
            info.Attributes.Should().HaveFlag(FileAttributes.Directory);
        }

        [Fact]
        public void FlushFileBuffersBasic()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempPath = cleaner.TempFolder;
                string tempFileName = cleaner.GetTestPath();

                using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GenericReadWrite, ShareMode.ReadWrite, CreationDisposition.CreateNew))
                {
                    FileMethods.FlushFileBuffers(file);
                }
            }
        }

        [Fact]
        public void GetFileNameByHandleBasic()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempPath = cleaner.TempFolder;
                string tempFileName = cleaner.GetTestPath();

                using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GenericReadWrite, ShareMode.ReadWrite, CreationDisposition.CreateNew))
                {
                    string fileName = FileMethods.GetFileNameByHandle(file);
                    tempFileName.Should().EndWith(fileName);
                }
            }
        }

        [Fact]
        public void GetStandardInfoByHandleBasic()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempPath = cleaner.TempFolder;
                string tempFileName = cleaner.GetTestPath();

                using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GenericReadWrite, ShareMode.ReadWrite, CreationDisposition.OpenExisting,
                    FileAttributes.None, FileFlags.BackupSemantics))
                {
                    var info = FileMethods.GetFileStandardInfoByHandle(directory);
                    info.Directory.Should().BeTrue();
                }

                using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GenericReadWrite, ShareMode.ReadWrite, CreationDisposition.CreateNew))
                {
                    var info = FileMethods.GetFileStandardInfoByHandle(file);
                    info.Directory.Should().BeFalse();
                    info.NumberOfLinks.Should().Be(1);
                    info.DeletePending.Should().BeFalse();
                    info.AllocationSize.Should().Be(0);
                    info.EndOfFile.Should().Be(0);
                }
            }
        }

        [Fact]
        public void GetBasicInfoByHandleBasic()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempPath = cleaner.TempFolder;
                string tempFileName = cleaner.GetTestPath();

                using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GenericRead, ShareMode.ReadWrite, CreationDisposition.OpenExisting,
                    FileAttributes.None, FileFlags.BackupSemantics))
                {
                    var directoryInfo = FileMethods.GetFileBasicInfoByHandle(directory);
                    directoryInfo.Attributes.Should().HaveFlag(FileAttributes.Directory);

                    using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GenericReadWrite, ShareMode.ReadWrite, CreationDisposition.CreateNew))
                    {
                        var fileInfo = FileMethods.GetFileBasicInfoByHandle(file);
                        fileInfo.Attributes.Should().NotHaveFlag(FileAttributes.Directory);
                        fileInfo.CreationTime.Should().BeAfter(directoryInfo.CreationTime);
                    }
                }
            }
        }

        [Fact]
        public void GetStreamInfoByHandleBasic()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempPath = cleaner.TempFolder;
                string tempFileName = cleaner.GetTestPath();

                using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GenericRead, ShareMode.ReadWrite, CreationDisposition.OpenExisting,
                    FileAttributes.None, FileFlags.BackupSemantics))
                {
                    var directoryInfo = FileMethods.GetStreamInformationByHandle(directory);
                    directoryInfo.Should().BeEmpty();

                    using (var file = FileMethods.CreateFile(tempFileName, DesiredAccess.GenericReadWrite, ShareMode.ReadWrite, CreationDisposition.CreateNew))
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
        }

        [Fact]
        public void GetMultipleStreamInfoByHandle()
        {
            using (var temp = new TestFileCleaner())
            {
                string source = temp.GetTestPath();
                using (var file = FileMethods.CreateFile(source, DesiredAccess.GenericRead, ShareMode.ReadWrite, CreationDisposition.CreateNew))
                {
                    file.IsInvalid.Should().BeFalse();
                }

                string destination = temp.GetTestPath();
                FileMethods.CopyFile(source, destination);

                string alternateStream = destination + @":Foo:$DATA";
                FileMethods.CopyFile(source, alternateStream);

                using (var file = FileMethods.CreateFile(destination, DesiredAccess.GenericReadWrite, ShareMode.ReadWrite, CreationDisposition.OpenExisting))
                {
                    var fileInfo = FileMethods.GetStreamInformationByHandle(file);
                    fileInfo.Should().BeEquivalentTo(new StreamInformation[]
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
                originalInfo.Attributes.Should().NotHaveFlag(FileAttributes.ReadOnly);
                FileMethods.SetFileAttributes(tempFileName, originalInfo.Attributes | FileAttributes.ReadOnly);
                var newInfo = FileMethods.GetFileAttributesEx(tempFileName);
                newInfo.Attributes.Should().HaveFlag(FileAttributes.ReadOnly);
                FileMethods.SetFileAttributes(tempFileName, originalInfo.Attributes);
                newInfo = FileMethods.GetFileAttributesEx(tempFileName);
                newInfo.Attributes.Should().NotHaveFlag(FileAttributes.ReadOnly);
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
                using (var file = FileMethods.CreateFile(source, DesiredAccess.GenericRead, ShareMode.ReadWrite, CreationDisposition.CreateNew))
                {
                    file.IsInvalid.Should().BeFalse();
                }

                string destination = temp.GetTestPath();
                FileMethods.CopyFile(source, destination);

                var info = FileMethods.GetFileAttributesEx(destination);
                info.Attributes.Should().NotHaveFlag(FileAttributes.Directory);
            }
        }

        [Fact]
        public void FindFirstFileNoFiles()
        {
            FileMethods.CreateFindOperation(Paths.Combine(FileMethods.GetTempPath(), System.IO.Path.GetRandomFileName()))
                .Should().BeEmpty();
        }

        [Theory,
            InlineData("*"),
            InlineData("*.*")]
        public void FindFileEmptyFolder(string pattern)
        {
            using (var temp = new TestFileCleaner())
            {
                string subdir = Paths.Combine(temp.TempFolder, "Subdir");
                DirectoryMethods.CreateDirectory(subdir);

                FileMethods.CreateFindOperation(Paths.Combine(subdir, pattern)).Select(s => s.FileName)
                    .Should().Contain(new string[] { ".", ".." });
            }
        }

        [Theory,
            InlineData(new byte[] { 0xDE })
            ]
        public void WriteFileBasic(byte[] data)
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = FileMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GenericReadWrite, 0, CreationDisposition.CreateNew))
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
                using (var fileHandle = FileMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GenericReadWrite, 0, CreationDisposition.CreateNew))
                {
                    FileMethods.SetFilePointer(fileHandle, 0, MoveMethod.Current).Should().Be(0);
                }
            }
        }

        [Theory,
            InlineData(new byte[] { 0xDE }),
            InlineData(new byte[] { 0xDE, 0xAD, 0xBE, 0xEF })
            ]
        public void ReadWriteFileBasic(byte[] data)
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = FileMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GenericReadWrite, 0, CreationDisposition.CreateNew))
                {
                    FileMethods.WriteFile(fileHandle, data).Should().Be((uint)data.Length);
                    FileMethods.GetFilePointer(fileHandle).Should().Be(data.Length);
                    FileMethods.SetFilePointer(fileHandle, 0, MoveMethod.Begin);
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
                using (var fileHandle = FileMethods.CreateFile(temp.GetTestPath(), DesiredAccess.GenericReadWrite, 0, CreationDisposition.CreateNew))
                {
                    FileMethods.GetFileSize(fileHandle).Should().Be(0);
                }
            }
        }

        [Fact]
        public void GetFileTypeDisk()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GenericRead, ShareMode.ReadWrite, CreationDisposition.OpenExisting,
                FileAttributes.None, FileFlags.BackupSemantics))
            {
                FileMethods.GetFileType(directory).Should().Be(FileType.FILE_TYPE_DISK);
            }
        }

        [Theory,
            // InlineData(@" "),  // 5 Access is denied (UnauthorizedAccess)
            // InlineData(@"...") // 5 
            InlineData(@"A "),
            InlineData(@"A.")
            ]
        public void CreateFileUnextendedTests(string fileName)
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = Paths.Combine(cleaner.TempFolder, fileName);
                using (var handle = FileMethods.CreateFile(filePath, DesiredAccess.GenericReadWrite, ShareMode.ReadWrite, CreationDisposition.CreateNew))
                {
                    handle.IsInvalid.Should().BeFalse();
                    FileMethods.FileExists(filePath).Should().BeTrue();
                }
            }
        }

        [Theory,
            InlineData(@" "),
            InlineData(@"..."),
            InlineData(@"A "),
            InlineData(@"A.")
            ]
        public void CreateFileExtendedTests(string fileName)
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = @"\\?\" + Paths.Combine(cleaner.TempFolder, fileName);
                using (var handle = FileMethods.CreateFile(filePath, DesiredAccess.GenericReadWrite, ShareMode.ReadWrite, CreationDisposition.CreateNew))
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

        [Theory,
            InlineData(@"C:\"),
            InlineData(@"\\?\C:\")
            ]
        public void FindFirstFileHandlesRoots(string path)
        {
            FileMethods.CreateFindOperation(path).Should().BeEmpty();
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
                    FileFlags.OpenReparsePoint           // To avoid traversing links
                        | FileFlags.BackupSemantics);    // To open directories

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
                    securityFlags: WInterop.Support.Environment.IsWindowsStoreApplication()
                        ? SecurityQosFlags.None
                        : SecurityQosFlags.QosPresent | SecurityQosFlags.Anonymous))
                {
                    stream.Should().NotBeNull();
                }
            }
        }

        [Theory,
            InlineData("")]
        public void FullPathErrorCases(string value)
        {
            Action action = () => FileMethods.GetFullPathName(value);
            action.ShouldThrow<System.IO.IOException>();
        }

        [Fact]
        public unsafe void GetDirectoryFilenamesFromHandle()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempDirectory = cleaner.GetTestPath();

                DirectoryMethods.CreateDirectory(tempDirectory);
                FileHelper.WriteAllText(Paths.Combine(tempDirectory, "GetDirectoryFilenamesFromHandle"), "GetDirectoryFilenamesFromHandle");
                using (var handle = DirectoryMethods.CreateDirectoryHandle(tempDirectory))
                {
                    FileMethods.GetDirectoryFilenamesFromHandle(handle).Should().Contain(new string[] { ".", "..", "GetDirectoryFilenamesFromHandle" });
                }
            }
        }

        [Fact]
        public unsafe void GetDirectoryFilenamesFromHandle_EmptyDirectory()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempDirectory = cleaner.GetTestPath();

                DirectoryMethods.CreateDirectory(tempDirectory);
                using (var handle = DirectoryMethods.CreateDirectoryHandle(tempDirectory))
                {
                    FileMethods.GetDirectoryFilenamesFromHandle(handle).Should().Contain(new string[] { ".", ".." });
                }
            }
        }
    }
}
