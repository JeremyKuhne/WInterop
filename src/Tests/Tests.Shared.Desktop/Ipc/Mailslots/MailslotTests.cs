// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Tests.Support;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using WInterop.Ipc;
using Xunit;

namespace DesktopTests.Ipc.MailslotTests
{
    public class MailslotTests
    {
        [Fact]
        public void BasicCreateMailslot()
        {
            const string mailslotName = @"\\.\mailslot\basiccreatemailslottest";

            using (var handle = MailslotMethods.CreateMailslot(mailslotName))
            {
                handle.IsInvalid.Should().BeFalse();

                using (var fileHandle = FileMethods.CreateFile(mailslotName, CreationDisposition.OpenExisting, 0))
                {
                    fileHandle.IsInvalid.Should().BeFalse();
                    FileMethods.GetFileType(fileHandle).Should().Be(FileType.Unknown);
                    var modeInfo = FileMethods.GetFileMode(fileHandle);

                    // The mailslot was opened synchronously
                    modeInfo.Should().Be(FileAccessMode.SynchronousNotAlertable);
                }
            }
        }

        [Fact]
        public void CreateMailslotAsync()
        {
            const string mailslotName = @"\\.\mailslot\asynccreatemailslottest";

            using (var handle = MailslotMethods.CreateMailslot(mailslotName))
            {
                handle.IsInvalid.Should().BeFalse();

                using (var fileHandle = FileMethods.CreateFile(mailslotName, CreationDisposition.OpenExisting, 0, ShareMode.ReadWrite,
                    FileAttributes.None, FileFlags.Overlapped))
                {
                    fileHandle.IsInvalid.Should().BeFalse();
                    FileMethods.GetFileType(fileHandle).Should().Be(FileType.Unknown);
                    var modeInfo = FileMethods.GetFileMode(fileHandle);

                    // The mailslot was opened asynchronously (e.g. no synchronous flag)
                    modeInfo.Should().Be((FileAccessMode)0);
                }
            }
        }

        [Fact]
        public void BasicGetInfo()
        {
            using (var handle = MailslotMethods.CreateMailslot(
                name: @"\\?\mailslot\basicgetinfotest",
                maxMessageSize: 256,
                readTimeout: 1000))
            {
                handle.IsInvalid.Should().BeFalse();
                var info = MailslotMethods.GetMailslotInfo(handle);
                info.MessageCount.Should().Be(0);
                info.NextSize.Should().Be(uint.MaxValue);
                info.MaxMessageSize.Should().Be(256);
                info.ReadTimeout.Should().Be(1000);
            }
        }

        [Fact]
        public void BasicGetSetInfoDefaults()
        {
            using (var handle = MailslotMethods.CreateMailslot(@"\\?\mailslot\basicgetsetinfodefaultstest"))
            {
                handle.IsInvalid.Should().BeFalse();
                var info = MailslotMethods.GetMailslotInfo(handle);
                info.MessageCount.Should().Be(0);
                info.NextSize.Should().Be(uint.MaxValue);
                info.MaxMessageSize.Should().Be(0);
                info.ReadTimeout.Should().Be(0);
                MailslotMethods.SetMailslotTimeout(handle, 100);
                MailslotMethods.GetMailslotInfo(handle).ReadTimeout.Should().Be(100);
            }
        }

        [Fact]
        public void BasicWriteMailSlot()
        {
            const string mailslotName = @"\\?\mailslot\basicwritemailslottest";
            using (var mailslotHandle = MailslotMethods.CreateMailslot(mailslotName))
            {
                FileHelper.WriteAllText(mailslotName, "basicwritetest");
            }
        }

        [Fact(Skip = "FileStream's logic for checking synchronous state is broken.")]
        public void BasicReadMailslot()
        {
            const string mailslotName = @"\\?\mailslot\basicreadmailslottest";
            using (var mailslotHandle = MailslotMethods.CreateMailslot(mailslotName))
            {
                string message = FileHelper.ReadAllText(mailslotName);
                message.Should().BeNullOrEmpty();
            }
        }
    }
}
