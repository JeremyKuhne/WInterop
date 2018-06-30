// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Linq;
using Tests.Support;
using WInterop.Storage;
using WInterop.Storage.Types;
using WInterop.Support;
using Xunit;

namespace File
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
                FileHelper.WriteAllText(Paths.Combine(cleaner.TempFolder, "foo.txt"), nameof(SimpleFilterFind2));
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
                string subdirA = Paths.Combine(cleaner.TempFolder, "A");
                StorageMethods.CreateDirectory(subdirA);
                string fileB = Paths.Combine(subdirA, "B");
                FileHelper.WriteAllText(fileB, "B file");
                string subdirC = Paths.Combine(subdirA, "C");
                StorageMethods.CreateDirectory(subdirC);
                string subdirD = Paths.Combine(subdirA, "D");
                StorageMethods.CreateDirectory(subdirD);
                string fileE = Paths.Combine(subdirD, "E");
                FileHelper.WriteAllText(fileE, "E file");

                var files = new FindOperation<string>(subdirA, recursive: true).ToArray();
                files.Should().BeEquivalentTo(System.IO.Directory.GetFileSystemEntries(subdirA, "*", System.IO.SearchOption.AllDirectories));
            }
        }
    }
}
