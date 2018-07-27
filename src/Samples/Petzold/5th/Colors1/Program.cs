// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Resources;
using WInterop.Windows;

namespace Colors1
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 9-1, Pages 359-362.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string szAppName = "Colors1";

            ModuleInstance module = ModuleInstance.GetModuleForType(typeof(Program));
            WindowClass wndclass = new WindowClass
            {
                Style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = IconId.Application,
                Cursor = CursorId.Arrow,
                Background = StockBrush.White,
                ClassName = szAppName
            };

            Windows.RegisterClass(ref wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "Color Scroll",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static Color[] crPrim = { Color.Red, Color.Green, Color.Blue };
        static BrushHandle[] hBrush = new BrushHandle[3];
        static BrushHandle hBrushStatic;
        static WindowHandle[] hwndScroll = new WindowHandle[3];
        static WindowHandle[] hwndLabel = new WindowHandle[3];
        static WindowHandle[] hwndValue = new WindowHandle[3];
        static WindowHandle hwndRect;
        static int[] color = new int[3];
        static int cyChar, idFocus;
        static Rectangle rcColor;
        static string[] szColorLabel = { "Red", "Green", "Blue" };
        static IntPtr[] OldScroll = new IntPtr[3];

        // We need to put the delegate in a static to prevent the callback from being collected
        static WindowProcedure s_ScrollProcedure = ScrollProcedure;


        static unsafe LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    ModuleInstance hInstance = window.GetWindowLong(WindowLong.InstanceHandle);

                    // Create the white-rectangle window against which the
                    // scroll bars will be positioned. The child window ID is 9.

                    hwndRect = Windows.CreateWindow("static", null,
                        WindowStyles.Child | WindowStyles.Visible | (WindowStyles)StaticStyles.WhiteRectangle,
                        ExtendedWindowStyles.Default,
                        new Rectangle(),
                        window, (IntPtr)9, hInstance, IntPtr.Zero);

                    for (int i = 0; i < 3; i++)
                    {
                        // The three scroll bars have IDs 0, 1, and 2, with
                        // scroll bar ranges from 0 through 255.
                        hwndScroll[i] = Windows.CreateWindow("scrollbar", null,
                            WindowStyles.Child | WindowStyles.Visible | WindowStyles.TabStop | (WindowStyles)ScrollBarStyles.Veritcal,
                            ExtendedWindowStyles.Default,
                            new Rectangle(),
                            window, (IntPtr)i, hInstance, IntPtr.Zero);

                        hwndScroll[i].SetScrollRange(ScrollBar.Control, 0, 255, false);
                        hwndScroll[i].SetScrollPosition(ScrollBar.Control, 0, false);

                        // The three color-name labels have IDs 3, 4, and 5,
                        // and text strings “Red”, “Green”, and “Blue”.
                        hwndLabel[i] = Windows.CreateWindow("static", szColorLabel[i],
                            WindowStyles.Child | WindowStyles.Visible | (WindowStyles)StaticStyles.Center,
                            ExtendedWindowStyles.Default,
                            new Rectangle(),
                            window, (IntPtr)i + 3, hInstance, IntPtr.Zero);

                        // The three color-value text fields have IDs 6, 7,
                        // and 8, and initial text strings of “0”.
                        hwndValue[i] = Windows.CreateWindow("static", "0",
                            WindowStyles.Child | WindowStyles.Visible | (WindowStyles)StaticStyles.Center,
                            ExtendedWindowStyles.Default,
                            new Rectangle(),
                            window, (IntPtr)i + 6, hInstance, IntPtr.Zero);

                        OldScroll[i] = hwndScroll[i].SetWindowProcedure(s_ScrollProcedure);

                        hBrush[i] = Gdi.CreateSolidBrush(crPrim[i]);
                    }

                    hBrushStatic = Gdi.GetSystemColorBrush(SystemColor.ButtonHighlight);
                    cyChar = Windows.GetDialogBaseUnits().Height;

                    return 0;
                case WindowMessage.Size:
                    int cxClient = lParam.LowWord;
                    int cyClient = lParam.HighWord;
                    rcColor = Rectangle.FromLTRB(cxClient / 2, 0, cxClient, cyClient);
                    hwndRect.MoveWindow(new Rectangle(0, 0, cxClient / 2, cyClient), repaint: true);

                    for (int i = 0; i < 3; i++)
                    {
                        hwndScroll[i].MoveWindow(
                            new Rectangle((2 * i + 1) * cxClient / 14, 2 * cyChar, cxClient / 14, cyClient - 4 * cyChar),
                            repaint: true);
                        hwndLabel[i].MoveWindow(
                            new Rectangle((4 * i + 1) * cxClient / 28, cyChar / 2, cxClient / 7, cyChar),
                            repaint: true);
                        hwndValue[i].MoveWindow(
                            new Rectangle((4 * i + 1) * cxClient / 28, cyClient - 3 * cyChar / 2, cxClient / 7, cyChar),
                            repaint: true);
                    }

                    window.SetFocus();
                    return 0;
                case WindowMessage.SetFocus:
                    hwndScroll[idFocus].SetFocus();
                    return 0;
                case WindowMessage.VerticalScroll:
                    int id = (int)((WindowHandle)lParam).GetWindowLong(WindowLong.Id);

                    switch ((ScrollCommand)wParam.LowWord)
                    {
                        case ScrollCommand.PageDown:
                            color[id] += 15;
                            goto case ScrollCommand.LineDown;
                        case ScrollCommand.LineDown:
                            color[id] = Math.Min(255, color[id] + 1);
                            break;
                        case ScrollCommand.PageUp:
                            color[id] -= 15;
                            goto case ScrollCommand.LineUp;
                        case ScrollCommand.LineUp:
                            color[id] = Math.Max(0, color[id] - 1);
                            break;
                        case ScrollCommand.Top:
                            color[id] = 0;
                            break;
                        case ScrollCommand.Bottom:
                            color[id] = 255;
                            break;
                        case ScrollCommand.ThumbPosition:
                        case ScrollCommand.ThumbTrack:
                            color[id] = wParam.HighWord;
                            break;
                        default:
                            return 0;
                    }

                    hwndScroll[id].SetScrollPosition(ScrollBar.Control, color[id], true);

                    hwndValue[id].SetWindowText(color[id].ToString());

                    // We'll dispose when we set the next brush
                    BrushHandle brush = Gdi.CreateSolidBrush(Color.FromArgb(color[0], color[1], color[2]));

                    window.SetClassBackgroundBrush(brush).Dispose();
                    window.InvalidateRectangle(rcColor, true);
                    return 0;
                case WindowMessage.ControlColorScrollBar:
                    return hBrush[(int)((WindowHandle)lParam).GetWindowLong(WindowLong.Id)];
                case WindowMessage.ControlColorStatic:
                    id = (int)((WindowHandle)lParam).GetWindowLong(WindowLong.Id);

                    if (id >= 3 && id <= 8)
                    {
                        DeviceContext dc = (DeviceContext)wParam;
                        dc.SetTextColor(crPrim[id % 3]);
                        dc.SetBackgroundColor(Windows.GetSystemColor(SystemColor.ButtonHighlight));
                        return hBrushStatic;
                    }
                    break;
                case WindowMessage.SystemColorChange:
                    hBrushStatic = Gdi.GetSystemColorBrush(SystemColor.ButtonHighlight);
                    return 0;
                case WindowMessage.Destroy:
                    window.SetClassBackgroundBrush(StockBrush.White).Dispose();

                    for (int i = 0; i < 3; i++)
                        hBrush[i].Dispose();

                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }

        static LRESULT ScrollProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            int id = (int)window.GetWindowLong(WindowLong.Id).ToInt64();

            switch (message)
            {
                case WindowMessage.KeyDown:
                    if ((VirtualKey)wParam == VirtualKey.Tab)
                        window.GetParent().GetDialogItem(
                            (id + ((Windows.GetKeyState(VirtualKey.Shift) & KeyState.Down) != 0 ? 2 : 1) % 3))
                            .SetFocus();
                    break;
                case WindowMessage.SetFocus:
                    idFocus = id;
                    break;
            }

            return WindowMethods.CallWindowProcedure(OldScroll[id], window, message, wParam, lParam);
        }
    }
}
