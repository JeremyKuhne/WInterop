// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi;
using WInterop.Modules.Types;
using WInterop.Resources;
using WInterop.Resources.Types;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633577.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WindowClass
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

        public unsafe static implicit operator WindowClass(WNDCLASSEX nativeClass)
        {
            var windowClass = new WindowClass
            {
                Size = nativeClass.cbSize,
                Style = nativeClass.style,
                WindowProcedure = Marshal.GetDelegateForFunctionPointer<WindowProcedure>(nativeClass.lpfnWndProc),
                ClassExtraBytes = nativeClass.cbClassExtra,
                WindowExtraBytes = nativeClass.cbWndExtra,
                Instance = nativeClass.hInstance,
                Icon = nativeClass.hIcon,
                Cursor = nativeClass.hCursor,
                Background = new BrushHandle(nativeClass.hbrBackground, ownsHandle: false),
                SmallIcon = nativeClass.hIconSm
            };

            if (nativeClass.lpszMenuName != IntPtr.Zero)
            {
                if (ResourceMacros.IS_INTRESOURCE(nativeClass.lpszMenuName))
                    windowClass.MenuId = (int)nativeClass.lpszMenuName;
                else
                    windowClass.MenuName = new string((char*)nativeClass.lpszMenuName.ToPointer());
            }

            if (nativeClass.lpszClassName != IntPtr.Zero)
            {
                if (Atom.IsAtom(nativeClass.lpszClassName))
                    windowClass.ClassAtom = nativeClass.lpszClassName;
                else
                    windowClass.ClassName = new string((char*)nativeClass.lpszClassName.ToPointer());
            }

            return windowClass;
        }

        public class Marshaller : IDisposable
        {
            private GCHandle _menuName;
            private GCHandle _className;

            public unsafe void FillNative(out WNDCLASSEX native, ref WindowClass managed)
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
                native.hIcon = (IntPtr)managed.Icon;
                native.hCursor = (IntPtr)managed.Cursor;
                native.hbrBackground = managed.Background.Handle;
                native.hIconSm = (IntPtr)managed.SmallIcon;

                if (managed.ClassName != null)
                {
                    _className = GCHandle.Alloc(managed.ClassName, GCHandleType.Pinned);
                    native.lpszClassName = _className.AddrOfPinnedObject();
                }
                else
                {
                    native.lpszClassName = managed.ClassAtom;
                }

                if (managed.MenuName != null)
                {
                    _menuName = GCHandle.Alloc(managed.MenuName, GCHandleType.Pinned);
                    native.lpszMenuName = _menuName.AddrOfPinnedObject();
                }
                else
                {
                    native.lpszMenuName = (IntPtr)managed.MenuId;
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
