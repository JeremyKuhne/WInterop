// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi;
using WInterop.Gdi.Types;
using WInterop.Resources;
using WInterop.Windows;
using WInterop.Windows.Types;

namespace WInterop.Extensions.WindowExtensions
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
        public static RECT GetClientRectangle(this WindowHandle window) => WindowMethods.GetClientRectangle(window);
        public static RECT GetWindowRectangle(this WindowHandle window) => WindowMethods.GetWindowRectangle(window);
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
        public static int ScrollWindow(this WindowHandle window, int dx, int dy, RECT scroll, RECT clip) => WindowMethods.ScrollWindow(window, dx, dy, scroll, clip);
        public static DeviceContext GetDeviceContext(this WindowHandle window) => GdiMethods.GetDeviceContext(window);
        public static DeviceContext GetWindowDeviceContext(this WindowHandle window) => GdiMethods.GetWindowDeviceContext(window);
        public static DeviceContext BeginPaint(this WindowHandle window) => GdiMethods.BeginPaint(window);
        public static DeviceContext BeginPaint(this WindowHandle window, out PAINTSTRUCT paintStruct) => GdiMethods.BeginPaint(window, out paintStruct);
        public static bool Invalidate(this WindowHandle window, bool erase = true) => GdiMethods.Invalidate(window, erase);
        public static bool InvalidateRectangle(this WindowHandle window, RECT rect, bool erase = true) => GdiMethods.InvalidateRectangle(window, rect, erase);
        public static bool UpdateWindow(this WindowHandle window) => GdiMethods.UpdateWindow(window);
        public static bool ValidateRectangle(this WindowHandle window, ref RECT rect) => GdiMethods.ValidateRectangle(window, ref rect);
        public static void MoveWindow(this WindowHandle window, int x, int y, int width, int height, bool repaint) => WindowMethods.MoveWindow(window, x, y, width, height, repaint);
        public static IntPtr GetWindowLong(this WindowHandle window, WindowLong index) => WindowMethods.GetWindowLong(window, index);
        public static IntPtr SetWindowLong(this WindowHandle window, WindowLong index, IntPtr value) => WindowMethods.SetWindowLong(window, index, value);
        public static void SetWindowText(this WindowHandle window, string text) => WindowMethods.SetWindowText(window, text);
        public static IntPtr SetClassLong(this WindowHandle window, ClassLong index, IntPtr value) => WindowMethods.SetClassLong(window, index, value);

        /// <summary>
        /// Sets the background brush for the window class. Returns the previous background brush.
        /// </summary>
        /// <param name="ownsHandle">
        /// Whether or not the returned brush should own the handle. If true the brush handle
        /// will be deleted when disposed / finalized.
        /// </param>
        public static BrushHandle SetClassBackgroundBrush(this WindowHandle window, BrushHandle brush, bool ownsHandle = true)
            => new BrushHandle(window.SetClassLong(ClassLong.BackgroundBrush, brush.DangerousGetHandle()), ownsHandle);
        public static bool GetMessage(this WindowHandle window, out MSG message, uint minMessage = 0, uint maxMessage = 0)
            => WindowMethods.GetMessage(out message, window, minMessage, maxMessage);
        public static LRESULT SendMessage(this WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
            => WindowMethods.SendMessage(window, message, wParam, lParam);
        public static LRESULT SendMessage(this WindowHandle window, ListBoxMessage message, WPARAM wParam, LPARAM lParam)
            => WindowMethods.SendMessage(window, (WindowMessage)message, wParam, lParam);
        public static bool ScreenToClient(this WindowHandle window, ref POINT point) => GdiMethods.ScreenToClient(window, ref point);
        public static bool ClientToScreen(this WindowHandle window, ref POINT point) => GdiMethods.ClientToScreen(window, ref point);
        public static void CreateCaret(this WindowHandle window, BitmapHandle bitmap, int width, int height) =>
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
