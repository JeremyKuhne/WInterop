// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Linq;
using WInterop.DirectoryManagement;
using WInterop.FileManagement;
using Tests.Support;
using WInterop.Utility;
using Xunit;

namespace Tests.FileManagementTests
{
    public partial class Behaviors
    {
        [Theory
            // Basic dot space handling
            InlineData(@"C:\", @"C:\")
            InlineData(@"C:\ ", @"C:\")
            InlineData(@"C:\.", @"C:\")
            InlineData(@"C:\..", @"C:\")
            InlineData(@"C:\...", @"C:\")
            InlineData(@"C:\ .", @"C:\")
            InlineData(@"C:\ ..", @"C:\")
            InlineData(@"C:\ ...", @"C:\")
            InlineData(@"C:\. ", @"C:\")
            InlineData(@"C:\.. ", @"C:\")
            InlineData(@"C:\... ", @"C:\")
            InlineData(@"C:\.\", @"C:\")
            InlineData(@"C:\..\", @"C:\")
            InlineData(@"C:\...\", @"C:\...\")
            InlineData(@"C:\ \", @"C:\ \")
            InlineData(@"C:\ .\", @"C:\ \")
            InlineData(@"C:\ ..\", @"C:\ ..\")
            InlineData(@"C:\ ...\", @"C:\ ...\")
            InlineData(@"C:\. \", @"C:\. \")
            InlineData(@"C:\.. \", @"C:\.. \")
            InlineData(@"C:\... \", @"C:\... \")
            InlineData(@"C:\A \", @"C:\A \")
            InlineData(@"C:\A \B", @"C:\A \B")

            // Same as above with prefix
            InlineData(@"\\?\C:\", @"\\?\C:\")
            InlineData(@"\\?\C:\ ", @"\\?\C:\")
            InlineData(@"\\?\C:\.", @"\\?\C:")           // Changes behavior, without \\?\, returns C:\
            InlineData(@"\\?\C:\..", @"\\?\")            // Changes behavior, without \\?\, returns C:\
            InlineData(@"\\?\C:\...", @"\\?\C:\")
            InlineData(@"\\?\C:\ .", @"\\?\C:\")
            InlineData(@"\\?\C:\ ..", @"\\?\C:\")
            InlineData(@"\\?\C:\ ...", @"\\?\C:\")
            InlineData(@"\\?\C:\. ", @"\\?\C:\")
            InlineData(@"\\?\C:\.. ", @"\\?\C:\")
            InlineData(@"\\?\C:\... ", @"\\?\C:\")
            InlineData(@"\\?\C:\.\", @"\\?\C:\")
            InlineData(@"\\?\C:\..\", @"\\?\")           // Changes behavior, without \\?\, returns C:\
            InlineData(@"\\?\C:\...\", @"\\?\C:\...\")

            // How deep can we go with prefix
            InlineData(@"\\?\C:\..\..", @"\\?\")
            InlineData(@"\\?\C:\..\..\..", @"\\?\")

            // Pipe tests
            InlineData(@"\\.\pipe", @"\\.\pipe")
            InlineData(@"\\.\pipe\", @"\\.\pipe\")
            InlineData(@"\\?\pipe", @"\\?\pipe")
            InlineData(@"\\?\pipe\", @"\\?\pipe\")

            // Basic dot space handling with UNCs
            InlineData(@"\\Server\Share\", @"\\Server\Share\")
            InlineData(@"\\Server\Share\ ", @"\\Server\Share\")
            InlineData(@"\\Server\Share\.", @"\\Server\Share")                      // UNCs can eat trailing separator
            InlineData(@"\\Server\Share\..", @"\\Server\Share")                     // UNCs can eat trailing separator
            InlineData(@"\\Server\Share\...", @"\\Server\Share\")
            InlineData(@"\\Server\Share\ .", @"\\Server\Share\")
            InlineData(@"\\Server\Share\ ..", @"\\Server\Share\")
            InlineData(@"\\Server\Share\ ...", @"\\Server\Share\")
            InlineData(@"\\Server\Share\. ", @"\\Server\Share\")
            InlineData(@"\\Server\Share\.. ", @"\\Server\Share\")
            InlineData(@"\\Server\Share\... ", @"\\Server\Share\")
            InlineData(@"\\Server\Share\.\", @"\\Server\Share\")
            InlineData(@"\\Server\Share\..\", @"\\Server\Share\")
            InlineData(@"\\Server\Share\...\", @"\\Server\Share\...\")

            // Slash direction makes no difference
            InlineData(@"//Server\Share\", @"\\Server\Share\")
            InlineData(@"//Server\Share\ ", @"\\Server\Share\")
            InlineData(@"//Server\Share\.", @"\\Server\Share")                      // UNCs can eat trailing separator
            InlineData(@"//Server\Share\..", @"\\Server\Share")                     // UNCs can eat trailing separator
            InlineData(@"//Server\Share\...", @"\\Server\Share\")
            InlineData(@"//Server\Share\ .", @"\\Server\Share\")
            InlineData(@"//Server\Share\ ..", @"\\Server\Share\")
            InlineData(@"//Server\Share\ ...", @"\\Server\Share\")
            InlineData(@"//Server\Share\. ", @"\\Server\Share\")
            InlineData(@"//Server\Share\.. ", @"\\Server\Share\")
            InlineData(@"//Server\Share\... ", @"\\Server\Share\")
            InlineData(@"//Server\Share\.\", @"\\Server\Share\")
            InlineData(@"//Server\Share\..\", @"\\Server\Share\")
            InlineData(@"//Server\Share\...\", @"\\Server\Share\...\")

            // Slash count breaks rooting
            InlineData(@"\\\Server\Share\", @"\\\Server\Share\")
            InlineData(@"\\\Server\Share\ ", @"\\\Server\Share\")
            InlineData(@"\\\Server\Share\.", @"\\\Server\Share")                     // UNCs can eat trailing separator
            InlineData(@"\\\Server\Share\..", @"\\\Server")                          // Paths without 2 initial slashes will not root the share
            InlineData(@"\\\Server\Share\...", @"\\\Server\Share\")
            InlineData(@"\\\Server\Share\ .", @"\\\Server\Share\")
            InlineData(@"\\\Server\Share\ ..", @"\\\Server\Share\")
            InlineData(@"\\\Server\Share\ ...", @"\\\Server\Share\")
            InlineData(@"\\\Server\Share\. ", @"\\\Server\Share\")
            InlineData(@"\\\Server\Share\.. ", @"\\\Server\Share\")
            InlineData(@"\\\Server\Share\... ", @"\\\Server\Share\")
            InlineData(@"\\\Server\Share\.\", @"\\\Server\Share\")
            InlineData(@"\\\Server\Share\..\", @"\\\Server\")                       // Paths without 2 initial slashes will not root the share
            InlineData(@"\\\Server\Share\...\", @"\\\Server\Share\...\")

            // Inital slash count is always kept
            InlineData(@"\\\\Server\Share\", @"\\\Server\Share\")
            InlineData(@"\\\\\Server\Share\", @"\\\Server\Share\")

            // Extended paths root to \\?\
            InlineData(@"\\?\UNC\Server\Share\", @"\\?\UNC\Server\Share\")
            InlineData(@"\\?\UNC\Server\Share\ ", @"\\?\UNC\Server\Share\")
            InlineData(@"\\?\UNC\Server\Share\.", @"\\?\UNC\Server\Share")
            InlineData(@"\\?\UNC\Server\Share\..", @"\\?\UNC\Server")               // Extended UNCs can eat into Server\Share
            InlineData(@"\\?\UNC\Server\Share\...", @"\\?\UNC\Server\Share\")
            InlineData(@"\\?\UNC\Server\Share\ .", @"\\?\UNC\Server\Share\")
            InlineData(@"\\?\UNC\Server\Share\ ..", @"\\?\UNC\Server\Share\")
            InlineData(@"\\?\UNC\Server\Share\ ...", @"\\?\UNC\Server\Share\")
            InlineData(@"\\?\UNC\Server\Share\. ", @"\\?\UNC\Server\Share\")
            InlineData(@"\\?\UNC\Server\Share\.. ", @"\\?\UNC\Server\Share\")
            InlineData(@"\\?\UNC\Server\Share\... ", @"\\?\UNC\Server\Share\")
            InlineData(@"\\?\UNC\Server\Share\.\", @"\\?\UNC\Server\Share\")
            InlineData(@"\\?\UNC\Server\Share\..\", @"\\?\UNC\Server\")             // Extended UNCs can eat into Server\Share
            InlineData(@"\\?\UNC\Server\Share\...\", @"\\?\UNC\Server\Share\...\")

            // How deep can we go with prefix
            InlineData(@"\\?\UNC\Server\Share\..\..", @"\\?\UNC")
            InlineData(@"\\?\UNC\Server\Share\..\..\..", @"\\?\")
            InlineData(@"\\?\UNC\Server\Share\..\..\..\..", @"\\?\")

            // Root slash behavior
            InlineData(@"C:/", @"C:\")
            InlineData(@"C:/..", @"C:\")
            InlineData(@"//Server/Share", @"\\Server\Share")
            InlineData(@"//Server/Share/..", @"\\Server\Share")
            InlineData(@"//Server//Share", @"\\Server\Share")
            InlineData(@"//Server//Share/..", @"\\Server\")                         // Double slash shares normalize but don't root correctly
            InlineData(@"//Server\\Share/..", @"\\Server\")
            InlineData(@"//?/", @"\\?\")
            // InlineData(@"\??\", @"D:\??\")                                       // \??\ will return the current directory's drive if passed to GetFullPathName
            // InlineData(@"/??/", @"D:\??\")
            InlineData(@"//./", @"\\.\")

            // Device behavior
            InlineData(@"CON", @"\\.\CON")
            InlineData(@"CON:Alt", @"\\.\CON")
            InlineData(@"LPT9", @"\\.\LPT9")

            InlineData(@"C:\A\B\.\..\C", @"C:\A\C")
            ]
        public void ValidateKnownFixedBehaviors(string value, string expected)
        {
            FileMethods.GetFullPathName(value).Should().Be(expected, $"source was {value}");
        }

        [Fact]
        public void GetFullPathNameLongPathBehaviors()
        {
            // ERROR_FILENAME_EXCED_RANGE (206)
            // GetFullPathName will fail if the passed in patch is longer than short.MaxValue - 2, even if the path will normalize below that value.
            // FileMethods.GetFullPathName(PathGenerator.CreatePathOfLength(@"C:\..\..\..\..", short.MaxValue - 2));

            // ERROR_INVALID_NAME (123)
            // GetFullPathName will fail if the passed in path normalizes over short.MaxValue - 2
            // FileMethods.GetFullPathName(new string('a', short.MaxValue - 2));

            FileMethods.GetFullPathName(PathGenerator.CreatePathOfLength(FileMethods.GetTempPath(), short.MaxValue - 2));

            // Works
            // NativeMethods.FileManagement.GetFullPathName(PathGenerator.CreatePathOfLength(@"C:\", short.MaxValue - 2));
        }
    }
}
