// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Support;
using WInterop.Windows.Types;

namespace WInterop.Resources
{
    public static partial class ResourceMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647486.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern int LoadStringW(
                ModuleInstance hInstance,
                int uID,
                out char* lpBuffer,
                int nBufferMax);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648402.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetCaretPos(
                out POINT lpPoint);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648405.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetCaretPos(
                int X,
                int Y);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648406.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool ShowCaret(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648403.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool HideCaret(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648399.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool CreateCaret(
                WindowHandle hWnd,
                BitmapHandle hBitmap,
                int nWidth,
                int nHeight);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648400.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool DestroyCaret();

            // uint.MaxValue is INFINITE, or doesn't blink
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648401.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetCaretBlinkTime();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648404.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetCaretBlinkTime(
                uint uMSeconds);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648072.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern SharedIconHandle LoadIconW(
                ModuleInstance hInstance,
                IntPtr lpIconName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648063.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool DestroyIcon(
                IntPtr hIcon);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648383.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static unsafe extern bool ClipCursor(
                RECT* lpRect);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648384.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern CursorHandle CopyCursor(
                CursorHandle pcur);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648385.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern CursorHandle CreateCursor(
                ModuleInstance hInst,
                int xHotSpot,
                int yHotSpot,
                int nWidth,
                int nHeight,
                byte[] pvANDPlane,
                byte[] pvXORPlane);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648386.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool DestroyCursor(
                IntPtr hCursor);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648387.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetClipCursor(
                out RECT lpRect);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648388.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern SharedCursorHandle GetCursor();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648389.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetCursorInfo(
                ref CURSORINFO pci);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648390.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetCursorPos(
                out POINT lpPoint);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa969464.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetPhysicalCursorPos(
                out POINT lpPoint);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648391.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern SharedCursorHandle LoadCursorW(
                ModuleInstance hInstance,
                IntPtr lpCursorName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648392.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern SharedCursorHandle LoadCursorFromFileW(
                IntPtr lpFileName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648393.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern SharedCursorHandle SetCursor(
                CursorHandle hCursor);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648394.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetCursorPos(
                int X,
                int Y);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa969465.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetPhysicalCursorPos(
                int X,
                int Y);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648395.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetSystemCursor(
                CursorHandle hcur,
                SystemCursor id);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648396.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern int ShowCursor(
                bool bShow);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647616.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool AppendMenuW(
                MenuHandle hMenu,
                MenuFlags uFlags,
                IntPtr uIDNewItem,
                IntPtr lpNewItem);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647619.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern MenuFlags CheckMenuItem(
                MenuHandle hmenu,
                uint uIDCheckItem,
                MenuFlags uCheck);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647621.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool CheckMenuRadioItem(
                MenuHandle hmenu,
                uint idFirst,
                uint idLast,
                uint idCheck,
                MenuFlags uFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647624.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern MenuHandle CreateMenu();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647626.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern MenuHandle CreatePopupMenu();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647626.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool DeleteMenu(
                MenuHandle hMenu,
                uint uPosition,
                MenuFlags uFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647631.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool DestroyMenu(
                IntPtr hMenu);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647633.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool DrawMenuBar(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647636.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool EnableMenuItem(
                MenuHandle hMenu,
                uint uIDEnableItem,
                MenuFlags uEnable);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647637.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool EndMenu();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647640.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern SharedMenuHandle GetMenu(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647833.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetMenuBarInfo(
                WindowHandle hwnd,
                MenuObject idObject,
                int idItem,
                ref MENUBARINFO pmbi);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647976.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetMenuDefaultItem(
                MenuHandle hMenu,
                bool fByPos,
                uint gmdiFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647977.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetMenuInfo(
                MenuHandle hMenu,
                ref MENUINFO lpcmi);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647978.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern int GetMenuItemCount(
                MenuHandle hMenu);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647979.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern uint GetMenuItemID(
                MenuHandle hMenu,
                int nPos);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647980.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool GetMenuItemInfoW(
                MenuHandle hMenu,
                uint uItem,
                bool fByPos,
                ref MENUITEMINFO lpmii);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647981.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetMenuItemRect(
                WindowHandle hWnd,
                MenuHandle hMenu,
                uint uItem,
                out RECT lprcItem);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647982.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern MenuFlags GetMenuState(
                MenuHandle hMenu,
                uint uId,
                MenuFlags uFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647983.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern int GetMenuString(
                MenuHandle hMenu,
                uint uIDItem,
                SafeHandle lpString,
                int nMaxCount,
                MenuFlags uFlag);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647984.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern SharedMenuHandle GetSubMenu(
                MenuHandle hMenu,
                int nPos);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647985.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern SharedMenuHandle GetSystemMenu(
                WindowHandle hWnd,
                bool bRevert);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647986.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool HiliteMenuItem(
                WindowHandle hwnd,
                MenuHandle hmenu,
                uint uItemHitite,
                MenuFlags uHilite);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647988.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool InsertMenuItemW(
                MenuHandle hMenu,
                uint uItem,
                bool fByPosition,
                [In] ref MENUITEMINFO lpmii);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647989.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool IsMenu(
                IntPtr hMenu);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647990.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern MenuHandle LoadMenuW(
                ModuleInstance hInstance,
                IntPtr lpMenuName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647991.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern MenuHandle LoadMenuIndirectW(
                SafeHandle lpMenuTemplate);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647992.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern int MenuItemFromPoint(
                WindowHandle hWnd,
                MenuHandle hMenu,
                POINT ptScreen);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647994.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool RemoveMenu(
                MenuHandle hMenu,
                uint uPosition,
                MenuFlags uFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647995.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetMenu(
                WindowHandle hWnd,
                MenuHandle hMenu);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647996.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetMenuDefaultItem(
                MenuHandle hMenu,
                uint uItem,
                bool fByPos);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647997.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetMenuInfo(
                MenuHandle hmenu,
                [In] ref MENUINFO lpcmi);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647998.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetMenuItemBitmaps(
                MenuHandle hMenu,
                uint uPosition,
                MenuFlags uFlags,
                BitmapHandle hBitmapUnchecked,
                BitmapHandle hBitmapChecked);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648001.aspx
            [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool SetMenuItemInfoW(
                MenuHandle hMenu,
                uint uItem,
                bool fByPosition,
                [In] ref MENUITEMINFO lpmii);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648003.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern BOOL TrackPopupMenuEx(
                WindowHandle hmenu,
                PopupMenuOptions fuFlags,
                int x,
                int y,
                WindowHandle hwnd,
                TPMPARAMS* lptpm);
        }
    }
}
