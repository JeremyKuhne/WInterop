// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

/// <summary>
///  [ID2D1TessellationSink]
/// </summary>
[Guid(InterfaceIds.IID_ID2D1TessellationSink)]
public readonly unsafe struct TesselationSink : TesselationSink.Interface, IDisposable
{
    private readonly ID2D1TessellationSink* _handle;

    internal TesselationSink(ID2D1TessellationSink* handle) => _handle = handle;

    public void AddTriangles(ReadOnlySpan<Triangle> triangles)
    {
        fixed (void* t = triangles)
        {
            _handle->AddTriangles((D2D1_TRIANGLE*)t, (uint)triangles.Length);
        }
    }

    public void Close() => _handle->Close();

    public void Dispose()
    {
        Close();
        _handle->Release();
    }

    internal interface Interface
    {
        void AddTriangles(ReadOnlySpan<Triangle> triangles);

        void Close();
    }
}
