// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Linq;
using Tests.Support;
using WInterop.DirectoryManagement;
using WInterop.FileManagement;
using WInterop.Support;
using Xunit;

namespace DesktopTests.FileManagement
{
    public class DirectFindTests
    {
        [Fact]
        public void SimpleFind()
        {
            using (var cleaner = new TestFileCleaner())
            {
                var files = new DirectFindOperation(cleaner.TempFolder).ToArray();
                files.Length.Should().Be(3);
                files[0].FileName.Should().Be(".");
                files[1].FileName.Should().Be("..");
                files[2].FileName.Should().Be("%WinteropFlagFile%");
            }
        }

        [Fact]
        public void SimpleFilterFind()
        {
            using (var cleaner = new TestFileCleaner())
            {
                var files = new DirectFindOperation(cleaner.TempFolder, false, "*win*").ToArray();
                files.Length.Should().Be(1);
                files[0].FileName.Should().Be("%WinteropFlagFile%");
            }
        }

        [Fact]
        public void FindRecursive()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string subdirA = Paths.Combine(cleaner.TempFolder, "A");
                DirectoryMethods.CreateDirectory(subdirA);
                string fileB = Paths.Combine(subdirA, "B");
                FileHelper.WriteAllText(fileB, "B file");
                string subdirC = Paths.Combine(subdirA, "C");
                DirectoryMethods.CreateDirectory(subdirC);
                string subdirD = Paths.Combine(subdirA, "D");
                DirectoryMethods.CreateDirectory(subdirD);
                string fileE = Paths.Combine(subdirD, "E");
                FileHelper.WriteAllText(fileE, "E file");

                var files = new DirectFindOperation(subdirA, recursive: true).ToArray();
            }
        }

        [Fact]
        public void TestGetAllEntries()
        {
            string path = @"P:\repos\corefx";
            string[] files = System.IO.Directory.GetFileSystemEntries(path, "*.txt", System.IO.SearchOption.AllDirectories);

            //string[] directFiles = new DirectFindOperation(path, true, "*.txt").Select(v => Paths.Combine(v.Directory, v.FileName)).ToArray();
            //Console.WriteLine("Hot");
            //directFiles = new DirectFindOperation(path, true, "*.txt").Select(v => Paths.Combine(v.Directory, v.FileName)).ToArray();
            //Console.WriteLine("Done");
        }
    }
}
