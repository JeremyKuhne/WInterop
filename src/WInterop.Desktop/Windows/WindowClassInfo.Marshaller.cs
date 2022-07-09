// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Windows;

public partial class WindowClassInfo
{
    public struct Marshaller : IDisposable
    {
        private GCHandle _menuName;
        private GCHandle _className;

        public unsafe void FillNative(out WNDCLASSEXW native, ref WindowClassInfo managed)
        {
            native.cbSize = (uint)sizeof(WNDCLASSEXW);
            native.style = (uint)managed.Style;
            native.lpfnWndProc =
                (delegate* unmanaged<HWND, uint, WPARAM, LPARAM, LRESULT>)Marshal.GetFunctionPointerForDelegate(
                    managed.WindowProcedure);
            native.cbClsExtra = managed.ClassExtraBytes;
            native.cbWndExtra = managed.WindowExtraBytes;

            // If the WindowClass struct goes out of scope, this would be bad,
            // but that would require it coming off the stack- which would be
            // pretty difficult to accomplish for users of this class.
            native.hInstance = managed.Instance ?? HINSTANCE.NULL;
            native.hIcon = managed.Icon;
            native.hCursor = managed.Cursor;
            native.hbrBackground = managed.Background.Handle;
            native.hIconSm = managed.SmallIcon;

            if (managed.ClassName is not null)
            {
                _className = GCHandle.Alloc(managed.ClassName, GCHandleType.Pinned);
                native.lpszClassName = (ushort*)_className.AddrOfPinnedObject();
            }
            else
            {
                native.lpszClassName = (ushort*)(nint)managed.ClassAtom;
            }

            if (managed.MenuName is not null)
            {
                _menuName = GCHandle.Alloc(managed.MenuName, GCHandleType.Pinned);
                native.lpszMenuName = (ushort*)_menuName.AddrOfPinnedObject();
            }
            else
            {
                native.lpszMenuName = (ushort*)(IntPtr)managed.MenuId;
            }
        }

        public void Dispose()
        {
            if (_className.IsAllocated)
            {
                _className.Free();
            }

            if (_menuName.IsAllocated)
            {
                _menuName.Free();
            }
        }
    }
}