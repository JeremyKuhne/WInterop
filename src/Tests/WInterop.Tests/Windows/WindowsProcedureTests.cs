﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;
using WInterop.Windows.Native;

namespace WindowsTests;

public class WindowsProcedureTests
{
    private static LResult CallDefaultProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
    }

    [Fact]
    public void WindowCallback_SendMessage()
    {
        WindowClassInfo myClass = new((window, message, wParam, lParam) =>
        {
            return 42;
        })
        {
            ClassName = "WindowCallback_SendMessage",
        };

        Atom atom = Windows.RegisterClass(myClass);
        atom.IsValid.Should().BeTrue();

        try
        {
            WindowHandle window = Windows.CreateWindow(atom, style: WindowStyles.Minimize | WindowStyles.Diabled);
            window.IsInvalid.Should().BeFalse();

            try
            {
                window.SendMessage(MessageType.Activate).Should().Be((LResult)42);
            }
            finally
            {
                window.DestroyWindow();
            }
        }
        finally
        {
            Windows.UnregisterClass(atom);
        }
    }

    [Fact]
    public void WindowCallback_Subclass()
    {
        int value = 42;
        WindowClassInfo myClass = new((window, message, wParam, lParam) =>
        {
            return value;
        })
        {
            ClassName = "WindowCallback_Subclass"
        };

        Atom atom = Windows.RegisterClass(myClass);
        atom.IsValid.Should().BeTrue();

        try
        {
            WindowHandle window = Windows.CreateWindow(atom, style: WindowStyles.Minimize | WindowStyles.Diabled);
            window.IsInvalid.Should().BeFalse();

            try
            {
                Windows.SendMessage(window, MessageType.Activate, 0, 0).Should().Be((LResult)42);

                WNDPROC previous = default;
                WindowProcedure subClass = (w, m, wParam, lParam) =>
                {
                    return Windows.CallWindowProcedure(previous, w, m, wParam, lParam);
                };

                value = 1999;
                previous = Windows.SetWindowProcedure(window, subClass);
                Windows.SendMessage(window, MessageType.Activate, 0, 0).Should().Be((LResult)1999);
                GC.KeepAlive(subClass);
            }
            finally
            {
                Windows.DestroyWindow(window);
            }
        }
        finally
        {
            Windows.UnregisterClass(atom, null);
        }
    }
}
