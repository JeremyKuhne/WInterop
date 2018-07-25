// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Windows;
using WInterop.Windows;
using Xunit;

namespace DesktopTests.Windows
{
    public class WindowsProcedureTests
    {
        static LRESULT CallDefaultProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            return WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        }

        [Fact]
        public void WindowCallback_SendMessage()
        {
            WindowClass myClass = new WindowClass
            {
                ClassName = "WindowCallback_SendMessage",
                WindowProcedure = (window, message, wParam, lParam) =>
                {
                    return 42;
                }
            };

            Atom atom = WindowMethods.RegisterClass(ref myClass);
            atom.IsValid.Should().BeTrue();

            try
            {
                WindowHandle window = WindowMethods.CreateWindow(atom, null, WindowStyles.Minimize | WindowStyles.Diabled);
                window.IsValid.Should().BeTrue();

                try
                {
                    WindowMethods.SendMessage(window, WindowMessage.Activate, 0, 0).Should().Be((LRESULT)42);
                }
                finally
                {
                    WindowMethods.DestroyWindow(window);
                }
            }
            finally
            {
                WindowMethods.UnregisterClass(atom, null);
            }
        }

        [Fact]
        public void WindowCallback_Subclass()
        {
            int value = 42;
            WindowClass myClass = new WindowClass
            {
                ClassName = "WindowCallback_Subclass",
                WindowProcedure = (window, message, wParam, lParam) =>
                {
                    return value;
                }
            };

            Atom atom = WindowMethods.RegisterClass(ref myClass);
            atom.IsValid.Should().BeTrue();

            try
            {
                WindowHandle window = WindowMethods.CreateWindow(atom, null, WindowStyles.Minimize | WindowStyles.Diabled);
                window.IsValid.Should().BeTrue();

                try
                {
                    WindowMethods.SendMessage(window, WindowMessage.Activate, 0, 0).Should().Be((LRESULT)42);

                    IntPtr previous = IntPtr.Zero;
                    WindowProcedure subClass = (w, m, wParam, lParam) =>
                    {
                        return WindowMethods.CallWindowProcedure(previous, w, m, wParam, lParam);
                    };

                    value = 1999;
                    previous = WindowMethods.SetWindowProcedure(window, subClass);
                    WindowMethods.SendMessage(window, WindowMessage.Activate, 0, 0).Should().Be((LRESULT)1999);
                    GC.KeepAlive(subClass);
                }
                finally
                {
                    WindowMethods.DestroyWindow(window);
                }
            }
            finally
            {
                WindowMethods.UnregisterClass(atom, null);
            }
        }
    }
}
