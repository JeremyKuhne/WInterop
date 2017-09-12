// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Tests.Support;
using WInterop.DirectoryManagement;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using WInterop.Support;
using Xunit;

namespace DesktopTests.FileManagement
{
    public class CreateDirectTests
    {
        [Fact]
        public void CreateRelativeToDirectoryHandle()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string fileName = System.IO.Path.GetRandomFileName();
                using (var directory = DirectoryMethods.CreateDirectoryHandle(cleaner.TempFolder))
                {
                    directory.IsInvalid.Should().BeFalse();

                    using (var file = FileMethods.CreateFileRelative(fileName, directory, CreateDisposition.Create))
                    {
                        file.IsInvalid.Should().BeFalse();
                    }

                    FileMethods.FileExists(Paths.Combine(cleaner.TempFolder, fileName)).Should().BeTrue();
                }
            }
        }

        [Fact]
        public void CreateFileDirect()
        {
            using (var cleaner = new TestFileCleaner())
            {
                using (var file = FileMethods.CreateFileDirect(@"\??\" + cleaner.GetTestPath(), CreateDisposition.Create))
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
                string path = cleaner.CreateTestFile("OpenFileDirect");
                using (var file = FileMethods.CreateFileDirect(@"\??\" + path, CreateDisposition.Open))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            }
        }
    }
}
