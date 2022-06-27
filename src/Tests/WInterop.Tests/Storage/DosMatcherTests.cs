// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Storage;

namespace StorageTests;

public class DosMatcherTests
{
    [Theory, MemberData(nameof(DosMatchData))]
    public static void DosMatch(string expression, string name, bool ignoreCase, bool expected)
    {
        DosMatcher.MatchPattern(expression, name.AsSpan(), ignoreCase).Should().Be(expected);
    }

    public static TheoryData<string, string, bool, bool> DosMatchData => new()
    {
            { null, "", false, false },
            { null, "", true, false },
            { "*", "", false, false },
            { "*", "", true, false },
            { "*", "ab", false, true },
            { "*", "AB", true, true },
            { "*foo", "foo", false, true },
            { "*foo", "foo", true, true },
            { "*foo", "FOO", false, false },
            { "*foo", "FOO", true, true },
            { "*foo", "nofoo", true, true },
            { "*foo", "NoFOO", true, true },
            { "*foo", "noFOO", false, false },

            { @"*", @"foo.txt", true, true },
            { @".", @"foo.txt", true, false },
            { @".", @"footxt", true, false },
            { @"*.*", @"foo.txt", true, true },
            { @"*.*", @"foo.", true, true },
            { @"*.*", @".foo", true, true },
            { @"*.*", @"footxt", true, false },
            { "<\"*", @"footxt", true, true },              // DOS equivalent of *.*
            { "<\"*", @"foo.txt", true, true },             // DOS equivalent of *.*
            { "<\"*", @".foo", true, true },                // DOS equivalent of *.*
            { "<\"*", @"foo.", true, true },                // DOS equivalent of *.*
            { ">\">", @"a.b", true, true },                 // DOS equivalent of ?.?
            { ">\">", @"a.", true, true },                  // DOS equivalent of ?.?
            { ">\">", @"a", true, true },                   // DOS equivalent of ?.?
            { ">\">", @"ab", true, false },                 // DOS equivalent of ?.?
            { ">\">", @"a.bc", true, false },               // DOS equivalent of ?.?
            { ">\">", @"ab.c", true, false },               // DOS equivalent of ?.?
            { ">>\">>", @"a.b", true, true },               // DOS equivalent of ??.??
            { ">>\"\">>", @"a.b", true, false },            // Not possible to do from DOS ??""??
            { ">>\">>", @"a.bc", true, true },              // DOS equivalent of ??.??
            { ">>\">>", @"ab.ba", true, true },             // DOS equivalent of ??.??
            { ">>\">>", @"ab.", true, true },               // DOS equivalent of ??.??
            { ">>\"\"\">>", @"ab.", true, true },           // Not possible to do from DOS ??"""??
            { ">>b\">>", @"ab.ba", true, false },           // DOS equivalent of ??b.??
            { "a>>\">>", @"ab.ba", true, true },            // DOS equivalent of a??.??
            { ">>\">>a", @"ab.ba", true, false },           // DOS equivalent of ??.??a
            { ">>\"b>>", @"ab.ba", true, true },            // DOS equivalent of ??.b??
            { ">>\"b>>", @"ab.b", true, true },             // DOS equivalent of ??.b??
            { ">>b.>>", @"ab.ba", true, false },
            { "a>>.>>", @"ab.ba", true, true },
            { ">>.>>a", @"ab.ba", true, false },
            { ">>.b>>", @"ab.ba", true, true },
            { ">>.b>>", @"ab.b", true, true },
            { ">>\">>\">>", @"ab.ba", true, true },         // DOS equivalent of ??.??.?? (The last " is an optional period)
            { ">>\">>\">>", @"abba", true, false },         // DOS equivalent of ??.??.?? (The first " isn't, so this doesn't match)
            { ">>\"ab\"ba", @"ab.ba", true, false },        // DOS equivalent of ??.ab.ba
            { "ab\"ba\">>", @"ab.ba", true, true },         // DOS equivalent of ab.ba.??
            { "ab\">>\"ba", @"ab.ba", true, false },        // DOS equivalent of ab.??.ba
            { ">>\">>\">>>", @"ab.ba.cab", true, true },    // DOS equivalent of ??.??.???
            { "a>>\"b>>\"c>>>", @"ab.ba.cab", true, true }, // DOS equivalent of a??.b??.c???
            { @"<", @"a", true, true },                     // DOS equivalent of *.
            { @"<", @"a.", true, true },                    // DOS equivalent of *.
            { @"<", @"a. ", true, false },                  // DOS equivalent of *.
            { @"<", @"a.b", true, false },                  // DOS equivalent of *.
            { @"foo<", @"foo.", true, true },               // DOS equivalent of foo*.
            { @"foo<", @"foo. ", true, false },             // DOS equivalent of foo*.
            { @"<<", @"a.b", true, true },
            { @"<<", @"a.b.c", true, true },
            { "<\"", @"a.b.c", true, false },
            { @"<.", @"a", true, false },
            { @"<.", @"a.", true, true },
            { @"<.", @"a.b", true, false },
        };
}
