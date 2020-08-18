// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.Support;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public class Window : IDisposable, IHandle<WindowHandle>, ILayoutHandler
    {
        // Stash the delegate to keep it from being collected
        private readonly WindowProcedure _windowProcedure;
        private readonly WNDPROC _priorWindowProcedure;

        private readonly WindowClass _windowClass;
        private readonly GCHandle _gcHandle;

        public WindowHandle Handle { get; }

        public event WindowsMessageEvent? MessageHandler;

        public Window(
            WindowClass windowClass,
            Rectangle bounds,
            string? windowName = default,
            WindowStyles style = WindowStyles.Overlapped,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            bool isMainWindow = false,
            Window? parentWindow = default,
            IntPtr parameters = default,
            MenuHandle menuHandle = default)
        {
            _windowClass = windowClass;
            if (!_windowClass.IsRegistered)
            {
                _windowClass.Register();
            }

            Handle = _windowClass.CreateWindow(
                bounds,
                windowName,
                style,
                extendedStyle,
                isMainWindow,
                parentWindow?.Handle ?? default,
                parameters,
                menuHandle);

            _windowProcedure = WindowProcedure;
            _priorWindowProcedure = (WNDPROC)Handle.GetWindowLong(WindowLong.WindowProcedure);
            Handle.SetWindowLong(WindowLong.WindowProcedure, Marshal.GetFunctionPointerForDelegate(_windowProcedure));

            // Stash our managed pointer so we can find the Window from an HWND
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            Handle.SetWindowLong(WindowLong.UserData, GCHandle.ToIntPtr(_gcHandle));
        }

        protected virtual LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            var handlers = MessageHandler;
            if (handlers is not null)
            {
                foreach (var handler in handlers.GetInvocationList().OfType<WindowsMessageEvent>())
                {
                    var result = handler(this, window, message, wParam, lParam);
                    if (result.HasValue)
                    {
                        return result.Value;
                    }
                }
            }

            return Windows.CallWindowProcedure(_priorWindowProcedure, window, message, wParam, lParam);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Handle.SetWindowLong(WindowLong.WindowProcedure, (IntPtr)_priorWindowProcedure);
        }

        void ILayoutHandler.Layout(Rectangle bounds) => Handle.MoveWindow(bounds, repaint: true);

        public static implicit operator HWND(Window window) => window.Handle;
    }
}
