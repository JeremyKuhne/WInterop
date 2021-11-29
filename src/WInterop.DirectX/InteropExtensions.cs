// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using TerraFX.Interop.Windows;
using WInterop.Errors;

namespace WInterop;

internal static class InteropExtensions
{
    internal static RectangleF ToRectangleF(this in D2D_RECT_F rect)
        => RectangleF.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);

    internal static D2D_RECT_F ToD2D(this in RectangleF rect)
        => new(rect.Left, rect.Top, rect.Right, rect.Bottom);

    internal static D2D_RECT_U ToD2D(this in Rectangle rect)
        => checked(new((uint)rect.Left, (uint)rect.Top, (uint)rect.Right, (uint)rect.Bottom));

    internal static PointF ToPointF(this in D2D_POINT_2F point)
        => Unsafe.As<D2D_POINT_2F, PointF>(ref Unsafe.AsRef(point));

    internal static D2D_POINT_2F ToD2D(this in PointF point)
        => Unsafe.As<PointF, D2D_POINT_2F>(ref Unsafe.AsRef(point));

    internal static SizeF ToSizeF(this in D2D_SIZE_F size)
        => Unsafe.As<D2D_SIZE_F, SizeF>(ref Unsafe.AsRef(size));

    internal static D2D_SIZE_F ToD2D(this in SizeF size)
        => Unsafe.As<SizeF, D2D_SIZE_F>(ref Unsafe.AsRef(size));

    internal static HResult ToHResult(this in HRESULT result)
        => (HResult)(int)result;

    internal static void ThrowIfFailed(this in HRESULT result, string? detail = null)
        => result.ToHResult().ThrowIfFailed(detail);
}
