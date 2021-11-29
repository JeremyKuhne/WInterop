// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Errors;
using WInterop.Modules;
using WInterop.Windows;
using WInterop.Windows.Native;
using Xunit;

namespace WindowsTests;

public class WindowClassTests
{
    [Fact]
    public unsafe void AtomSize()
    {
        sizeof(Atom).Should().Be(2);
    }

    [Fact]
    public unsafe void CreateStructSize()
    {
        sizeof(CREATESTRUCT).Should().Be(Environment.Is64BitProcess ? 80 : 48);
    }

    [Fact]
    public unsafe void WindowClassSize()
    {
        sizeof(WNDCLASSEX).Should().Be(Environment.Is64BitProcess ? 80 : 48);
    }

    [Fact]
    public void GetClassInfo_NotRegistered()
    {
        Action action = () => Windows.GetClassInfo(null, Path.GetRandomFileName());
        action.Should().Throw<IOException>().And
            .HResult.Should().Be((int)WindowsError.ERROR_CLASS_DOES_NOT_EXIST.ToHResult());
    }

    [Fact]
    public void GetClassInfo_Global()
    {
        var info = Windows.GetClassInfo(null, "Button");
        info.Background.IsInvalid.Should().BeTrue();
        info.Style.Should().Be(ClassStyle.VerticalRedraw | ClassStyle.HorizontalRedraw | ClassStyle.DoubleClicks | ClassStyle.ParentDeviceContext);
        info.WindowExtraBytes.Should().Be(8);
    }

    [Fact]
    public void GetClassInfo_GlobalAtom()
    {
        var info = Windows.GetClassInfo(null, new Atom(32768));
    }

    [Fact]
    public void CreateWindow_Global()
    {
        WindowHandle window = default;
        try
        {
            window = Windows.CreateWindow("BUTTON", "CreateWindow_Global", WindowStyles.Diabled | WindowStyles.Minimize);
            window.IsInvalid.Should().BeFalse();
        }
        finally
        {
            if (!window.IsInvalid) window.DestroyWindow();
        }
    }

    [Fact]
    public void GetClassName_Global()
    {
        WindowHandle window = default;
        try
        {
            window = Windows.CreateWindow("button", "GetClassName_Global", WindowStyles.Diabled | WindowStyles.Minimize);
            window.IsInvalid.Should().BeFalse();
            window.GetClassName().Should().Be("Button");
        }
        finally
        {
            if (!window.IsInvalid) window.DestroyWindow();
        }
    }

    [Fact]
    public void GetClassLong_GlobalAtom()
    {
        WindowHandle window = new();
        try
        {
            window = Windows.CreateWindow("bUttOn", "GetClassLong_GlobalAtom", WindowStyles.Diabled | WindowStyles.Minimize);
            window.IsInvalid.Should().BeFalse();
            Atom atom = window.GetClassLong(ClassLong.Atom);
            atom.IsValid.Should().BeTrue();
            window.DestroyWindow();
            window = Windows.CreateWindow(atom, "GetClassLong_GlobalAtom", WindowStyles.Diabled | WindowStyles.Minimize);
            window.GetClassName().Should().Be("Button");
        }
        finally
        {
            if (!window.IsInvalid) window.DestroyWindow();
        }
    }

    private static LResult CallDefaultProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        return window.DefaultWindowProcedure(message, wParam, lParam);
    }

    [Fact]
    public void RegisterClass_UnregisterClassAtom()
    {
        WindowClassInfo myClass = new()
        {
            ClassName = "RegisterClass_UnregisterClassAtom",
            Style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
            WindowProcedure = CallDefaultProcedure
        };

        Atom atom = Windows.RegisterClass(ref myClass);
        atom.IsValid.Should().BeTrue();

        try
        {
            var info = Windows.GetClassInfo(Modules.GetModuleHandle(null), atom);
            info.ClassName.Should().Be(null);
            info.ClassAtom.Should().Be(atom);
            info.Style.Should().Be(ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw);
        }
        finally
        {
            Windows.UnregisterClass(atom, null);
            Action action =
                () => Windows.GetClassInfo(Modules.GetModuleHandle(null), atom);
            action.Should().Throw<IOException>().And
                .HResult.Should().Be((int)WindowsError.ERROR_INVALID_HANDLE.ToHResult());
        }
    }

    [Fact]
    public void RegisterClass_UnregisterClassName()
    {
        WindowClassInfo myClass = new()
        {
            ClassName = "RegisterClass_UnregisterClassName",
            Style = ClassStyle.HorizontalRedraw,
            WindowProcedure = CallDefaultProcedure
        };

        Atom atom = Windows.RegisterClass(ref myClass);
        atom.IsValid.Should().BeTrue();

        try
        {
            var info = Windows.GetClassInfo(Modules.GetModuleHandle(null), "RegisterClass_UnregisterClassName");
            info.ClassName.Should().Be("RegisterClass_UnregisterClassName");
            info.ClassAtom.Should().Be(Atom.Null);
            info.Style.Should().Be(ClassStyle.HorizontalRedraw);
        }
        finally
        {
            Windows.UnregisterClass("RegisterClass_UnregisterClassName", null);
            Action action =
                () => Windows.GetClassInfo(Modules.GetModuleHandle(null), "RegisterClass_UnregisterClassName");
            action.Should().Throw<IOException>().And
                .HResult.Should().Be((int)WindowsError.ERROR_CLASS_DOES_NOT_EXIST.ToHResult());
        }
    }

    [Fact]
    public void RegisterClass_UnregisterActiveWindow()
    {
        WindowClassInfo myClass = new()
        {
            ClassName = "RegisterClass_UnregisterActiveWindow",
            WindowProcedure = CallDefaultProcedure,
        };

        Atom atom = Windows.RegisterClass(ref myClass);
        atom.IsValid.Should().BeTrue();

        try
        {
            WindowHandle window = Windows.CreateWindow(
                atom,
                "RegisterClass_UnregisterActiveWindow",
                WindowStyles.Diabled | WindowStyles.Minimize);

            window.IsInvalid.Should().BeFalse();

            try
            {
                Action action = () => Windows.UnregisterClass(atom, null);
                action.Should().Throw<IOException>().And
                    .HResult.Should().Be((int)WindowsError.ERROR_CLASS_HAS_WINDOWS.ToHResult());
            }
            finally
            {
                window.DestroyWindow();
            }
        }
        finally
        {
            Windows.UnregisterClass(atom, null);
        }
    }

    [Fact]
    public void RegisterClass_GetSetClassLong()
    {
        // Some docs claim that 40 is the max, but that isn't true (at least in recent OSes)
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633574.aspx
        WindowClassInfo myClass = new()
        {
            ClassName = "RegisterClass_GetSetClassLong",
            Style = ClassStyle.HorizontalRedraw,
            WindowProcedure = CallDefaultProcedure,
            ClassExtraBytes = 80
        };

        Atom atom = Windows.RegisterClass(ref myClass);
        atom.IsValid.Should().BeTrue();

        try
        {
            WindowHandle window = Windows.CreateWindow(
                atom,
                "RegisterClass_GetSetClassLong_Window",
                WindowStyles.Diabled | WindowStyles.Minimize);

            window.IsInvalid.Should().BeFalse();

            try
            {
                var info = Windows.GetClassInfo(Modules.GetModuleHandle(null), atom);
                info.ClassExtraBytes.Should().Be(80);

                IntPtr result = Windows.SetClassLong(window, (ClassLong)72, (IntPtr)0x0000BEEF);
                result.Should().Be(IntPtr.Zero);

                window.GetClassLong((ClassLong)72).Should().Be((IntPtr)0x0000BEEF);
            }
            finally
            {
                window.DestroyWindow();
            }
        }
        finally
        {
            Windows.UnregisterClass(atom, null);
        }
    }

    [Fact]
    public void RegisterClass_GetSetWindowLong()
    {
        // Some docs claim that 40 is the max, but that isn't true (at least in recent OSes)
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633574.aspx
        WindowClassInfo myClass = new()
        {
            ClassName = "RegisterClass_GetSetWindowLong",
            WindowProcedure = CallDefaultProcedure,
            WindowExtraBytes = 80
        };

        Atom atom = Windows.RegisterClass(ref myClass);
        atom.IsValid.Should().BeTrue();

        try
        {
            WindowHandle window = Windows.CreateWindow(
                atom,
                "RegisterClass_GetSetWindowLong_Window",
                WindowStyles.Diabled | WindowStyles.Minimize);

            window.IsInvalid.Should().BeFalse();

            try
            {
                var info = Windows.GetClassInfo(Modules.GetModuleHandle(null), atom);
                info.WindowExtraBytes.Should().Be(80);

                IntPtr result = window.SetWindowLong((WindowLong)72, (IntPtr)0x0000BEEF);
                result.Should().Be(IntPtr.Zero);

                window.GetWindowLong((WindowLong)72).Should().Be((IntPtr)0x0000BEEF);
            }
            finally
            {
                window.DestroyWindow();
            }
        }
        finally
        {
            Windows.UnregisterClass(atom, null);
        }
    }
}
