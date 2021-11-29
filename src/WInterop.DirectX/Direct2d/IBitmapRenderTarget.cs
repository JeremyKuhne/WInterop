// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

public interface IBitmapRenderTarget : IRenderTarget
{
    Bitmap Bitmap { get; }
}
