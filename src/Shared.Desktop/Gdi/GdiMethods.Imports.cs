// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi.Types;
using WInterop.Support;
using WInterop.Windows.Types;

namespace WInterop.Gdi
{
    public static partial class GdiMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/dd144877.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern int GetDeviceCaps(
                DeviceContext hdc,
                DeviceCapability nIndex);

            // https://msdn.microsoft.com/en-us/library/dd144947.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern IntPtr GetWindowDC(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/dd144871.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern IntPtr GetDC(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/dd183490.aspx
            [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern DeviceContext CreateDCW(
                string lpszDriver,
                string lpszDevice,
                string lpszOutput,
                DEVMODE* lpInitData);

            // https://msdn.microsoft.com/en-us/library/dd183505.aspx
            [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern DeviceContext CreateICW(
                string lpszDriver,
                string lpszDevice,
                string lpszOutput,
                DEVMODE* lpInitData);

            // https://msdn.microsoft.com/en-us/library/dd183489.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern DeviceContext CreateCompatibleDC(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd183533.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool DeleteDC(
                IntPtr hdc);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/dd162920.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool ReleaseDC(
                WindowHandle hWnd,
                IntPtr hdc);

            // https://msdn.microsoft.com/en-us/library/dd183399.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool CancelDC(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd183533.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern GdiObjectHandle GetStockObject(
                StockObject stockObject);

            // https://msdn.microsoft.com/en-us/library/dd183518.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern BrushHandle CreateSolidBrush(
                COLORREF crColor);

            // https://msdn.microsoft.com/en-us/library/dd162957.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern IntPtr SelectObject(
                DeviceContext hdc,
                GdiObjectHandle hgdiobj);

            // https://msdn.microsoft.com/en-us/library/dd183539.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool DeleteObject(
                IntPtr hObject);

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
            public static extern BitmapHandle CreateBitmap(
                int nWidth,
                int nHeight,
                uint cPlanes,
                uint cBitsPerPel,
                IntPtr lpvBits);

            // https://msdn.microsoft.com/en-us/library/dd144905.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern ObjectType GetObjectType(
                IntPtr h);

            // https://msdn.microsoft.com/en-us/library/dd144925.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern IntPtr GetStockObject(
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
            public static extern IntPtr BeginPaint(
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
                [In] ref PAINTSTRUCT lpPaint);

            // https://msdn.microsoft.com/en-us/library/dd162498.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static unsafe extern int DrawTextW(
                DeviceContext hDC,
                char* lpchText,
                int nCount,
                ref RECT lpRect,
                TextFormat uFormat);

            // https://msdn.microsoft.com/en-us/library/dd145133.aspx
            [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern bool TextOutW(
                DeviceContext hdc,
                int nXStart,
                int nYStart,
                char* lpString,
                int cchString);

            // https://msdn.microsoft.com/en-us/library/dd145129.aspx
            [DllImport(Libraries.User32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern int TabbedTextOutW(
                DeviceContext hdc,
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
                DeviceContext hdc,
                out TEXTMETRIC lptm);

            // https://msdn.microsoft.com/en-us/library/dd145091.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern TextAlignment SetTextAlign(
                DeviceContext hdc,
                TextAlignment fMode);

            // https://msdn.microsoft.com/en-us/library/dd144932.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern TextAlignment GetTextAlign(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd145094.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool SetTextJustification(
                DeviceContext hdc,
                int nBreakExtra,
                int nBreakCount);

            // https://msdn.microsoft.com/en-us/library/dd145092.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern int SetTextCharacterExtra(
                DeviceContext hdc,
                int nCharExtra);

            // https://msdn.microsoft.com/en-us/library/dd144933.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern int GetTextCharacterExtra(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd145093.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern COLORREF SetTextColor(
                DeviceContext hdc,
                COLORREF crColor);

            // https://msdn.microsoft.com/en-us/library/dd144934.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern COLORREF GetTextColor(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd183499.aspx
            [DllImport(Libraries.Gdi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern FontHandle CreateFontW(
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
                DeviceContext hdc,
                ref LOGFONT lpLogfont,
                EnumFontFamExProc lpEnumFonFamExProc,
                LPARAM lParam,
                uint dwFlags);

            // https://msdn.microsoft.com/en-us/library/dd183354.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool AngleArc(
                DeviceContext hdc,
                int X,
                int Y,
                uint dwRadius,
                float eStartAngle,
                float eSweepAngle);

            // https://msdn.microsoft.com/en-us/library/dd183357.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool Arc(
                DeviceContext hdc,
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
                DeviceContext hdc,
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
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd144909.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern COLORREF GetPixel(
                DeviceContext hdc,
                int nXPos,
                int nYPos);

            // https://msdn.microsoft.com/en-us/library/dd145078.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern COLORREF SetPixel(
                DeviceContext hdc,
                int X,
                int Y,
                COLORREF crColor);

            // https://msdn.microsoft.com/en-us/library/dd145079.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool SetPixelV(
                DeviceContext hdc,
                int X,
                int Y,
                COLORREF crColor);

            // https://msdn.microsoft.com/en-us/library/dd145029.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool LineTo(
                DeviceContext hdc,
                int nXEnd,
                int nYEnd);

            // https://msdn.microsoft.com/en-us/library/dd145069.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool MoveToEx(
                DeviceContext hdc,
                int X,
                int Y,
                POINT* lpPoint);

            // https://msdn.microsoft.com/en-us/library/dd144910.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern PolyFillMode GetPolyFillMode(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd145080.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern PolyFillMode SetPolyFillMode(
                DeviceContext hdc,
                PolyFillMode iPolyFillMode);

            // https://msdn.microsoft.com/en-us/library/dd162811.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool PolyBezier(
                DeviceContext hdc,
                POINT* lppt,
                uint cPoints);

            // https://msdn.microsoft.com/en-us/library/dd162812.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool PolyBezierTo(
                DeviceContext hdc,
                POINT* lppt,
                uint cCount);

            // https://msdn.microsoft.com/en-us/library/dd162813.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool PolyDraw(
                DeviceContext hdc,
                POINT* lppt,
                PointType* lpbTypes,
                int cCount);

            // https://msdn.microsoft.com/en-us/library/dd162815.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool Polyline(
                DeviceContext hdc,
                POINT[] lppt,
                int cPoints);

            // https://msdn.microsoft.com/en-us/library/dd162816.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool PolylineTo(
                DeviceContext hdc,
                POINT* lppt,
                uint cCount);

            // https://msdn.microsoft.com/en-us/library/dd162819.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool PolyPolyline(
                DeviceContext hdc,
                POINT* lppt,
                uint[] lpdwPolyPoints,
                uint cCount);

            // https://msdn.microsoft.com/en-us/library/dd162961.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern ArcDirection SetArcDirection(
                DeviceContext hdc,
                ArcDirection ArcDirection);

            // https://msdn.microsoft.com/en-us/library/dd183428.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool Chord(
                DeviceContext hdc,
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
                DeviceContext hdc,
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect);

            // https://msdn.microsoft.com/en-us/library/dd162719.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool FillRect(
                DeviceContext hDC,
                [In] ref RECT lprc,
                BrushHandle hbr);

            // https://msdn.microsoft.com/en-us/library/dd144838.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool FrameRect(
                DeviceContext hDC,
                [In] ref RECT lprc,
                BrushHandle hbr);

            // https://msdn.microsoft.com/en-us/library/dd145007.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool InvertRect(
                DeviceContext hDC,
                [In] ref RECT lprc);

            // https://msdn.microsoft.com/en-us/library/dd162479.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool DrawFocusRect(
                DeviceContext hDC,
                [In] ref RECT lprc);

            // https://msdn.microsoft.com/en-us/library/dd162799.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool Pie(
                DeviceContext hdc,
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
            public unsafe static extern bool Polygon(
                DeviceContext hdc,
                POINT* lpPoints,
                int nCount);

            // https://msdn.microsoft.com/en-us/library/dd162818.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool PolyPolygon(
                DeviceContext hdc,
                POINT* lpPoints,
                int[] lpPolyCounts,
                int nCount);

            // https://msdn.microsoft.com/en-us/library/dd162898.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool Rectangle(
                DeviceContext hdc,
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect);

            // https://msdn.microsoft.com/en-us/library/dd162944.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool RoundRect(
                DeviceContext hdc,
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
                ref POINT lpPoint);

            // https://msdn.microsoft.com/en-us/library/dd183466.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool CombineTransform(
                out XFORM lpxformResult,
                [In] ref XFORM lpxform1,
                [In] ref XFORM lpxform2);

            // https://msdn.microsoft.com/en-us/library/dd162474.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool DPtoLP(
                DeviceContext hdc,
                POINT* lpPoints,
                int nCount);

            // https://msdn.microsoft.com/en-us/library/dd144870.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool GetCurrentPositionEx(
                DeviceContext hdc,
                out POINT lpPoint);

            // https://msdn.microsoft.com/en-us/library/dn376360.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool GetDisplayAutoRotationPreferences(
                out ORIENTATION_PREFERENCE pOrientation);

            // https://msdn.microsoft.com/en-us/library/dd144892.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern GraphicsMode GetGraphicsMode(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd144897.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern MapMode GetMapMode(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd144945.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool GetViewportExtEx(
                DeviceContext hdc,
                out SIZE lpSize);

            // https://msdn.microsoft.com/en-us/library/dd144946.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool GetViewportOrgEx(
                DeviceContext hdc,
                out POINT lpPoint);

            // https://msdn.microsoft.com/en-us/library/dd144948.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool GetWindowExtEx(
                DeviceContext hdc,
                out SIZE lpSize);

            // https://msdn.microsoft.com/en-us/library/dd144949.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool GetWindowOrgEx(
                DeviceContext hdc,
                out POINT lpPoint);

            // https://msdn.microsoft.com/en-us/library/dd144953.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool GetWorldTransform(
                DeviceContext hdc,
                out XFORM lpXform);

            // https://msdn.microsoft.com/en-us/library/dd145042.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool LPtoDP(
                DeviceContext hdc,
                POINT* lpPoints,
                int nCount);

            // https://msdn.microsoft.com/en-us/library/dd145046.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public unsafe static extern int MapWindowPoints(
                WindowHandle hWndFrom,
                WindowHandle hWndTo,
                POINT* lpPoints,
                uint cPoints);

            // https://msdn.microsoft.com/en-us/library/dd145060.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool ModifyWorldTransform(
                DeviceContext hdc,
                [In] ref XFORM lpXform,
                WorldTransformMode iMode);

            // https://msdn.microsoft.com/en-us/library/dd162748.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool OffsetViewportOrgEx(
                DeviceContext hdc,
                int nXOffset,
                int nYOffset,
                POINT* lpPoint);

            // https://msdn.microsoft.com/en-us/library/dd162749.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool OffsetWindowOrgEx(
                DeviceContext hdc,
                int nXOffset,
                int nYOffset,
                POINT* lpPoint);

            // https://msdn.microsoft.com/en-us/library/dd162947.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool ScaleViewportExtEx(
                DeviceContext hdc,
                int Xnum,
                int Xdenom,
                int Ynum,
                int Ydenom,
                out SIZE lpSize);

            // https://msdn.microsoft.com/en-us/library/dd162948.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool ScaleWindowExtEx(
                DeviceContext hdc,
                int Xnum,
                int Xdenom,
                int Ynum,
                int Ydenom,
                out SIZE lpSize);

            // https://msdn.microsoft.com/en-us/library/dd162952.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool ScreenToClient(
                WindowHandle hWnd,
                ref POINT lpPoint);

            // https://msdn.microsoft.com/en-us/library/dn376361.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern bool SetDisplayAutoRotationPreferences(
                ORIENTATION_PREFERENCE orientation);

            // https://msdn.microsoft.com/en-us/library/dd162977.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool SetGraphicsMode(
                DeviceContext hdc,
                GraphicsMode iMode);

            // https://msdn.microsoft.com/en-us/library/dd162980.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern MapMode SetMapMode(
                DeviceContext hdc,
                MapMode fnMapMode);

            // https://msdn.microsoft.com/en-us/library/dd145098.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool SetViewportExtEx(
                DeviceContext hdc,
                int nXExtent,
                int nYExtent,
                SIZE* lpSize);

            // https://msdn.microsoft.com/en-us/library/dd145099.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool SetViewportOrgEx(
                DeviceContext hdc,
                int nXExtent,
                int nYExtent,
                POINT* lpPoint);

            // https://msdn.microsoft.com/en-us/library/dd145100.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool SetWindowExtEx(
                DeviceContext hdc,
                int nXExtent,
                int nYExtent,
                SIZE* lpSize);

            // https://msdn.microsoft.com/en-us/library/dd145101.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern bool SetWindowOrgEx(
                DeviceContext hdc,
                int nXExtent,
                int nYExtent,
                POINT* lpPoint);

            // https://msdn.microsoft.com/en-us/library/dd145104.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool SetWorldTransform(
                DeviceContext hdc,
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
                POINT pt);

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
                RegionHandle hrgnDest,
                RegionHandle hrgnSrc1,
                RegionHandle hrgnSrc2,
                CombineRegionMode fnCombineMode);

            // https://msdn.microsoft.com/en-us/library/dd183496.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionHandle CreateEllipticRgn(
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect);

            // https://msdn.microsoft.com/en-us/library/dd183497.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionHandle CreateEllipticRgnIndirect(
                [In] ref RECT lprc);

            // https://msdn.microsoft.com/en-us/library/dd183511.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern RegionHandle CreatePolygonRgn(
                POINT* lppt,
                int cPoints,
                PolyFillMode fnPolyFillMode);

            // https://msdn.microsoft.com/en-us/library/dd183512.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern RegionHandle CreatePolyPolygonRgn(
                POINT* lppt,
                int[] lpPolyCounts,
                int nCount,
                PolyFillMode fnPolyFillMode);

            // https://msdn.microsoft.com/en-us/library/dd183514.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionHandle CreateRectRgn(
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect);

            // https://msdn.microsoft.com/en-us/library/dd183515.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionHandle CreateRectRgnIndirect(
                [In] ref RECT lprc);

            // https://msdn.microsoft.com/en-us/library/dd183516.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionHandle CreateRoundRectRgn(
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nWidthEllipse,
                int nHeightEllipse);

            // https://msdn.microsoft.com/en-us/library/dd162700.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool EqualRgn(
                RegionHandle hSrcRgn1,
                RegionHandle hSrcRgn2);

            // https://msdn.microsoft.com/en-us/library/dd162706.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public unsafe static extern RegionHandle ExtCreateRegion(
                [In] ref XFORM lpXform,
                uint nCount,
                RGNDATA* lpRgnData);

            // https://msdn.microsoft.com/en-us/library/dd162720.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool FillRgn(
                DeviceContext hdc,
                RegionHandle hrgn,
                BrushHandle hbr);

            // https://msdn.microsoft.com/en-us/library/dd144839.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool FrameRgn(
                DeviceContext hdc,
                RegionHandle hrgn,
                BrushHandle hbr,
                int nWidth,
                int nHeight);

            // https://msdn.microsoft.com/en-us/library/dd144920.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern uint GetRegionData(
                RegionHandle hRgn,
                uint dwCount,
                IntPtr lpRgnData);

            // https://msdn.microsoft.com/en-us/library/dd144921.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionType GetRgnBox(
                RegionHandle hrgn,
                out RECT lprc);

            // https://msdn.microsoft.com/en-us/library/dd145008.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool InvertRgn(
                DeviceContext hdc,
                RegionHandle hrgn);

            // https://msdn.microsoft.com/en-us/library/dd162747.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionType OffsetRgn(
                RegionHandle hrgn,
                int nXOffset,
                int nYOffset);

            // https://msdn.microsoft.com/en-us/library/dd162767.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool PaintRgn(
                DeviceContext hdc,
                RegionHandle hrgn);

            // https://msdn.microsoft.com/en-us/library/dd162883.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool PtInRegion(
                RegionHandle hrgn,
                int X,
                int Y);

            // https://msdn.microsoft.com/en-us/library/dd162906.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool RectInRegion(
                RegionHandle hrgn,
                [In] ref RECT lprc);

            // https://msdn.microsoft.com/en-us/library/dd145087.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern bool SetRectRgn(
                RegionHandle hrgn,
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect);

            // https://msdn.microsoft.com/en-us/library/dd162702.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionType ExcludeClipRect(
                DeviceContext hdc,
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect);

            // https://msdn.microsoft.com/en-us/library/dd162712.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionType ExtSelectClipRgn(
                DeviceContext hdc,
                RegionHandle hrgn,
                CombineRegionMode fnMode);

            // https://msdn.microsoft.com/en-us/library/dd144865.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionType GetClipBox(
                DeviceContext hdc,
                [In] ref RECT lprc);

            // https://msdn.microsoft.com/en-us/library/dd144866.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern int GetClipRgn(
                DeviceContext hdc,
                RegionHandle hrgn);

            // https://msdn.microsoft.com/en-us/library/dd144899.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern int GetMetaRgn(
                DeviceContext hdc,
                RegionHandle hrgn);

            // https://msdn.microsoft.com/en-us/library/dd144918.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern int GetRandomRgn(
                DeviceContext hdc,
                RegionHandle hrgn,
                int iNum);

            // https://msdn.microsoft.com/en-us/library/dd144998.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionType IntersectClipRect(
                DeviceContext hdc,
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect);

            // https://msdn.microsoft.com/en-us/library/dd162745.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionType OffsetClipRgn(
                DeviceContext hdc,
                int nXOffset,
                int nYOffset);

            // https://msdn.microsoft.com/en-us/library/dd162890.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern BOOL PtVisible(
                DeviceContext hdc,
                int X,
                int Y);

            // https://msdn.microsoft.com/en-us/library/dd162908.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern BOOL RectVisible(
                DeviceContext hdc,
                [In] ref RECT lprc);

            // https://msdn.microsoft.com/en-us/library/dd162954(v=vs.85).aspx
            [DllImport(Libraries.Gdi32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SelectClipPath(
                DeviceContext hdc,
                CombineRegionMode iMode);

            // https://msdn.microsoft.com/en-us/library/dd162955.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionType SelectClipRgn(
                DeviceContext hdc,
                RegionHandle hrgn);

            // https://msdn.microsoft.com/en-us/library/dd145075.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RegionType SetMetaRgn(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd144853.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern BackgroundMode GetBkMode(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd162965.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern BackgroundMode SetBkMode(
                DeviceContext hdc,
                BackgroundMode iBkMode);

            // https://msdn.microsoft.com/en-us/library/dd144922.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RasterOperation GetROP2(
                DeviceContext hdc);

            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern RasterOperation SetROP2(
                DeviceContext hdc,
                RasterOperation fnDrawMode);

            // https://msdn.microsoft.com/en-us/library/dd144852.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern COLORREF GetBkColor(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd162964.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern COLORREF SetBkColor(
                DeviceContext hdc,
                COLORREF crColor);

            // https://msdn.microsoft.com/en-us/library/dd162969.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern COLORREF SetDCBrushColor(
                DeviceContext hdc,
                COLORREF crColor);

            // https://msdn.microsoft.com/en-us/library/dd144872.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern COLORREF GetDCBrushColor(
                DeviceContext hdc);

            // https://msdn.microsoft.com/en-us/library/dd144927.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern IntPtr GetSysColorBrush(
                SystemColor nIndex);

            // https://msdn.microsoft.com/en-us/library/dd183509.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern PenHandle CreatePen(
                PenStyle fnPenStyle,
                int nWidth,
                COLORREF crColor);

            // https://msdn.microsoft.com/en-us/library/dd162705.aspx
            [DllImport(Libraries.Gdi32, ExactSpelling = true)]
            public static extern PenHandle ExtCreatePen(
                uint dwPenStyle,
                uint dwWidth,
                [In] ref LOGBRUSH lplb,
                uint dwStyleCount,
                uint[] lpStyle);
        }
    }
}
