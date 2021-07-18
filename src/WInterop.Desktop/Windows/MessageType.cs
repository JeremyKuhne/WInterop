// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public enum MessageType : uint
    {
        /// <summary>
        ///  Null message. [WM_NULL]
        /// </summary>
        Null = 0x0000,

        /// <summary>
        ///  Sent when an application requests that a window be created by calling the CreateWindowEx or CreateWindow
        ///  function. (The message is sent before the function returns.) The window procedure of the new window
        ///  receives this message after the window is created, but before the window becomes visible. [WM_CREATE]
        /// </summary>
        Create = 0x0001,

        /// <summary>
        ///  Sent after a window has been moved. [WM_DESTROY]
        /// </summary>
        /// <remarks>
        ///  LParam contains the x and y coordinates of the upper-left client area of the window. LOWORD is x, HIWORD
        ///  is y.
        ///
        ///  This message is not sent if <see cref="WindowPositionChanged"/> is handled without calling the default
        ///  window procedure.
        /// </remarks>
        Destroy = 0x0002,

        /// <summary>
        ///  [WM_MOVE]
        /// </summary>
        Move = 0x0003,

        /// <summary>
        ///  Change in the size of the client area of the window. [WM_SIZE]
        /// </summary>
        Size = 0x0005,

        /// <summary>
        ///  [WM_ACTIVATE]
        /// </summary>
        Activate = 0x0006,

        /// <summary>
        ///  [WM_SETFOCUS]
        /// </summary>
        SetFocus = 0x0007,

        /// <summary>
        ///  [WM_KILLFOCUS]
        /// </summary>
        KillFocus = 0x0008,

        /// <summary>
        ///  [WM_ENABLE]
        /// </summary>
        Enable = 0x000A,

        /// <summary>
        ///  [WM_SETREDRAW]
        /// </summary>
        SetRedraw = 0x000B,

        /// <summary>
        ///  [WM_SETTEXT]
        ///  https://docs.microsoft.com/windows/win32/winmsg/wm-settext
        /// </summary>
        SetText = 0x000C,

        /// <summary>
        ///  [WM_GETTEXT]
        /// </summary>
        GetText = 0x000D,

        /// <summary>
        ///  [WM_GETTEXTLENGTH]
        /// </summary>
        GetTextLength = 0x000E,

        /// <summary>
        ///  [WM_PAINT]
        /// </summary>
        Paint = 0x000F,

        /// <summary>
        ///  [WM_CLOSE]
        /// </summary>
        Close = 0x0010,

        /// <summary>
        ///  [WM_QUIT]
        /// </summary>
        Quit = 0x0012,

        /// <summary>
        ///  [WM_ERASEBKGND]
        /// </summary>
        EraseBackground = 0x0014,

        /// <summary>
        ///  [WM_SYSCOLORCHANGE]
        /// </summary>
        SystemColorChange = 0x0015,

        /// <summary>
        ///  [WM_SHOWWINDOW]
        /// </summary>
        ShowWindow = 0x0018,

        /// <summary>
        ///  [WM_WININICHANGE]
        /// </summary>
        WinIniChange = 0x001A,

        /// <summary>
        ///  [WM_SETTINGCHANGE]
        /// </summary>
        SettingChange = WinIniChange,

        /// <summary>
        ///  [WM_DEVMODECHANGE]
        /// </summary>
        DevModeChange = 0x001B,

        /// <summary>
        ///  [WM_ACTIVATEAPP]
        /// </summary>
        ActivateApp = 0x001C,

        /// <summary>
        ///  [WM_FONTCHANGE]
        /// </summary>
        FontChange = 0x001D,

        /// <summary>
        ///  [WM_TIMECHANGE]
        /// </summary>
        TimeChange = 0x001E,

        /// <summary>
        ///  [WM_CANCELMODE]
        /// </summary>
        CancelMode = 0x001F,

        /// <summary>
        ///  [WM_SETCURSOR]
        /// </summary>
        SetCursor = 0x0020,

        /// <summary>
        ///  [WM_MOUSEACTIVATE]
        /// </summary>
        MouseActivate = 0x0021,

        /// <summary>
        ///  [WM_CHILDACTIVATE]
        /// </summary>
        ChildActivate = 0x0022,

        /// <summary>
        ///  [WM_QUEUESYNC]
        /// </summary>
        QueueSync = 0x0023,

        /// <summary>
        ///  [WM_GETMINMAXINFO]
        /// </summary>
        GetMinMaxInfo = 0x0024,

        /// <summary>
        ///  [WM_PAINTICON]
        /// </summary>
        PaintIcon = 0x0026,

        /// <summary>
        ///  [WM_ICONERASEBKGND]
        /// </summary>
        IconEraseBackground = 0x0027,

        /// <summary>
        ///  [WM_NEXTDLGCTL]
        /// </summary>
        NextDialogControl = 0x0028,

        /// <summary>
        ///  [WM_SPOOLERSTATUS]
        /// </summary>
        SpoolerStatus = 0x002A,

        /// <summary>
        ///  [WM_DRAWITEM]
        /// </summary>
        DrawItem = 0x002B,

        /// <summary>
        ///  [WM_MEASUREITEM]
        /// </summary>
        MeasureItem = 0x002C,

        /// <summary>
        ///  [WM_DELETEITEM]
        /// </summary>
        DeleteItem = 0x002D,

        /// <summary>
        ///  [WM_VKEYTOITEM]
        /// </summary>
        VirtualKeyToItem = 0x002E,

        /// <summary>
        ///  [WM_CHARTOITEM]
        /// </summary>
        CharToItem = 0x002F,

        /// <summary>
        ///  [WM_SETFONT]
        ///  https://docs.microsoft.com/windows/win32/winmsg/wm-setfont
        /// </summary>
        SetFont = 0x0030,

        /// <summary>
        ///  [WM_GETFONT]
        ///  https://docs.microsoft.com/windows/win32/winmsg/wm-getfont
        /// </summary>
        GetFont = 0x0031,

        /// <summary>
        ///  [WM_SETHOTKEY]
        /// </summary>
        SetHotKey = 0x0032,

        /// <summary>
        ///  [WM_GETHOTKEY]
        /// </summary>
        GetHotKey = 0x0033,

        /// <summary>
        ///  [WM_QUERYDRAGICON]
        /// </summary>
        QueryDragIcon = 0x0037,

        /// <summary>
        ///  [WM_COMPAREITEM]
        /// </summary>
        CompareItem = 0x0039,

        /// <summary>
        ///  [WM_GETOBJECT]
        /// </summary>
        GetObject = 0x003D,

        /// <summary>
        ///  [WM_COMPACTING]
        /// </summary>
        Compacting = 0x0041,

        /// <summary>
        ///  [WM_COMMNOTIFY]
        /// </summary>
        CommNotify = 0x0044,

        /// <summary>
        ///  [WM_WINDOWPOSCHANGING]
        /// </summary>
        WindowPositionChanging = 0x0046,

        /// <summary>
        ///  Sent to a window whose size, position, or place in the Z order has changed. [WM_WINDOWPOSCHANGED]
        /// </summary>
        /// <remarks>
        ///  LParam contains a pointer to <see cref="WindowPosition"/>.
        /// </remarks>
        WindowPositionChanged = 0x0047,

        /// <summary>
        ///  [WM_POWER]
        /// </summary>
        Power = 0x0048,

        /// <summary>
        ///  [WM_COPYDATA]
        /// </summary>
        CopyData = 0x004A,

        /// <summary>
        ///  [WM_CANCELJOURNAL]
        /// </summary>
        CancelJournal = 0x004B,

        /// <summary>
        ///  [WM_NOTIFY]
        /// </summary>
        Notify = 0x004E,

        /// <summary>
        ///  [WM_INPUTLANGCHANGEREQUEST]
        /// </summary>
        InputLanguageChangeRequest = 0x0050,

        /// <summary>
        ///  [WM_INPUTLANGCHANGE]
        /// </summary>
        InputLanguageChange = 0x0051,

        /// <summary>
        ///  [WM_TCARD]
        /// </summary>
        TrainingCard = 0x0052,

        /// <summary>
        ///  [WM_HELP]
        /// </summary>
        Help = 0x0053,

        /// <summary>
        ///  [WM_USERCHANGED]
        /// </summary>
        UserChanged = 0x0054,

        /// <summary>
        ///  [WM_NOTIFYFORMAT]
        /// </summary>
        NotifyFormat = 0x0055,

        /// <summary>
        ///  [WM_CONTEXTMENU]
        /// </summary>
        ContextMenu = 0x007B,

        /// <summary>
        ///  [WM_STYLECHANGING]
        /// </summary>
        StyleChanging = 0x007C,

        /// <summary>
        ///  [WM_STYLECHANGED]
        /// </summary>
        StyleChanged = 0x007D,

        /// <summary>
        ///  [WM_DISPLAYCHANGE]
        /// </summary>
        DisplayChange = 0x007E,

        /// <summary>
        ///  [WM_GETICON]
        /// </summary>
        GetIcon = 0x007F,

        /// <summary>
        ///  [WM_SETICON]
        /// </summary>
        SetIcon = 0x0080,

        /// <summary>
        ///  [WM_NCCREATE]
        /// </summary>
        NonClientCreate = 0x0081,

        /// <summary>
        ///  [WM_NCDESTROY]
        /// </summary>
        NonClientDestroy = 0x0082,

        /// <summary>
        ///  [WM_NCCALCSIZE]
        /// </summary>
        NonClientCalculateSize = 0x0083,

        /// <summary>
        ///  [WM_NCHITTEST]
        /// </summary>
        NonClientHitTest = 0x0084,

        /// <summary>
        ///  [WM_NCPAINT]
        /// </summary>
        NonClientPaint = 0x0085,

        /// <summary>
        ///  [WM_NCACTIVATE]
        /// </summary>
        NonClientActivate = 0x0086,

        /// <summary>
        ///  [WM_GETDLGCODE]
        /// </summary>
        GetDialogCode = 0x0087,

        /// <summary>
        ///  [WM_SYNCPAINT]
        /// </summary>
        SyncPaint = 0x0088,

        /// <summary>
        ///  [WM_NCMOUSEMOVE]
        /// </summary>
        NonClientMouseMove = 0x00A0,

        /// <summary>
        ///  [WM_NCLBUTTONDOWN]
        /// </summary>
        NonClientLeftButtonDown = 0x00A1,

        /// <summary>
        ///  [WM_NCLBUTTONUP]
        /// </summary>
        NonClientLeftButtonUp = 0x00A2,

        /// <summary>
        ///  [WM_NCLBUTTONDBLCLK]
        /// </summary>
        NonClientLeftButtonDoubleClick = 0x00A3,

        /// <summary>
        ///  [WM_NCRBUTTONDOWN]
        /// </summary>
        NonClientRightButtonDown = 0x00A4,

        /// <summary>
        ///  [WM_NCRBUTTONUP]
        /// </summary>
        NonClientRightButtonUp = 0x00A5,

        /// <summary>
        ///  [WM_NCRBUTTONDBLCLK]
        /// </summary>
        NonClientRightButtonDoubleClick = 0x00A6,

        /// <summary>
        ///  [WM_NCMBUTTONDOWN]
        /// </summary>
        NonClientMiddleButtonDown = 0x00A7,

        /// <summary>
        ///  [WM_NCMBUTTONUP]
        /// </summary>
        NonClientMiddleButtonUp = 0x00A8,

        /// <summary>
        ///  [WM_NCMBUTTONDBLCLK]
        /// </summary>
        NonClientMiddleButtonDoubleClick = 0x00A9,

        /// <summary>
        ///  [WM_NCXBUTTONDOWN]
        /// </summary>
        NonClientExtraButtonDown = 0x00AB,

        /// <summary>
        ///  [WM_NCXBUTTONUP]
        /// </summary>
        NonClientExtraButtonUp = 0x00AC,

        /// <summary>
        ///  [WM_NCXBUTTONDBLCLK]
        /// </summary>
        NonClientExtraButtonDoubleClick = 0x00AD,

        /// <summary>
        ///  [WM_INPUT_DEVICE_CHANGE]
        /// </summary>
        InputDeviceChange = 0x00FE,

        /// <summary>
        ///  [WM_INPUT]
        /// </summary>
        Input = 0x00FF,

        /// <summary>
        ///  Key is pressed without the ALT key down.
        ///  WPARAM is the VirtualKey code. LPARAM is repeat count, scan code, and flags.
        ///  [WM_KEYDOWN] https://msdn.microsoft.com/en-us/library/windows/desktop/ms646280.aspx
        /// </summary>
        KeyDown = 0x0100,

        /// <summary>
        ///  Key is released without the ALT key down.
        ///  WPARAM is the VirtualKey code. LPARAM is repeat count, scan code, and flags.
        ///  [WM_KEYUP] https://msdn.microsoft.com/en-us/library/windows/desktop/ms646281.aspx
        /// </summary>
        KeyUp = 0x0101,

        /// <summary>
        ///  Character code of a key press.
        ///  WPARAM is the VirtualKey code. LPARAM is repeat count, scan code, and flags.
        ///  [WM_CHAR] https://msdn.microsoft.com/en-us/library/windows/desktop/ms646276.aspx
        /// </summary>
        Char = 0x0102,

        /// <summary>
        ///  Character code generated by a dead key [key that combines to make another character,
        ///  such as the umlaut key.]
        ///  WPARAM is the VirtualKey code. LPARAM is repeat count, scan code, and flags.
        ///  [WM_DEADCHAR] https://msdn.microsoft.com/en-us/library/windows/desktop/ms646277.aspx
        /// </summary>
        DeadChar = 0x0103,

        /// <summary>
        ///  Key down when either the ALT key is down or no window has keyboard focus.
        ///  WPARAM is the VirtualKey code. LPARAM is repeat count, scan code, and flags.
        ///  [WM_SYSKEYDOWN] https://msdn.microsoft.com/en-us/library/windows/desktop/ms646286.aspx
        /// </summary>
        SystemKeyDown = 0x0104,

        /// <summary>
        ///  Key up when either the ALT key is down or no window has keyboard focus.
        ///  WPARAM is the VirtualKey code. LPARAM is repeat count, scan code, and flags.
        ///  [WM_SYSKEYUP] https://msdn.microsoft.com/en-us/library/windows/desktop/ms646287.aspx
        /// </summary>
        SystemKeyUp = 0x0105,

        /// <summary>
        ///  Character code of a key pressed while the ALT key is down.
        ///  WPARAM is the VirtualKey code. LPARAM is repeat count, scan code, and flags.
        ///  [WM_SYSCHAR] https://msdn.microsoft.com/en-us/library/windows/desktop/ms646357.aspx
        /// </summary>
        SystemChar = 0x0106,

        /// <summary>
        ///  Character code of a dead key pressed while the ALT key is down.
        ///  WPARAM is the VirtualKey code. LPARAM is repeat count, scan code, and flags.
        ///  [WM_SYSDEADCHAR] https://msdn.microsoft.com/en-us/library/windows/desktop/ms646285.aspx
        /// </summary>
        SystemDeadChar = 0x0107,

        /// <summary>
        ///  [WM_UNICHAR]
        /// </summary>
        UnicodeChar = 0x0109,

        /// <summary>
        ///  [WM_IME_STARTCOMPOSITION]
        /// </summary>
        ImeStartComposition = 0x010D,

        /// <summary>
        ///  [WM_IME_ENDCOMPOSITION]
        /// </summary>
        ImeEndComposition = 0x010E,

        /// <summary>
        ///  [WM_IME_COMPOSITION]
        /// </summary>
        ImeComposition = 0x010F,

        /// <summary>
        ///  [WM_INITDIALOG]
        /// </summary>
        InitDialog = 0x0110,

        /// <summary>
        ///  [WM_COMMAND]
        /// </summary>
        Command = 0x0111,

        /// <summary>
        ///  [WM_SYSCOMMAND]
        /// </summary>
        SystemCommand = 0x0112,

        /// <summary>
        ///  [WM_TIMER]
        /// </summary>
        Timer = 0x0113,

        /// <summary>
        ///  [WM_HSCROLL]
        /// </summary>
        HorizontalScroll = 0x0114,

        /// <summary>
        ///  [WM_VSCROLL]
        /// </summary>
        VerticalScroll = 0x0115,

        /// <summary>
        ///  [WM_INITMENU]
        /// </summary>
        InitMenu = 0x0116,

        /// <summary>
        ///  [WM_INITMENUPOPUP]
        /// </summary>
        InitMenuPopUp = 0x0117,

        /// <summary>
        ///  [WM_GESTURE]
        /// </summary>
        Gesture = 0x0119,

        /// <summary>
        ///  [WM_GESTURENOTIFY]
        /// </summary>
        GestureNotify = 0x011A,

        /// <summary>
        ///  [WM_MENUSELECT]
        /// </summary>
        MenuSelect = 0x011F,

        /// <summary>
        ///  [WM_MENUCHAR]
        /// </summary>
        MenuChar = 0x0120,

        /// <summary>
        ///  [WM_ENTERIDLE]
        /// </summary>
        EnterIdle = 0x0121,

        /// <summary>
        ///  [WM_MENURBUTTONUP]
        /// </summary>
        MenuRightButtonUp = 0x0122,

        /// <summary>
        ///  [WM_MENUDRAG]
        /// </summary>
        MenuDrag = 0x0123,

        /// <summary>
        ///  [WM_MENUGETOBJECT]
        /// </summary>
        MenuGetObject = 0x0124,

        /// <summary>
        ///  [WM_UNINITMENUPOPUP]
        /// </summary>
        UninitializeMenupPopUp = 0x0125,

        /// <summary>
        ///  [WM_MENUCOMMAND]
        /// </summary>
        MenuCommand = 0x0126,

        /// <summary>
        ///  [WM_CHANGEUISTATE]
        /// </summary>
        ChangeUIState = 0x0127,

        /// <summary>
        ///  [WM_UPDATEUISTATE]
        /// </summary>
        UpdateUIState = 0x0128,

        /// <summary>
        ///  [WM_QUERYUISTATE]
        /// </summary>
        QueryUIState = 0x0129,

        /// <summary>
        ///  [WM_CTLCOLORMSGBOX]
        /// </summary>
        ControlColorMessageBox = 0x0132,

        /// <summary>
        ///  [WM_CTLCOLOREDIT]
        /// </summary>
        ControlColorEdit = 0x0133,

        /// <summary>
        ///  [WM_CTLCOLORLISTBOX]
        /// </summary>
        ControlColorListBox = 0x0134,

        /// <summary>
        ///  [WM_CTLCOLORBTN]
        /// </summary>
        ControlColorButton = 0x0135,

        /// <summary>
        ///  [WM_CTLCOLORDLG]
        /// </summary>
        ControlColorDialog = 0x0136,

        /// <summary>
        ///  [WM_CTLCOLORSCROLLBAR]
        /// </summary>
        ControlColorScrollBar = 0x0137,

        /// <summary>
        ///  [WM_CTLCOLORSTATIC]
        /// </summary>
        ControlColorStatic = 0x0138,

        /// <summary>
        ///  [WM_MOUSEMOVE]
        /// </summary>
        MouseMove = 0x0200,

        /// <summary>
        ///  [WM_LBUTTONDOWN]
        /// </summary>
        LeftButtonDown = 0x0201,

        /// <summary>
        ///  [WM_LBUTTONUP]
        /// </summary>
        LeftButtonUp = 0x0202,

        /// <summary>
        ///  [WM_LBUTTONDBLCLK]
        /// </summary>
        LeftButtonDoubleClick = 0x0203,

        /// <summary>
        ///  [WM_RBUTTONDOWN]
        /// </summary>
        RightButtonDown = 0x0204,

        /// <summary>
        ///  [WM_RBUTTONUP]
        /// </summary>
        RightButtonUp = 0x0205,

        /// <summary>
        ///  [WM_RBUTTONDBLCLK]
        /// </summary>
        RightButtonDoubleClick = 0x0206,

        /// <summary>
        ///  [WM_MBUTTONDOWN]
        /// </summary>
        MiddleButtonDown = 0x0207,

        /// <summary>
        ///  [WM_MBUTTONUP]
        /// </summary>
        MiddleButtonUp = 0x0208,

        /// <summary>
        ///  [WM_MBUTTONDBLCLK]
        /// </summary>
        MiddleButtonDoubleClick = 0x0209,

        /// <summary>
        ///  [WM_MOUSEWHEEL]
        /// </summary>
        MouseWheel = 0x020A,

        /// <summary>
        ///  [WM_XBUTTONDOWN]
        /// </summary>
        ExtraButtonDown = 0x020B,

        /// <summary>
        ///  [WM_XBUTTONUP]
        /// </summary>
        ExtraButtonUp = 0x020C,

        /// <summary>
        ///  [WM_XBUTTONDBLCLK]
        /// </summary>
        ExtraButtonDoubleClick = 0x020D,

        /// <summary>
        ///  [WM_MOUSEHWHEEL]
        /// </summary>
        MouseHorizontalWheel = 0x020E,

        /// <summary>
        ///  [WM_PARENTNOTIFY]
        /// </summary>
        ParentNotify = 0x0210,

        /// <summary>
        ///  [WM_ENTERMENULOOP]
        /// </summary>
        EnterMenuLoop = 0x0211,

        /// <summary>
        ///  [WM_EXITMENULOOP]
        /// </summary>
        ExitMenuLoop = 0x0212,

        /// <summary>
        ///  [WM_NEXTMENU]
        /// </summary>
        NextMenu = 0x0213,

        /// <summary>
        ///  [WM_SIZING]
        /// </summary>
        Sizing = 0x0214,

        /// <summary>
        ///  [WM_CAPTURECHANGED]
        /// </summary>
        CaptureChanged = 0x0215,

        /// <summary>
        ///  [WM_MOVING]
        /// </summary>
        Moving = 0x0216,

        /// <summary>
        ///  [WM_POWERBROADCAST]
        /// </summary>
        PowerBroadcast = 0x0218,

        /// <summary>
        ///  [WM_DEVICECHANGE]
        /// </summary>
        DeviceChange = 0x0219,

        /// <summary>
        ///  [WM_MDICREATE]
        /// </summary>
        MdiCreate = 0x0220,

        /// <summary>
        ///  [WM_MDIDESTROY]
        /// </summary>
        MdiDestroy = 0x0221,

        /// <summary>
        ///  [WM_MDIACTIVATE]
        /// </summary>
        MdiActivate = 0x0222,

        /// <summary>
        ///  [WM_MDIRESTORE]
        /// </summary>
        MdiRestore = 0x0223,

        /// <summary>
        ///  [WM_MDINEXT]
        /// </summary>
        MdiNext = 0x0224,

        /// <summary>
        ///  [WM_MDIMAXIMIZE]
        /// </summary>
        MdiMaximize = 0x0225,

        /// <summary>
        ///  [WM_MDITILE]
        /// </summary>
        MdiTile = 0x0226,

        /// <summary>
        ///  [WM_MDICASCADE]
        /// </summary>
        MdiCascade = 0x0227,

        /// <summary>
        ///  [WM_MDIICONARRANGE]
        /// </summary>
        MdiIconArrange = 0x0228,

        /// <summary>
        ///  [WM_MDIGETACTIVE]
        /// </summary>
        MdiGetActive = 0x0229,

        /// <summary>
        ///  [WM_MDISETMENU]
        /// </summary>
        MdiSetMenu = 0x0230,

        /// <summary>
        ///  [WM_ENTERSIZEMOVE]
        /// </summary>
        EnterSizeMove = 0x0231,

        /// <summary>
        ///  [WM_EXITSIZEMOVE]
        /// </summary>
        ExitSizeMove = 0x0232,

        /// <summary>
        ///  [WM_DROPFILES]
        /// </summary>
        DropFiles = 0x0233,

        /// <summary>
        ///  [WM_MDIREFRESHMENU]
        /// </summary>
        MdiRefreshMenu = 0x0234,

        /// <summary>
        ///  [WM_POINTERDEVICECHANGE]
        /// </summary>
        PointerDeviceChange = 0x0238,

        /// <summary>
        ///  [WM_POINTERDEVICEINRANGE]
        /// </summary>
        PointDeviceInRange = 0x0239,

        /// <summary>
        ///  [WM_POINTERDEVICEOUTOFRANGE]
        /// </summary>
        PointerDeviceOutOfRange = 0x023A,

        /// <summary>
        ///  [WM_TOUCH]
        /// </summary>
        Touch = 0x0240,

        /// <summary>
        ///  [WM_NCPOINTERUPDATE]
        /// </summary>
        NonClientPointerUpdate = 0x0241,

        /// <summary>
        ///  [WM_NCPOINTERDOWN]
        /// </summary>
        NonClientPointerDown = 0x0242,

        /// <summary>
        ///  [WM_NCPOINTERUP]
        /// </summary>
        NonClientPointerUp = 0x0243,

        /// <summary>
        ///  [WM_POINTERUPDATE]
        /// </summary>
        PointerUpdate = 0x0245,

        /// <summary>
        ///  [WM_POINTERDOWN]
        /// </summary>
        PointerDown = 0x0246,

        /// <summary>
        ///  [WM_POINTERUP]
        /// </summary>
        PointerUp = 0x0247,

        /// <summary>
        ///  [WM_POINTERENTER]
        /// </summary>
        PointerEnter = 0x0249,

        /// <summary>
        ///  [WM_POINTERLEAVE]
        /// </summary>
        PointerLeave = 0x024A,

        /// <summary>
        ///  [WM_POINTERACTIVATE]
        /// </summary>
        PointerActivate = 0x024B,

        /// <summary>
        ///  [WM_POINTERCAPTURECHANGED]
        /// </summary>
        PointerCaptureChanged = 0x024C,

        /// <summary>
        ///  [WM_TOUCHHITTESTING]
        /// </summary>
        TouchHitTesting = 0x024D,

        /// <summary>
        ///  [WM_POINTERWHEEL]
        /// </summary>
        PointerWheel = 0x024E,

        /// <summary>
        ///  [WM_POINTERHWHEEL]
        /// </summary>
        PointerHorizontalWheel = 0x024F,

        /// <summary>
        ///  [WM_POINTERROUTEDTO]
        /// </summary>
        PointerRoutedTo = 0x0251,

        /// <summary>
        ///  [WM_POINTERROUTEDAWAY]
        /// </summary>
        PointerRoutedAway = 0x0252,

        /// <summary>
        ///  [WM_POINTERROUTEDRELEASED]
        /// </summary>
        PointerRoutedReleased = 0x0253,

        /// <summary>
        ///  [WM_IME_SETCONTEXT]
        /// </summary>
        ImeSetContext = 0x0281,

        /// <summary>
        ///  [WM_IME_NOTIFY]
        /// </summary>
        ImeNotify = 0x0282,

        /// <summary>
        ///  [WM_IME_CONTROL]
        /// </summary>
        ImeControl = 0x0283,

        /// <summary>
        ///  [WM_IME_COMPOSITIONFULL]
        /// </summary>
        ImeCompositionFull = 0x0284,

        /// <summary>
        ///  [WM_IME_SELECT]
        /// </summary>
        ImeSelect = 0x0285,

        /// <summary>
        ///  [WM_IME_CHAR]
        /// </summary>
        ImeChar = 0x0286,

        /// <summary>
        ///  [WM_IME_REQUEST]
        /// </summary>
        ImeRequest = 0x0288,

        /// <summary>
        ///  [WM_IME_KEYDOWN]
        /// </summary>
        ImeKeyDown = 0x0290,

        /// <summary>
        ///  [WM_IME_KEYUP]
        /// </summary>
        ImeKeyUp = 0x0291,

        /// <summary>
        ///  [WM_MOUSEHOVER]
        /// </summary>
        MouseHover = 0x02A1,

        /// <summary>
        ///  [WM_MOUSELEAVE]
        /// </summary>
        MouseLeave = 0x02A3,

        /// <summary>
        ///  [WM_NCMOUSEHOVER]
        /// </summary>
        NonClientMouseHover = 0x02A0,

        /// <summary>
        ///  [WM_NCMOUSELEAVE]
        /// </summary>
        NonClientMouseLeave = 0x02A2,

        /// <summary>
        ///  [WM_WTSSESSION_CHANGE]
        /// </summary>
        WtsSessionChange = 0x02B1,

        /// <summary>
        ///  [WM_DPICHANGED]
        /// </summary>
        DpiChanged = 0x02E0,

        /// <summary>
        ///  [WM_DPICHANGED_BEFOREPARENT]
        ///  https://docs.microsoft.com/windows/win32/hidpi/wm-dpichanged
        /// </summary>
        DpiChangedBeforeParent = 0x02E2,

        /// <summary>
        ///  [WM_DPICHANGED_AFTERPARENT]
        /// </summary>
        DpiChangedAfterParent = 0x02E3,

        /// <summary>
        ///  [WM_GETDPISCALEDSIZE]
        /// </summary>
        GetDpiScaledSize = 0x02E4,

        /// <summary>
        ///  [WM_CUT]
        /// </summary>
        Cut = 0x0300,

        /// <summary>
        ///  [WM_COPY]
        /// </summary>
        Copy = 0x0301,

        /// <summary>
        ///  [WM_PASTE]
        /// </summary>
        Paste = 0x0302,

        /// <summary>
        ///  [WM_CLEAR]
        /// </summary>
        Clear = 0x0303,

        /// <summary>
        ///  [WM_UNDO]
        /// </summary>
        Undo = 0x0304,

        /// <summary>
        ///  [WM_RENDERFORMAT]
        /// </summary>
        RenderFormat = 0x0305,

        /// <summary>
        ///  [WM_RENDERALLFORMATS]
        /// </summary>
        RenderAllFormats = 0x0306,

        /// <summary>
        ///  [WM_DESTROYCLIPBOARD]
        /// </summary>
        DestroyClipboard = 0x0307,

        /// <summary>
        ///  [WM_DRAWCLIPBOARD]
        /// </summary>
        DrawClipboard = 0x0308,

        /// <summary>
        ///  [WM_PAINTCLIPBOARD]
        /// </summary>
        PaintClipboard = 0x0309,

        /// <summary>
        ///  [WM_VSCROLLCLIPBOARD]
        /// </summary>
        VerticalScrollClipboard = 0x030A,

        /// <summary>
        ///  [WM_SIZECLIPBOARD]
        /// </summary>
        SizeClipboard = 0x030B,

        /// <summary>
        ///  [WM_ASKCBFORMATNAME]
        /// </summary>
        AskClipboardFormatName = 0x030C,

        /// <summary>
        ///  [WM_CHANGECBCHAIN]
        /// </summary>
        ChangeClipboardChain = 0x030D,

        /// <summary>
        ///  [WM_HSCROLLCLIPBOARD]
        /// </summary>
        HorizontalScrollClipboard = 0x030E,

        /// <summary>
        ///  [WM_QUERYNEWPALETTE]
        /// </summary>
        QueryNewPalette = 0x030F,

        /// <summary>
        ///  [WM_PALETTEISCHANGING]
        /// </summary>
        PaletteIsChanging = 0x0310,

        /// <summary>
        ///  [WM_PALETTECHANGED]
        /// </summary>
        PaletteChanged = 0x0311,

        /// <summary>
        ///  [WM_HOTKEY]
        /// </summary>
        HotKey = 0x0312,

        /// <summary>
        ///  [WM_PRINT]
        /// </summary>
        Print = 0x0317,

        /// <summary>
        ///  [WM_PRINTCLIENT]
        /// </summary>
        PrintClient = 0x0318,

        /// <summary>
        ///  [WM_APPCOMMAND]
        /// </summary>
        AppCommand = 0x0319,

        /// <summary>
        ///  [WM_THEMECHANGED]
        /// </summary>
        ThemeChanged = 0x031A,

        /// <summary>
        ///  [WM_CLIPBOARDUPDATE]
        /// </summary>
        ClipboardUpdate = 0x031D,

        /// <summary>
        ///  [WM_DWMCOMPOSITIONCHANGED]
        /// </summary>
        DwmCompositionChanged = 0x031E,

        /// <summary>
        ///  [WM_DWMNCRENDERINGCHANGED]
        /// </summary>
        DwmNonClientRenderingChanged = 0x031F,

        /// <summary>
        ///  [WM_DWMCOLORIZATIONCOLORCHANGED]
        /// </summary>
        DwmColorizationColorChanged = 0x0320,

        /// <summary>
        ///  [WM_DWMWINDOWMAXIMIZEDCHANGE]
        /// </summary>
        DwmWindowMaximizedChange = 0x0321,

        /// <summary>
        ///  [WM_DWMSENDICONICTHUMBNAIL]
        /// </summary>
        DwmSendIconicThumbnail = 0x0323,

        /// <summary>
        ///  [WM_DWMSENDICONICLIVEPREVIEWBITMAP]
        /// </summary>
        DwmSendIconicLivePreviewBitmap = 0x0326,

        /// <summary>
        ///  [WM_GETTITLEBARINFOEX]
        /// </summary>
        GetTitleBarInfo = 0x033F
    }
}