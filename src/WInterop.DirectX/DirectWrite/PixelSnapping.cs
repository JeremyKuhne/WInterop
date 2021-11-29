// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The <see cref="PixelSnapping"/> interface defines the pixel snapping properties of a text renderer.
    ///  [IDWritePixelSnapping]
    /// </summary>
    [Guid(InterfaceIds.IID_IDWritePixelSnapping)]
    public unsafe readonly struct PixelSnapping : PixelSnapping.Interface, IDisposable
    {
        private readonly IDWritePixelSnapping* _handle;

        internal PixelSnapping(IDWritePixelSnapping* handle) => _handle = handle;

        public Matrix3x2 GetCurrentTransform(IntPtr clientDrawingContext)
        {
            Matrix3x2 matrix;
            _handle->GetCurrentTransform((void*)clientDrawingContext, (DWRITE_MATRIX*)&matrix).ThrowIfFailed();
            return matrix;
        }

        public float GetPixelsPerDip(IntPtr clientDrawingContext)
        {
            float pixels;
            _handle->GetPixelsPerDip((void*)clientDrawingContext, &pixels).ThrowIfFailed();
            return pixels;
        }

        public bool IsPixelSnappingDisabled(IntPtr clientDrawingContext)
        {
            TerraFX.Interop.Windows.BOOL result;
            _handle->IsPixelSnappingDisabled((void*)clientDrawingContext, &result).ThrowIfFailed();
            return result;
        }

        internal static ref PixelSnapping From<TFrom>(in TFrom from)
            where TFrom : unmanaged, Interface
            => ref Unsafe.AsRef<PixelSnapping>(Unsafe.AsPointer(ref Unsafe.AsRef(from)));

        public void Dispose() => _handle->Release();

        internal interface Interface
        {
            /// <summary>
            ///  Determines whether pixel snapping is disabled. The recommended default is FALSE,
            ///  unless doing animation that requires subpixel vertical placement.
            /// </summary>
            /// <param name="clientDrawingContext">The context passed to IDWriteTextLayout::Draw.</param>
            bool IsPixelSnappingDisabled(IntPtr clientDrawingContext);

            /// <summary>
            ///  Gets the current transform that maps abstract coordinates to DIPs,
            ///  which may disable pixel snapping upon any rotation or shear.
            /// </summary>
            /// <param name="clientDrawingContext">The context passed to IDWriteTextLayout::Draw.</param>
            Matrix3x2 GetCurrentTransform(IntPtr clientDrawingContext);

            /// <summary>
            ///  Gets the number of physical pixels per DIP. A DIP (device-independent pixel) is 1/96 inch,
            ///  so the pixelsPerDip value is the number of logical pixels per inch divided by 96 (yielding
            ///  a value of 1 for 96 DPI and 1.25 for 120).
            /// </summary>
            /// <param name="clientDrawingContext">The context passed to IDWriteTextLayout::Draw.</param>
            float GetPixelsPerDip(IntPtr clientDrawingContext);
        }
    }
}
