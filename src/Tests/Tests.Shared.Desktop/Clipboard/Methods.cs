// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Clipboard;
using Xunit;

namespace ClipboardTests
{
    public class Methods
    {
        [Fact]
        public void GetClipboardFormatNameForBuiltIn()
        {
            Action action = () => Clipboard.GetClipboardFormatName((uint)ClipboardFormat.CF_TEXT);

            // You can't get the name for built-in types
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GetAvailableClipboardFormatsBasic()
        {
            uint[] formats = Clipboard.GetAvailableClipboardFormats();
            foreach (uint format in formats)
            {
                if (!Enum.IsDefined(typeof(ClipboardFormat), format))
                {
                    string name = Clipboard.GetClipboardFormatName(format);
                    name.Should().NotBeNullOrEmpty();
                }
            }
        }
    }
}
