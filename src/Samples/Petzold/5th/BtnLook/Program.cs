// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Extensions.WindowExtensions;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

namespace BtnLook
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
            const string szAppName = "BtnLook";

            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
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

            Windows.RegisterClass(wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "Button Look",
                WindowStyle.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static ButtonStyles[] button =
        {
            ButtonStyles.PushButton,
            ButtonStyles.DefaultPushButton,
            ButtonStyles.CheckBox,
            ButtonStyles.AutoCheckBox,
            ButtonStyles.RadioButton,
            ButtonStyles.ThreeState,
            ButtonStyles.AutoThreeState,
            ButtonStyles.GroupBox,
            ButtonStyles.AutoRadioButton,
            ButtonStyles.OwnerDrawn
        };

        static WindowHandle[] hwndButton;
        static RECT rect;
        static SIZE baseUnits;

        static unsafe  LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    baseUnits = Windows.GetDialogBaseUnits();
                    hwndButton = new WindowHandle[button.Length];
                    CREATESTRUCT* create = (CREATESTRUCT*)(void*)lParam;
                    for (int i = 0; i < button.Length; i++)
                        hwndButton[i] = Windows.CreateWindow(create->Instance, "button", button[i].ToString(),
                            WindowStyle.Child | WindowStyle.Visible | (WindowStyle)button[i],
                            ExtendedWindowStyle.None,
                            baseUnits.cx, baseUnits.cy * (1 + 2 * i),
                            20 * baseUnits.cx, 7 * baseUnits.cy / 4,
                            window, (IntPtr)i, IntPtr.Zero);
                    return 0;
                case WindowMessage.Size:
                    rect.left = 24 * baseUnits.cx;
                    rect.top = 2 * baseUnits.cy;
                    rect.right = lParam.LowWord;
                    rect.bottom = lParam.HighWord;
                    return 0;
                case WindowMessage.Paint:
                    window.InvalidateRectangle(rect, true);
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.SelectObject(StockFont.SystemFixed);
                        dc.SetBackgroundMode(BackgroundMode.Transparent);
                        dc.TextOut(24 * baseUnits.cx, baseUnits.cy, "message       wParam        lParam");
                        dc.TextOut(24 * baseUnits.cx, baseUnits.cy, "_______       ______        ______");
                    }
                    return 0;
                case WindowMessage.DrawItem:
                case WindowMessage.Command:
                    window.ScrollWindow(0, -baseUnits.cy, rect, rect);
                    using (DeviceContext dc = window.GetDeviceContext())
                    {
                        dc.SelectObject(StockFont.SystemFixed);
                        dc.TextOut(24 * baseUnits.cx, baseUnits.cy * (rect.bottom / baseUnits.cy - 1),
                            $"{message,-13} {wParam.HighWord:X4}-{wParam.LowWord:X4}     {lParam.HighWord:X4}-{lParam.LowWord:X4}");
                    }
                    window.ValidateRectangle(ref rect);
                    return 0;
                case WindowMessage.Destroy:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
