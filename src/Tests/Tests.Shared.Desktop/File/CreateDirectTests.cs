// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using FluentAssertions;
using Tests.Support;
using WInterop.Storage;
using WInterop.Support;
using Xunit;

namespace DesktopTests.File
{
    public class CreateDirectTests
    {
        [Fact]
        public void CreateRelativeToDirectoryHandle()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string fileName = System.IO.Path.GetRandomFileName();
                using (var directory = StorageMethods.CreateDirectoryHandle(cleaner.TempFolder))
                {
                    directory.IsInvalid.Should().BeFalse();

                    using (var file = StorageMethods.CreateFileRelative(fileName, directory, CreateDisposition.Create))
                    {
                        file.IsInvalid.Should().BeFalse();
                    }

                    StorageMethods.FileExists(Paths.Combine(cleaner.TempFolder, fileName)).Should().BeTrue();
                }
            }
        }

        [Fact]
        public void CreateFileDirect()
        {
            using (var cleaner = new TestFileCleaner())
            {
                using (var file = StorageMethods.CreateFileDirect(@"\??\" + cleaner.GetTestPath(), CreateDisposition.Create))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            }
        }

        [Fact]
        public void OpenFileDirect()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string path = cleaner.CreateTestFile(nameof(OpenFileDirect));
                using (var file = StorageMethods.CreateFileDirect(@"\??\" + path, CreateDisposition.Open))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            }
        }

        [Fact]
        public void OpenFileDirect_WithSpan()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string path = @"\??\" + cleaner.CreateTestFile(nameof(OpenFileDirect_WithSpan));
                string wrongPath =  path + "foo";
                using (var file = StorageMethods.CreateFileDirect(wrongPath.AsSpan().Slice(0, path.Length), CreateDisposition.Open))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            }
        }

        [Fact]
        public void OpenDirectoryDirect()
        {
            using (var cleaner = new TestFileCleaner())
            {
                using (var directory = StorageMethods.CreateDirectoryHandle(cleaner.TempFolder))
                {
                    directory.IsInvalid.Should().BeFalse("can open the root directory");

                    string name = System.IO.Path.GetRandomFileName();
                    string path = Paths.Combine(cleaner.TempFolder, name);
                    StorageMethods.CreateDirectory(path);

                    using (var subdir = StorageMethods.CreateDirectoryHandle(directory, name))
                    {
                        subdir.IsInvalid.Should().BeFalse("can open subdir from handle");
                    }
                }
            }
        }

        [Fact]
        public void CreateDirectoryDirect()
        {
            using (var cleaner = new TestFileCleaner())
            {
                using (var directory = StorageMethods.CreateDirectoryHandle(cleaner.TempFolder))
                {
                    directory.IsInvalid.Should().BeFalse();

                    string name = System.IO.Path.GetRandomFileName();
                    using (var subdir = StorageMethods.CreateDirectory(directory, name))
                    {
                        subdir.IsInvalid.Should().BeFalse();
                        StorageMethods.DirectoryExists(Paths.Combine(cleaner.TempFolder, name)).Should().BeTrue();
                    }
                }
            }
        }
    }
}
