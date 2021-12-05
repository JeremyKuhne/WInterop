// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Runtime.InteropServices;
using Tests.Support;
using WInterop.Com;
using WInterop.Errors;
using WInterop.Storage;
using Xunit;

namespace ComTests;

public class StorageTests
{
    [Fact]
    public unsafe void CreateIStorage()
    {
        using var cleaner = new TestFileCleaner();
        string path = cleaner.GetTestPath();
        using var storage = Com.CreateStorage(path);
        storage.IsNull.Should().BeFalse();
        Storage.FileExists(path).Should().BeTrue();

        using var stats = storage.Stat(StatFlag.Default);
        stats.Name.ToString().Should().Be(path);
        stats.StorageType.Should().Be(StorageType.Storage);
        stats.ClassId.Should().Be(Guid.Empty, "should be null for newly created objects");
    }

    [Fact]
    public unsafe void IsIStorage()
    {
        using var cleaner = new TestFileCleaner();
        string path = cleaner.GetTestPath();
        using var _ = Com.CreateStorage(path);
        Com.IsStorageFile(path).Should().BeTrue();
    }

    [Fact]
    public unsafe void OpenIStorage()
    {
        using var cleaner = new TestFileCleaner();
        string path = cleaner.GetTestPath();
        Guid guid = Guid.NewGuid();
        using (var storage = Com.CreateStorage(path))
        {
            storage.SetClass(guid);
            storage.Commit();
        }

        using (var storage = Com.OpenStorage(path))
        {
            storage.IsNull.Should().BeFalse();
            using var stats = storage.Stat();
            stats.Name.ToString().Should().Be(path);
            stats.StorageType.Should().Be(StorageType.Storage);
            stats.ClassId.Should().Be(guid);
        }
    }

    [Fact]
    public unsafe void CreateSubStorage()
    {
        using var cleaner = new TestFileCleaner();
        string path = cleaner.GetTestPath();
        using var storage = Com.CreateStorage(path);
        using var subStorage = storage.CreateStorage("substorage", StorageMode.Create | StorageMode.ReadWrite | StorageMode.ShareExclusive);
        subStorage.IsNull.Should().BeFalse();

        using var stats = subStorage.Stat();
        stats.Name.ToString().Should().Be("substorage");
        stats.StorageType.Should().Be(StorageType.Storage);
    }

    [Fact]
    public unsafe void CreateStream()
    {
        using var cleaner = new TestFileCleaner();
        string path = cleaner.GetTestPath();
        using var storage = Com.CreateStorage(path);
        using var stream = storage.CreateStream("mystream", StorageMode.Create | StorageMode.ReadWrite | StorageMode.ShareExclusive);
        stream.IsNull.Should().BeFalse();

        using var stats = stream.Stat();
        stats.Name.ToString().Should().Be("mystream");
        stats.StorageType.Should().Be(StorageType.Stream);
    }

    [Fact]
    public unsafe void RenameStream()
    {
        using var cleaner = new TestFileCleaner();
        string path = cleaner.GetTestPath();
        using var storage = Com.CreateStorage(path);
        using (var stream = storage.CreateStream("name", StorageMode.Create | StorageMode.ReadWrite | StorageMode.ShareExclusive))
        {
            stream.IsNull.Should().BeFalse();

            using var stats = stream.Stat();
            stats.Name.ToString().Should().Be("name");
            stats.StorageType.Should().Be(StorageType.Stream);

            Action action = () => storage.RenameElement("name", "newname");

            // Can't rename until after we close the stream
            action.Should().Throw<COMException>().And.HResult.Should().Be((int)HResult.STG_E_ACCESSDENIED);
        }

        storage.RenameElement("name", "newname");

        Action action2 = () => storage.OpenStream("name", StorageMode.ShareExclusive);
        action2.Should().Throw<COMException>().And.HResult.Should().Be((int)HResult.STG_E_FILENOTFOUND);

        using var renamedStream = storage.OpenStream("newname", StorageMode.ShareExclusive);
        renamedStream.IsNull.Should().BeFalse();
    }

    [Fact]
    public unsafe void EnumStorage()
    {
        using var cleaner = new TestFileCleaner();
        string path = cleaner.GetTestPath();
        using var storage = Com.CreateStorage(path);
        using var stream = storage.CreateStream("mystream", StorageMode.Create | StorageMode.ReadWrite | StorageMode.ShareExclusive);
        using var subStorage = storage.CreateStorage("substorage", StorageMode.Create | StorageMode.ReadWrite | StorageMode.ShareExclusive);
        storage.Commit();

        using var e = storage.Enumerate();
        int count = 0;

        while (e.TryGetNext(out var stats))
        {
            try
            {
                count++;
                switch (stats.StorageType)
                {
                    case StorageType.Storage:
                        stats.Name.ToString().Should().Be("substorage");
                        break;
                    case StorageType.Stream:
                        stats.Name.ToString().Should().Be("mystream");
                        break;
                    default:
                        false.Should().BeTrue($"unexpected type {stats.StorageType}");
                        break;
                }
            }
            finally
            {
                stats.Dispose();
            }
        }

        count.Should().Be(2);
    }
}
