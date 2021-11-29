// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Gdi;
using WInterop.Gdi.Native;
using WInterop.Modules;
using WInterop.Windows.Native;

namespace WInterop.Windows;

// https://docs.microsoft.com/windows/win32/api/winuser/ns-winuser-wndclassexw
public struct WindowClassInfo
{
    public uint Size;
    public ClassStyle Style;
    public WindowProcedure WindowProcedure;
    public int ClassExtraBytes;
    public int WindowExtraBytes;
    public ModuleInstance Instance;
    public IconHandle Icon;
    public CursorHandle Cursor;
    public string MenuName;
    public int MenuId;
    public string ClassName;
    public Atom ClassAtom;
    public IconHandle SmallIcon;
    private HBRUSH _background;

    public BrushHandle Background
    {
        get => new BrushHandle(_background, ownsHandle: false);
        set => _background = value.HBRUSH;
    }

    public static unsafe implicit operator WindowClassInfo(WNDCLASSEX nativeClass)
    {
        var windowClass = new WindowClassInfo
        {
            Size = nativeClass.cbSize,
            Style = nativeClass.style,
            WindowProcedure = Marshal.GetDelegateForFunctionPointer<WindowProcedure>(nativeClass.lpfnWndProc),
            ClassExtraBytes = nativeClass.cbClassExtra,
            WindowExtraBytes = nativeClass.cbWndExtra,
            Instance = nativeClass.hInstance,
            Icon = new IconHandle(nativeClass.hIcon, ownsHandle: false),
            Cursor = new CursorHandle(nativeClass.hCursor, ownsHandle: false),
            _background = new BrushHandle(nativeClass.hbrBackground, ownsHandle: false),
            SmallIcon = new IconHandle(nativeClass.hIconSm, ownsHandle: false)
        };

        if (nativeClass.lpszMenuName != null)
        {
            if (Windows.IsIntResource((IntPtr)nativeClass.lpszMenuName))
                windowClass.MenuId = (int)nativeClass.lpszMenuName;
            else
                windowClass.MenuName = new string(nativeClass.lpszMenuName);
        }

        if (nativeClass.lpszClassName != null)
        {
            if (Atom.IsAtom((IntPtr)nativeClass.lpszClassName))
                windowClass.ClassAtom = (IntPtr)nativeClass.lpszClassName;
            else
                windowClass.ClassName = new string(nativeClass.lpszClassName);
        }

        return windowClass;
    }

    public struct Marshaller : IDisposable
    {
        private GCHandle _menuName;
        private GCHandle _className;

        public unsafe void FillNative(out WNDCLASSEX native, ref WindowClassInfo managed)
        {
            native.cbSize = (uint)sizeof(WNDCLASSEX);
            native.style = managed.Style;
            native.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(managed.WindowProcedure);
            native.cbClassExtra = managed.ClassExtraBytes;
            native.cbWndExtra = managed.WindowExtraBytes;

            // If the WindowClass struct goes out of scope, this would be bad,
            // but that would require it coming off the stack- which would be
            // pretty difficult to accomplish for users of this class.
            native.hInstance = (IntPtr)managed.Instance;
            native.hIcon = managed.Icon;
            native.hCursor = managed.Cursor;
            native.hbrBackground = managed.Background.HBRUSH;
            native.hIconSm = managed.SmallIcon;

            if (managed.ClassName != null)
            {
                _className = GCHandle.Alloc(managed.ClassName, GCHandleType.Pinned);
                native.lpszClassName = (char*)_className.AddrOfPinnedObject();
            }
            else
            {
                native.lpszClassName = (char*)(IntPtr)managed.ClassAtom;
            }

            if (managed.MenuName != null)
            {
                _menuName = GCHandle.Alloc(managed.MenuName, GCHandleType.Pinned);
                native.lpszMenuName = (char*)_menuName.AddrOfPinnedObject();
            }
            else
            {
                native.lpszMenuName = (char*)(IntPtr)managed.MenuId;
            }
        }

        public void Dispose()
        {
            if (_className.IsAllocated)
                _className.Free();
            if (_menuName.IsAllocated)
                _menuName.Free();
        }
    }
}