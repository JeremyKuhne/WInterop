// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using Tests.Support;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using WInterop.ProcessAndThreads;
using WInterop.Support;
using WInterop.VolumeManagement;
using Xunit;

namespace DesktopTests.FileManagementTests
{
    public class FileManagementBehaviors
    {
        [Theory,
            Trait("Environment", "CurrentDirectory"),
            InlineData(@"C:", @"C:\Users"),
            InlineData(@"C", @"E:\C")
            ]
        public void ValidateKnownRelativeBehaviors(string value, string expected)
        {
            // TODO: Need to modify to work with actually present drives and skip if there
            // isn't more than one.

            // Set the current directory to D: and the hidden env for C:'s last current directory
            ProcessMethods.SetEnvironmentVariable(@"=C:", @"C:\Users");
            using (new TempCurrentDirectory(@"E:\"))
            {
                FileMethods.GetFullPathName(value).Should().Be(expected);
            }
        }

        [Theory,
            InlineData(@"C:\PROGRA~1", @"C:\Program Files"),
            InlineData(@"C:\.\PROGRA~1", @"C:\.\Program Files"),
            ]
        public void ValidateLongPathNameBehaviors(string value, string expected)
        {
            using (new TempCurrentDirectory(@"C:\Users"))
            {
                FileMethods.GetLongPathName(value).Should().Be(expected);
            }
        }

        [Fact]
        public void LongPathNameThrowsFileNotFound()
        {
            string path = System.IO.Path.GetRandomFileName();
            Action action = () => FileMethods.GetLongPathName(path);
            action.ShouldThrow<System.IO.FileNotFoundException>();
        }

        [Theory,
            InlineData(@"C:"),
            InlineData(@"C:\"),
            InlineData(@"C:\."),
            InlineData(@"\\.\C:\"),
            InlineData(@"\\?\C:\"),
            InlineData(@"\\.\C:"),
            InlineData(@"\\?\C:"),
            ]
        public void CreateFileOnDriveRoot(string path)
        {
            SafeFileHandle handle = FileMethods.CreateFile(
                path,
                CreationDisposition.OpenExisting,
                0,
                ShareMode.ReadWrite,
                FileAttributes.None,
                FileFlags.BackupSemantics);

            handle.IsInvalid.Should().BeFalse();
        }

        [Fact]
        public void CopyFileExNonExistantBehaviors()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string source = cleaner.GetTestPath();
                string destination = cleaner.GetTestPath();

                Action action = () => FileMethods.CopyFileEx(source, destination);
                action.ShouldThrow<System.IO.FileNotFoundException>();

                source = Paths.Combine(source, "file");
                action.ShouldThrow<System.IO.DirectoryNotFoundException>();
            }
        }

        [Fact]
        public void File_CopyNonExistantBehaviors()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string source = cleaner.GetTestPath();
                string destination = cleaner.GetTestPath();

                Action action = () => System.IO.File.Copy(source, destination);
                action.ShouldThrow<System.IO.FileNotFoundException>();

                source = Paths.Combine(source, "file");
                action.ShouldThrow<System.IO.DirectoryNotFoundException>();
            }
        }

        [Theory,
            InlineData(@"*", @"foo.txt", true, true),
            InlineData(@".", @"foo.txt", true, false),
            InlineData(@".", @"footxt", true, false),
            InlineData(@"*.*", @"foo.txt", true, true),
            InlineData(@"*.*", @"foo.", true, true),
            InlineData(@"*.*", @".foo", true, true),
            InlineData(@"*.*", @"footxt", true, false),
            InlineData("<\"*", @"footxt", true, true),              // DOS equivalent of *.*
            InlineData("<\"*", @"foo.txt", true, true),             // DOS equivalent of *.*
            InlineData("<\"*", @".foo", true, true),                // DOS equivalent of *.*
            InlineData("<\"*", @"foo.", true, true),                // DOS equivalent of *.*
            InlineData(">\">", @"a.b", true, true),                 // DOS equivalent of ?.?
            InlineData(">\">", @"a.", true, true),                  // DOS equivalent of ?.?
            InlineData(">\">", @"a", true, true),                   // DOS equivalent of ?.?
            InlineData(">\">", @"ab", true, false),                 // DOS equivalent of ?.?
            InlineData(">\">", @"a.bc", true, false),               // DOS equivalent of ?.?
            InlineData(">\">", @"ab.c", true, false),               // DOS equivalent of ?.?
            InlineData(">>\">>", @"a.b", true, true),               // DOS equivalent of ??.??
            InlineData(">>\"\">>", @"a.b", true, false),            // Not possible to do from DOS ??""??
            InlineData(">>\">>", @"a.bc", true, true),              // DOS equivalent of ??.??
            InlineData(">>\">>", @"ab.ba", true, true),             // DOS equivalent of ??.??
            InlineData(">>\">>", @"ab.", true, true),               // DOS equivalent of ??.??
            InlineData(">>\"\"\">>", @"ab.", true, true),           // Not possible to do from DOS ??"""??
            InlineData(">>b\">>", @"ab.ba", true, false),           // DOS equivalent of ??b.??
            InlineData("a>>\">>", @"ab.ba", true, true),            // DOS equivalent of a??.??
            InlineData(">>\">>a", @"ab.ba", true, false),           // DOS equivalent of ??.??a
            InlineData(">>\"b>>", @"ab.ba", true, true),            // DOS equivalent of ??.b??
            InlineData(">>\"b>>", @"ab.b", true, true),             // DOS equivalent of ??.b??
            InlineData(">>b.>>", @"ab.ba", true, false),
            InlineData("a>>.>>", @"ab.ba", true, true),
            InlineData(">>.>>a", @"ab.ba", true, false),
            InlineData(">>.b>>", @"ab.ba", true, true),
            InlineData(">>.b>>", @"ab.b", true, true),
            InlineData(">>\">>\">>", @"ab.ba", true, true),         // DOS equivalent of ??.??.?? (The last " is an optional period)
            InlineData(">>\">>\">>", @"abba", true, false),         // DOS equivalent of ??.??.?? (The first " isn't, so this doesn't match)
            InlineData(">>\"ab\"ba", @"ab.ba", true, false),        // DOS equivalent of ??.ab.ba
            InlineData("ab\"ba\">>", @"ab.ba", true, true),         // DOS equivalent of ab.ba.??
            InlineData("ab\">>\"ba", @"ab.ba", true, false),        // DOS equivalent of ab.??.ba
            InlineData(">>\">>\">>>", @"ab.ba.cab", true, true),    // DOS equivalent of ??.??.???
            InlineData("a>>\"b>>\"c>>>", @"ab.ba.cab", true, true), // DOS equivalent of a??.b??.c???
            InlineData(@"<", @"a", true, true),                     // DOS equivalent of *.
            InlineData(@"<", @"a.", true, true),                    // DOS equivalent of *.
            InlineData(@"<", @"a. ", true, false),                  // DOS equivalent of *.
            InlineData(@"<", @"a.b", true, false),                  // DOS equivalent of *.
            InlineData(@"foo<", @"foo.", true, true),               // DOS equivalent of foo*.
            InlineData(@"foo<", @"foo. ", true, false),             // DOS equivalent of foo*.
            InlineData(@"<<", @"a.b", true, true),
            InlineData(@"<<", @"a.b.c", true, true),
            InlineData("<\"", @"a.b.c", true, false),
            InlineData(@"<.", @"a", true, false),
            InlineData(@"<.", @"a.", true, true),
            InlineData(@"<.", @"a.b", true, false),
            ]
        public void IsNameInExpression(string expression, string name, bool ignoreCase, bool expected)
        {
            FileMethods.IsNameInExpression(expression, name, ignoreCase).Should().Be(expected,
                $"'{expression}' in '{name}' with ignoreCase of {ignoreCase}");
        }
    }
}
