// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.Gdi.Native;
using WInterop.Support.Buffers;

namespace WInterop.Gdi
{
    public static partial class Gdi
    {
        public static BrushHandle CreateSolidBrush(Color color) => new BrushHandle(Imports.CreateSolidBrush(color));

        public static PenHandle GetCurrentPen(in DeviceContext context)
            => new PenHandle((HPEN)Imports.GetCurrentObject(context, ObjectType.Pen), ownsHandle: false);

        public static BrushHandle GetStockBrush(StockBrush brush)
            => new BrushHandle((HBRUSH)Imports.GetStockObject((int)brush), ownsHandle: false);

        public static PenHandle GetStockPen(StockPen pen) => new PenHandle((HPEN)Imports.GetStockObject((int)pen), ownsHandle: false);

        public static PenHandle CreatePen(PenStyle style, int width, Color color) => new PenHandle(Imports.CreatePen(style, width, color));

        public static PenHandle CreatePen(PenStyleExtended style, uint width, Color color, PenEndCap endCap = PenEndCap.Round, PenJoin join = PenJoin.Round)
        {
            LOGBRUSH brush = new LOGBRUSH
            {
                lbColor = color,
                lpStyle = BrushStyle.Solid
            };

            return new PenHandle(Imports.ExtCreatePen(
                (uint)style | (uint)PenType.Geometric | (uint)endCap | (uint)join,
                width,
                in brush,
                0,
                null));
        }

        public unsafe static Color GetPenColor(PenHandle pen)
        {
            switch (Imports.GetObjectType(pen))
            {
                case ObjectType.Pen:
                    LOGPEN logPen = new LOGPEN();
                    int size = sizeof(LOGPEN);
                    if (Imports.GetObjectW(pen, size, &logPen) == size)
                        return logPen.lopnColor;
                    break;
                case ObjectType.ExtendedPen:
                    BufferHelper.BufferInvoke((HeapBuffer buffer) =>
                    {
                        size = Imports.GetObjectW(pen, 0, null);
                        if (size == 0)
                            throw new InvalidOperationException();
                        buffer.EnsureByteCapacity((ulong)size);
                        size = Imports.GetObjectW(pen, size, buffer.VoidPointer);
                        if (size < sizeof(EXTLOGPEN))
                            throw new InvalidOperationException();
                        return ((EXTLOGPEN*)buffer.VoidPointer)->elpColor;
                    });
                    break;
            }

            throw new InvalidOperationException();
        }


        public static Color GetPixel(in DeviceContext context, int x, int y) => Imports.GetPixel(context, x, y);

        public static bool SetPixel(in DeviceContext context, int x, int y, COLORREF color) => Imports.SetPixelV(context, x, y, color);

        public unsafe static bool MoveTo(in DeviceContext context, int x, int y) => Imports.MoveToEx(context, x, y, null);

        public static bool LineTo(in DeviceContext context, int x, int y) => Imports.LineTo(context, x, y);

        public static PolyFillMode GetPolyFillMode(in DeviceContext context) => Imports.GetPolyFillMode(context);

        public static PolyFillMode SetPolyFillMode(in DeviceContext context, PolyFillMode fillMode) => Imports.SetPolyFillMode(context, fillMode);

        public static bool Polygon(in DeviceContext context, params Point[] points) => Polygon(context, points.AsSpan());

        public static bool Polygon(in DeviceContext context, ReadOnlySpan<Point> points) => Imports.Polygon(context, ref MemoryMarshal.GetReference(points), points.Length);

        public static bool Polyline(in DeviceContext context, params Point[] points) => Polyline(context, points.AsSpan());

        public static bool Polyline(in DeviceContext context, ReadOnlySpan<Point> points) => Imports.Polyline(context, ref MemoryMarshal.GetReference(points), points.Length);

        public static bool Rectangle(in DeviceContext context, Rectangle rectangle) => Imports.Rectangle(context, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);

        public static bool Ellipse(in DeviceContext context, Rectangle bounds) => Imports.Ellipse(context, bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);

        public static bool RoundRectangle(in DeviceContext context, Rectangle rectangle, Size corner)
            => Imports.RoundRect(context, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom, corner.Width, corner.Height);

        public static bool FillRectangle(in DeviceContext context, Rectangle rectangle, BrushHandle brush)
        {
            RECT rect = rectangle;
            return Imports.FillRect(context, ref rect, brush);
        }

        public static bool FrameRectangle(in DeviceContext context, Rectangle rectangle, BrushHandle brush)
        {
            RECT rect = rectangle;
            return Imports.FrameRect(context, ref rect, brush);
        }

        public static bool InvertRectangle(in DeviceContext context, Rectangle rectangle)
        {
            RECT rect = rectangle;
            return Imports.InvertRect(context, ref rect);
        }

        public static bool DrawFocusRectangle(in DeviceContext context, Rectangle rectangle)
        {
            RECT rect = rectangle;
            return Imports.DrawFocusRect(context, ref rect);
        }

        public unsafe static bool PolyBezier(in DeviceContext context, params Point[] points)
        {
            return PolyBezier(context, points.AsSpan());
        }

        public unsafe static bool PolyBezier(in DeviceContext context, ReadOnlySpan<Point> points)
        {
            return Imports.PolyBezier(context, ref MemoryMarshal.GetReference(points), (uint)points.Length);
        }
    }
}
