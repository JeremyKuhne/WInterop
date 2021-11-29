// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Text;
using Tests.Support;
using WInterop.Com;
using Xunit;

namespace ComTests;

public class StreamTests
{
    [Fact]
    public unsafe void ComStreamConstruction()
    {
        using var cleaner = new TestFileCleaner();
        string path = cleaner.GetTestPath();
        IStorage storage = (IStorage)Com.CreateStorage(path, InterfaceIds.IID_IStorage);

        ComStream stream;
        using (stream = new ComStream(storage.CreateStream("mystream", StorageMode.Create | StorageMode.ReadWrite | StorageMode.ShareExclusive)))
        {
            stream.ToString().Should().Be("mystream");
            stream.StorageMode.Should().Be(StorageMode.ReadWrite | StorageMode.ShareExclusive);
            stream.StorageType.Should().Be(StorageType.Stream);
            stream.CanRead.Should().BeTrue();
            stream.CanSeek.Should().BeTrue();
            stream.CanWrite.Should().BeTrue();
            stream.Length.Should().Be(0);
            stream.Position.Should().Be(0);
        }
    }

    [Fact]
    public unsafe void ComStreamTextReadWrite()
    {
        using var cleaner = new TestFileCleaner();
        string path = cleaner.GetTestPath();
        IStorage storage = (IStorage)Com.CreateStorage(path, InterfaceIds.IID_IStorage);

        ComStream stream;
        using (stream = new ComStream(storage.CreateStream("mystream", StorageMode.Create | StorageMode.ReadWrite | StorageMode.ShareExclusive)))
        {
            using (StreamWriter writer = new(stream, Encoding.Unicode, 1024, leaveOpen: true))
            {
                writer.WriteLine("This is line one.");
                stream.Length.Should().Be(0);
                stream.Position.Should().Be(0);
                writer.Flush();
                stream.Length.Should().Be(40);
                stream.Position.Should().Be(40);
                writer.WriteLine("This is line two.");
                stream.Length.Should().Be(40);
                stream.Position.Should().Be(40);
                writer.Flush();
                stream.Length.Should().Be(78);
                stream.Position.Should().Be(78);
            }

            stream.Stream.Should().NotBeNull();

            using (StreamReader reader = new(stream, Encoding.Unicode, detectEncodingFromByteOrderMarks: false, 1024, leaveOpen: true))
            {
                stream.Position = 0;
                stream.Position.Should().Be(0);
                reader.ReadLine().Should().Be("This is line one.");
                reader.ReadLine().Should().Be("This is line two.");
            }

            stream.Stream.Should().NotBeNull();
        }
    }
}
