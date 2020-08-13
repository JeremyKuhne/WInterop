// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Runtime.InteropServices;
using Tests.Support;
using WInterop.Com;
using WInterop.Errors;
using WInterop.Storage;
using Xunit;

namespace ComTests
{
    public class StorageTests
    {
        [Fact]
        public unsafe void CreateIStorage()
        {
            using var cleaner = new TestFileCleaner();
            string path = cleaner.GetTestPath();
            object created = Com.CreateStorage(path, InterfaceIds.IID_IStorage);
            created.Should().NotBeNull();
            IStorage storage = created as IStorage;
            storage.Should().NotBeNull();
            Storage.FileExists(path).Should().BeTrue();

            storage.Stat(out var stats, StatFlag.Default);
            stats.GetAndFreeString().Should().Be(path);
            stats.type.Should().Be(StorageType.Storage);
            stats.clsid.Should().Be(Guid.Empty, "should be null for newly created objects");
        }

        [Fact]
        public unsafe void IsIStorage()
        {
            using var cleaner = new TestFileCleaner();
            string path = cleaner.GetTestPath();
            object created = Com.CreateStorage(path, InterfaceIds.IID_IStorage);
            Com.IsStorageFile(path).Should().BeTrue();
        }

        [Fact]
        public unsafe void OpenIStorage()
        {
            using var cleaner = new TestFileCleaner();
            string path = cleaner.GetTestPath();
            IStorage storage = (IStorage)Com.CreateStorage(path, InterfaceIds.IID_IStorage);
            Guid guid = new Guid();
            storage.SetClass(ref guid);
            storage.Commit(StorageCommit.Default);
            Marshal.ReleaseComObject(storage);
            storage = (IStorage)Com.OpenStorage(path, InterfaceIds.IID_IStorage);
            storage.Should().NotBeNull();

            storage.Stat(out var stats, StatFlag.Default);
            stats.GetAndFreeString().Should().Be(path);
            stats.type.Should().Be(StorageType.Storage);
            stats.clsid.Should().Be(guid);
        }

        [Fact]
        public unsafe void CreateSubStorage()
        {
            using var cleaner = new TestFileCleaner();
            string path = cleaner.GetTestPath();
            IStorage storage = (IStorage)Com.CreateStorage(path, InterfaceIds.IID_IStorage);
            IStorage subStorage = storage.CreateStorage("substorage", StorageMode.Create | StorageMode.ReadWrite | StorageMode.ShareExclusive);
            subStorage.Should().NotBeNull();

            subStorage.Stat(out var stats, StatFlag.Default);
            stats.GetAndFreeString().Should().Be("substorage");
            stats.type.Should().Be(StorageType.Storage);
        }

        [Fact]
        public unsafe void CreateStream()
        {
            using var cleaner = new TestFileCleaner();
            string path = cleaner.GetTestPath();
            IStorage storage = (IStorage)Com.CreateStorage(path, InterfaceIds.IID_IStorage);
            IStream stream = storage.CreateStream("mystream", StorageMode.Create | StorageMode.ReadWrite | StorageMode.ShareExclusive);
            stream.Should().NotBeNull();

            stream.Stat(out var stats, StatFlag.Default);
            stats.GetAndFreeString().Should().Be("mystream");
            stats.type.Should().Be(StorageType.Stream);
        }

        [Fact]
        public unsafe void RenameStream()
        {
            using var cleaner = new TestFileCleaner();
            string path = cleaner.GetTestPath();
            IStorage storage = (IStorage)Com.CreateStorage(path, InterfaceIds.IID_IStorage);
            IStream stream = storage.CreateStream("name", StorageMode.Create | StorageMode.ReadWrite | StorageMode.ShareExclusive);
            stream.Should().NotBeNull();

            stream.Stat(out var stats, StatFlag.Default);
            stats.GetAndFreeString().Should().Be("name");
            stats.type.Should().Be(StorageType.Stream);

            Action action = () => storage.RenameElement("name", "newname");

            // Can't rename until after we close the stream
            action.Should().Throw<COMException>().And.HResult.Should().Be((int)HResult.STG_E_ACCESSDENIED);
            Marshal.ReleaseComObject(stream);
            action();

            action = () => stream = storage.OpenStream("name", IntPtr.Zero, StorageMode.ShareExclusive);
            action.Should().Throw<COMException>().And.HResult.Should().Be((int)HResult.STG_E_FILENOTFOUND);

            stream = storage.OpenStream("newname", IntPtr.Zero, StorageMode.ShareExclusive);
            stream.Should().NotBeNull();
        }

        [Fact]
        public unsafe void EnumStorage()
        {
            using var cleaner = new TestFileCleaner();
            string path = cleaner.GetTestPath();
            IStorage storage = (IStorage)Com.CreateStorage(path, InterfaceIds.IID_IStorage);
            IStream stream = storage.CreateStream("mystream", StorageMode.Create | StorageMode.ReadWrite | StorageMode.ShareExclusive);
            IStorage subStorage = storage.CreateStorage("substorage", StorageMode.Create | StorageMode.ReadWrite | StorageMode.ShareExclusive);
            storage.Commit();

            IEnumSTATSTG e = storage.EnumElements();
            WInterop.Com.Native.STATSTG stat = default;
            int count = 0;
            while (e.Next(1, ref stat) > 0)
            {
                count++;
                switch (stat.type)
                {
                    case StorageType.Storage:
                        stat.GetAndFreeString().Should().Be("substorage");
                        break;
                    case StorageType.Stream:
                        stat.GetAndFreeString().Should().Be("mystream");
                        break;
                    default:
                        false.Should().BeTrue($"unexpected type {stat.type}");
                        break;
                }
            }

            count.Should().Be(2);
        }
    }
}
