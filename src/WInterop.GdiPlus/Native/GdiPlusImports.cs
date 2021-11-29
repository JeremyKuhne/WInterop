// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Com;
using WInterop.Gdi.Native;
using WInterop.GdiPlus.EmfPlus;

namespace WInterop.GdiPlus.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class GdiPlusImports
{
    // IMPORTANT: GDI+ may or may not set last error either implicitly or explicitly.
    // When the result is GpStatus.Win32Error it *may* have set the last error value
    // if the facility is Win32. The API is very inconsistent. It is entirely possible
    // to get a Win32Error and have ERROR_SUCCESS, that doesn't mean the call was
    // actually successful.
    //
    // It is also noted in the documentation that the creation methods may return an
    // out of memory error for other issues other than out of memory.

    // MOST imports here are from the flat projection. As this is just a single page
    // with all of the signatures links on the Gdip* imports are to the relevant C++ api.
    //
    // https://docs.microsoft.com/windows/win32/gdiplus/-gdiplus-graphics-flat

    // https://docs.microsoft.com/windows/win32/api/gdiplusinit/nf-gdiplusinit-gdiplusstartup
    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdiplusStartup(
        nuint** token,
        StartupInput* input,
        StartupOutput** output);

    // https://docs.microsoft.com/windows/win32/api/gdiplusinit/nf-gdiplusinit-gdiplusshutdown
    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe void GdiplusShutdown(
        nuint* token);

    // https://docs.microsoft.com/windows/win32/api/gdiplusgraphics/nf-gdiplusgraphics-graphics-fromhdc(inhdc)
    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipCreateFromHDC(
        HDC hdc,
        GpGraphics* graphics);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipGetImageGraphicsContext(
        GpImage image,
        GpGraphics* graphics);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipDeleteGraphics(
        GpGraphics graphics);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipDisposeImage(
        GpImage image);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipSetSmoothingMode(
        GpGraphics graphics,
        SmoothingMode smoothingMode);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipCreatePen1(
        ARGB color,
        float width,
        GpUnit unit,
        GpPen* pen);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipSetPenColor(
        GpPen pen,
        ARGB argb);

    [SuppressGCTransition]
    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipGetPenColor(
        GpPen pen,
        ARGB* argb);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipDeletePen(
        GpPen pen);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipCreateSolidFill(
        ARGB color,
        GpBrush* brush);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipDeleteBrush(
        GpBrush brush);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipDrawLinesI(
        GpGraphics graphics,
        GpPen pen,
        Point* points,
        int count);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipFillEllipseI(
        GpGraphics graphics,
        GpBrush brush,
        int x,
        int y,
        int width,
        int height);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipDrawRectangle(
        GpGraphics graphics,
        GpPen pen,
        float x,
        float y,
        float width,
        float height);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipDrawRectangleI(
        GpGraphics graphics,
        GpPen pen,
        int x,
        int y,
        int width,
        int height);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipFillRectangle(
        GpGraphics graphics,
        GpBrush brush,
        float x,
        float y,
        float width,
        float height);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipFillRectangleI(
        GpGraphics graphics,
        GpBrush brush,
        int x,
        int y,
        int width,
        int height);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipGraphicsClear(
        GpGraphics graphics,
        ARGB color);

    // https://docs.microsoft.com/windows/win32/api/Gdiplusgraphics/nf-gdiplusgraphics-graphics-gethalftonepalette
    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern HPALETTE GdipCreateHalftonePalette();


    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipGetMetafileHeaderFromMetafile(
        GpMetafile metafile,
        MetafileHeader* header);

    /// <remarks>
    ///  Getting the metafile handle puts the GpMetafile in an invalid state.
    /// </remarks>
    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipGetHemfFromMetafile(
        GpMetafile metafile,
        out HENHMETAFILE hEmf);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern GpStatus GdipCreateMetafileFromStream(
        IStream stream,
        out GpMetafile metafile);

    [DllImport(Libraries.GdiPlus, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipRecordMetafileStream(
        IStream stream,
        HDC referenceHdc,
        EmfType type,
        RectangleF* frameRect,
        MetafileFrameUnit frameUnit,
        char* description,
        out GpMetafile metafile);

    [DllImport(Libraries.GdiPlus, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipRecordMetafileStreamI(
        IStream stream,
        HDC referenceHdc,
        EmfType type,
        Rectangle* frameRect,
        MetafileFrameUnit frameUnit,
        char* description,
        out GpMetafile metafile);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipEnumerateMetafileDestPoint(
        GpGraphics graphics,
        GpMetafile metafile,
        ref PointF destPoint,
        EnumerateMetafilePlusCallback callback,
        IntPtr callbackData,
        GpImageAttributes imageAttributes);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipDeleteMatrix(
        GpMatrix matrix);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipCreateRegion(
        out GpRegion region);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipIsVisibleClipEmpty(
        GpGraphics graphics,
        out BOOL result);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipIsEmptyRegion(
        GpRegion region,
        GpGraphics graphics,
        out BOOL result);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipIsInfiniteRegion(
        GpRegion region,
        GpGraphics graphics,
        out BOOL result);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipGetClip(
        GpGraphics graphics,
        GpRegion region);

    [DllImport(Libraries.GdiPlus, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe GpStatus GdipDeleteRegion(
        GpRegion region);
}
