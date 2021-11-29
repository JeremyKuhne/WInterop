// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.GdiPlus.Native;

namespace WInterop.GdiPlus;

public class Matrix : IDisposable
{
    private readonly GpMatrix _gpMatrix;

    public Matrix(GpMatrix gpMatrix)
    {
        if (gpMatrix.Handle == 0)
            throw new ArgumentNullException(nameof(gpMatrix));

        _gpMatrix = gpMatrix;
    }

    private void Dispose(bool disposing)
    {
        GpStatus status = GdiPlusImports.GdipDeleteMatrix(_gpMatrix);
        if (disposing)
        {
            status.ThrowIfFailed();
        }
    }

    ~Matrix() => Dispose(disposing: false);

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
