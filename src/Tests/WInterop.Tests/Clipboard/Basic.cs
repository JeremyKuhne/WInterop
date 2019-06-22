// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Linq;
using System.Text;
using WInterop.Clipboard;
using WInterop.Errors;
using WInterop.Windows;
using Xunit;

namespace ClipboardTests
{
    public class Basic
    {
        [Fact(Skip = "This doesn't seem to work in RS5")]
        public void GetClipboardOwner()
        {
            WindowHandle window = Clipboard.GetClipboardOwner();

            // Don't know that this is documented and/or going to be consistent.
            window.GetClassName().Should().Be("CLIPBRDWNDCLASS");
        }

        [Fact]
        public void GetOpenClipboardWindow()
        {
            WindowHandle window = Clipboard.GetOpenClipboardWindow();

            // This could, of course, fail if some window has the clipboard open.
            window.IsNull.Should().BeTrue();
        }

        [Fact(Skip = "This doesn't seem to work in RS5")]
        public void OpenClipboard()
        {
            Clipboard.OpenClipboard();
            try
            {
                WindowHandle window = Clipboard.GetOpenClipboardWindow();
                window.IsNull.Should().BeTrue();

                window = Clipboard.GetClipboardOwner();

                // Don't know that this is documented and/or going to be consistent.
                window.GetClassName().Should().Be("CLIPBRDWNDCLASS");
            }
            finally
            {
                Clipboard.CloseClipboard();
            }
        }

        [Fact]
        public void EmptyClipboard()
        {
            Action action = () => Clipboard.EmptyClipboard();
            action.Should().Throw<WInteropIOException>().And.HResult.Should().Be((int)WindowsError.ERROR_CLIPBOARD_NOT_OPEN.ToHResult());
        }

        [Fact]
        public void OpenAndEmptyClipboard()
        {
            Clipboard.OpenClipboard();
            try
            {
                Clipboard.EmptyClipboard();

                WindowHandle window = Clipboard.GetClipboardOwner();
                window.IsNull.Should().BeTrue();
            }
            finally
            {
                Clipboard.CloseClipboard();
            }
        }

        [Fact(Skip = "Setting isn't working when run with other tests")]
        public void SetClipboardText()
        {
            Clipboard.OpenClipboard();
            try
            {
                Clipboard.EmptyClipboard();
                Clipboard.SetClipboardUnicodeText(nameof(SetClipboardText).AsSpan());
                ClipboardFormat[] formats = Clipboard.GetAvailableClipboardFormats().Select((uint f) => (ClipboardFormat)f).ToArray();
                formats.Should().Contain(ClipboardFormat.UnicodeText);
                formats.Length.Should().Be(1);
            }
            finally
            {
                Clipboard.CloseClipboard();
            }
        }

        [Fact(Skip = "Setting isn't working when run with other tests")]
        public void SetClipboardRtf()
        {
            Clipboard.OpenClipboard();
            try
            {
                Clipboard.EmptyClipboard();
                Clipboard.SetClipboardUnicodeText("This is a test.".AsSpan());
                uint format = Clipboard.RegisterClipboardFormat("Rich Text Format");
                string rich = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\fhiminor\f0\fnil\fcharset0 Calibri;}{\f1\fmodern\fcharset0 Consolas;}}\uc1\pard\sl0\slmult1\fs22\b This\b0  is a \ul test\ul0 .}";
                byte[] bytes = Encoding.ASCII.GetBytes(rich);
                Clipboard.SetClipboardBinaryData(
                   bytes.AsSpan(),
                   (ClipboardFormat)format);
            }
            finally
            {
                Clipboard.CloseClipboard();
            }
        }

        [Fact]
        public void GetClipboardFormatNameForBuiltIn()
        {
            Action action = () => Clipboard.GetClipboardFormatName((uint)ClipboardFormat.Text);

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
