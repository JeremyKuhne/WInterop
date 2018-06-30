// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Tests.File
{
    using FluentAssertions;
    using WInterop.Storage.Types;
    using Xunit;

    public class FileManagementTypes
    {
        [Fact]
        public unsafe void FileAttributeDataSize()
        {
            sizeof(WIN32_FILE_ATTRIBUTE_DATA).Should().Be(36);

            // An extra sanity check to make sure we're aligning correctly as
            // we're forcing uint size packing.
            WIN32_FILE_ATTRIBUTE_DATA data = new WIN32_FILE_ATTRIBUTE_DATA();
            ((ulong)&data.nFileSize - (ulong)&data).Should().Be(28);
        }

        [Fact]
        public unsafe void Win32FindDataSize()
        {
            sizeof(WIN32_FIND_DATA).Should().Be(592);
        }

        [Fact]
        public unsafe void CopyFile2MessageSize()
        {
            sizeof(COPYFILE2_MESSAGE).Should().Be(WInterop.Support.Environment.Is64BitProcess ? 80 : 72);
        }
    }
}
