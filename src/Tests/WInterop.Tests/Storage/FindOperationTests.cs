// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.IO;
using System.Linq;
using Tests.Support;
using WInterop.Storage;
using Xunit;

namespace StorageTests
{
    public class FindOperationTests
    {
        [Fact]
        public void SimpleFind()
        {
            using (var cleaner = new TestFileCleaner())
            {
                var files = new FindOperation<FindResult>(cleaner.TempFolder).ToArray();
                files.Length.Should().Be(1);
                files[0].FileName.Should().Be("%WinteropFlagFile%");
            }
        }

        [Fact]
        public void SimpleFilterFind()
        {
            using (var cleaner = new TestFileCleaner())
            {
                var files = new FindOperation<FindResult>(cleaner.TempFolder, "*win*").ToArray();
                files.Length.Should().Be(1);
                files[0].FileName.Should().Be("%WinteropFlagFile%");
            }
        }

        [Fact]
        public void SimpleFilterFind2()
        {
            using (var cleaner = new TestFileCleaner())
            {
                FileHelper.WriteAllText(Path.Join(cleaner.TempFolder, "foo.txt"), nameof(SimpleFilterFind2));
                var files = new FindOperation<string>(cleaner.TempFolder, "*.txt", true).ToArray();
                files.Length.Should().Be(1);
                files[0].Should().EndWith("foo.txt");
            }
        }

        [Fact]
        public void FindRecursive()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string subdirA = Path.Join(cleaner.TempFolder, "A");
                Storage.CreateDirectory(subdirA);
                string fileB = Path.Join(subdirA, "B");
                FileHelper.WriteAllText(fileB, "B file");
                string subdirC = Path.Join(subdirA, "C");
                Storage.CreateDirectory(subdirC);
                string subdirD = Path.Join(subdirA, "D");
                Storage.CreateDirectory(subdirD);
                string fileE = Path.Join(subdirD, "E");
                FileHelper.WriteAllText(fileE, "E file");

                var files = new FindOperation<string>(subdirA, recursive: true).ToArray();
                files.Should().BeEquivalentTo(Directory.GetFileSystemEntries(subdirA, "*", System.IO.SearchOption.AllDirectories));
            }
        }
    }
}
