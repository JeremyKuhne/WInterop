// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Support;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public class WindowClass
    {
        // Stash the delegate to keep it from being collected
        private readonly WindowProcedure _windowProcedure;
        private WNDCLASSEX _wndClass;
        private readonly string _className;
        private readonly string _menuName;

        public Atom Atom { get; private set; }
        public WindowHandle MainWindow { get; private set; }
        public ModuleInstance ModuleInstance { get; }

        public unsafe WindowClass(
            string className = default,
            ModuleInstance moduleInstance = default,
            ClassStyle classStyle = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
            BrushHandle backgroundBrush = default,
            IconHandle icon = default,
            IconHandle smallIcon = default,
            CursorHandle cursor = default,
            string menuName = null,
            int menuId = 0,
            int classExtraBytes = 0,
            int windowExtraBytes = 0)
        {
            // Handle default values
            className = className ?? Guid.NewGuid().ToString();

            if (backgroundBrush == default)
                backgroundBrush = SystemColor.Window;
            else if (backgroundBrush == BrushHandle.NoBrush)
                backgroundBrush = default;

            if (icon == default)
                icon = IconId.Application;
            else if (icon == IconHandle.NoIcon)
                icon = default;

            if (cursor == default)
                cursor = CursorId.Arrow;
            else if (cursor == CursorHandle.NoCursor)
                cursor = default;

            // Unfortunately GetHINSTANCE isn't part of .NET Standard
            if (moduleInstance == default)
            {
                var method = typeof(Marshal).GetMethod("GetHINSTANCE");
                Module module = Assembly.GetCallingAssembly().Modules.First();
                moduleInstance = (IntPtr)method.Invoke(null, new object[] { module });
            }

            if (menuId != 0 && menuName != null)
                throw new ArgumentException("Can only set menu name or ID.");

            _windowProcedure = WindowProcedure;
            ModuleInstance = moduleInstance;

            _className = className;
            _menuName = menuName ?? string.Empty;

            _wndClass = new WNDCLASSEX
            {
                cbSize = (uint)sizeof(WNDCLASSEX),
                style = classStyle,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(_windowProcedure),
                cbClassExtra = classExtraBytes,
                cbWndExtra = windowExtraBytes,
                hInstance = (IntPtr)moduleInstance,
                hIcon = icon,
                hCursor = cursor,
                hbrBackground = backgroundBrush,
                hIconSm = smallIcon,
                lpszMenuName = (char*)menuId
            };
        }

        public bool IsRegistered => Atom.IsValid;

        public BrushHandle BackgroundBrush
            => new BrushHandle(_wndClass.hbrBackground, ownsHandle: false);

        public unsafe WindowClass Register()
        {
            fixed (char* name = _className)
            fixed (char* menuName = _menuName)
            {
                _wndClass.lpszClassName = name;
                if (!string.IsNullOrEmpty(_menuName))
                    _wndClass.lpszMenuName = menuName;

                Atom atom = Imports.RegisterClassExW(ref _wndClass);
                if (!atom.IsValid)
                    throw Errors.GetIoExceptionForLastError();
                Atom = atom;
                return this;
            }
        }

        public WindowHandle CreateWindow(
            string windowName = null,
            WindowStyles style = WindowStyles.Overlapped,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            bool isMainWindow = false,
            WindowHandle parentWindow = default,
            IntPtr parameters = default,
            MenuHandle menuHandle = default)
        {
            return CreateWindow(Windows.DefaultBounds, windowName, style, extendedStyle, isMainWindow, parentWindow, parameters, menuHandle);
        }

        public WindowHandle CreateWindow(
            Rectangle bounds,
            string windowName = null,
            WindowStyles style = WindowStyles.Overlapped,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            bool isMainWindow = false,
            WindowHandle parentWindow = default,
            IntPtr parameters = default,
            MenuHandle menuHandle = default)
        {
            if (!IsRegistered)
                throw new ArgumentException("Window class must be registered before using.");

            WindowHandle window = Windows.CreateWindow(
                Atom,
                windowName,
                style,
                extendedStyle,
                bounds,
                parentWindow,
                menuHandle,
                parameters: parameters);

            if (isMainWindow)
                MainWindow = window;

            return window;
        }

        protected virtual LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Destroy:
                    if (window == MainWindow)
                        Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
