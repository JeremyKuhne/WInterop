// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Windows;

namespace WInterop.Gdi.Native
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        // https://msdn.microsoft.com/en-us/library/dd144877.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int GetDeviceCaps(
            HDC hdc,
            DeviceCapability nIndex);

        // https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getwindowdc
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern HDC GetWindowDC(
            WindowHandle hWnd);

        // https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getdc
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern HDC GetDC(
            WindowHandle hWnd);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/nf-wingdi-createdcw
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public unsafe static extern HDC CreateDCW(
            string pwszDriver,
            string pwszDevice,
            string pszPort,
            DEVMODE* pdm);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/nf-wingdi-createicw
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public unsafe static extern HDC CreateICW(
            string pwszDriver,
            string pwszDevice,
            string pszPort,
            DEVMODE* pdm);

        // https://msdn.microsoft.com/en-us/library/dd183489.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HDC CreateCompatibleDC(
            HDC hdc);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/nf-wingdi-deletedc
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool DeleteDC(
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/dd162920.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool ReleaseDC(
            WindowHandle hWnd,
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/dd183399.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool CancelDC(
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/dd183533.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HGDIOBJ GetStockObject(
            StockObject stockObject);

        // https://msdn.microsoft.com/en-us/library/dd183518.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HBRUSH CreateSolidBrush(
            COLORREF crColor);

        // https://msdn.microsoft.com/en-us/library/dd144869.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HGDIOBJ GetCurrentObject(
            HDC hdc,
            ObjectType uObjectType);

        // https://msdn.microsoft.com/en-us/library/dd144904.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public unsafe static extern int GetObjectW(
            HGDIOBJ hgdiobj,
            int cbBuffer,
            void* lpvObject);

        // https://msdn.microsoft.com/en-us/library/dd162957.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HGDIOBJ SelectObject(
            HDC hdc,
            HGDIOBJ h);

        // https://msdn.microsoft.com/en-us/library/dd183539.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool DeleteObject(
            HGDIOBJ hObject);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/dd162609.aspx
        [DllImport(Libraries.User32, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern bool EnumDisplayDevicesW(
            string lpDevice,
            uint iDevNum,
            ref DISPLAY_DEVICE lpDisplayDevice,
            uint dwFlags);

        // https://msdn.microsoft.com/en-us/library/dd162611.aspx
        [DllImport(Libraries.User32, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern bool EnumDisplaySettingsW(
            string lpszDeviceName,
            uint iModeNum,
            ref DEVMODE lpDevMode);

        // https://msdn.microsoft.com/en-us/library/dd183485.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HBITMAP CreateBitmap(
            int nWidth,
            int nHeight,
            uint cPlanes,
            uint cBitsPerPel,
            IntPtr lpvBits);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/nf-wingdi-createcompatiblebitmap
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HBITMAP CreateCompatibleBitmap(
            HDC hdc,
            int cx,
            int cy);

        // https://msdn.microsoft.com/en-us/library/dd144905.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern ObjectType GetObjectType(
            HGDIOBJ h);

        // https://msdn.microsoft.com/en-us/library/dd144925.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HGDIOBJ GetStockObject(
            int fnObject);

        // https://msdn.microsoft.com/en-us/library/dd145167.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool UpdateWindow(
            WindowHandle hWnd);

        // https://msdn.microsoft.com/en-us/library/dd145194.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool ValidateRect(
            WindowHandle hWnd,
            [In] ref RECT lpRect);

        // https://msdn.microsoft.com/en-us/library/dd183362.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern HDC BeginPaint(
            WindowHandle hwnd,
            out PAINTSTRUCT lpPaint);

        // https://msdn.microsoft.com/en-us/library/dd145002.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public unsafe static extern bool InvalidateRect(
            WindowHandle hWnd,
            RECT* lpRect,
            bool bErase);

        // https://msdn.microsoft.com/en-us/library/dd162598.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool EndPaint(
            WindowHandle hwnd,
            in PAINTSTRUCT lpPaint);

        // https://msdn.microsoft.com/en-us/library/dd162498.aspx
        [DllImport(Libraries.User32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int DrawTextW(
            HDC hDC,
            ref char lpchText,
            int nCount,
            ref RECT lpRect,
            TextFormat uFormat);

        // https://msdn.microsoft.com/en-us/library/dd145133.aspx
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern bool TextOutW(
            HDC hdc,
            int nXStart,
            int nYStart,
            ref char lpString,
            int cchString);

        // https://msdn.microsoft.com/en-us/library/dd145129.aspx
        [DllImport(Libraries.User32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int TabbedTextOutW(
            HDC hdc,
            int X,
            int Y,
            string lpString,
            int nCount,
            int nTabPositions,
            int[] lpnTabStopPositions,
            int nTabOrigin);

        // https://msdn.microsoft.com/en-us/library/dd144941.aspx
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern bool GetTextMetricsW(
            HDC hdc,
            out TEXTMETRIC lptm);

        // https://msdn.microsoft.com/en-us/library/dd145091.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern TextAlignment SetTextAlign(
            HDC hdc,
            TextAlignment fMode);

        // https://msdn.microsoft.com/en-us/library/dd144932.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern TextAlignment GetTextAlign(
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/dd145094.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool SetTextJustification(
            HDC hdc,
            int nBreakExtra,
            int nBreakCount);

        // https://msdn.microsoft.com/en-us/library/dd145092.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int SetTextCharacterExtra(
            HDC hdc,
            int nCharExtra);

        // https://msdn.microsoft.com/en-us/library/dd144933.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int GetTextCharacterExtra(
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/dd145093.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF SetTextColor(
            HDC hdc,
            COLORREF crColor);

        // https://msdn.microsoft.com/en-us/library/dd144934.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF GetTextColor(
            HDC hdc);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/nf-wingdi-createfontw
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern HFONT CreateFontW(
            int nHeight,
            int nWidth,
            int nEscapement,
            int nOrientation,
            FontWeight fnWeight,
            bool fdwItalic,
            bool fdwUnderline,
            bool fdwStrikeOut,
            uint fdwCharSet,
            uint fdwOutputPrecision,
            uint fdwClipPrecision,
            uint fdwQuality,
            uint fdwPitchAndFamily,
            string lpszFace);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int EnumFontFamiliesExW(
            HDC hdc,
            ref LOGFONT lpLogfont,
            EnumFontFamExProc lpEnumFonFamExProc,
            LPARAM lParam,
            uint dwFlags);

        // https://msdn.microsoft.com/en-us/library/dd183354.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool AngleArc(
            HDC hdc,
            int X,
            int Y,
            uint dwRadius,
            float eStartAngle,
            float eSweepAngle);

        // https://msdn.microsoft.com/en-us/library/dd183357.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool Arc(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nXStartArc,
            int nYStartArc,
            int nXEndArc,
            int nYEndArc);

        // https://msdn.microsoft.com/en-us/library/dd183358.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool ArcTo(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nXRadial1,
            int nYRadial1,
            int nXRadial2,
            int nYRadial2);

        // https://msdn.microsoft.com/en-us/library/dd144848.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern ArcDirection GetArcDirection(
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/dd144909.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF GetPixel(
            HDC hdc,
            int nXPos,
            int nYPos);

        // https://msdn.microsoft.com/en-us/library/dd145078.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF SetPixel(
            HDC hdc,
            int X,
            int Y,
            COLORREF crColor);

        // https://msdn.microsoft.com/en-us/library/dd145079.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool SetPixelV(
            HDC hdc,
            int X,
            int Y,
            COLORREF crColor);

        // https://msdn.microsoft.com/en-us/library/dd145029.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool LineTo(
            HDC hdc,
            int nXEnd,
            int nYEnd);

        // https://msdn.microsoft.com/en-us/library/dd145069.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public unsafe static extern bool MoveToEx(
            HDC hdc,
            int X,
            int Y,
            Point* lpPoint);

        // https://msdn.microsoft.com/en-us/library/dd144910.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern PolyFillMode GetPolyFillMode(
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/dd145080.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern PolyFillMode SetPolyFillMode(
            HDC hdc,
            PolyFillMode iPolyFillMode);

        // https://msdn.microsoft.com/en-us/library/dd162811.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PolyBezier(
            HDC hdc,
            ref Point lppt,
            uint cPoints);

        // https://msdn.microsoft.com/en-us/library/dd162812.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PolyBezierTo(
            HDC hdc,
            ref Point lppt,
            uint cCount);

        // https://msdn.microsoft.com/en-us/library/dd162813.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PolyDraw(
            HDC hdc,
            ref Point lppt,
            ref PointType lpbTypes,
            int cCount);

        // https://msdn.microsoft.com/en-us/library/dd162815.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool Polyline(
            HDC hdc,
            ref Point lppt,
            int cPoints);

        // https://msdn.microsoft.com/en-us/library/dd162816.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PolylineTo(
            HDC hdc,
            ref Point lppt,
            uint cCount);

        // https://msdn.microsoft.com/en-us/library/dd162819.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PolyPolyline(
            HDC hdc,
            ref Point lppt,
            uint[] lpdwPolyPoints,
            uint cCount);

        // https://msdn.microsoft.com/en-us/library/dd162961.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern ArcDirection SetArcDirection(
            HDC hdc,
            ArcDirection ArcDirection);

        // https://msdn.microsoft.com/en-us/library/dd183428.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool Chord(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nXRadial1,
            int nYRadial1,
            int nXRadial2,
            int nYRadial2);

        // https://msdn.microsoft.com/en-us/library/dd162510(v=vs.85).aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool Ellipse(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/en-us/library/dd162719.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool FillRect(
            HDC hDC,
            [In] ref RECT lprc,
            HBRUSH hbr);

        // https://msdn.microsoft.com/en-us/library/dd144838.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool FrameRect(
            HDC hDC,
            [In] ref RECT lprc,
            HBRUSH hbr);

        // https://msdn.microsoft.com/en-us/library/dd145007.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool InvertRect(
            HDC hDC,
            [In] ref RECT lprc);

        // https://msdn.microsoft.com/en-us/library/dd162479.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool DrawFocusRect(
            HDC hDC,
            [In] ref RECT lprc);

        // https://msdn.microsoft.com/en-us/library/dd162799.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool Pie(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nXRadial1,
            int nYRadial1,
            int nXRadial2,
            int nYRadial2);

        // https://msdn.microsoft.com/en-us/library/dd162814.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool Polygon(
            HDC hdc,
            ref Point lpPoints,
            int nCount);

        // https://msdn.microsoft.com/en-us/library/dd162818.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PolyPolygon(
            HDC hdc,
            ref Point lpPoints,
            int[] lpPolyCounts,
            int nCount);

        // https://msdn.microsoft.com/en-us/library/dd162898.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool Rectangle(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/en-us/library/dd162944.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool RoundRect(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidth,
            int nHeight);

        // https://msdn.microsoft.com/en-us/library/dd183434.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool ClientToScreen(
            WindowHandle hWnd,
            ref Point lpPoint);

        // https://msdn.microsoft.com/en-us/library/dd183466.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool CombineTransform(
            out XFORM lpxformResult,
            [In] ref XFORM lpxform1,
            [In] ref XFORM lpxform2);

        // https://msdn.microsoft.com/en-us/library/dd162474.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public unsafe static extern bool DPtoLP(
            HDC hdc,
            ref Point lpPoints,
            int nCount);

        // https://msdn.microsoft.com/en-us/library/dd144870.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GetCurrentPositionEx(
            HDC hdc,
            out Point lpPoint);

        // https://msdn.microsoft.com/en-us/library/dn376360.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool GetDisplayAutoRotationPreferences(
            out ORIENTATION_PREFERENCE pOrientation);

        // https://msdn.microsoft.com/en-us/library/dd144892.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern GraphicsMode GetGraphicsMode(
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/dd144897.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern MapMode GetMapMode(
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/dd144945.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GetViewportExtEx(
            HDC hdc,
            out SIZE lpSize);

        // https://msdn.microsoft.com/en-us/library/dd144946.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GetViewportOrgEx(
            HDC hdc,
            out Point lpPoint);

        // https://msdn.microsoft.com/en-us/library/dd144948.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GetWindowExtEx(
            HDC hdc,
            out SIZE lpSize);

        // https://msdn.microsoft.com/en-us/library/dd144949.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GetWindowOrgEx(
            HDC hdc,
            out Point lpPoint);

        // https://msdn.microsoft.com/en-us/library/dd144953.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GetWorldTransform(
            HDC hdc,
            out XFORM lpXform);

        // https://msdn.microsoft.com/en-us/library/dd145042.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool LPtoDP(
            HDC hdc,
            ref Point lpPoints,
            int nCount);

        // https://msdn.microsoft.com/en-us/library/dd145046.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern int MapWindowPoints(
            WindowHandle hWndFrom,
            WindowHandle hWndTo,
            ref Point lpPoints,
            uint cPoints);

        // https://msdn.microsoft.com/en-us/library/dd145060.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool ModifyWorldTransform(
            HDC hdc,
            [In] ref XFORM lpXform,
            WorldTransformMode iMode);

        // https://msdn.microsoft.com/en-us/library/dd162748.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public unsafe static extern bool OffsetViewportOrgEx(
            HDC hdc,
            int nXOffset,
            int nYOffset,
            Point* lpPoint);

        // https://msdn.microsoft.com/en-us/library/dd162749.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public unsafe static extern bool OffsetWindowOrgEx(
            HDC hdc,
            int nXOffset,
            int nYOffset,
            Point* lpPoint);

        // https://msdn.microsoft.com/en-us/library/dd162947.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool ScaleViewportExtEx(
            HDC hdc,
            int Xnum,
            int Xdenom,
            int Ynum,
            int Ydenom,
            out SIZE lpSize);

        // https://msdn.microsoft.com/en-us/library/dd162948.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool ScaleWindowExtEx(
            HDC hdc,
            int Xnum,
            int Xdenom,
            int Ynum,
            int Ydenom,
            out SIZE lpSize);

        // https://msdn.microsoft.com/en-us/library/dd162952.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool ScreenToClient(
            WindowHandle hWnd,
            ref Point lpPoint);

        // https://msdn.microsoft.com/en-us/library/dn376361.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool SetDisplayAutoRotationPreferences(
            ORIENTATION_PREFERENCE orientation);

        // https://msdn.microsoft.com/en-us/library/dd162977.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool SetGraphicsMode(
            HDC hdc,
            GraphicsMode iMode);

        // https://msdn.microsoft.com/en-us/library/dd162980.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern MapMode SetMapMode(
            HDC hdc,
            MapMode fnMapMode);

        // https://msdn.microsoft.com/en-us/library/dd145098.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public unsafe static extern bool SetViewportExtEx(
            HDC hdc,
            int nXExtent,
            int nYExtent,
            SIZE* lpSize);

        // https://msdn.microsoft.com/en-us/library/dd145099.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public unsafe static extern bool SetViewportOrgEx(
            HDC hdc,
            int nXExtent,
            int nYExtent,
            Point* lpPoint);

        // https://msdn.microsoft.com/en-us/library/dd145100.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public unsafe static extern bool SetWindowExtEx(
            HDC hdc,
            int nXExtent,
            int nYExtent,
            SIZE* lpSize);

        // https://msdn.microsoft.com/en-us/library/dd145101.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public unsafe static extern bool SetWindowOrgEx(
            HDC hdc,
            int nXExtent,
            int nYExtent,
            Point* lpPoint);

        // https://msdn.microsoft.com/en-us/library/dd145104.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool SetWorldTransform(
            HDC hdc,
            [In] ref XFORM lpXform);

        // https://msdn.microsoft.com/en-us/library/dd183481.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool CopyRect(
            out RECT lprcDst,
            [In] ref RECT lprcSrc);

        // https://msdn.microsoft.com/en-us/library/dd162699.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool EqualRect(
            [In] ref RECT lprc1,
            [In] ref RECT lprc2);

        // https://msdn.microsoft.com/en-us/library/dd144994.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool InflateRect(
            ref RECT lprc,
            int dx,
            int dy);

        // https://msdn.microsoft.com/en-us/library/dd145001.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool IntersectRect(
            out RECT lprcDst,
            [In] ref RECT lprcSrc1,
            [In] ref RECT lprcSrc2);

        // https://msdn.microsoft.com/en-us/library/dd145017.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool IsRectEmpty(
            [In] ref RECT lprc);

        // https://msdn.microsoft.com/en-us/library/dd162746.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool OffsetRect(
            ref RECT lprc,
            int dx,
            int dy);

        // https://msdn.microsoft.com/en-us/library/dd162882.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool PtInRect(
            [In] ref RECT lprc,
            Point pt);

        // https://msdn.microsoft.com/en-us/library/dd145085.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool SetRect(
            out RECT lprc,
            int xLeft,
            int yTop,
            int xRight,
            int yBottom);

        // https://msdn.microsoft.com/en-us/library/dd145086.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool SetRectEmpty(
            out RECT lprc);

        // https://msdn.microsoft.com/en-us/library/dd145125.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool SubtractRect(
            out RECT lprcDst,
            [In] ref RECT lprcSrc1,
            [In] ref RECT lprcSrc2);

        // https://msdn.microsoft.com/en-us/library/dd145163.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool UnionRect(
            out RECT lprcDst,
            [In] ref RECT lprcSrc1,
            [In] ref RECT lprcSrc2);

        // https://msdn.microsoft.com/en-us/library/dd183465.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType CombineRgn(
            HRGN hrgnDest,
            HRGN hrgnSrc1,
            HRGN hrgnSrc2,
            CombineRegionMode fnCombineMode);

        // https://msdn.microsoft.com/en-us/library/dd183496.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HRGN CreateEllipticRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/en-us/library/dd183497.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HRGN CreateEllipticRgnIndirect(
            [In] ref RECT lprc);

        // https://msdn.microsoft.com/en-us/library/dd183511.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public unsafe static extern HRGN CreatePolygonRgn(
            Point* lppt,
            int cPoints,
            PolyFillMode fnPolyFillMode);

        // https://msdn.microsoft.com/en-us/library/dd183512.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public unsafe static extern HRGN CreatePolyPolygonRgn(
            Point* lppt,
            int[] lpPolyCounts,
            int nCount,
            PolyFillMode fnPolyFillMode);

        // https://msdn.microsoft.com/en-us/library/dd183514.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HRGN CreateRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/en-us/library/dd183515.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HRGN CreateRectRgnIndirect(
            [In] ref RECT lprc);

        // https://msdn.microsoft.com/en-us/library/dd183516.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HRGN CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        // https://msdn.microsoft.com/en-us/library/dd162700.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool EqualRgn(
            HRGN hSrcRgn1,
            HRGN hSrcRgn2);

        // https://msdn.microsoft.com/en-us/library/dd162706.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public unsafe static extern HRGN ExtCreateRegion(
            [In] ref XFORM lpXform,
            uint nCount,
            RGNDATA* lpRgnData);

        // https://msdn.microsoft.com/en-us/library/dd162720.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool FillRgn(
            HDC hdc,
            HRGN hrgn,
            HBRUSH hbr);

        // https://msdn.microsoft.com/en-us/library/dd144839.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool FrameRgn(
            HDC hdc,
            HRGN hrgn,
            HBRUSH hbr,
            int nWidth,
            int nHeight);

        // https://msdn.microsoft.com/en-us/library/dd144920.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern uint GetRegionData(
            HRGN hRgn,
            uint dwCount,
            IntPtr lpRgnData);

        // https://msdn.microsoft.com/en-us/library/dd144921.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType GetRgnBox(
            HRGN hrgn,
            out RECT lprc);

        // https://msdn.microsoft.com/en-us/library/dd145008.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool InvertRgn(
            HDC hdc,
            HRGN hrgn);

        // https://msdn.microsoft.com/en-us/library/dd162747.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType OffsetRgn(
            HRGN hrgn,
            int nXOffset,
            int nYOffset);

        // https://msdn.microsoft.com/en-us/library/dd162767.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PaintRgn(
            HDC hdc,
            HRGN hrgn);

        // https://msdn.microsoft.com/en-us/library/dd162883.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PtInRegion(
            HRGN hrgn,
            int X,
            int Y);

        // https://msdn.microsoft.com/en-us/library/dd162906.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool RectInRegion(
            HRGN hrgn,
            [In] ref RECT lprc);

        // https://msdn.microsoft.com/en-us/library/dd145087.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool SetRectRgn(
            HRGN hrgn,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/en-us/library/dd162702.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType ExcludeClipRect(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/en-us/library/dd162712.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType ExtSelectClipRgn(
            HDC hdc,
            HRGN hrgn,
            CombineRegionMode fnMode);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/nf-wingdi-getclipbox
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType GetClipBox(
            HDC hdc,
            out RECT lprect);

        // https://msdn.microsoft.com/en-us/library/dd144866.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int GetClipRgn(
            HDC hdc,
            HRGN hrgn);

        // https://msdn.microsoft.com/en-us/library/dd144899.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int GetMetaRgn(
            HDC hdc,
            HRGN hrgn);

        // https://msdn.microsoft.com/en-us/library/dd144918.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int GetRandomRgn(
            HDC hdc,
            HRGN hrgn,
            int iNum);

        // https://msdn.microsoft.com/en-us/library/dd144998.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType IntersectClipRect(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/en-us/library/dd162745.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType OffsetClipRgn(
            HDC hdc,
            int nXOffset,
            int nYOffset);

        // https://msdn.microsoft.com/en-us/library/dd162890.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern BOOL PtVisible(
            HDC hdc,
            int X,
            int Y);

        // https://msdn.microsoft.com/en-us/library/dd162908.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern BOOL RectVisible(
            HDC hdc,
            [In] ref RECT lprc);

        // https://msdn.microsoft.com/en-us/library/dd162954(v=vs.85).aspx
        [DllImport(Libraries.Gdi32, SetLastError = true, ExactSpelling = true)]
        public static extern bool SelectClipPath(
            HDC hdc,
            CombineRegionMode iMode);

        // https://msdn.microsoft.com/en-us/library/dd162955.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType SelectClipRgn(
            HDC hdc,
            HRGN hrgn);

        // https://msdn.microsoft.com/en-us/library/dd145075.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType SetMetaRgn(
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/dd144853.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern BackgroundMode GetBkMode(
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/dd162965.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern BackgroundMode SetBkMode(
            HDC hdc,
            BackgroundMode iBkMode);

        // https://msdn.microsoft.com/en-us/library/dd144922.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern PenMixMode GetROP2(
            HDC hdc);

        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern PenMixMode SetROP2(
            HDC hdc,
            PenMixMode fnDrawMode);

        // https://msdn.microsoft.com/en-us/library/dd144852.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF GetBkColor(
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/dd162964.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF SetBkColor(
            HDC hdc,
            COLORREF crColor);

        // https://msdn.microsoft.com/en-us/library/dd162969.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF SetDCBrushColor(
            HDC hdc,
            COLORREF crColor);

        // https://msdn.microsoft.com/en-us/library/dd144872.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF GetDCBrushColor(
            HDC hdc);

        // https://msdn.microsoft.com/en-us/library/dd144927.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern HBRUSH GetSysColorBrush(
            SystemColor nIndex);

        // https://msdn.microsoft.com/en-us/library/dd183509.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HPEN CreatePen(
            PenStyle fnPenStyle,
            int nWidth,
            COLORREF crColor);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/nf-wingdi-extcreatepen
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HPEN ExtCreatePen(
            uint iPenStyle,
            uint cWidth,
            in LOGBRUSH plbrush,
            uint cStyle,
            uint[] pstyle);

        // https://msdn.microsoft.com/en-us/library/dd144844.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GdiFlush();

        // https://msdn.microsoft.com/en-us/library/dd144845.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern uint GdiGetBatchLimit();

        // https://msdn.microsoft.com/en-us/library/dd144846.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern uint GdiSetBatchLimit(
            uint dwLimit);

        // https://msdn.microsoft.com/en-us/library/dd183370.aspx
        [DllImport(Libraries.Gdi32, SetLastError = true, ExactSpelling = true)]
        public static extern bool BitBlt(
            HDC hdcDest,
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            HDC hdcSrc,
            int nXSrc,
            int nYSrc,
            RasterOperation dwRop);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/nf-wingdi-setstretchbltmode
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern StretchMode SetStretchBltMode(
            HDC hdc,
            StretchMode mode);

        // https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/nf-wingdi-stretchblt
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool StretchBlt(
            HDC hdcDest,
            int xDest,
            int yDest,
            int wDest,
            int hDest,
            HDC hdcSrc,
            int xSrc,
            int ySrc,
            int wSrc,
            int hSrc,
            RasterOperation dwRop);
    }
}
