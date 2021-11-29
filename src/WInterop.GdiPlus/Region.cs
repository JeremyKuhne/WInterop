// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.GdiPlus.Native;

namespace WInterop.GdiPlus;

public class Region : IDisposable
{
    private readonly GpRegion _gpRegion;

    public Region(GpRegion gpRegion)
    {
        if (gpRegion.Handle == 0)
            throw new ArgumentNullException(nameof(gpRegion));

        _gpRegion = gpRegion;
    }

    public static implicit operator GpRegion(Region region) => region._gpRegion;

    private void Dispose(bool disposing)
    {
        GpStatus status = GdiPlusImports.GdipDeleteRegion(_gpRegion);
        if (disposing)
        {
            status.ThrowIfFailed();
        }
    }

    ~Region() => Dispose(disposing: false);

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
