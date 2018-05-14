// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Runtime.InteropServices;
using Tests.Support;
using WInterop.Com;
using WInterop.Com.Types;
using WInterop.FileManagement;
using Xunit;

namespace Tests.Com
{
    public class StorageTests
    {
        [Fact]
        public unsafe void CreateIStorage()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string path = cleaner.GetTestPath();
                object created = ComMethods.CreateStorage(path, InterfaceIds.IID_IStorage);
                created.Should().NotBeNull();
                IStorage storage = created as IStorage;
                storage.Should().NotBeNull();
                FileMethods.FileExists(path).Should().BeTrue();

                storage.Stat(out var stats, StatFlag.Default);
                try
                {
                    string name = new string(stats.pwcsName);
                    name.Should().Be(path);
                    stats.type.Should().Be(StorageType.Storage);
                    stats.clsid.Should().Be(Guid.Empty, "should be null for newly created objects");
                }
                finally
                {
                    Marshal.FreeCoTaskMem((IntPtr)stats.pwcsName);
                }
            }
        }

        [Fact]
        public unsafe void OpenIStorage()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string path = cleaner.GetTestPath();
                IStorage storage = (IStorage)ComMethods.CreateStorage(path, InterfaceIds.IID_IStorage);
                storage.Commit(StorageCommit.Default);
                Marshal.ReleaseComObject(storage);
                storage = (IStorage)ComMethods.OpenStorage(path, InterfaceIds.IID_IStorage);
                storage.Should().NotBeNull();

                storage.Stat(out var stats, StatFlag.Default);
                try
                {
                    string name = new string(stats.pwcsName);
                    name.Should().Be(path);
                    stats.type.Should().Be(StorageType.Storage);
                    stats.clsid.Should().Be(Guid.Empty, "should be null for newly created objects");
                }
                finally
                {
                    Marshal.FreeCoTaskMem((IntPtr)stats.pwcsName);
                }
            }
        }
    }
}
