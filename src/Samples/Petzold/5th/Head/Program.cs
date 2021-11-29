// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System.Drawing;
using System.Text;
using WInterop.Gdi;
using WInterop.Windows;
using WInterop.Storage;
using WInterop.Windows.Native;

namespace Head;

/// <summary>
///  Sample from Programming Windows, 5th Edition.
///  Original (c) Charles Petzold, 1998
///  Figure 9-9, Pages 411-415.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Windows.CreateMainWindowAndRun(new Head(), "Head");
    }
}

internal class Head : WindowClass
{
    public Head() : base(backgroundBrush: SystemColor.ButtonFace) { }

    private static readonly FileTypes s_dirAttr = FileTypes.ReadWrite | FileTypes.ReadOnly | FileTypes.Hidden | FileTypes.System
        | FileTypes.Directory | FileTypes.Archive | FileTypes.Drives;
    private static readonly TextFormat s_dtFlags = TextFormat.WordBreak | TextFormat.ExpandTabs | TextFormat.NoClip | TextFormat.NoPrefix;
    private WindowHandle _hwndList, _hwndText;
    private WNDPROC _existingListBoxWndProc;
    private WindowProcedure _listBoxProcedure;
    private const int ID_LIST = 1;
    private const int ID_TEXT = 2;
    private const int MAXREAD = 8192;
    private bool _bValidFile;
    private readonly char[] _szFile = new char[256];
    private readonly byte[] _buffer = new byte[MAXREAD];
    private readonly char[] _decoded = new char[MAXREAD];
    private Rectangle _rect;

    protected override unsafe LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        const string filter = "*.*";

        switch (message)
        {
            case MessageType.Create:
                Size baseUnits = Windows.GetDialogBaseUnits();
                _rect = Rectangle.FromLTRB(20 * baseUnits.Width, 3 * baseUnits.Height, _rect.Right, _rect.Bottom);

                // Create listbox and static text windows.
                _hwndList = Windows.CreateWindow(
                    className: "listbox",
                    style: WindowStyles.Child | WindowStyles.Visible | (WindowStyles)ListBoxStyles.Standard,
                    bounds: new Rectangle(baseUnits.Width, baseUnits.Height * 3,
                        baseUnits.Width * 13 + Windows.GetSystemMetrics(SystemMetric.VerticalScrollWidth), baseUnits.Height * 10),
                    parentWindow: window,
                    menuHandle: (MenuHandle)ID_LIST,
                    instance: ModuleInstance);

                _hwndText = Windows.CreateWindow(
                    className: "static",
                    windowName: Storage.GetCurrentDirectory(),
                    style: WindowStyles.Child | WindowStyles.Visible | (WindowStyles)StaticStyles.Left,
                    bounds: new Rectangle(baseUnits.Width, baseUnits.Height, baseUnits.Width * 260, baseUnits.Height),
                    parentWindow: window,
                    menuHandle: (MenuHandle)ID_TEXT,
                    instance: ModuleInstance);

                _existingListBoxWndProc = _hwndList.SetWindowProcedure(_listBoxProcedure = ListBoxProcedure);

                fixed (char* f = filter)
                    _hwndList.SendMessage(ListBoxMessage.Directory, (uint)s_dirAttr, f);
                return 0;
            case MessageType.Size:
                _rect = Rectangle.FromLTRB(_rect.Left, _rect.Top, lParam.LowWord, lParam.HighWord);
                return 0;
            case MessageType.SetFocus:
                _hwndList.SetFocus();
                return 0;
            case MessageType.Command:
                if (wParam.LowWord == ID_LIST
                    && (wParam.HighWord == (ushort)ListBoxNotification.DoubleClick))
                {
                    uint i = _hwndList.SendMessage(ListBoxMessage.GetCurrentSelection, 0, 0);
                    if (i == WindowDefines.LB_ERR)
                        break;

                    int iLength = _hwndList.SendMessage(ListBoxMessage.GetTextLength, i, 0) + 1;
                    fixed (char* textBuffer = _szFile)
                    {
                        int result = _hwndList.SendMessage(ListBoxMessage.GetText, i, textBuffer);
                        SafeFileHandle hFile = null;
                        try
                        {
                            using (hFile = Storage.CreateFile(_szFile.AsSpan(0, result),
                                CreationDisposition.OpenExisting, DesiredAccess.GenericRead, ShareModes.Read))
                            {
                                if (!hFile.IsInvalid)
                                {
                                    _bValidFile = true;
                                    _hwndText.SetWindowText(Storage.GetCurrentDirectory());
                                }
                            }
                            hFile = null;
                        }
                        catch
                        {
                        }

                        Span<char> dir = stackalloc char[2];
                        if (hFile == null && _szFile[0] == ('['))
                        {
                            _bValidFile = false;

                            // If setting the directory doesn’t work, maybe it’s a drive change, so try that.
                            try
                            {
                                _szFile[result - 1] = '\0';
                                Storage.SetCurrentDirectory(_szFile.AsSpan(1, result - 2));
                            }
                            catch
                            {
                                dir[0] = _szFile[2];
                                dir[1] = ':';

                                try { Storage.SetCurrentDirectory(dir); }
                                catch { }
                            }

                            // Get the new directory name and fill the list box.
                            _hwndText.SetWindowText(Storage.GetCurrentDirectory());
                            _hwndList.SendMessage(ListBoxMessage.ResetContent, 0, 0);
                            fixed (char* f = filter)
                                _hwndList.SendMessage(ListBoxMessage.Directory, (uint)s_dirAttr, f);
                        }
                    }

                    window.Invalidate();
                }
                return 0;
            case MessageType.Paint:
                if (!_bValidFile)
                    break;

                uint bytesRead;
                using (var hFile = Storage.CreateFile(_szFile, CreationDisposition.OpenExisting,
                    DesiredAccess.GenericRead, ShareModes.Read))
                {
                    if (hFile.IsInvalid)
                    {
                        _bValidFile = false;
                        break;
                    }

                    bytesRead = Storage.ReadFile(hFile, _buffer);
                }

                using (DeviceContext dc = window.BeginPaint())
                {
                    dc.SelectObject(StockFont.SystemFixed);
                    dc.SetTextColor(SystemColor.ButtonText);
                    dc.SetBackgroundColor(SystemColor.ButtonFace);
                    Encoding.UTF8.GetDecoder().Convert(_buffer.AsSpan(0, (int)bytesRead), _decoded.AsSpan(), true, out _, out int charCount, out _);
                    dc.DrawText(_decoded.AsSpan(0, charCount), _rect, s_dtFlags);
                }

                return 0;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }

    private LResult ListBoxProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        if (message == MessageType.KeyDown && (VirtualKey)wParam == VirtualKey.Return)
        {
            window.GetParent().SendMessage(
                MessageType.Command,
                new WParam(1, (ushort)ListBoxNotification.DoubleClick),
                (LParam)window);
        }

        return Windows.CallWindowProcedure(_existingListBoxWndProc, window, message, wParam, lParam);
    }
}
