// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Windows;
using Xunit;

namespace WindowsTests
{
    public class ThemesTests
    {
        [Fact]
        public unsafe void GetCurrentThemeName()
        {
            string themeName = Themes.GetCurrentThemeName();

            // Name would be something like: C:\WINDOWS\resources\themes\Aero\Aero.msstyles

            themeName.Length.Should().BeGreaterThan(0);
            themeName.Should().EndWith(".msstyles");
        }
    }
}
