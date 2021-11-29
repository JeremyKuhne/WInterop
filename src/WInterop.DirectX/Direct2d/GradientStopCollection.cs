// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

/// <summary>
///  Represents an collection of gradient stops that can then be the source resource
///  for either a linear or radial gradient brush. [ID2D1GradientStopCollection]
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[Guid(InterfaceIds.IID_ID2D1GradientStopCollection)]
public readonly unsafe struct GradientStopCollection : GradientStopCollection.Interface, IDisposable
{
    internal readonly ID2D1GradientStopCollection* _handle;

    internal GradientStopCollection(ID2D1GradientStopCollection* collection) => _handle = collection;

    public Factory GetFactory() => Resource.From(this).GetFactory();

    public Gamma GetColorInterpolationGamma() => (Gamma)_handle->GetColorInterpolationGamma();

    public ExtendMode GetExtendMode() => (ExtendMode)_handle->GetExtendMode();

    public uint GetGradientStopCount() => _handle->GetGradientStopCount();

    public void GetGradientStops(Span<GradientStop> gradientStops)
    {
        fixed (GradientStop* g = gradientStops)
        {
            _handle->GetGradientStops((D2D1_GRADIENT_STOP*)g, (uint)gradientStops.Length);
        }
    }

    public void Dispose() => _handle->Release();

    internal interface Interface : Resource.Interface
    {
        /// <summary>
        ///  Returns the number of stops in the gradient.
        /// </summary>
        uint GetGradientStopCount();

        /// <summary>
        ///  Copies the gradient stops from the collection into the caller's interface.  The
        ///  returned colors have straight alpha.
        /// </summary>
        unsafe void GetGradientStops(Span<GradientStop> gradientStops);

        /// <summary>
        ///  Returns whether the interpolation occurs with 1.0 or 2.2 gamma.
        /// </summary>
        Gamma GetColorInterpolationGamma();

        ExtendMode GetExtendMode();
    }
}
