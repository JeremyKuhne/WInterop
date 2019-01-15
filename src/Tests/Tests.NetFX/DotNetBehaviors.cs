// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using System.Reflection;
using Tests.Support;
using Xunit;

namespace DotNetTests
{
    /// <summary>
    /// Tests to validate and document .NET API behaviors
    /// </summary>
    public class DotNetBehaviors
    {
        [Theory,
            // InlineData(@"", @"")                                     // ArgumentException

            // Basic dot space handling
            InlineData(@"C:\", @"C:\"),
            InlineData(@"C:\ ", @"C:\"),
            InlineData(@"C:\.", @"C:\"),
            InlineData(@"C:\..", @"C:\"),
            InlineData(@"C:\...", @"C:\"),
            // InlineData(@"C:\ .", @"C:\")                              // THROWS
            // InlineData(@"C:\ ..", @"C:\")                             // THROWS
            // InlineData(@"C:\ ...", @"C:\")                            // THROWS
            InlineData(@"C:\. ", @"C:\"),
            InlineData(@"C:\.. ", @"C:\"),
            InlineData(@"C:\... ", @"C:\"),
            InlineData(@"C:\.\", @"C:\"),
            InlineData(@"C:\..\", @"C:\"),
            InlineData(@"C:\...\", @"C:\"),                             // DIFFERS- Native is identical
            InlineData(@"C:\ \", @"C:\"),                               // DIFFERS- Native is identical
            // InlineData(@"C:\ .\", @"C:\ \")                          // THROWS
            // InlineData(@"C:\ ..\", @"C:\ ..\")                       // THROWS
            // InlineData(@"C:\ ...\", @"C:\ ...\")                     // THROWS
            InlineData(@"C:\. \", @"C:\"),                              // DIFFERS- Native is identical
            InlineData(@"C:\.. \", @"C:\"),                             // DIFFERS- Native is identical
            InlineData(@"C:\... \", @"C:\"),                            // DIFFERS- Native is identical
            InlineData(@"C:\A \", @"C:\A\"),                            // DIFFERS- Native is identical
            InlineData(@"C:\A \B", @"C:\A\B"),                          // DIFFERS- Native is identical

            // Basic dot space handling with UNCs
            InlineData(@"\\Server\Share\", @"\\Server\Share\"),
            InlineData(@"\\Server\Share\ ", @"\\Server\Share\"),
            // InlineData(@"\\Server\Share\.", @"\\Server\Share")       // UNCs can eat trailing separator THROWS ArgumentException
            InlineData(@"\\Server\Share\..", @"\\Server\Share"),        // UNCs can eat trailing separator
            InlineData(@"\\Server\Share\...", @"\\Server\Share"),       // DIFFERS- Native has a trailing slash
            // InlineData(@"\\Server\Share\ .", @"\\Server\Share\")     // THROWS
            // InlineData(@"\\Server\Share\ ..", @"\\Server\Share\")    // THROWS
            // InlineData(@"\\Server\Share\ ...", @"\\Server\Share\")   // THROWS
            InlineData(@"\\Server\Share\. ", @"\\Server\Share"),        // DIFFERS- Native has a trailing slash
            InlineData(@"\\Server\Share\.. ", @"\\Server\Share"),       // DIFFERS- Native has a trailing slash
            InlineData(@"\\Server\Share\... ", @"\\Server\Share"),      // DIFFERS- Native has a trailing slash
            InlineData(@"\\Server\Share\.\", @"\\Server\Share\"),
            InlineData(@"\\Server\Share\..\", @"\\Server\Share\"),
            InlineData(@"\\Server\Share\...\", @"\\Server\Share\"),     // DIFFERS- Native is identical

            // InlineData(@"C:\Foo:Bar", @"C:\Foo:Bar")                 // NotSupportedException
            ]
        public void GetFullPathBehaviors(string value, string expected)
        {
            Path.GetFullPath(value).Should().Be(expected, $"source was {value}");
        }


        [Theory,
            // InlineData(@"", @"")                                     // ArgumentException
            InlineData(@".", @"."),
            InlineData(@"..", @".."),
            InlineData(@"...", @".."),                                  // Dot only segments beyond 2 are eaten
            InlineData(@"\...", @"\.."),                                // Dot only segments beyond 2 are eaten
            InlineData(@"\...\", @"\..\"),                              // Dot only segments beyond 2 are eaten
            InlineData(@"...\", @"..\"),                                // Dot only segments beyond 2 are eaten
            InlineData(@"a...", "a"),                                   // All trailing dots are eaten
            InlineData(@"\a...\b", @"\a\b"),                             // All trailing dots are eaten
            InlineData(@"123.../foo", @"123\foo"),
            InlineData(@"\", @"\"),
            InlineData(@"\\", @"\\"),
            InlineData(@"\\Server", @"\\Server"),
            InlineData(@"/", @"\"),
            InlineData(@"  C:\", @"C:\"),                               // Initial whitespace is only eaten if the path begins with C: or \
            InlineData(@"  C", @"  C"),
            InlineData(@" \\", @"\\"),
            InlineData(@" \", @"\"),
            InlineData(@" /", @"\"),
            InlineData(@".\PROGRA~1", @".\PROGRA~1"),
            InlineData(@"C:\PROGRA~1", @"C:\PROGRA~1"),
            // InlineData(@"c:\ . .\foo", @"c:\foo")                    // ArgumentException
            ]
        public void ValidateNoFullCheckBehaviors(string value, string expected)
        {
            NormalizePath(value).Should().Be(expected);
        }

        [Theory,
            InlineData(@"C:\PROGRA~1", @"C:\Program Files"),
            InlineData(@"C:\.\PROGRA~1", @"C:\.\Program Files")
            ]
        public void ValidateNoFullCheckExpandShortPathBehaviors(string value, string expected)
        {
            NormalizePath(value, fullCheck: false, expandShortPaths: true).Should().Be(expected);
        }

        // Long paths need to exist for long path expansion. As such, relative paths like @".\PROGRA~1"
        // could either resolve to @".\PROGRA~1" or @".\Program Files".

        [Fact]
        public void ValidateNoFullCheckExpandShortPathRelativeExists()
        {
            using (new TempCurrentDirectory(@"C:\"))
            {
                NormalizePath(@".\PROGRA~1", fullCheck: false, expandShortPaths: true).Should().Be(@".\Program Files");
            }
        }

        [Fact]
        public void ValidateNoFullCheckExpandShortPathRelativeNotExists()
        {
            using (new TempCurrentDirectory(@"C:\Users"))
            {
                NormalizePath(@".\PROGRA~1", fullCheck: false, expandShortPaths: true).Should().Be(@".\PROGRA~1");
            }
        }

        private static MethodInfo _normalizeMethod;

        private string NormalizePath(string path, bool fullCheck = false, bool expandShortPaths = false, int maxPathLength = 260)
        {
            if (_normalizeMethod == null)
            {
                _normalizeMethod = typeof(Path).GetMethod(
                    "NormalizePath",
                    BindingFlags.Static | BindingFlags.NonPublic,
                    binder: null,
                    types: new Type[] { typeof(string), typeof(bool), typeof(int), typeof(bool) },
                    modifiers: null);
            }

            return _normalizeMethod.Invoke(null, new object[] { path, fullCheck, maxPathLength, expandShortPaths }) as string;
        }

        [Theory,
            // InlineData(@"", @"")                                    // ArgumentException
            InlineData(null, null),
            InlineData(@"..\..\files.txt", @"..\.."),
            InlineData(@"../../files.txt", @"..\.."),
            InlineData(@"..\\..\\files.txt", @"..\.."),
            InlineData(@"\", null),
            InlineData(@"\a", @"\"),
            InlineData(@"/", null),
            InlineData(@"\\", null),
            InlineData(@"\\\", null),
            InlineData(@"\\\a", null),
            InlineData(@"\\\a\", null),
            InlineData(@"\\\a\b", null),
            InlineData(@"\\\a\b\c", @"\\a\b"),
            InlineData(@".\PROGRA~1", @"."),
            InlineData(@"C:\PROGRA~1", @"C:\"),
            InlineData(@".\PROGRA~1\A.TXT", @".\PROGRA~1"),
            InlineData(@"C:\PROGRA~1\A.TXT", @"C:\Program Files"),
            InlineData(@"C:\.\PROGRA~1\A.TXT", @"C:\.\Program Files")
            ]
        public void ValidateGetDirectoryNameBehaviors(string input, string expected)
        {
            Path.GetDirectoryName(input).Should().Be(expected);
        }
    }
}
