// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Clipboard;
using WInterop.Clipboard.DataTypes;
using Xunit;

namespace DesktopTests.ClipboardTests
{
    public class Methods
    {
        [Fact]
        public void GetClipboardFormatNameForBuiltIn()
        {
            Action action = () => ClipboardMethods.GetClipboardFormatName((uint)ClipboardFormat.CF_TEXT);

            // You can't get the name for built-in types
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void GetAvailableClipboardFormatsBasic()
        {
            uint[] formats = ClipboardMethods.GetAvailableClipboardFormats();
            foreach (uint format in formats)
            {
                if (!Enum.IsDefined(typeof(ClipboardFormat), format))
                {
                    string name = ClipboardMethods.GetClipboardFormatName(format);
                    name.Should().NotBeNullOrEmpty();
                }
            }
        }
    }
}
