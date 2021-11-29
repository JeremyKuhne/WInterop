// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  Renders to an intermediate texture created by the CreateCompatibleRenderTarget method.
///  [ID2D1BitmapRenderTarget]
/// </summary>
internal unsafe class BitmapRenderTarget : RenderTarget, IBitmapRenderTarget
{
    private readonly ID2D1BitmapRenderTarget* _handle;

    internal BitmapRenderTarget(ID2D1BitmapRenderTarget* handle)
        : base((ID2D1RenderTarget*)handle)
        => _handle = handle;

    public Bitmap Bitmap
    {
        get
        {
            ID2D1Bitmap* bitmap;
            _handle->GetBitmap(&bitmap).ThrowIfFailed();
            return new(bitmap);
        }
    }
}
