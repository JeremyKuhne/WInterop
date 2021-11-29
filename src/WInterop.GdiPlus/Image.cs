// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.GdiPlus.Native;

namespace WInterop.GdiPlus;

public class Image : IDisposable
{
    protected GpImage _gpImage;

    public static implicit operator GpImage(Image image) => image._gpImage;

    private void Dispose(bool disposing)
    {
        GdiPlusImports.GdipDisposeImage(_gpImage).ThrowIfFailed();
    }

    ~Image() => Dispose(disposing: false);

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
