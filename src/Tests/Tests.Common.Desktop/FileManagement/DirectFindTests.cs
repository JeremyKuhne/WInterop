// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Linq;
using Tests.Support;
using WInterop.FileManagement;
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
                var files = new DirectFindOperation(cleaner.TempFolder, "*win*").ToArray();
                files.Length.Should().Be(1);
                files[0].FileName.Should().Be("%WinteropFlagFile%");
            }
        }
    }
}
