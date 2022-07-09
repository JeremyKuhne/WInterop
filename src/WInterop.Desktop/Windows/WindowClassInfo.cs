// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Gdi;
using WInterop.Modules;

namespace WInterop.Windows;

// https://docs.microsoft.com/windows/win32/api/winuser/ns-winuser-wndclassexw
public partial class WindowClassInfo
{
    public uint Size;
    public ClassStyle Style;
    public WindowProcedure WindowProcedure;
    public int ClassExtraBytes;
    public int WindowExtraBytes;
    public ModuleInstance? Instance;
    public IconHandle Icon;
    public CursorHandle Cursor;
    public string? MenuName;
    public int MenuId;
    public string? ClassName;
    public Atom ClassAtom;
    public IconHandle SmallIcon;
    private HBRUSH _background;

    public WindowClassInfo(WindowProcedure windowProcedure)
    {
        WindowProcedure = windowProcedure;
    }

    public BrushHandle Background
    {
        get => new(_background, ownsHandle: false);
        set => _background = value.Handle;
    }

    public static unsafe implicit operator WindowClassInfo(WNDCLASSEXW nativeClass)
    {
        var windowClass = new WindowClassInfo(
            Marshal.GetDelegateForFunctionPointer<WindowProcedure>((IntPtr)nativeClass.lpfnWndProc))
        {
            Size = nativeClass.cbSize,
            Style = (ClassStyle)nativeClass.style,
            ClassExtraBytes = nativeClass.cbClsExtra,
            WindowExtraBytes = nativeClass.cbWndExtra,
            Instance = new(nativeClass.hInstance, ownsHandle: false),
            Icon = new(nativeClass.hIcon, ownsHandle: false),
            Cursor = new(nativeClass.hCursor, ownsHandle: false),
            _background = nativeClass.hbrBackground,
            SmallIcon = new(nativeClass.hIconSm, ownsHandle: false)
        };

        if (nativeClass.lpszMenuName is not null)
        {
            if (Windows.IsIntResource((IntPtr)nativeClass.lpszMenuName))
            {
                windowClass.MenuId = (int)nativeClass.lpszMenuName;
            }
            else
            {
                windowClass.MenuName = new string((char*)nativeClass.lpszMenuName);
            }
        }

        if (nativeClass.lpszClassName is not null)
        {
            if (Atom.IsAtom((IntPtr)nativeClass.lpszClassName))
            {
                windowClass.ClassAtom = (IntPtr)nativeClass.lpszClassName;
            }
            else
            {
                windowClass.ClassName = new string((char*)nativeClass.lpszClassName);
            }
        }

        return windowClass;
    }
}