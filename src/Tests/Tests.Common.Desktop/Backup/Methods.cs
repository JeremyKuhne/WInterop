// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using FluentAssertions;
using WInterop.Backup;
using Tests.Support;
using Xunit;

namespace DesktopTests.BackupTests
{
    public class Methods
    {
        [Fact]
        public void NoAlternateStreamData()
        {
            using (var cleaner = new TestFileCleaner())
            {
                BackupDesktopMethods.GetAlternateStreamInformation(cleaner.CreateTestFile("NoAlternateStreamData")).Should().BeEmpty();
            }
        }

        [Fact]
        public void OneAlternateDataStream()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string testFile = cleaner.CreateTestFile("OneAlternateDataStream");
                string firstStream = testFile + ":First";
                FileHelper.WriteAllText(firstStream, "First alternate data stream");

                var info = BackupDesktopMethods.GetAlternateStreamInformation(testFile);
                info.Should().HaveCount(1);
                info.First().Name.Should().Be(":First:$DATA");
            }
        }
    }
}
