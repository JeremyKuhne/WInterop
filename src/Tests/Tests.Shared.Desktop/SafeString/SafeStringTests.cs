// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.SafeString;
using WInterop.SafeString.Unsafe;
using WInterop.Support.Buffers;
using Xunit;

namespace DesktopTests.SafeString
{
    public class SafeStringTests
    {
        [Theory,
            InlineData("Fizzle", "FIZZLE"),
            InlineData("POP", "POP"),
            InlineData("craCKLE", "CRACKLE"),
            InlineData("swi\0zzle", "SWI\0ZZLE")]
        public unsafe void ToUpperInvariant(string value, string expected)
        {
            using (var buffer = new StringBuffer(value))
            {
                UNICODE_STRING s = buffer.ToUnicodeString();
                StringMethods.ToUpperInvariant(ref s);
                s.ToString().Should().Be(expected);
            }
        }
    }
}
