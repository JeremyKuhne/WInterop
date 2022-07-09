// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;
using WInterop.Windows.Native;

namespace Colors1;

/// <summary>
///  Sample from Programming Windows, 5th Edition.
///  Original (c) Charles Petzold, 1998
///  Figure 9-1, Pages 359-362.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Windows.CreateMainWindowAndRun(new Colors1(), "Color Scroll");
    }
}

internal class Colors1 : WindowClass
{
    private readonly Color[] crPrim = { Color.Red, Color.Green, Color.Blue };
    private readonly BrushHolder[] hBrush = new BrushHolder[3];
    private BrushHolder hBrushStatic;
    private readonly WindowHandle[] hwndScroll = new WindowHandle[3];
    private readonly WindowHandle[] hwndLabel = new WindowHandle[3];
    private readonly WindowHandle[] hwndValue = new WindowHandle[3];
    private WindowHandle hwndRect;
    private readonly int[] color = new int[3];
    private int cyChar, idFocus;
    private Rectangle rcColor;
    private readonly string[] szColorLabel = { "Red", "Green", "Blue" };
    private readonly WNDPROC[] OldScroll = new WNDPROC[3];

    // We need to put the delegate in a static to prevent the callback from being collected
    private WindowProcedure _scrollProcedure;

    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.Create:
                // Create the white-rectangle window against which the
                // scroll bars will be positioned. The child window ID is 9.

                hwndRect = Windows.CreateWindow(
                    className: "static",
                    style: WindowStyles.Child | WindowStyles.Visible | (WindowStyles)StaticStyles.WhiteRectangle,
                    parentWindow: window,
                    menuHandle: (MenuHandle)9,
                    instance: ModuleInstance);

                for (int i = 0; i < 3; i++)
                {
                    // The three scroll bars have IDs 0, 1, and 2, with
                    // scroll bar ranges from 0 through 255.
                    hwndScroll[i] = Windows.CreateWindow(
                        className: "scrollbar",
                        style: WindowStyles.Child | WindowStyles.Visible | WindowStyles.TabStop | (WindowStyles)ScrollBarStyles.Veritcal,
                        parentWindow: window,
                        menuHandle: (MenuHandle)i,
                        instance: ModuleInstance);

                    hwndScroll[i].SetScrollRange(ScrollBar.Control, 0, 255, false);
                    hwndScroll[i].SetScrollPosition(ScrollBar.Control, 0, false);

                    // The three color-name labels have IDs 3, 4, and 5,
                    // and text strings “Red”, “Green”, and “Blue”.
                    hwndLabel[i] = Windows.CreateWindow(
                        className: "static",
                        windowName: szColorLabel[i],
                        style: WindowStyles.Child | WindowStyles.Visible | (WindowStyles)StaticStyles.Center,
                        parentWindow: window,
                        menuHandle: (MenuHandle)(i + 3),
                        instance: ModuleInstance);

                    // The three color-value text fields have IDs 6, 7,
                    // and 8, and initial text strings of “0”.
                    hwndValue[i] = Windows.CreateWindow(
                        className: "static",
                        windowName: "0",
                        style: WindowStyles.Child | WindowStyles.Visible | (WindowStyles)StaticStyles.Center,
                        parentWindow: window,
                        menuHandle: (MenuHandle)(i + 6),
                        instance: ModuleInstance);

                    OldScroll[i] = hwndScroll[i].SetWindowProcedure(_scrollProcedure = ScrollProcedure);

                    hBrush[i] = Gdi.CreateSolidBrush(crPrim[i]);
                }

                hBrushStatic = Gdi.GetSystemColorBrush(SystemColor.ButtonHighlight);
                cyChar = Windows.GetDialogBaseUnits().Height;

                return 0;
            case MessageType.Size:
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
            case MessageType.SetFocus:
                hwndScroll[idFocus].SetFocus();
                return 0;
            case MessageType.VerticalScroll:
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
            case MessageType.ControlColorScrollBar:
                return (BrushHandle)hBrush[(int)((WindowHandle)lParam).GetWindowLong(WindowLong.Id)];
            case MessageType.ControlColorStatic:
                id = (int)((WindowHandle)lParam).GetWindowLong(WindowLong.Id);

                if (id >= 3 && id <= 8)
                {
                    DeviceContext dc = (DeviceContext)wParam;
                    dc.SetTextColor(crPrim[id % 3]);
                    dc.SetBackgroundColor(Windows.GetSystemColor(SystemColor.ButtonHighlight));
                    return (BrushHandle)hBrushStatic;
                }
                break;
            case MessageType.SystemColorChange:
                hBrushStatic = Gdi.GetSystemColorBrush(SystemColor.ButtonHighlight);
                return 0;
            case MessageType.Destroy:
                window.SetClassBackgroundBrush(StockBrush.White).Dispose();

                for (int i = 0; i < 3; i++)
                    hBrush[i].Dispose();

                break;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }

    private LResult ScrollProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        int id = (int)window.GetWindowLong(WindowLong.Id);

        switch (message)
        {
            case MessageType.KeyDown:
                if ((VirtualKey)wParam == VirtualKey.Tab)
                    window.GetParent().GetDialogItem(
                        (id + ((Windows.GetKeyState(VirtualKey.Shift) & KeyState.Down) != 0 ? 2 : 1) % 3))
                        .SetFocus();
                break;
            case MessageType.SetFocus:
                idFocus = id;
                break;
        }

        return Windows.CallWindowProcedure(OldScroll[id], window, message, wParam, lParam);
    }
}
