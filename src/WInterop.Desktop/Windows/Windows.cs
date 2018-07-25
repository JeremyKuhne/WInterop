// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.Gdi;
using WInterop.Gdi.Types;
using WInterop.Globalization;
using WInterop.Modules.Types;
using WInterop.Resources;
using WInterop.Resources.Types;
using WInterop.SystemInformation;
using WInterop.SystemInformation.Types;
using WInterop.Windows.Types;

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
            int x,
            int y,
            int width,
            int height,
            WindowHandle parentWindow,
            IntPtr menuHandle,
            ModuleInstance instance,
            IntPtr parameters) => WindowMethods.CreateWindow(
                className, windowName, style, extendedStyle, x, y, width, height, parentWindow, menuHandle, instance, parameters);

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
        public static RegionHandle CreateEllipticRegion(int left, int top, int right, int bottom) => GdiMethods.CreateEllipticRegion(left, top, right, bottom);
        public static RegionHandle CreateRectangleRegion(int left, int top, int right, int bottom) => GdiMethods.CreateRectangleRegion(left, top, right, bottom);
        public static CommandId MessageBox(string text, string caption, MessageBoxType type = MessageBoxType.Ok) => WindowMethods.MessageBox(text, caption, type);
        public static void MessageBeep(BeepType type = BeepType.SimpleBeep) => ErrorMethods.MessageBeep(type);
        public static int GetSystemMetrics(SystemMetric metric) => WindowMethods.GetSystemMetrics(metric);
        public static KeyState GetKeyState(VirtualKey key) => WindowMethods.GetKeyState(key);
        public static string GetKeyNameText(LPARAM lParam) => WindowMethods.GetKeyNameText(lParam);
        public static COLORREF GetSystemColor(SystemColor systemColor) => WindowMethods.GetSystemColor(systemColor);
        public static BrushHandle GetSystemColorBrush(SystemColor systemColor) => GdiMethods.GetSystemColorBrush(systemColor);
        public static BrushHandle CreateSolidBrush(byte red, byte green, byte blue) => GdiMethods.CreateSolidBrush(red, green, blue);
        public static BrushHandle CreateSolidBrush(COLORREF color) => GdiMethods.CreateSolidBrush(color.R, color.G, color.B);
        public static PenHandle CreatePen(PenStyle style, int width, COLORREF color) => GdiMethods.CreatePen(style, width, color);

        public static PenHandle CreatePen(PenStyleExtended style, uint width, COLORREF color, PenEndCap endCap = PenEndCap.Round, PenJoin join = PenJoin.Round)
            => GdiMethods.CreatePen(style, width, color, endCap, join);

        public static FontHandle CreateFont(
            int height,
            int width,
            int escapement,
            int orientation,
            FontWeight weight,
            bool italic,
            bool underline,
            bool strikeout,
            CharacterSet characterSet,
            OutputPrecision outputPrecision,
            ClippingPrecision clippingPrecision,
            Quality quality,
            FontPitch pitch,
            FontFamily family,
            string typeface) => GdiMethods.CreateFont(
                height, width, escapement, orientation, weight, italic, underline, strikeout, characterSet, outputPrecision, clippingPrecision, quality, pitch, family, typeface);

        public static void SetCaretPosition(int x, int y) => ResourceMethods.SetCaretPosition(x, y);
        public static void DestroyCaret() => ResourceMethods.DestroyCaret();
        public static WindowHandle GetFocus() => WindowMethods.GetFocus();
        public static void ReleaseCapture() => WindowMethods.ReleaseCapture();

        public static SystemParameters SystemParameters => SystemParameters.Instance;
        public static LocaleInfo LocaleInfo => LocaleInfo.Instance;

        public static SYSTEMTIME GetLocalTime() => SystemInformationMethods.GetLocalTime();

        public static DeviceContext CreateDeviceContext(string driver, string device) => GdiMethods.CreateDeviceContext(driver, device);
        public static DeviceContext CreateInformationContext(string driver, string device) => GdiMethods.CreateInformationContext(driver, device);

        public static SIZE GetDialogBaseUnits() => WindowMethods.GetDialogBaseUnits();
    }
}
