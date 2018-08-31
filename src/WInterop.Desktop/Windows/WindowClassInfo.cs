// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Windows.Unsafe;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633577.aspx
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
        public BrushHandle Background;
        public string MenuName;
        public int MenuId;
        public string ClassName;
        public Atom ClassAtom;
        public IconHandle SmallIcon;

        public unsafe static implicit operator WindowClassInfo(WNDCLASSEX nativeClass)
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
                Background = new BrushHandle(nativeClass.hbrBackground, ownsHandle: false),
                SmallIcon = new IconHandle(nativeClass.hIconSm, ownsHandle: false)
            };

            if (nativeClass.lpszMenuName != null)
            {
                if (ResourceMacros.IS_INTRESOURCE((IntPtr)nativeClass.lpszMenuName))
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
}
