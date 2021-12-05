// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Errors;
using WInterop.Support.Buffers;

namespace WInterop.Gdi;

public static unsafe partial class Gdi
{
    public static BrushHandle CreateSolidBrush(Color color) => new(TerraFXWindows.CreateSolidBrush(color.ToCOLORREF()));

    public static PenHandle GetCurrentPen(in this DeviceContext context)
        => new((HPEN)TerraFXWindows.GetCurrentObject(context, (uint)ObjectType.Pen), ownsHandle: false);

    public static BrushHandle GetCurrentBrush(in this DeviceContext context)
        => new((HBRUSH)TerraFXWindows.GetCurrentObject(context, (uint)ObjectType.Brush), ownsHandle: false);

    public static PaletteHandle GetCurrentPalette(in this DeviceContext context)
        => new((HPALETTE)TerraFXWindows.GetCurrentObject(context, (uint)ObjectType.Palette), ownsHandle: false);

    public static BrushHandle GetStockBrush(StockBrush brush)
        => new((HBRUSH)TerraFXWindows.GetStockObject((int)brush), ownsHandle: false);

    public static PenHandle GetStockPen(StockPen pen) => new((HPEN)TerraFXWindows.GetStockObject((int)pen), ownsHandle: false);

    public static PenHandle CreatePen(PenStyle style, int width, Color color)
        => new(TerraFXWindows.CreatePen((int)style, width, color.ToCOLORREF()));

    public static PenHandle CreatePen(
        PenStyleExtended style,
        uint width,
        Color color,
        PenEndCap endCap = PenEndCap.Round,
        PenJoin join = PenJoin.Round)
    {
        LOGBRUSH brush = new()
        {
            lbColor = color.ToCOLORREF(),
            lbStyle = (uint)BrushStyle.Solid
        };

        return new PenHandle(TerraFXWindows.ExtCreatePen(
            (uint)style | (uint)PenType.Geometric | (uint)endCap | (uint)join,
            width,
            &brush,
            0,
            null));
    }

    public static Color GetPenColor(PenHandle pen)
    {
        switch ((ObjectType)TerraFXWindows.GetObjectType(pen))
        {
            case ObjectType.Pen:
                LOGPEN logPen = default;
                int size = sizeof(LOGPEN);
                if (TerraFXWindows.GetObjectW(pen.HPEN, size, &logPen) == size)
                    return logPen.lopnColor.ToColor();
                break;
            case ObjectType.ExtendedPen:
                BufferHelper.BufferInvoke((HeapBuffer buffer) =>
                {
                    size = TerraFXWindows.GetObjectW(pen.HPEN, 0, null);
                    if (size == 0)
                        throw new InvalidOperationException();
                    buffer.EnsureByteCapacity((ulong)size);
                    size = TerraFXWindows.GetObjectW(pen.HPEN, size, buffer.VoidPointer);
                    return size < sizeof(EXTLOGPEN)
                        ? throw new InvalidOperationException()
                        : ((EXTLOGPEN*)buffer.VoidPointer)->elpColor;
                });
                break;
        }

        throw new InvalidOperationException();
    }

    public static Color GetPixel(this in DeviceContext context, Point point)
        => TerraFXWindows.GetPixel(context, point.X, point.Y).ToColor();

    public static bool SetPixel(this in DeviceContext context, Point point, Color color)
        => TerraFXWindows.SetPixelV(context, point.X, point.Y, color.ToCOLORREF());

    public static bool MoveTo(this in DeviceContext context, Point point)
        => TerraFXWindows.MoveToEx(context, point.X, point.Y, null);

    public static bool MoveTo(this in DeviceContext context, int x, int y)
        => TerraFXWindows.MoveToEx(context, x, y, null);

    public static bool LineTo(this in DeviceContext context, Point point)
        => TerraFXWindows.LineTo(context, point.X, point.Y);

    public static bool LineTo(this in DeviceContext context, int x, int y)
        => TerraFXWindows.LineTo(context, x, y);

    public static PolyFillMode GetPolyFillMode(this in DeviceContext context)
        => (PolyFillMode)TerraFXWindows.GetPolyFillMode(context);

    public static PolyFillMode SetPolyFillMode(this in DeviceContext context, PolyFillMode fillMode)
        => (PolyFillMode)TerraFXWindows.SetPolyFillMode(context, (int)fillMode);

    public static bool Polygon(this in DeviceContext context, params Point[] points) => Polygon(context, points.AsSpan());

    public static bool Polygon(this in DeviceContext context, ReadOnlySpan<Point> points)
    {
        fixed (void* p = points)
        {
            return TerraFXWindows.Polygon(context, (POINT*)&p, points.Length);
        }
    }

    public static bool Polyline(this in DeviceContext context, params Point[] points) => Polyline(context, points.AsSpan());

    public static unsafe bool Polyline(this in DeviceContext context, ReadOnlySpan<Point> points)
    {
        fixed (void* p = points)
        {
            return TerraFXWindows.Polyline(context, (POINT*)p, points.Length);
        }
    }

    public static bool Rectangle(this in DeviceContext context, Rectangle rectangle)
        => TerraFXWindows.Rectangle(context, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);

    public static bool Ellipse(this in DeviceContext context, Rectangle bounds)
        => TerraFXWindows.Ellipse(context, bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);

    public static bool RoundRectangle(this in DeviceContext context, Rectangle rectangle, Size corner)
        => TerraFXWindows.RoundRect(context, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom, corner.Width, corner.Height);

    public static unsafe bool FillRectangle(this in DeviceContext context, Rectangle rectangle, BrushHandle brush)
    {
        RECT rect = rectangle.ToRECT();
        return TerraFXWindows.FillRect(context, &rect, brush) != 0;
    }

    public static unsafe bool FrameRectangle(this in DeviceContext context, Rectangle rectangle, BrushHandle brush)
    {
        RECT rect = rectangle.ToRECT();
        return TerraFXWindows.FrameRect(context, &rect, brush) != 0;
    }

    public static unsafe bool InvertRectangle(this in DeviceContext context, Rectangle rectangle)
    {
        RECT rect = rectangle.ToRECT();
        return TerraFXWindows.InvertRect(context, &rect);
    }

    public static unsafe bool DrawFocusRectangle(this in DeviceContext context, Rectangle rectangle)
    {
        RECT rect = rectangle.ToRECT();
        return TerraFXWindows.DrawFocusRect(context, &rect);
    }

    public static unsafe bool PolyBezier(this in DeviceContext context, params Point[] points)
        => PolyBezier(context, points.AsSpan());

    public static unsafe bool PolyBezier(this in DeviceContext context, ReadOnlySpan<Point> points)
    {
        fixed (void* p = points)
        {
            return TerraFXWindows.PolyBezier(context, (POINT*)p, (uint)points.Length);
        }
    }

    public static void BufferedPaintInitialize() => TerraFXWindows.BufferedPaintInit().ThrowIfFailed();

    public static void BufferedPaintUninitialize() => TerraFXWindows.BufferedPaintUnInit().ThrowIfFailed();
}