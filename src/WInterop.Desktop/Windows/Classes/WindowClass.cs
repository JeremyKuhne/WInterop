// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Gdi;
using WInterop.Modules;
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
            string? className = default,
            ModuleInstance? moduleInstance = default,
            ClassStyle classStyle = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
            BrushHandle backgroundBrush = default,
            IconHandle icon = default,
            IconHandle smallIcon = default,
            CursorHandle cursor = default,
            string? menuName = null,
            int menuId = 0,
            int classExtraBytes = 0,
            int windowExtraBytes = 0)
        {
            // Handle default values
            className ??= Guid.NewGuid().ToString();

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

            moduleInstance ??= new ModuleInstance(Marshal.GetHINSTANCE(Assembly.GetCallingAssembly().Modules.First()));

            if (menuId != 0 && menuName != null)
                throw new ArgumentException($"Can't set both {nameof(menuName)} and {nameof(menuId)}.");

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

        public WindowClass(string registeredClassName)
        {
            _windowProcedure = WindowProcedure;
            _className = registeredClassName;
            _menuName = string.Empty;
            ModuleInstance = ModuleInstance.Null;
        }

        public bool IsRegistered => Atom.IsValid || ModuleInstance == ModuleInstance.Null;

        public BrushHandle BackgroundBrush
            => new BrushHandle(_wndClass.hbrBackground, ownsHandle: false);

        /// <summary>
        ///  Registers this <see cref="WindowClass"/> so that instances can be created.
        /// </summary>
        public unsafe WindowClass Register()
        {
            fixed (char* name = _className)
            fixed (char* menuName = _menuName)
            {
                _wndClass.lpszClassName = name;
                if (!string.IsNullOrEmpty(_menuName))
                    _wndClass.lpszMenuName = menuName;

                Atom atom = WindowsImports.RegisterClassExW(ref _wndClass);
                if (!atom.IsValid)
                    Error.ThrowLastError();
                Atom = atom;
                return this;
            }
        }

        /// <summary>
        ///  Creates an instance of this <see cref="WindowClass"/>.
        /// </summary>
        /// <param name="bounds">
        ///  Pass <see cref="Windows.DefaultBounds"/> for the default size.
        /// </param>
        /// <param name="windowName">
        ///  The text for the title bar when using <see cref="WindowStyles.Caption"/> or <see cref="WindowStyles.Overlapped"/>.
        ///  For buttons, checkboxes, and other static controls this is the text of the control or a resource reference.
        /// </param>
        /// <param name="isMainWindow">
        ///  Set this to indicate that this is the main window for the application and closing it should terminate the message loop.
        /// </param>
        public virtual WindowHandle CreateWindow(
            Rectangle bounds,
            string? windowName = null,
            WindowStyles style = WindowStyles.Overlapped,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            bool isMainWindow = false,
            WindowHandle parentWindow = default,
            IntPtr parameters = default,
            MenuHandle menuHandle = default)
        {
            if (!IsRegistered)
                throw new ArgumentException("Window class must be registered before using.");

            WindowHandle window = Atom.IsValid
                ? Windows.CreateWindow(
                    Atom,
                    windowName,
                    style,
                    extendedStyle,
                    bounds,
                    parentWindow,
                    menuHandle,
                    parameters)
                : Windows.CreateWindow(
                    _className,
                    windowName,
                    style,
                    extendedStyle,
                    bounds,
                    parentWindow,
                    menuHandle,
                    parameters);

            if (!Atom.IsValid)
            {
                Atom = window.GetClassLong(ClassLong.Atom);
            }

            if (isMainWindow)
                MainWindow = window;

            return window;
        }

        protected virtual LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Destroy:
                    if (window == MainWindow)
                        Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
