// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Resources;

namespace WInterop.Windows
{
    /// <summary>
    /// Extension methods for Window handles
    /// </summary>
    public static partial class WindowExtensions
    {
        public static bool ShowWindow(this WindowHandle window, ShowWindow command) => WindowMethods.ShowWindow(window, command);
        public static WindowHandle GetWindow(this WindowHandle window, GetWindowOption option) => WindowMethods.GetWindow(window, option);
        public static WindowHandle GetTopWindow(this WindowHandle window) => WindowMethods.GetTopWindow(window);
        public static WindowHandle GetParent(this WindowHandle window) => WindowMethods.GetParent(window);
        public static string GetClassName(this WindowHandle window) => WindowMethods.GetClassName(window);
        public static bool IsWindow(this WindowHandle window) => WindowMethods.IsWindow(window);
        public static bool IsWindowVisible(this WindowHandle window) => WindowMethods.IsWindowVisible(window);
        public static bool IsWindowUnicode(this WindowHandle window) => WindowMethods.IsWindowUnicode(window);
        public static CommandId MessageBox(this WindowHandle owner, string text, string caption, MessageBoxType type = MessageBoxType.Ok)
            => WindowMethods.MessageBox(owner, text, caption, type);
        public static Rectangle GetClientRectangle(this WindowHandle window) => WindowMethods.GetClientRectangle(window);
        public static Rectangle GetWindowRectangle(this WindowHandle window) => WindowMethods.GetWindowRectangle(window);
        public static void SetScrollRange(this WindowHandle window, ScrollBar scrollBar, int min, int max, bool redraw)
            => WindowMethods.SetScrollRange(window, scrollBar, min, max, redraw);
        public static int SetScrollPosition(this WindowHandle window, ScrollBar scrollBar, int position, bool redraw)
            => WindowMethods.SetScrollPosition(window, scrollBar, position, redraw);
        public static int SetScrollInfo(this WindowHandle window, ScrollBar scrollBar, ref SCROLLINFO scrollInfo, bool redraw)
            => WindowMethods.SetScrollInfo(window, scrollBar, ref scrollInfo, redraw);
        public static int GetScrollPosition(this WindowHandle window, ScrollBar scrollBar)
            => WindowMethods.GetScrollPosition(window, scrollBar);
        public static void GetScrollInfo(this WindowHandle window, ScrollBar scrollBar, ref SCROLLINFO scrollInfo)
            => WindowMethods.GetScrollInfo(window, scrollBar, ref scrollInfo);
        public static int ScrollWindow(this WindowHandle window, int dx, int dy) => WindowMethods.ScrollWindow(window, dx, dy);
        public static int ScrollWindow(this WindowHandle window, int dx, int dy, Rectangle scroll, Rectangle clip) => WindowMethods.ScrollWindow(window, dx, dy, scroll, clip);
        public static Gdi.DeviceContext GetDeviceContext(this WindowHandle window) => Gdi.Gdi.GetDeviceContext(window);
        public static Gdi.DeviceContext GetWindowDeviceContext(this WindowHandle window) => Gdi.Gdi.GetWindowDeviceContext(window);
        public static Gdi.DeviceContext BeginPaint(this WindowHandle window) => Gdi.Gdi.BeginPaint(window);
        public static Gdi.DeviceContext BeginPaint(this WindowHandle window, out Gdi.PaintStruct paintStruct) => Gdi.Gdi.BeginPaint(window, out paintStruct);
        public static bool Invalidate(this WindowHandle window, bool erase = true) => Gdi.Gdi.Invalidate(window, erase);
        public static bool InvalidateRectangle(this WindowHandle window, Rectangle rect, bool erase = true) => Gdi.Gdi.InvalidateRectangle(window, rect, erase);
        public static bool UpdateWindow(this WindowHandle window) => Gdi.Gdi.UpdateWindow(window);
        public static bool ValidateRectangle(this WindowHandle window, ref Rectangle rectangle) => Gdi.Gdi.ValidateRectangle(window, ref rectangle);
        public static void MoveWindow(this WindowHandle window, Rectangle position, bool repaint) => WindowMethods.MoveWindow(window, position, repaint);
        public static IntPtr GetWindowLong(this WindowHandle window, WindowLong index) => WindowMethods.GetWindowLong(window, index);
        public static IntPtr SetWindowLong(this WindowHandle window, WindowLong index, IntPtr value)
            => WindowMethods.SetWindowLong(window, index, value);
        public static IntPtr SetWindowProcedure(this WindowHandle window, WindowProcedure newCallback)
            => WindowMethods.SetWindowProcedure(window, newCallback);
        public static void SetWindowText(this WindowHandle window, string text) => WindowMethods.SetWindowText(window, text);
        public static IntPtr SetClassLong(this WindowHandle window, ClassLong index, IntPtr value) => WindowMethods.SetClassLong(window, index, value);
        public static Gdi.BrushHandle SetClassBackgroundBrush(this WindowHandle window, Gdi.BrushHandle brush, bool ownsHandle = true)
            => WindowMethods.SetClassBackgroundBrush(window, brush, ownsHandle);
        public static bool GetMessage(this WindowHandle window, out MSG message, uint minMessage = 0, uint maxMessage = 0)
            => WindowMethods.GetMessage(out message, window, minMessage, maxMessage);
        public static LRESULT SendMessage(this WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
            => WindowMethods.SendMessage(window, message, wParam, lParam);
        public static LRESULT SendMessage(this WindowHandle window, ListBoxMessage message, WPARAM wParam, LPARAM lParam)
            => WindowMethods.SendMessage(window, (WindowMessage)message, wParam, lParam);
        public static bool ScreenToClient(this WindowHandle window, ref Point point) => Gdi.Gdi.ScreenToClient(window, ref point);
        public static bool ClientToScreen(this WindowHandle window, ref Point point) => Gdi.Gdi.ClientToScreen(window, ref point);
        public static void CreateCaret(this WindowHandle window, Gdi.BitmapHandle bitmap, int width, int height) =>
            ResourceMethods.CreateCaret(window, bitmap, width, height);
        public static void ShowCaret(this WindowHandle window) => ResourceMethods.ShowCaret(window);
        public static void HideCaret(this WindowHandle window) => ResourceMethods.HideCaret(window);
        public static WindowHandle GetDialogItem(this WindowHandle window, int id) => WindowMethods.GetDialogItem(window, id);
        public static WindowHandle SetFocus(this WindowHandle window) => WindowMethods.SetFocus(window);
        public static WindowHandle SetCapture(this WindowHandle window) => WindowMethods.SetCapture(window);
        public static TimerId SetTimer(this WindowHandle window, TimerId id, uint interval, TimerProcedure callback = null, uint delayTolerance = 0)
            => WindowMethods.SetTimer(window, id, interval, callback, delayTolerance);
        public static void KillTimer(this WindowHandle window, TimerId id) => WindowMethods.KillTimer(window, id);
    }
}
