// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.Gdi;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources;
using WInterop.Resources.Types;
using WInterop.Windows.Types;

namespace WInterop.Windows
{
    public static class Windows
    {
        public static Atom RegisterClass(WindowClass windowClass) => WindowMethods.RegisterClass(windowClass);
        public static WindowHandle CreateWindow(
                    SafeModuleHandle instance,
                    string className,
                    string windowName,
                    WindowStyle style,
                    ExtendedWindowStyle extendedStyle = ExtendedWindowStyle.None,
                    int x = WindowDefines.CW_USEDEFAULT,
                    int y = WindowDefines.CW_USEDEFAULT,
                    int width = WindowDefines.CW_USEDEFAULT,
                    int height = WindowDefines.CW_USEDEFAULT) => WindowMethods.CreateWindow(instance, className, windowName, style, extendedStyle, x, y, width, height);
        public static WindowHandle CreateWindow(
            SafeModuleHandle instance,
            string className,
            string windowName,
            WindowStyle style,
            ExtendedWindowStyle extendedStyle,
            int x,
            int y,
            int width,
            int height,
            WindowHandle parentWindow,
            IntPtr menuHandle,
            IntPtr parameters) => WindowMethods.CreateWindow(
                instance, className, windowName, style, extendedStyle, x, y, width, height, parentWindow, menuHandle, parameters);
        public static bool GetMessage(out MSG message, uint minMessage = 0, uint maxMessage = 0) => WindowMethods.GetMessage(out message, WindowHandle.Null, minMessage, maxMessage);
        public static bool PeekMessage(out MSG message, uint minMessage, uint maxMessage, PeekMessageOptions options)
            => WindowMethods.PeekMessage(out message, WindowHandle.Null, minMessage, maxMessage, options);
        public static bool TranslateMessage(ref MSG message) => WindowMethods.TranslateMessage(ref message);
        public static bool DispatchMessage(ref MSG message) => WindowMethods.DispatchMessage(ref message);
        public static void PostQuitMessage(int exitCode) => WindowMethods.PostQuitMessage(exitCode);
        public static LRESULT DefaultWindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
            => WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        public static int ShowCursor(bool show) => ResourceMethods.ShowCursor(show);
        public static POINT GetCursorPosition() => ResourceMethods.GetCursorPosition();
        public static CursorHandle SetCursor(CursorHandle cursor) => ResourceMethods.SetCursor(cursor);
        public static CursorHandle SetCursor(CursorId id) => ResourceMethods.SetCursor(ResourceMethods.LoadCursor(id));
        public static void SetCursorPosition(int x, int y) => ResourceMethods.SetCursorPosition(x, y);
        public static RegionHandle CreateEllipticRegion(int left, int top, int right, int bottom) => GdiMethods.CreateEllipticRegion(left, top, right, bottom);
        public static RegionHandle CreateRectangleRegion(int left, int top, int right, int bottom) => GdiMethods.CreateRectangleRegion(left, top, right, bottom);
        public static CommandId MessageBox(string text, string caption, MessageBoxType type = MessageBoxType.MB_OK) => WindowMethods.MessageBox(text, caption, type);
        public static void MessageBeep(MessageBeepType type = MessageBeepType.SimpleBeep) => ErrorMethods.MessageBeep(type);
        public static int GetSystemMetrics(SystemMetric metric) => WindowMethods.GetSystemMetrics(metric);
        public static string GetKeyNameText(LPARAM lParam) => WindowMethods.GetKeyNameText(lParam);
        public static BrushHandle CreateSolidBrush(byte red, byte green, byte blue) => GdiMethods.CreateSolidBrush(red, green, blue);
        public static PenHandle CreatePen(PenStyle style, int width, COLORREF color) => GdiMethods.CreatePen(style, width, color);

        public static PenHandle CreatePen(PenStyleExtended style, uint width, COLORREF color, PenEndCap endCap = PenEndCap.PS_ENDCAP_ROUND, PenJoin join = PenJoin.PS_JOIN_ROUND)
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
            PitchAndFamily pitchAndFamily,
            string typeface) => GdiMethods.CreateFont(
                height, width, escapement, orientation, weight, italic, underline, strikeout, characterSet, outputPrecision, clippingPrecision, quality, pitchAndFamily, typeface);

        public static void SetCaretPosition(int x, int y) => ResourceMethods.SetCaretPosition(x, y);
        public static void DestroyCaret() => ResourceMethods.DestroyCaret();
        public static WindowHandle GetFocus() => WindowMethods.GetFocus();
        public static void ReleaseCapture() => WindowMethods.ReleaseCapture();

        public static SystemParameters SystemParameters => SystemParameters.Instance;
    }
}
