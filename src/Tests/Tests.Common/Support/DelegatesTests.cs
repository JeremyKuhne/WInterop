// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Support;
using Xunit;

namespace Tests.Support
{
    public class DelegatesTests
    {
        private delegate bool StringToBool(string value);

        [Fact]
        public void SimpleGetDelegate()
        {
            var endsInDirectorySeparator = Delegates.CreateDelegate<StringToBool>(@"WInterop.Support.Paths, " + Delegates.BaseLibrary, "EndsInDirectorySeparator");
            endsInDirectorySeparator.Should().NotBeNull();
            endsInDirectorySeparator(@"a").Should().BeFalse();
            endsInDirectorySeparator(@"a\").Should().BeTrue();
        }
    }
}
