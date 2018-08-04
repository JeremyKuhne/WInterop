// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Linq;
using Tests.Shared.Support.Resources;
using Tests.Support;
using WInterop.Storage;
using WInterop.Support;
using WInterop.Windows;
using WInterop.WindowsStore;
using Xunit;
using IO = System.IO;

namespace Tests.FileManagementTests
{
    public partial class FileManagementTests
    {
        [Fact]
        public void GetTempPathBasic()
        {
            Storage.GetTempPath().Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetFullPathBasic()
        {
            string tempPath = Storage.GetTempPath();
            Storage.GetFullPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetFullPathNameForCurrent()
        {
            string fullPath = Storage.GetFullPathName(".");
            fullPath.Length.Should().BeGreaterThan(2);
            fullPath[1].Should().Be(':');
        }

        [Fact]
        public void GetTempFileNameBasic()
        {
            string tempPath = Storage.GetTempPath();
            string tempFileName = Storage.GetTempFileName(tempPath, "tfn");
            try
            {
                tempFileName.Should().StartWith(tempPath);
            }
            finally
            {
                Storage.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void DeleteFileBasic()
        {
            string tempPath = Storage.GetTempPath();
            string tempFileName = Storage.GetTempFileName(tempPath, "tfn");
            try
            {
                System.IO.File.Exists(tempFileName).Should().BeTrue();
            }
            finally
            {
                Storage.DeleteFile(tempFileName);
                System.IO.File.Exists(tempFileName).Should().BeFalse();
            }
        }

        [Fact]
        public void CreateFileBasic()
        {
            string tempPath = Storage.GetTempPath();
            string tempFileName = Storage.GetTempFileName(tempPath, "tfn");
            try
            {
                using (var file = Storage.CreateFile(tempFileName, CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            }
            finally
            {
                Storage.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void CreateFileOpenDriveRoot()
        {
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                using (var file = Storage.CreateFile(@"C:\.", CreationDisposition.OpenExisting, 0, ShareModes.ReadWrite,
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

                using (var file = Storage.CreateFile(tempFileName, CreationDisposition.CreateNew, DesiredAccess.GenericRead))
                {
                    file.IsInvalid.Should().BeFalse();
                    System.IO.File.Exists(tempFileName).Should().BeTrue();
                }
            }
        }

        [Fact]
        public void GetFileAttributesExBasic()
        {
            string tempPath = Storage.GetTempPath();
            var info = Storage.GetFileAttributesEx(tempPath);
            info.dwFileAttributes.Should().HaveFlag(FileAttributes.Directory);
        }

        [Fact]
        public void FlushFileBuffersBasic()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempPath = cleaner.TempFolder;
                string tempFileName = cleaner.GetTestPath();

                using (var file = Storage.CreateFile(tempFileName, CreationDisposition.CreateNew))
                {
                    Storage.FlushFileBuffers(file);
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

                using (var file = Storage.CreateFile(tempFileName, CreationDisposition.CreateNew))
                {
                    string fileName = Storage.GetFileNameLegacy(file);
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

                using (var directory = Storage.CreateDirectoryHandle(tempPath))
                {
                    var info = Storage.GetFileStandardInformation(directory);
                    info.Directory.Should().BeTrue();
                }

                using (var file = Storage.CreateFile(tempFileName, CreationDisposition.CreateNew))
                {
                    var info = Storage.GetFileStandardInformation(file);
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

                using (var directory = Storage.CreateDirectoryHandle(tempPath))
                {
                    var directoryInfo = Storage.GetFileBasicInformation(directory);
                    directoryInfo.Attributes.Should().HaveFlag(FileAttributes.Directory);

                    using (var file = Storage.CreateFile(tempFileName, CreationDisposition.CreateNew))
                    {
                        var fileInfo = Storage.GetFileBasicInformation(file);
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

                using (var directory = Storage.CreateDirectoryHandle(tempPath))
                {
                    var directoryInfo = Storage.GetStreamInformation(directory);
                    directoryInfo.Should().BeEmpty();

                    using (var file = Storage.CreateFile(tempFileName, CreationDisposition.CreateNew))
                    {
                        var fileInfo = Storage.GetStreamInformation(file);
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
                using (var file = Storage.CreateFile(source, CreationDisposition.CreateNew))
                {
                    file.IsInvalid.Should().BeFalse();
                }

                string destination = temp.GetTestPath();
                Storage.CopyFile(source, destination);

                string alternateStream = destination + @":Foo:$DATA";
                Storage.CopyFile(source, alternateStream);

                using (var file = Storage.CreateFile(destination, CreationDisposition.OpenExisting))
                {
                    var fileInfo = Storage.GetStreamInformation(file);
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
            string tempPath = Storage.GetTempPath();
            string tempFileName = Storage.GetTempFileName(tempPath, "tfn");
            try
            {
                var originalInfo = Storage.GetFileAttributesEx(tempFileName);
                originalInfo.dwFileAttributes.Should().NotHaveFlag(FileAttributes.ReadOnly);
                Storage.SetFileAttributes(tempFileName, originalInfo.dwFileAttributes | FileAttributes.ReadOnly);
                var newInfo = Storage.GetFileAttributesEx(tempFileName);
                newInfo.dwFileAttributes.Should().HaveFlag(FileAttributes.ReadOnly);
                Storage.SetFileAttributes(tempFileName, originalInfo.dwFileAttributes);
                newInfo = Storage.GetFileAttributesEx(tempFileName);
                newInfo.dwFileAttributes.Should().NotHaveFlag(FileAttributes.ReadOnly);
            }
            finally
            {
                Storage.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void CopyFileBasic()
        {
            using (var temp = new TestFileCleaner())
            {
                string source = temp.GetTestPath();
                using (var file = Storage.CreateFile(source, CreationDisposition.CreateNew, DesiredAccess.GenericRead))
                {
                    file.IsInvalid.Should().BeFalse();
                }

                string destination = temp.GetTestPath();
                Storage.CopyFile(source, destination);

                var info = Storage.GetFileAttributesEx(destination);
                info.dwFileAttributes.Should().NotHaveFlag(FileAttributes.Directory);
            }
        }

        [Fact]
        public void FindFirstFileNoFiles()
        {
            Storage.CreateFindOperation<string>(Storage.GetTempPath(), nameFilter: System.IO.Path.GetRandomFileName())
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
                Storage.CreateDirectory(subdir);

                Storage.CreateFindOperation(subdir, nameFilter: pattern,
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
                using (var fileHandle = Storage.CreateFile(temp.GetTestPath(), CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0))
                {
                    Storage.WriteFile(fileHandle, data).Should().Be((uint)data.Length);
                    Storage.GetFilePointer(fileHandle).Should().Be(data.Length);
                }
            }
        }

        [Fact]
        public void GetFilePositionForEmptyFile()
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = Storage.CreateFile(temp.GetTestPath(), CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0))
                {
                    Storage.SetFilePointer(fileHandle, 0, MoveMethod.Current).Should().Be(0);
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
                using (var fileHandle = Storage.CreateFile(temp.GetTestPath(), CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0))
                {
                    Storage.WriteFile(fileHandle, data).Should().Be((uint)data.Length);
                    Storage.GetFilePointer(fileHandle).Should().Be(data.Length);
                    Storage.SetFilePointer(fileHandle, 0, MoveMethod.Begin);
                    byte[] outBuffer = new byte[data.Length];
                    Storage.ReadFile(fileHandle, outBuffer).Should().Be((uint)data.Length);
                    outBuffer.Should().BeEquivalentTo(data);
                }
            }
        }

        [Fact]
        public void GetEmptyFileSize()
        {
            using (var temp = new TestFileCleaner())
            {
                using (var fileHandle = Storage.CreateFile(temp.GetTestPath(), CreationDisposition.CreateNew, DesiredAccess.GenericReadWrite, 0))
                {
                    Storage.GetFileSize(fileHandle).Should().Be(0);
                }
            }
        }

        [Fact]
        public void GetFileTypeDisk()
        {
            string tempPath = Storage.GetTempPath();
            using (var directory = Storage.CreateDirectoryHandle(tempPath))
            {
                Storage.GetFileType(directory).Should().Be(FileType.Disk);
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
                using (var handle = Storage.CreateFile(filePath, CreationDisposition.CreateNew))
                {
                    handle.IsInvalid.Should().BeFalse();
                    Storage.FileExists(filePath).Should().BeTrue();
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
                using (var handle = Storage.CreateFile(filePath, CreationDisposition.CreateNew))
                {
                    handle.IsInvalid.Should().BeFalse();
                    Storage.FlushFileBuffers(handle);
                    Storage.FileExists(filePath).Should().BeTrue();
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
                Storage.FileExists(filePath).Should().BeTrue();
                Storage.PathExists(filePath).Should().BeTrue();
                Storage.DirectoryExists(filePath).Should().BeFalse();
            }
        }

        [Fact]
        public void FileNotExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                Storage.FileExists(filePath).Should().BeFalse();
                Storage.PathExists(filePath).Should().BeFalse();
                Storage.DirectoryExists(filePath).Should().BeFalse();
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

                Storage.FileExists(filePath).Should().BeTrue();
                Storage.PathExists(filePath).Should().BeTrue();
                Storage.DirectoryExists(filePath).Should().BeFalse();
            }
        }

        [Fact]
        public void LongPathFileNotExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                string filePath = cleaner.GetTestPath();

                Storage.FileExists(filePath).Should().BeFalse();
                Storage.PathExists(filePath).Should().BeFalse();
                Storage.DirectoryExists(filePath).Should().BeFalse();
            }
        }

        [Fact]
        public void DirectoryExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string directoryPath = cleaner.GetTestPath();
                FileHelper.CreateDirectoryRecursive(directoryPath);

                Storage.FileExists(directoryPath).Should().BeFalse();
                Storage.PathExists(directoryPath).Should().BeTrue();
                Storage.DirectoryExists(directoryPath).Should().BeTrue();
            }
        }

        [Fact]
        public void DirectoryNotExistsTests()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string directoryPath = cleaner.GetTestPath();

                Storage.FileExists(directoryPath).Should().BeFalse();
                Storage.PathExists(directoryPath).Should().BeFalse();
                Storage.DirectoryExists(directoryPath).Should().BeFalse();
            }
        }

        [Fact]
        public void InfoForNonExistantLongPath()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                Storage.TryGetFileInfo(longPath).Should().BeNull();
            }
        }


        [Fact]
        public void FileTypeOfFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                using (var testFile = Storage.CreateFile(cleaner.GetTestPath(), CreationDisposition.CreateNew))
                {
                    Storage.GetFileType(testFile).Should().Be(FileType.Disk);
                }
            }
        }

        [Theory,
            InlineData(@"C:\"),
            InlineData(@"\\?\C:\")
            ]
        public void FindFirstFileHandlesRoots(string path)
        {
            Storage.CreateFindOperation<string>(path).Should().NotBeEmpty();
            Storage.CreateFindOperation<string>(path, null).Should().NotBeEmpty();
        }

        [Fact]
        public void GetFileNameByHandle()
        {
            // Can't open the Users folder in a Store app
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                // @"C:\" -> @"\"
                var fileHandle = Storage.CreateFileSystemIo(
                    @"C:\Users",
                    0,                  // We don't care about read or write, we're just getting metadata with this handle
                    IO.FileShare.ReadWrite,
                    IO.FileMode.Open,
                    0,
                    FileFlags.OpenReparsePoint           // To avoid traversing links
                        | FileFlags.BackupSemantics);    // To open directories

                string name = Storage.GetFileNameLegacy(fileHandle);
                name.Should().Be(@"\Users");
            });
        }

        [Fact]
        public void CreateStreamSystemIoDefines()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                using (var stream = Storage.CreateFileStream(
                    path: filePath,
                    fileAccess: IO.FileAccess.Write,
                    fileShare: IO.FileShare.ReadWrite,
                    fileMode: IO.FileMode.Append,
                    fileAttributes: 0,
                    securityFlags: WindowsStore.IsWindowsStoreApplication()
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
            Action action = () => Storage.GetFullPathName(value);
            action.Should().Throw<System.IO.IOException>();
        }

        [Fact]
        public unsafe void GetDirectoryFilenamesFromHandle()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempDirectory = cleaner.GetTestPath();

                Storage.CreateDirectory(tempDirectory);
                FileHelper.WriteAllText(Paths.Combine(tempDirectory, "GetDirectoryFilenamesFromHandle"), "GetDirectoryFilenamesFromHandle");
                using (var handle = Storage.CreateDirectoryHandle(tempDirectory))
                {
                    Storage.GetDirectoryFilenames(handle).Should().Contain(new string[] { ".", "..", "GetDirectoryFilenamesFromHandle" });
                }
            }
        }

        [Fact]
        public unsafe void GetDirectoryFilenamesFromHandle_EmptyDirectory()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string tempDirectory = cleaner.GetTestPath();

                Storage.CreateDirectory(tempDirectory);
                using (var handle = Storage.CreateDirectoryHandle(tempDirectory))
                {
                    Storage.GetDirectoryFilenames(handle).Should().Contain(new string[] { ".", ".." });
                }
            }
        }

        [Fact(Skip = "This is close to impossible to get to pass consistently")]
        public unsafe void ReadWithOpenHandle()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string cursorPath = cleaner.GetTestPath() + ".cur";

                // Create a handle with read only sharing and leave open
                using (IO.FileStream file = new IO.FileStream(cursorPath, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.Read))
                {
                    // Write out a valid file
                    using (IO.Stream data = TestFiles.GetTestCursor())
                    {
                        data.CopyTo(file);
                    }
                    file.Flush();

                    // See that we can open a handle on it directly
                    using (IO.FileStream file2 = new IO.FileStream(cursorPath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)) { }

                    // Try letting the OS read it in
                    using (Windows.LoadCursorFromFile(cursorPath)) { };
                }
            }
        }
    }
}
