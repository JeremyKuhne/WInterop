// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Diagnostics;
using System.Linq;
using Tests.Support;
using WInterop.Storage;
using Xunit;

namespace DesktopTests.File
{
    public class FileInformationTests
    {
        [Fact]
        public void TestFileRights()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string testFile = cleaner.CreateTestFile(nameof(TestFileRights));

                // CreateFile ALWAYS adds SYNCHRONIZE & FILE_READ_ATTRIBUTES.
                using (var handle = StorageMethods.CreateFile(testFile, CreationDisposition.OpenExisting, DesiredAccess.ReadControl))
                {
                    StorageMethods.GetRights(handle).Should().Be(FileAccessRights.ReadAttributes | FileAccessRights.ReadControl | FileAccessRights.Synchronize);
                }

                using (var handle = StorageMethods.CreateFile(testFile, CreationDisposition.OpenExisting, DesiredAccess.ReadAttributes))
                {
                    StorageMethods.GetRights(handle).Should().Be(FileAccessRights.ReadAttributes | FileAccessRights.Synchronize);
                }

                using (var handle = StorageMethods.CreateFile(testFile, CreationDisposition.OpenExisting, DesiredAccess.Synchronize))
                {
                    StorageMethods.GetRights(handle).Should().Be(FileAccessRights.ReadAttributes | FileAccessRights.Synchronize);
                }

                using (var handle = StorageMethods.CreateFile(testFile, CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
                {
                    StorageMethods.GetRights(handle).Should().Be(FileAccessRights.ReadAttributes | FileAccessRights.ReadControl
                        | FileAccessRights.Synchronize | FileAccessRights.ReadData | FileAccessRights.ReadExtendedAttributes);
                }

                // DesiredAccess.Synchronize is required for synchronous access.
                string directTestFile = @"\??\" + testFile;
                using (var handle = StorageMethods.CreateFileDirect(directTestFile, CreateDisposition.Open, DesiredAccess.Synchronize))
                {
                    StorageMethods.GetRights(handle).Should().Be(FileAccessRights.Synchronize);
                }

                // Open async
                using (var handle = StorageMethods.CreateFileDirect(directTestFile, CreateDisposition.Open, DesiredAccess.ReadAttributes, ShareModes.ReadWrite, 0, 0))
                {
                    StorageMethods.GetRights(handle).Should().Be(FileAccessRights.ReadAttributes);
                }

                using (var handle = StorageMethods.CreateFileDirect(directTestFile, CreateDisposition.Open, DesiredAccess.ReadAttributes | DesiredAccess.Synchronize, ShareModes.ReadWrite, 0, 0))
                {
                    StorageMethods.GetRights(handle).Should().Be(FileAccessRights.ReadAttributes | FileAccessRights.Synchronize);
                }
            }
        }

        [Fact(Skip = "This test is difficult to get timing to go correctly.")]
        public void ProcessWithActiveHandle()
        {
            using (var cleaner = new TestFileCleaner())
            {
                const int MAX_TRIES = 10;

                // It is tricky to get an open handle conflict- give it a few tries
                for (int i = 0; i < MAX_TRIES; i++)
                {
                    string testFile = cleaner.CreateTestFile(nameof(ProcessWithActiveHandle));

                    // Create an open handle on the test file. Note that this must come first as
                    // the redirect (>>) will refuse to execute if there is already an open handle.
                    Process process = null;
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = $"/K copy con >> {testFile}",
                        WindowStyle = ProcessWindowStyle.Hidden
                    };

                    try
                    {
                        process = Process.Start(startInfo);

                        // The existing cmd handle won't let us access much
                        using (var handle = StorageMethods.CreateFile(testFile, CreationDisposition.OpenExisting,
                            DesiredAccess.ReadAttributes, ShareModes.ReadWrite | ShareModes.Delete))
                        {
                            var ids = StorageMethods.GetProcessIds(handle);
                            if (ids.Count() == 0)
                                continue;

                            // If this ends up being problematic (i.e. antivirus gets us a count of 2)
                            // we can look to see we have an id that matches the known process and that we
                            // don't contain our own process id.
                            ids.Should().HaveCount(1);
                            ((int)(ulong)ids.First()).Should().Be(process.Id);
                            return;
                        }
                    }
                    finally
                    {
                        process?.CloseMainWindow();
                    }
                }

                false.Should().BeTrue($"Did not get a locked file in {MAX_TRIES} tries.");
            }
        }
    }
}
