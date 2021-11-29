// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Gdi.Native;
using WInterop.Support.Buffers;

namespace WInterop.Gdi;

public static partial class Gdi
{
    public static BrushHandle CreateSolidBrush(Color color) => new BrushHandle(GdiImports.CreateSolidBrush(color));

    public static PenHandle GetCurrentPen(in this DeviceContext context)
        => new PenHandle((HPEN)GdiImports.GetCurrentObject(context, ObjectType.Pen), ownsHandle: false);

    public static BrushHandle GetCurrentBrush(in this DeviceContext context)
        => new BrushHandle((HBRUSH)GdiImports.GetCurrentObject(context, ObjectType.Brush), ownsHandle: false);

    public static PaletteHandle GetCurrentPalette(in this DeviceContext context)
        => new PaletteHandle((HPALETTE)GdiImports.GetCurrentObject(context, ObjectType.Palette), ownsHandle: false);

    public static BrushHandle GetStockBrush(StockBrush brush)
        => new BrushHandle((HBRUSH)GdiImports.GetStockObject((int)brush), ownsHandle: false);

    public static PenHandle GetStockPen(StockPen pen) => new PenHandle((HPEN)GdiImports.GetStockObject((int)pen), ownsHandle: false);

    public static PenHandle CreatePen(PenStyle style, int width, Color color) => new PenHandle(GdiImports.CreatePen(style, width, color));

    public static PenHandle CreatePen(PenStyleExtended style, uint width, Color color, PenEndCap endCap = PenEndCap.Round, PenJoin join = PenJoin.Round)
    {
        LOGBRUSH brush = new LOGBRUSH
        {
            lbColor = color,
            lpStyle = BrushStyle.Solid
        };

        return new PenHandle(GdiImports.ExtCreatePen(
            (uint)style | (uint)PenType.Geometric | (uint)endCap | (uint)join,
            width,
            in brush,
            0,
            null));
    }

    public static unsafe Color GetPenColor(PenHandle pen)
    {
        switch (GdiImports.GetObjectType(pen))
        {
            case ObjectType.Pen:
                LOGPEN logPen = default;
                int size = sizeof(LOGPEN);
                if (GdiImports.GetObjectW(pen, size, &logPen) == size)
                    return logPen.lopnColor;
                break;
            case ObjectType.ExtendedPen:
                BufferHelper.BufferInvoke((HeapBuffer buffer) =>
                {
                    size = GdiImports.GetObjectW(pen, 0, null);
                    if (size == 0)
                        throw new InvalidOperationException();
                    buffer.EnsureByteCapacity((ulong)size);
                    size = GdiImports.GetObjectW(pen, size, buffer.VoidPointer);
                    if (size < sizeof(EXTLOGPEN))
                        throw new InvalidOperationException();
                    return ((EXTLOGPEN*)buffer.VoidPointer)->elpColor;
                });
                break;
        }

        throw new InvalidOperationException();
    }

    public static Color GetPixel(this in DeviceContext context, Point point)
        => GdiImports.GetPixel(context, point.X, point.Y);

    public static bool SetPixel(this in DeviceContext context, Point point, Color color)
        => GdiImports.SetPixelV(context, point.X, point.Y, color);

    public static unsafe bool MoveTo(this in DeviceContext context, Point point)
        => GdiImports.MoveToEx(context, point.X, point.Y, null);

    public static unsafe bool MoveTo(this in DeviceContext context, int x, int y)
        => GdiImports.MoveToEx(context, x, y, null);

    public static bool LineTo(this in DeviceContext context, Point point)
        => GdiImports.LineTo(context, point.X, point.Y);

    public static bool LineTo(this in DeviceContext context, int x, int y)
        => GdiImports.LineTo(context, x, y);

    public static PolyFillMode GetPolyFillMode(this in DeviceContext context) => GdiImports.GetPolyFillMode(context);

    public static PolyFillMode SetPolyFillMode(this in DeviceContext context, PolyFillMode fillMode)
        => GdiImports.SetPolyFillMode(context, fillMode);

    public static bool Polygon(this in DeviceContext context, params Point[] points) => Polygon(context, points.AsSpan());

    public static bool Polygon(this in DeviceContext context, ReadOnlySpan<Point> points)
        => GdiImports.Polygon(context, ref MemoryMarshal.GetReference(points), points.Length);

    public static bool Polyline(this in DeviceContext context, params Point[] points) => Polyline(context, points.AsSpan());

    public static bool Polyline(this in DeviceContext context, ReadOnlySpan<Point> points)
        => GdiImports.Polyline(context, ref MemoryMarshal.GetReference(points), points.Length);

    public static bool Rectangle(this in DeviceContext context, Rectangle rectangle)
        => GdiImports.Rectangle(context, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);

    public static bool Ellipse(this in DeviceContext context, Rectangle bounds)
        => GdiImports.Ellipse(context, bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);

    public static bool RoundRectangle(this in DeviceContext context, Rectangle rectangle, Size corner)
        => GdiImports.RoundRect(context, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom, corner.Width, corner.Height);

    public static bool FillRectangle(this in DeviceContext context, Rectangle rectangle, BrushHandle brush)
    {
        Rect rect = rectangle;
        return GdiImports.FillRect(context, ref rect, brush);
    }

    public static bool FrameRectangle(this in DeviceContext context, Rectangle rectangle, BrushHandle brush)
    {
        Rect rect = rectangle;
        return GdiImports.FrameRect(context, ref rect, brush);
    }

    public static bool InvertRectangle(this in DeviceContext context, Rectangle rectangle)
    {
        Rect rect = rectangle;
        return GdiImports.InvertRect(context, ref rect);
    }

    public static bool DrawFocusRectangle(this in DeviceContext context, Rectangle rectangle)
    {
        Rect rect = rectangle;
        return GdiImports.DrawFocusRect(context, ref rect);
    }

    public static unsafe bool PolyBezier(this in DeviceContext context, params Point[] points)
        => PolyBezier(context, points.AsSpan());

    public static unsafe bool PolyBezier(this in DeviceContext context, ReadOnlySpan<Point> points)
        => GdiImports.PolyBezier(context, ref MemoryMarshal.GetReference(points), (uint)points.Length);

    public static void BufferedPaintInitialize() => GdiImports.BufferedPaintInit().ThrowIfFailed();

    public static void BufferedPaintUninitialize() => GdiImports.BufferedPaintUnInit();
}