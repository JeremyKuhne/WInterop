// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;
using WInterop.Windows.Types;

namespace WInterop.Gdi.Types
{
    /// <summary>
    /// DeviceContext handle (HDC)
    /// </summary>
    public readonly struct DeviceContext : IDisposable
    {
        public HDC Handle => _paint.hdc;
        private readonly WindowHandle _window;
        private readonly CollectionType _type;
        private readonly PAINTSTRUCT _paint;

        public DeviceContext(HDC handle)
        {
            _window = default;
            _paint = default;
            _paint.hdc = handle;
            _type = CollectionType.None;
        }

        public DeviceContext(HDC handle, WindowHandle windowHandle)
        {
            _window = windowHandle;
            _paint = default;
            _paint.hdc = handle;
            _type = windowHandle.HWND == IntPtr.Zero
                ? CollectionType.Release : CollectionType.Delete;
        }

        public DeviceContext(HDC handle, WindowHandle windowHandle, PAINTSTRUCT paint)
        {
            _window = windowHandle;
            _paint = paint;
            _type = CollectionType.EndPaint;
        }

        public void Dispose()
        {
            switch (_type)
            {
                case CollectionType.Delete:
                    if (!GdiMethods.Imports.DeleteDC(Handle))
                        throw Errors.GetIoExceptionForLastError();
                    break;
                case CollectionType.Release:
                    if (!GdiMethods.Imports.ReleaseDC(_window, Handle))
                        throw Errors.GetIoExceptionForLastError();
                    break;
                case CollectionType.EndPaint:
                    if (!GdiMethods.Imports.EndPaint(_window, in _paint))
                        throw Errors.GetIoExceptionForLastError();
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public bool IsInvalid => Handle.IsInvalid;

        public static implicit operator HDC(DeviceContext context) => context.Handle;
        public static explicit operator DeviceContext(WPARAM wparam) => new DeviceContext(new HDC((IntPtr)wparam));

        private enum CollectionType
        {
            None,
            Delete,
            Release,
            EndPaint
        }

        public struct DeleteHDC
        {
            private HDC _handle;
            public static implicit operator DeviceContext(DeleteHDC handle) => new DeviceContext(handle._handle, default);
        }
    }
}
