// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Storage;
using WInterop.Storage.Native;
using Xunit;

namespace StorageTests;

public class FileManagementTypes
{
    [Fact]
    public unsafe void FileAttributeDataSize()
    {
        sizeof(Win32FileAttributeData).Should().Be(36);

        // An extra sanity check to make sure we're aligning correctly as
        // we're forcing uint size packing.
        Win32FileAttributeData data = new Win32FileAttributeData();
        ((ulong)&data.FileSize - (ulong)&data).Should().Be(28);
    }

    [Fact]
    public unsafe void Win32FindDataSize()
    {
        sizeof(Win32FindData).Should().Be(592);
    }

    [Fact]
    public unsafe void CopyFile2MessageSize()
    {
        sizeof(COPYFILE2_MESSAGE).Should().Be(Environment.Is64BitProcess ? 80 : 72);
    }
}
