// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Tests.Support;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using Xunit;

namespace DesktopTests.FileManagement
{
    public class FileInformationTests
    {
        [Fact]
        public void TestFileRights()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string testFile = cleaner.CreateTestFile("TestFileRights");

                // CreateFile ALWAYS adds SYNCHRONIZE & FILE_READ_ATTRIBUTES.
                using (var handle = FileMethods.CreateFile(testFile, CreationDisposition.OpenExisting, DesiredAccess.ReadControl))
                {
                    FileMethods.GetRights(handle).Should().Be(FileAccessRights.ReadAttributes | FileAccessRights.ReadControl | FileAccessRights.Synchronize);
                }

                using (var handle = FileMethods.CreateFile(testFile, CreationDisposition.OpenExisting, DesiredAccess.ReadAttributes))
                {
                    FileMethods.GetRights(handle).Should().Be(FileAccessRights.ReadAttributes | FileAccessRights.Synchronize);
                }

                using (var handle = FileMethods.CreateFile(testFile, CreationDisposition.OpenExisting, DesiredAccess.Synchronize))
                {
                    FileMethods.GetRights(handle).Should().Be(FileAccessRights.ReadAttributes | FileAccessRights.Synchronize);
                }

                using (var handle = FileMethods.CreateFile(testFile, CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
                {
                    FileMethods.GetRights(handle).Should().Be(FileAccessRights.ReadAttributes | FileAccessRights.ReadControl
                        | FileAccessRights.Synchronize | FileAccessRights.ReadData | FileAccessRights.ReadExtendedAttributes);
                }

                // DesiredAccess.Synchronize is required for synchronous access.
                string directTestFile = @"\??\" + testFile;
                using (var handle = FileMethods.CreateFileDirect(directTestFile, CreateDisposition.Open, DesiredAccess.Synchronize))
                {
                    FileMethods.GetRights(handle).Should().Be(FileAccessRights.Synchronize);
                }

                // Open async
                using (var handle = FileMethods.CreateFileDirect(directTestFile, CreateDisposition.Open, DesiredAccess.ReadAttributes, ShareMode.ReadWrite, 0, 0))
                {
                    FileMethods.GetRights(handle).Should().Be(FileAccessRights.ReadAttributes);
                }

                using (var handle = FileMethods.CreateFileDirect(directTestFile, CreateDisposition.Open, DesiredAccess.ReadAttributes | DesiredAccess.Synchronize, ShareMode.ReadWrite, 0, 0))
                {
                    FileMethods.GetRights(handle).Should().Be(FileAccessRights.ReadAttributes | FileAccessRights.Synchronize);
                }
            }
        }
    }
}
