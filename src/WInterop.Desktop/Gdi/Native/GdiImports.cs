// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Windows;

namespace WInterop.Gdi.Native
{
    /// <summary>
    ///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class GdiImports
    {
        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getdevicecaps
        [SuppressGCTransition]
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int GetDeviceCaps(
            HDC hdc,
            DeviceCapability nIndex);

        // https://docs.microsoft.com/windows/desktop/api/winuser/nf-winuser-getwindowdc
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern HDC GetWindowDC(
            WindowHandle hWnd);

        // https://docs.microsoft.com/windows/desktop/api/winuser/nf-winuser-getdc
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern HDC GetDC(
            WindowHandle hWnd);

        // https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-createdcw
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern unsafe HDC CreateDCW(
            string pwszDriver,
            string? pwszDevice,
            string? pszPort,
            DeviceMode* pdm);

        // https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-createicw
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern unsafe HDC CreateICW(
            string pwszDriver,
            string pwszDevice,
            string? pszPort,
            DeviceMode* pdm);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-createcompatibledc
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HDC CreateCompatibleDC(
            HDC hdc);

        // https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-deletedc
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool DeleteDC(
            HDC hdc);

        // https://msdn.microsoft.com/library/windows/desktop/dd162920.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool ReleaseDC(
            WindowHandle hWnd,
            HDC hdc);

        // https://msdn.microsoft.com/library/dd183399.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool CancelDC(
            HDC hdc);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-savedc
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int SaveDC(
            HDC hdc);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-restoredc
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int RestoreDC(
            HDC hdc,
            int nSavedDC);

        // https://msdn.microsoft.com/library/dd183533.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HGDIOBJ GetStockObject(
            StockObject stockObject);

        // https://msdn.microsoft.com/library/dd183518.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HBRUSH CreateSolidBrush(
            COLORREF crColor);

        // https://msdn.microsoft.com/library/dd144869.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HGDIOBJ GetCurrentObject(
            HDC hdc,
            ObjectType uObjectType);

        // https://msdn.microsoft.com/library/dd144904.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe int GetObjectW(
            HGDIOBJ hgdiobj,
            int cbBuffer,
            void* lpvObject);

        // https://msdn.microsoft.com/library/dd162957.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HGDIOBJ SelectObject(
            HDC hdc,
            HGDIOBJ h);

        // https://msdn.microsoft.com/library/dd183539.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool DeleteObject(
            HGDIOBJ hObject);

        // https://msdn.microsoft.com/library/windows/desktop/dd162609.aspx
        [DllImport(Libraries.User32, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern bool EnumDisplayDevicesW(
            string lpDevice,
            uint iDevNum,
            ref DisplayDevice lpDisplayDevice,
            uint dwFlags);

        // https://msdn.microsoft.com/library/dd162611.aspx
        [DllImport(Libraries.User32, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern bool EnumDisplaySettingsW(
            string lpszDeviceName,
            uint iModeNum,
            ref DeviceMode lpDevMode);

        // https://msdn.microsoft.com/library/dd183485.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HBITMAP CreateBitmap(
            int nWidth,
            int nHeight,
            uint cPlanes,
            uint cBitsPerPel,
            IntPtr lpvBits);

        // https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-createcompatiblebitmap
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HBITMAP CreateCompatibleBitmap(
            HDC hdc,
            int cx,
            int cy);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getobjecttype
        [SuppressGCTransition]
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern ObjectType GetObjectType(
            HGDIOBJ h);

        // https://msdn.microsoft.com/library/dd144925.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HGDIOBJ GetStockObject(
            int fnObject);

        // https://msdn.microsoft.com/library/dd145167.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool UpdateWindow(
            WindowHandle hWnd);

        // https://msdn.microsoft.com/library/dd145194.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern unsafe bool ValidateRect(
            WindowHandle hWnd,
            Rect* lpRect);

        // https://msdn.microsoft.com/library/dd183362.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern HDC BeginPaint(
            WindowHandle hwnd,
            ref PAINTSTRUCT lpPaint);

        // https://msdn.microsoft.com/library/dd145002.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern unsafe bool InvalidateRect(
            WindowHandle hWnd,
            Rect* lpRect,
            bool bErase);

        // https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-endpaint
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool EndPaint(
            WindowHandle hwnd,
            in PAINTSTRUCT lpPaint);

        // https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-drawtextw
        [DllImport(Libraries.User32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static unsafe extern int DrawTextW(
            HDC hDC,
            char* lpchText,
            int nCount,
            ref Rect lpRect,
            TextFormat uFormat);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-textoutw
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern bool TextOutW(
            HDC hdc,
            int nXStart,
            int nYStart,
            ref char lpString,
            int cchString);

        // https://msdn.microsoft.com/library/dd145129.aspx
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

        // https://msdn.microsoft.com/library/dd144941.aspx
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern bool GetTextMetricsW(
            HDC hdc,
            out TextMetrics lptm);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-settextalign
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern TextAlignment SetTextAlign(
            HDC hdc,
            TextAlignment fMode);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-gettextalign
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern TextAlignment GetTextAlign(
            HDC hdc);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-settextjustification
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool SetTextJustification(
            HDC hdc,
            int nBreakExtra,
            int nBreakCount);

        // https://msdn.microsoft.com/library/dd145092.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int SetTextCharacterExtra(
            HDC hdc,
            int nCharExtra);

        // https://msdn.microsoft.com/library/dd144933.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int GetTextCharacterExtra(
            HDC hdc);

        // https://msdn.microsoft.com/library/dd145093.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF SetTextColor(
            HDC hdc,
            COLORREF crColor);

        // https://msdn.microsoft.com/library/dd144934.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF GetTextColor(
            HDC hdc);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-createfontw
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
            string? lpszFace);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int EnumFontFamiliesExW(
            HDC hdc,
            ref LogicalFont lpLogfont,
            EnumerateFontFamilyExtendedProcedure lpEnumFonFamExProc,
            LParam lParam,
            uint dwFlags);

        // https://msdn.microsoft.com/library/dd183354.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool AngleArc(
            HDC hdc,
            int X,
            int Y,
            uint dwRadius,
            float eStartAngle,
            float eSweepAngle);

        // https://msdn.microsoft.com/library/dd183357.aspx
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

        // https://msdn.microsoft.com/library/dd183358.aspx
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

        // https://msdn.microsoft.com/library/dd144848.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern ArcDirection GetArcDirection(
            HDC hdc);

        // https://msdn.microsoft.com/library/dd144909.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF GetPixel(
            HDC hdc,
            int nXPos,
            int nYPos);

        // https://msdn.microsoft.com/library/dd145078.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF SetPixel(
            HDC hdc,
            int X,
            int Y,
            COLORREF crColor);

        // https://msdn.microsoft.com/library/dd145079.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool SetPixelV(
            HDC hdc,
            int X,
            int Y,
            COLORREF crColor);

        // https://msdn.microsoft.com/library/dd145029.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool LineTo(
            HDC hdc,
            int nXEnd,
            int nYEnd);

        // https://msdn.microsoft.com/library/dd145069.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe bool MoveToEx(
            HDC hdc,
            int X,
            int Y,
            Point* lpPoint);

        // https://msdn.microsoft.com/library/dd144910.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern PolyFillMode GetPolyFillMode(
            HDC hdc);

        // https://msdn.microsoft.com/library/dd145080.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern PolyFillMode SetPolyFillMode(
            HDC hdc,
            PolyFillMode iPolyFillMode);

        // https://msdn.microsoft.com/library/dd162811.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PolyBezier(
            HDC hdc,
            ref Point lppt,
            uint cPoints);

        // https://msdn.microsoft.com/library/dd162812.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PolyBezierTo(
            HDC hdc,
            ref Point lppt,
            uint cCount);

        // https://msdn.microsoft.com/library/dd162813.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PolyDraw(
            HDC hdc,
            ref Point lppt,
            ref PointType lpbTypes,
            int cCount);

        // https://msdn.microsoft.com/library/dd162815.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool Polyline(
            HDC hdc,
            ref Point lppt,
            int cPoints);

        // https://msdn.microsoft.com/library/dd162816.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PolylineTo(
            HDC hdc,
            ref Point lppt,
            uint cCount);

        // https://msdn.microsoft.com/library/dd162819.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PolyPolyline(
            HDC hdc,
            ref Point lppt,
            uint[] lpdwPolyPoints,
            uint cCount);

        // https://msdn.microsoft.com/library/dd162961.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern ArcDirection SetArcDirection(
            HDC hdc,
            ArcDirection ArcDirection);

        // https://msdn.microsoft.com/library/dd183428.aspx
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

        // https://msdn.microsoft.com/library/dd162510(v=vs.85).aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool Ellipse(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/library/dd162719.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool FillRect(
            HDC hDC,
            [In] ref Rect lprc,
            HBRUSH hbr);

        // https://msdn.microsoft.com/library/dd144838.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool FrameRect(
            HDC hDC,
            [In] ref Rect lprc,
            HBRUSH hbr);

        // https://msdn.microsoft.com/library/dd145007.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool InvertRect(
            HDC hDC,
            [In] ref Rect lprc);

        // https://msdn.microsoft.com/library/dd162479.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool DrawFocusRect(
            HDC hDC,
            [In] ref Rect lprc);

        // https://msdn.microsoft.com/library/dd162799.aspx
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

        // https://msdn.microsoft.com/library/dd162814.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool Polygon(
            HDC hdc,
            ref Point lpPoints,
            int nCount);

        // https://msdn.microsoft.com/library/dd162818.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PolyPolygon(
            HDC hdc,
            ref Point lpPoints,
            int[] lpPolyCounts,
            int nCount);

        // https://msdn.microsoft.com/library/dd162898.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool Rectangle(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/library/dd162944.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool RoundRect(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidth,
            int nHeight);

        // https://msdn.microsoft.com/library/dd183434.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool ClientToScreen(
            WindowHandle hWnd,
            ref Point lpPoint);

        // https://msdn.microsoft.com/library/dd183466.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool CombineTransform(
            out Matrix3x2 lpxformResult,
            [In] ref Matrix3x2 lpxform1,
            [In] ref Matrix3x2 lpxform2);

        // https://msdn.microsoft.com/library/dd162474.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe bool DPtoLP(
            HDC hdc,
            ref Point lpPoints,
            int nCount);

        // https://msdn.microsoft.com/library/dd144870.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GetCurrentPositionEx(
            HDC hdc,
            out Point lpPoint);

        // https://msdn.microsoft.com/library/dn376360.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool GetDisplayAutoRotationPreferences(
            out OrientationPreference pOrientation);

        // https://msdn.microsoft.com/library/dd144892.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern GraphicsMode GetGraphicsMode(
            HDC hdc);

        // https://msdn.microsoft.com/library/dd144897.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern MappingMode GetMapMode(
            HDC hdc);

        // https://msdn.microsoft.com/library/dd144945.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GetViewportExtEx(
            HDC hdc,
            out Size lpSize);

        // https://msdn.microsoft.com/library/dd144946.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GetViewportOrgEx(
            HDC hdc,
            out Point lpPoint);

        // https://msdn.microsoft.com/library/dd144948.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GetWindowExtEx(
            HDC hdc,
            out Size lpSize);

        // https://msdn.microsoft.com/library/dd144949.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GetWindowOrgEx(
            HDC hdc,
            out Point lpPoint);

        // https://msdn.microsoft.com/library/dd144953.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GetWorldTransform(
            HDC hdc,
            out Matrix3x2 lpXform);

        // https://msdn.microsoft.com/library/dd145042.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool LPtoDP(
            HDC hdc,
            ref Point lpPoints,
            int nCount);

        // https://msdn.microsoft.com/library/dd145046.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern int MapWindowPoints(
            WindowHandle hWndFrom,
            WindowHandle hWndTo,
            ref Point lpPoints,
            uint cPoints);

        // https://msdn.microsoft.com/library/dd145060.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool ModifyWorldTransform(
            HDC hdc,
            [In] ref Matrix3x2 lpXform,
            WorldTransformMode iMode);

        // https://msdn.microsoft.com/library/dd162748.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe bool OffsetViewportOrgEx(
            HDC hdc,
            int nXOffset,
            int nYOffset,
            Point* lpPoint);

        // https://msdn.microsoft.com/library/dd162749.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe bool OffsetWindowOrgEx(
            HDC hdc,
            int nXOffset,
            int nYOffset,
            Point* lpPoint);

        // https://msdn.microsoft.com/library/dd162947.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool ScaleViewportExtEx(
            HDC hdc,
            int Xnum,
            int Xdenom,
            int Ynum,
            int Ydenom,
            out Size lpSize);

        // https://msdn.microsoft.com/library/dd162948.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool ScaleWindowExtEx(
            HDC hdc,
            int Xnum,
            int Xdenom,
            int Ynum,
            int Ydenom,
            out Size lpSize);

        // https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-screentoclient
        [SuppressGCTransition]
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool ScreenToClient(
            WindowHandle hWnd,
            ref Point lpPoint);

        // https://msdn.microsoft.com/library/dn376361.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool SetDisplayAutoRotationPreferences(
            OrientationPreference orientation);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-setgraphicsmode
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern GraphicsMode SetGraphicsMode(
            HDC hdc,
            GraphicsMode iMode);

        // https://msdn.microsoft.com/library/dd162980.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern MappingMode SetMapMode(
            HDC hdc,
            MappingMode fnMapMode);

        // https://msdn.microsoft.com/library/dd145098.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe bool SetViewportExtEx(
            HDC hdc,
            int nXExtent,
            int nYExtent,
            Size* lpSize);

        // https://msdn.microsoft.com/library/dd145099.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe bool SetViewportOrgEx(
            HDC hdc,
            int nXExtent,
            int nYExtent,
            Point* lpPoint);

        // https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-setwindowextex
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe bool SetWindowExtEx(
            HDC hdc,
            int x,
            int y,
            Size* lpSize);

        // https://msdn.microsoft.com/library/dd145101.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe bool SetWindowOrgEx(
            HDC hdc,
            int nXExtent,
            int nYExtent,
            Point* lpPoint);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-setworldtransform
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool SetWorldTransform(
            HDC hdc,
            [In] ref Matrix3x2 lpXform);

        // https://msdn.microsoft.com/library/dd183481.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool CopyRect(
            out Rect lprcDst,
            [In] ref Rect lprcSrc);

        // https://msdn.microsoft.com/library/dd162699.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool EqualRect(
            [In] ref Rect lprc1,
            [In] ref Rect lprc2);

        // https://msdn.microsoft.com/library/dd144994.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool InflateRect(
            ref Rect lprc,
            int dx,
            int dy);

        // https://msdn.microsoft.com/library/dd145001.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool IntersectRect(
            out Rect lprcDst,
            [In] ref Rect lprcSrc1,
            [In] ref Rect lprcSrc2);

        // https://msdn.microsoft.com/library/dd145017.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool IsRectEmpty(
            [In] ref Rect lprc);

        // https://msdn.microsoft.com/library/dd162746.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool OffsetRect(
            ref Rect lprc,
            int dx,
            int dy);

        // https://msdn.microsoft.com/library/dd162882.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool PtInRect(
            [In] ref Rect lprc,
            Point pt);

        // https://msdn.microsoft.com/library/dd145085.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool SetRect(
            out Rect lprc,
            int xLeft,
            int yTop,
            int xRight,
            int yBottom);

        // https://msdn.microsoft.com/library/dd145086.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool SetRectEmpty(
            out Rect lprc);

        // https://msdn.microsoft.com/library/dd145125.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool SubtractRect(
            out Rect lprcDst,
            [In] ref Rect lprcSrc1,
            [In] ref Rect lprcSrc2);

        // https://msdn.microsoft.com/library/dd145163.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern bool UnionRect(
            out Rect lprcDst,
            [In] ref Rect lprcSrc1,
            [In] ref Rect lprcSrc2);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-combinergn
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType CombineRgn(
            HRGN hrgnDest,
            HRGN hrgnSrc1,
            HRGN hrgnSrc2,
            CombineRegionMode iMode);

        // https://msdn.microsoft.com/library/dd183496.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HRGN CreateEllipticRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/library/dd183497.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HRGN CreateEllipticRgnIndirect(
            [In] ref Rect lprc);

        // https://msdn.microsoft.com/library/dd183511.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe HRGN CreatePolygonRgn(
            Point* lppt,
            int cPoints,
            PolyFillMode fnPolyFillMode);

        // https://msdn.microsoft.com/library/dd183512.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe HRGN CreatePolyPolygonRgn(
            Point* lppt,
            int[] lpPolyCounts,
            int nCount,
            PolyFillMode fnPolyFillMode);

        // https://msdn.microsoft.com/library/dd183514.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HRGN CreateRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/library/dd183515.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HRGN CreateRectRgnIndirect(
            [In] ref Rect lprc);

        // https://msdn.microsoft.com/library/dd183516.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HRGN CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        // https://msdn.microsoft.com/library/dd162700.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool EqualRgn(
            HRGN hSrcRgn1,
            HRGN hSrcRgn2);

        // https://msdn.microsoft.com/library/dd162706.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe HRGN ExtCreateRegion(
            [In] ref Matrix3x2 lpXform,
            uint nCount,
            RGNDATA* lpRgnData);

        // https://msdn.microsoft.com/library/dd162720.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool FillRgn(
            HDC hdc,
            HRGN hrgn,
            HBRUSH hbr);

        // https://msdn.microsoft.com/library/dd144839.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool FrameRgn(
            HDC hdc,
            HRGN hrgn,
            HBRUSH hbr,
            int nWidth,
            int nHeight);

        // https://msdn.microsoft.com/library/dd144920.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern uint GetRegionData(
            HRGN hRgn,
            uint dwCount,
            IntPtr lpRgnData);

        // https://msdn.microsoft.com/library/dd144921.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType GetRgnBox(
            HRGN hrgn,
            out Rect lprc);

        // https://msdn.microsoft.com/library/dd145008.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool InvertRgn(
            HDC hdc,
            HRGN hrgn);

        // https://msdn.microsoft.com/library/dd162747.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType OffsetRgn(
            HRGN hrgn,
            int nXOffset,
            int nYOffset);

        // https://msdn.microsoft.com/library/dd162767.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PaintRgn(
            HDC hdc,
            HRGN hrgn);

        // https://msdn.microsoft.com/library/dd162883.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool PtInRegion(
            HRGN hrgn,
            int X,
            int Y);

        // https://msdn.microsoft.com/library/dd162906.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool RectInRegion(
            HRGN hrgn,
            [In] ref Rect lprc);

        // https://msdn.microsoft.com/library/dd145087.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool SetRectRgn(
            HRGN hrgn,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/library/dd162702.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType ExcludeClipRect(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/library/dd162712.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType ExtSelectClipRgn(
            HDC hdc,
            HRGN hrgn,
            CombineRegionMode fnMode);

        // https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-getclipbox
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType GetClipBox(
            HDC hdc,
            out Rect lprect);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getcliprgn
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int GetClipRgn(
            HDC hdc,
            HRGN hrgn);

        // https://msdn.microsoft.com/library/dd144899.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int GetMetaRgn(
            HDC hdc,
            HRGN hrgn);

        // https://msdn.microsoft.com/library/dd144918.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern int GetRandomRgn(
            HDC hdc,
            HRGN hrgn,
            int iNum);

        // https://msdn.microsoft.com/library/dd144998.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType IntersectClipRect(
            HDC hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        // https://msdn.microsoft.com/library/dd162745.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType OffsetClipRgn(
            HDC hdc,
            int nXOffset,
            int nYOffset);

        // https://msdn.microsoft.com/library/dd162890.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern IntBoolean PtVisible(
            HDC hdc,
            int X,
            int Y);

        // https://msdn.microsoft.com/library/dd162908.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern IntBoolean RectVisible(
            HDC hdc,
            [In] ref Rect lprc);

        // https://msdn.microsoft.com/library/dd162954(v=vs.85).aspx
        [DllImport(Libraries.Gdi32, SetLastError = true, ExactSpelling = true)]
        public static extern bool SelectClipPath(
            HDC hdc,
            CombineRegionMode iMode);

        // https://msdn.microsoft.com/library/dd162955.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType SelectClipRgn(
            HDC hdc,
            HRGN hrgn);

        // https://msdn.microsoft.com/library/dd145075.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern RegionType SetMetaRgn(
            HDC hdc);

        // https://msdn.microsoft.com/library/dd144853.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern BackgroundMode GetBkMode(
            HDC hdc);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-setbkmode
        [SuppressGCTransition]
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern BackgroundMode SetBkMode(
            HDC hdc,
            BackgroundMode iBkMode);

        // https://msdn.microsoft.com/library/dd144922.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern PenMixMode GetROP2(
            HDC hdc);

        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern PenMixMode SetROP2(
            HDC hdc,
            PenMixMode fnDrawMode);

        // https://msdn.microsoft.com/library/dd144852.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF GetBkColor(
            HDC hdc);

        // https://msdn.microsoft.com/library/dd162964.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF SetBkColor(
            HDC hdc,
            COLORREF crColor);

        // https://msdn.microsoft.com/library/dd162969.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF SetDCBrushColor(
            HDC hdc,
            COLORREF crColor);

        // https://msdn.microsoft.com/library/dd144872.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern COLORREF GetDCBrushColor(
            HDC hdc);

        // https://msdn.microsoft.com/library/dd144927.aspx
        [DllImport(Libraries.User32, ExactSpelling = true)]
        public static extern HBRUSH GetSysColorBrush(
            SystemColor nIndex);

        // https://msdn.microsoft.com/library/dd183509.aspx
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HPEN CreatePen(
            PenStyle fnPenStyle,
            int nWidth,
            COLORREF crColor);

        // https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-extcreatepen
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HPEN ExtCreatePen(
            uint iPenStyle,
            uint cWidth,
            in LOGBRUSH plbrush,
            uint cStyle,
            uint[]? pstyle);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-gdiflush
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern bool GdiFlush();

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-gdigetbatchlimit
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern uint GdiGetBatchLimit();

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-gdisetbatchlimit
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern uint GdiSetBatchLimit(
            uint dwLimit);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-bitblt
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

        // https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-setstretchbltmode
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern StretchMode SetStretchBltMode(
            HDC hdc,
            StretchMode mode);

        // https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-stretchblt
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

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getboundsrect
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern BoundsState GetBoundsRect(
            HDC hdc,
            out Rect lprect,
            BoundsState flags);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-setboundsrect
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern BoundsState SetBoundsRect(
            HDC hdc,
            ref Rect lprect,
            BoundsState flags);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getpaletteentries
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe uint GetPaletteEntries(
            HPALETTE hpal,
            uint iStart,
            uint cEntries,
            PaletteEntry* pPalEntries);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getsystempaletteentries
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe uint GetSystemPaletteEntries(
            HDC hdc,
            uint iStart,
            uint cEntries,
            PaletteEntry* pPalEntries);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-selectpalette
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HPALETTE SelectPalette(
            HDC hdc,
            HPALETTE hPal,
            IntBoolean bForceBkgd);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-playmetafilerecord
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe IntBoolean PlayMetaFileRecord(
            HDC hdc,
            HGDIOBJ* lpHandleTable,
            METARECORD* lpMR,
            uint noObjs);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-enummetafile
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern IntBoolean EnumMetaFile(
            HDC hdc,
            HMETAFILE hmf,
            MFENUMPROC proc,
            LParam param);

        // typedef int (CALLBACK* ENHMFENUMPROC) (_In_ HDC hdc, _In_reads_(nHandles) HANDLETABLE FAR* lpht, _In_ CONST ENHMETARECORD* lpmr, _In_ int nHandles, _In_opt_ LPARAM data);

        // Enhanced Metafile Function Declarations

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-closeenhmetafile
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern HENHMETAFILE CloseEnhMetaFile(
            HDC hdc);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-copyenhmetafilew
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern HENHMETAFILE CopyEnhMetaFileW(
            HENHMETAFILE hEnh,
            string lpFileName);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-createenhmetafilew
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern unsafe HDC CreateEnhMetaFileW(
            HDC hdc,
            string lpFilename,
            Rect* lprc,
            string lpDesc);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-deleteenhmetafile
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern IntBoolean DeleteEnhMetaFile(
            HENHMETAFILE hmf);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-enumenhmetafile
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe IntBoolean EnumEnhMetaFile(
            HDC hdc,
            HENHMETAFILE hmf,
            ENHMFENUMPROC proc,
            LParam param,
            Rect* lpRect);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getenhmetafilew
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern HENHMETAFILE GetEnhMetaFileW(
            string lpName);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getenhmetafilebits
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe uint GetEnhMetaFileBits(
            HENHMETAFILE hEMF,
            uint nSize,
            byte* lpData);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getenhmetafiledescriptionw
        [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern unsafe uint GetEnhMetaFileDescriptionW(
            HENHMETAFILE hemf,
            uint cchBuffer,
            char* lpDescription);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getenhmetafileheader
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe uint GetEnhMetaFileHeader(
            HENHMETAFILE hemf,
            uint nSize,
            ENHMETAHEADER* lpEnhMetaHeader);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getenhmetafilepaletteentries
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe uint GetEnhMetaFilePaletteEntries(
            HENHMETAFILE hemf,
            uint nNumEntries,
            PaletteEntry* lpPaletteEntries);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getenhmetafilepixelformat
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe uint GetEnhMetaFilePixelFormat(
            HENHMETAFILE hemf,
            uint cbBuffer,
            PixelFormatDescriptor* ppfd);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-getwinmetafilebits
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe uint GetWinMetaFileBits(
            HENHMETAFILE hemf,
            uint cbData16,
            byte* pData16,
            int iMapMode,
            HDC hdcRef);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-playenhmetafile
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe IntBoolean PlayEnhMetaFile(
            HDC hdc,
            HENHMETAFILE hmf,
            ref Rect lprect);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-playenhmetafilerecord
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe IntBoolean PlayEnhMetaFileRecord(
            HDC hdc,
            HGDIOBJ* pht,
            ENHMETARECORD* pmr,
            uint cht);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-setenhmetafilebits
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe HENHMETAFILE SetEnhMetaFileBits(
            uint nSize,
            byte* pb);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-setwinmetafilebits
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe HENHMETAFILE SetWinMetaFileBits(
            uint nSize,
            byte* lpMeta16Data,
            HDC hdcRef,
            METAFILEPICT* lpMFP);

        // https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-gdicomment
        [DllImport(Libraries.Gdi32, ExactSpelling = true)]
        public static extern unsafe IntBoolean GdiComment(
            HDC hdc,
            uint nSize,
            byte* lpData);

        // https://channel9.msdn.com/blogs/pdc2008/pc43
        // https://docs.microsoft.com/windows/win32/api/uxtheme/nf-uxtheme-beginbufferedpaint
        [DllImport(Libraries.UxTheme, ExactSpelling = true)]
        public static extern unsafe HPAINTBUFFER BeginBufferedPaint(
            HDC hdcTarget,
            in Rect prcTarget,
            BufferFormat dwFormat,
            BP_PAINTPARAMS* pPaintParams,
            out HDC phdc);

        // https://docs.microsoft.com/windows/win32/api/uxtheme/nf-uxtheme-endbufferedpaint
        [DllImport(Libraries.UxTheme, ExactSpelling = true)]
        public static extern HResult EndBufferedPaint(
            HPAINTBUFFER hBufferedPaint,
            IntBoolean fUpdateTarget);

        // https://docs.microsoft.com/windows/win32/api/uxtheme/nf-uxtheme-bufferedpaintinit
        [DllImport(Libraries.UxTheme, ExactSpelling = true)]
        public static extern HResult BufferedPaintInit();

        // https://docs.microsoft.com/windows/win32/api/uxtheme/nf-uxtheme-bufferedpaintuninit
        [DllImport(Libraries.UxTheme, ExactSpelling = true)]
        public static extern HResult BufferedPaintUnInit();
    }
}
