// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.Windows;
using WInterop.Windows.Types;
using Xunit;

namespace DesktopTests.Windows
{
    public class WindowClassTests
    {
        [Fact]
        public unsafe void WindowClassSize()
        {
            sizeof(WNDCLASSEX).Should().Be(80);
        }

        [Fact]
        public void GetClassInfo_NotRegistered()
        {
            Action action = () => WindowMethods.GetClassInfo(null, Path.GetRandomFileName());
            action.ShouldThrow<IOException>().And
                .HResult.Should().Be((int)ErrorMacros.HRESULT_FROM_WIN32(WindowsError.ERROR_CLASS_DOES_NOT_EXIST));
        }

        [Fact]
        public void GetClassInfo_Global()
        {
            var info = WindowMethods.GetClassInfo(null, "Button");
            info.Background.IsInvalid.Should().BeFalse();
            info.Style.Should().Be(ClassStyle.VerticalRedraw | ClassStyle.HorizontalRedraw | ClassStyle.DoubleClicks | ClassStyle.ParentDeviceContext);
            info.WindowExtraBytes.Should().Be(8);
        }

        [Fact]
        public void GetClassInfo_GlobalAtom()
        {
            var info = WindowMethods.GetClassInfo(null, new Atom(32768));
        }

        [Fact]
        public void CreateWindow_Global()
        {
            WindowHandle window = new WindowHandle();
            try
            {
                window = WindowMethods.CreateWindow("BUTTON", "CreateWindow_Global", WindowStyles.Diabled | WindowStyles.Minimize);
                window.IsValid.Should().BeTrue();
            }
            finally
            {
                if (window.IsValid) WindowMethods.DestroyWindow(window);
            }
        }

        [Fact]
        public void GetClassName_Global()
        {
            WindowHandle window = new WindowHandle();
            try
            {
                window = WindowMethods.CreateWindow("button", "GetClassName_Global", WindowStyles.Diabled | WindowStyles.Minimize);
                window.IsValid.Should().BeTrue();
                WindowMethods.GetClassName(window).Should().Be("Button");
            }
            finally
            {
                if (window.IsValid) WindowMethods.DestroyWindow(window);
            }
        }

        [Fact]
        public void GetClassLong_GlobalAtom()
        {
            WindowHandle window = new WindowHandle();
            try
            {
                window = WindowMethods.CreateWindow("bUttOn", "GetClassLong_GlobalAtom", WindowStyles.Diabled | WindowStyles.Minimize);
                window.IsValid.Should().BeTrue();
                Atom atom = WindowMethods.GetClassLong(window, ClassLong.Atom);
                atom.IsValid.Should().BeTrue();
                WindowMethods.DestroyWindow(window);
                window = WindowMethods.CreateWindow(atom, "GetClassLong_GlobalAtom", WindowStyles.Diabled | WindowStyles.Minimize);
                WindowMethods.GetClassName(window).Should().Be("Button");
            }
            finally
            {
                if (window.IsValid) WindowMethods.DestroyWindow(window);
            }
        }
    }
}
