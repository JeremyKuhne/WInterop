// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

[StructLayout(LayoutKind.Sequential)]
[Guid(InterfaceIds.IID_ID2D1StrokeStyle)]
public readonly unsafe struct StrokeStyle : StrokeStyle.Interface, IDisposable
{
    internal readonly ID2D1StrokeStyle* _handle;

    internal StrokeStyle(ID2D1StrokeStyle* handle) => _handle = handle;

    public Factory GetFactory() => Resource.From(this).GetFactory();

    public CapStyle StartCap => (CapStyle)_handle->GetStartCap();

    public CapStyle EndCap => (CapStyle)_handle->GetEndCap();

    public CapStyle DashCap => (CapStyle)_handle->GetDashCap();

    public float MiterLimit => _handle->GetMiterLimit();

    public LineJoin LineJoin => (LineJoin)_handle->GetLineJoin();

    public float DashOffset => _handle->GetDashOffset();

    public DashStyle DashStyle => (DashStyle)_handle->GetDashStyle();

    public uint DashesCount => _handle->GetDashesCount();

    public void GetDashes(Span<float> dashes)
    {
        fixed (float* f = dashes)
        {
            _handle->GetDashes(f, (uint)dashes.Length);
        }
    }

    public void Dispose() => _handle->Release();

    internal interface Interface : Resource.Interface
    {
        CapStyle StartCap { get; }

        CapStyle EndCap { get; }

        CapStyle DashCap { get; }

        float MiterLimit { get; }

        LineJoin LineJoin { get; }

        float DashOffset { get; }

        DashStyle DashStyle { get; }

        uint DashesCount { get; }

        /// <summary>
        ///  Returns the dashes from the object into a user allocated array.
        ///  Call <see cref="DashesCount"/> to retrieve the required size.
        /// </summary>
        void GetDashes(Span<float> dashes);
    }
}
