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

namespace Tests.FileManagementTests
{
    public partial class FileManagementTests
    {
        [Fact]
        public void GetTempPathBasic()
        {
            StorageMethods.GetTempPath().Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetFullPathBasic()
        {
            string tempPath = StorageMethods.GetTempPath();
            StorageMethods.GetFullPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetFullPathNameForCurrent()
        {
            string fullPath = StorageMethods.GetFullPathName(".");
            fullPath.Length.Should().BeGreaterThan(2);
            fullPath[1].Should().Be(':');
        }

        [Fact]
        public void GetTempFileNameBasic()
        {
            string tempPath = StorageMethods.GetTempPath();
            string tempFileName = StorageMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                tempFileName.Should().StartWith(tempPath);
            }
            finally
            {
                StorageMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void DeleteFileBasic()
        {
            string tempPath = StorageMethods.GetTempPath();
            string tempFileName = StorageMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                System.IO.File.Exists(tempFileName).Should().BeTrue();
            }
            finally
            {
                StorageMethods.DeleteFile(tempFileName);
                System.IO.File.Exists(tempFileName).Should().BeFalse();
            }
        }

        [Fact]
        public void CreateFileBasic()
        {
            string tempPath = StorageMethods.GetTempPath();
            string tempFileName = StorageMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                using (var file = StorageMethods.CreateFile(tempFileName, CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            }
            finally
            {
                StorageMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void CreateFileOpenDriveRoot()
        {
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                using (var file = StorageMethods.CreateFile(@"C:\.", CreationDisposition.OpenExisting, 0, ShareModes.ReadWrite,
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

                using (var file = StorageMethods.CreateFile(tempFileName, CreationDisposition.CreateNew, DesiredAccess.GenericRead))
                {
                    file.IsInvalid.Should().BeFalse();
                    System.IO.File.Exists(tempFileName).Should().BeTrue();
                }
            }
        }

        [Fact]
        public void GetFileAttributesExBasic()
        {
            string tempPath = StorageMethods.GetTempPath();
            var info = StorageMethods.GetFileAttributesEx(tempPath);
            info.dwFileAttributes.Should().HaveFlag(FileAttributes.Directory);
        }

        [Fact]
        public void FlushFileBuffersBasic()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempPath = cleaner.TempFolder;
                string tempFileName = cleaner.GetTestPath();

                using (var file = StorageMethods.CreateFile(tempFileName, CreationDisposition.CreateNew))
                {
                    StorageMethods.FlushFileBuffers(file);
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

                using (var file = StorageMethods.CreateFile(tempFileName, CreationDisposition.CreateNew))
                {
                    string fileName = StorageMethods.GetFileNameLegacy(file);
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

                using (var directory = StorageMethods.CreateDirectoryHandle(tempPath))
                {
                    var info = StorageMethods.GetFileStandardInformation(directory);
                    info.Directory.Should().BeTrue();
                }

                using (var file = StorageMethods.CreateFile(tempFileName, CreationDisposition.CreateNew))
                {
                    var info = StorageMethods.GetFileStandardInformation(file);
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

                using (var directory = StorageMethods.CreateDirectoryHandle(tempPath))
                {
                    var directoryInfo = StorageMethods.GetFileBasicInformation(directory);
                    directoryInfo.Attributes.Should().HaveFlag(FileAttributes.Directory);

                    using (var file = StorageMethods.CreateFile(tempFileName, CreationDisposition.CreateNew))
                    {
                        var fileInfo = StorageMethods.GetFileBasicInformation(file);
                        fileInfo.Attributes.Should().NotHaveFlag(FileAttributes.Directory);
                        fileInfo.CreationTimeUtc.Should().BeAfter(directoryInfo.CreationTimeUtc);
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

                using (var directory = StorageMethods.CreateDirectoryHandle(tempPath))
                {
                    var directoryInfo = StorageMethods.GetStreamInformation(directory);
                    directoryInfo.Should().BeEmpty();

                    using (var file = StorageMethods.CreateFile(tempFileName, CreationDisposition.CreateNew))
                    {
                        var fileInfo = StorageMethods.GetStreamInformation(file);
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
                using (var file = StorageMethods.CreateFile(source, CreationDisposition.CreateNew))
                {
                    file.IsInvalid.Should().BeFalse();
                }

                string destination = temp.GetTestPath();
                StorageMethods.CopyFile(source, destination);

                string alternateStream = destination + @":Foo:$DATA";
                StorageMethods.CopyFile(source, alternateStream);

                using (var file = StorageMethods.CreateFile(destination, CreationDisposition.OpenExisting))
                {
                    var fileInfo = StorageMethods.GetStreamInformation(file);
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
            string tempPath = StorageMethods.GetTempPath();
            string tempFileName = StorageMethods.GetTempFileName(tempPath, "tfn");
            try
            {
                var originalInfo = StorageMethods.GetFileAttributesEx(tempFileName);
                originalInfo.dwFileAttributes.Should().NotHaveFlag(FileAttributes.ReadOnly);
                StorageMethods.SetFileAttributes(tempFileName, originalInfo.dwFileAttributes | FileAttributes.ReadOnly);
                var newInfo = StorageMethods.GetFileAttributesEx(tempFileName);
                newInfo.dwFileAttributes.Should().HaveFlag(FileAttributes.ReadOnly);
                StorageMethods.SetFileAttributes(tempFileName, originalInfo.dwFileAttributes);
                newInfo = StorageMethods.GetFileAttributesEx(tempFileName);
                newInfo.dwFileAttributes.Should().NotHaveFlag(FileAttributes.ReadOnly);
            }
            finally
            {
                StorageMethods.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void CopyFileBasic()
        {
            using (var temp = new TestFileCleaner())
            {
                string source = temp.GetTestPath();
                using (var file = StorageMethods.CreateFile(source, CreationDisposition.CreateNew, DesiredAccess.GenericRead))
                {
                    file.IsInvalid.Should().BeFalse();
                }

                string destination = temp.GetTestPath();
                StorageMethods.CopyFile(source, destination);

                var info = StorageMethods.GetFileAttributesEx(destination);
                info.dwFileAttributes.Should().NotHaveFlag(FileAttributes.Directory);
            }
        }

        [Fact]
        public void FindFirstFileNoFiles()
        {
            StorageMethods.CreateFindOperation<string>(StorageMethods.GetTempPath(), nameFilter: System.IO.Path.GetRandomFileName())
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
                StorageMethods.CreateDirectory(subdir);

                StorageMethods.CreateFindOperation(subdir, nameFilter: pattern,
                    findTransform: FindTransforms.ToFileName.Instance, findFilter: FindFilters.All.Instance)
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
                using (var fileHandle = StorageMethods.CreateFile(temp.GetTestPath(), CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0))
                {
                    StorageMethods.WriteFile(fileHandle, data).Should().Be((uint)data.Length);
                    StorageMethods.GetFilePointer(fileHandle).Should().Be(data.Length);
                }
            }
        }

        [Fact]
        public void GetFilePositionForEmptyFile()
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = StorageMethods.CreateFile(temp.GetTestPath(), CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0))
                {
                    StorageMethods.SetFilePointer(fileHandle, 0, MoveMethod.Current).Should().Be(0);
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
                using (var fileHandle = StorageMethods.CreateFile(temp.GetTestPath(), CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0))
                {
                    StorageMethods.WriteFile(fileHandle, data).Should().Be((uint)data.Length);
                    StorageMethods.GetFilePointer(fileHandle).Should().Be(data.Length);
                    StorageMethods.SetFilePointer(fileHandle, 0, MoveMethod.Begin);
                    byte[] outBuffer = new byte[data.Length];
                    StorageMethods.ReadFile(fileHandle, outBuffer).Should().Be((uint)data.Length);
                    outBuffer.Should().BeEquivalentTo(data);
                }
            }
        }

        [Fact]
        public void GetEmptyFileSize()
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = StorageMethods.CreateFile(temp.GetTestPath(), CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0))
                {
                    StorageMethods.GetFileSize(fileHandle).Should().Be(0);
                }
            }
        }

        [Fact]
        public void GetFileTypeDisk()
        {
            string tempPath = StorageMethods.GetTempPath();
            using (var directory = StorageMethods.CreateDirectoryHandle(tempPath))
            {
                StorageMethods.GetFileType(directory).Should().Be(FileType.Disk);
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
                using (var handle = StorageMethods.CreateFile(filePath, CreationDisposition.CreateNew))
                {
                    handle.IsInvalid.Should().BeFalse();
                    StorageMethods.FileExists(filePath).Should().BeTrue();
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
                using (var handle = StorageMethods.CreateFile(filePath, CreationDisposition.CreateNew))
                {
                    handle.IsInvalid.Should().BeFalse();
                    StorageMethods.FlushFileBuffers(handle);
                    StorageMethods.FileExists(filePath).Should().BeTrue();
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
                StorageMethods.FileExists(filePath).Should().BeTrue();
                StorageMethods.PathExists(filePath).Should().BeTrue();
                StorageMethods.DirectoryExists(filePath).Should().BeFalse();
            }
        }

        [Fact]
        public void FileNotExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                StorageMethods.FileExists(filePath).Should().BeFalse();
                StorageMethods.PathExists(filePath).Should().BeFalse();
                StorageMethods.DirectoryExists(filePath).Should().BeFalse();
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

                StorageMethods.FileExists(filePath).Should().BeTrue();
                StorageMethods.PathExists(filePath).Should().BeTrue();
                StorageMethods.DirectoryExists(filePath).Should().BeFalse();
            }
        }

        [Fact]
        public void LongPathFileNotExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                string filePath = cleaner.GetTestPath();

                StorageMethods.FileExists(filePath).Should().BeFalse();
                StorageMethods.PathExists(filePath).Should().BeFalse();
                StorageMethods.DirectoryExists(filePath).Should().BeFalse();
            }
        }

        [Fact]
        public void DirectoryExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string directoryPath = cleaner.GetTestPath();
                FileHelper.CreateDirectoryRecursive(directoryPath);

                StorageMethods.FileExists(directoryPath).Should().BeFalse();
                StorageMethods.PathExists(directoryPath).Should().BeTrue();
                StorageMethods.DirectoryExists(directoryPath).Should().BeTrue();
            }
        }

        [Fact]
        public void DirectoryNotExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string directoryPath = cleaner.GetTestPath();

                StorageMethods.FileExists(directoryPath).Should().BeFalse();
                StorageMethods.PathExists(directoryPath).Should().BeFalse();
                StorageMethods.DirectoryExists(directoryPath).Should().BeFalse();
            }
        }

        [Fact]
        public void InfoForNonExistantLongPath()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                StorageMethods.TryGetFileInfo(longPath).Should().BeNull();
            }
        }


        [Fact]
        public void FileTypeOfFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                using (var testFile = StorageMethods.CreateFile(cleaner.GetTestPath(), CreationDisposition.CreateNew))
                {
                    StorageMethods.GetFileType(testFile).Should().Be(FileType.Disk);
                }
            }
        }

        [Theory,
            InlineData(@"C:\"),
            InlineData(@"\\?\C:\")
            ]
        public void FindFirstFileHandlesRoots(string path)
        {
            StorageMethods.CreateFindOperation<string>(path).Should().NotBeEmpty();
            StorageMethods.CreateFindOperation<string>(path, null).Should().NotBeEmpty();
        }

        [Fact]
        public void GetFileNameByHandle()
        {
            // Can't open the Users folder in a Store app
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                // @"C:\" -> @"\"
                var fileHandle = StorageMethods.CreateFileSystemIo(
                    @"C:\Users",
                    0,                  // We don't care about read or write, we're just getting metadata with this handle
                    System.IO.FileShare.ReadWrite,
                    System.IO.FileMode.Open,
                    0,
                    FileFlags.OpenReparsePoint           // To avoid traversing links
                        | FileFlags.BackupSemantics);    // To open directories

                string name = StorageMethods.GetFileNameLegacy(fileHandle);
                name.Should().Be(@"\Users");
            });
        }

        [Fact]
        public void CreateStreamSystemIoDefines()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                using (var stream = StorageMethods.CreateFileStream(
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
            Action action = () => StorageMethods.GetFullPathName(value);
            action.Should().Throw<System.IO.IOException>();
        }

        [Fact]
        public unsafe void GetDirectoryFilenamesFromHandle()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempDirectory = cleaner.GetTestPath();

                StorageMethods.CreateDirectory(tempDirectory);
                FileHelper.WriteAllText(Paths.Combine(tempDirectory, "GetDirectoryFilenamesFromHandle"), "GetDirectoryFilenamesFromHandle");
                using (var handle = StorageMethods.CreateDirectoryHandle(tempDirectory))
                {
                    StorageMethods.GetDirectoryFilenames(handle).Should().Contain(new string[] { ".", "..", "GetDirectoryFilenamesFromHandle" });
                }
            }
        }

        [Fact]
        public unsafe void GetDirectoryFilenamesFromHandle_EmptyDirectory()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempDirectory = cleaner.GetTestPath();

                StorageMethods.CreateDirectory(tempDirectory);
                using (var handle = StorageMethods.CreateDirectoryHandle(tempDirectory))
                {
                    StorageMethods.GetDirectoryFilenames(handle).Should().Contain(new string[] { ".", ".." });
                }
            }
        }
    }
}
