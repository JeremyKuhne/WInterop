// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi.Types;

namespace WInterop.GdiPlus
{
    public static partial class GdiPlusMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // IMPORTANT: GDI+ may or may not set last error either implicitly or explicitly.
            // When the result is GpStatus.Win32Error it *may* have set the last error value
            // if the facility is Win32. The API is very inconsistent. It is entirely possible
            // to get a Win32Error and have ERROR_SUCCESS, that doesn't mean the call was
            // actually successful.
            //
            // It is also noted in the documentation that the creation methods may return an
            // out of memory error for other issues other than out of memory.

            // https://msdn.microsoft.com/en-us/library/ms534077.aspx
            [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
            public static extern GpStatus GdiplusStartup(
                out UIntPtr token,
                [In] ref GdiplusStartupInput input,
                out GdiplusStartupOutput output);

            // https://msdn.microsoft.com/en-us/library/ms534076.aspx
            [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
            public static extern void GdiplusShutdown(
                UIntPtr token);

            // https://msdn.microsoft.com/en-us/library/ms536160.aspx
            [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
            public static extern GpStatus GdipCreateFromHDC(
                DeviceContext hdc,
                out GpGraphics graphics);

            [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
            public static extern GpStatus GdipDeleteGraphics(
                IntPtr grpahics);

            [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
            public static extern GpStatus GdipSetSmoothingMode(
                GpGraphics graphics,
                SmoothingMode smoothingMode);

            [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
            public static extern GpStatus GdipCreatePen1(
                ARGB color,
                float width,
                GpUnit unit,
                out GpPen pen);

            [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
            public static extern GpStatus GdipDeletePen(
                IntPtr pen);

            [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
            public static extern GpStatus GdipCreateSolidFill(
                ARGB color,
                out GpSolidBrush brush);

            [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
            public static extern GpStatus GdipDeleteBrush(
                IntPtr brush);

            [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern GpStatus GdipDrawLinesI(
                GpGraphics graphics,
                GpPen pen,
                GpPoint* points,
                int count);

            [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern GpStatus GdipFillEllipseI(
                GpGraphics graphics,
                GpBrush brush,
                int x,
                int y,
                int width,
                int height);
        }
    }
}
