// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.ErrorHandling;
using WInterop.Globalization;
using WInterop.Modules;
using WInterop.Resources;
using WInterop.SystemInformation;
using WInterop.SystemInformation.Types;

namespace WInterop.Windows
{
    public static class Windows
    {
        public static Atom RegisterClass(ref WindowClass windowClass) => WindowMethods.RegisterClass(ref windowClass);

        public static WindowHandle CreateWindow(
                    string className,
                    string windowName,
                    WindowStyles style,
                    ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
                    int x = WindowDefines.CW_USEDEFAULT,
                    int y = WindowDefines.CW_USEDEFAULT,
                    int width = WindowDefines.CW_USEDEFAULT,
                    int height = WindowDefines.CW_USEDEFAULT,
                    ModuleInstance instance = null)
                    => WindowMethods.CreateWindow(className, windowName, style, extendedStyle, x, y, width, height, instance);

        public static WindowHandle CreateWindow(
                    ModuleInstance instance,
                    string className,
                    string windowName,
                    WindowStyles style,
                    ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
                    int x = WindowDefines.CW_USEDEFAULT,
                    int y = WindowDefines.CW_USEDEFAULT,
                    int width = WindowDefines.CW_USEDEFAULT,
                    int height = WindowDefines.CW_USEDEFAULT)
                    => WindowMethods.CreateWindow(className, windowName, style, extendedStyle, x, y, width, height, instance);

        public static WindowHandle CreateWindow(
            string className,
            string windowName,
            WindowStyles style,
            ExtendedWindowStyles extendedStyle,
            Rectangle bounds,
            WindowHandle parentWindow,
            IntPtr menuHandle,
            ModuleInstance instance,
            IntPtr parameters) => WindowMethods.CreateWindow(
                className, windowName, style, extendedStyle, bounds, parentWindow, menuHandle, instance, parameters);

        public static WindowHandle CreateWindow(
            string className,
            string windowName,
            WindowStyles style,
            ExtendedWindowStyles extendedStyle,
            Size size,
            WindowHandle parentWindow,
            IntPtr menuHandle,
            ModuleInstance instance,
            IntPtr parameters) => WindowMethods.CreateWindow(
                className, windowName, style, extendedStyle, new Rectangle(default, size), parentWindow, menuHandle, instance, parameters);

        public static bool GetMessage(out MSG message, uint minMessage = 0, uint maxMessage = 0) => WindowMethods.GetMessage(out message, WindowHandle.Null, minMessage, maxMessage);
        public static bool PeekMessage(out MSG message, uint minMessage, uint maxMessage, PeekMessageOptions options)
            => WindowMethods.PeekMessage(out message, WindowHandle.Null, minMessage, maxMessage, options);
        public static bool TranslateMessage(ref MSG message) => WindowMethods.TranslateMessage(ref message);
        public static bool DispatchMessage(ref MSG message) => WindowMethods.DispatchMessage(ref message);
        public static void PostQuitMessage(int exitCode) => WindowMethods.PostQuitMessage(exitCode);
        public static LRESULT DefaultWindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
            => WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        public static int ShowCursor(bool show) => ResourceMethods.ShowCursor(show);
        public static Point GetCursorPosition() => ResourceMethods.GetCursorPosition();
        public static CursorHandle SetCursor(CursorHandle cursor) => ResourceMethods.SetCursor(cursor);
        public static CursorHandle SetCursor(CursorId id) => ResourceMethods.SetCursor(ResourceMethods.LoadCursor(id));
        public static void SetCursorPosition(int x, int y) => ResourceMethods.SetCursorPosition(x, y);
        public static CommandId MessageBox(string text, string caption, MessageBoxType type = MessageBoxType.Ok) => WindowMethods.MessageBox(text, caption, type);
        public static void MessageBeep(BeepType type = BeepType.SimpleBeep) => ErrorMethods.MessageBeep(type);
        public static int GetSystemMetrics(SystemMetric metric) => WindowMethods.GetSystemMetrics(metric);
        public static KeyState GetKeyState(VirtualKey key) => WindowMethods.GetKeyState(key);
        public static string GetKeyNameText(LPARAM lParam) => WindowMethods.GetKeyNameText(lParam);
        public static Color GetSystemColor(SystemColor systemColor) => WindowMethods.GetSystemColor(systemColor);
        public static void SetCaretPosition(int x, int y) => ResourceMethods.SetCaretPosition(x, y);
        public static void DestroyCaret() => ResourceMethods.DestroyCaret();
        public static WindowHandle GetFocus() => WindowMethods.GetFocus();
        public static void ReleaseCapture() => WindowMethods.ReleaseCapture();

        public static SystemParameters SystemParameters => SystemParameters.Instance;
        public static LocaleInfo LocaleInfo => LocaleInfo.Instance;

        public static SYSTEMTIME GetLocalTime() => SystemInformationMethods.GetLocalTime();
        public static Size GetDialogBaseUnits() => WindowMethods.GetDialogBaseUnits();
    }
}
