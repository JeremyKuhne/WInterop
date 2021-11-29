// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Root bitmap resource, linearly scaled on a draw call. [ID2D1Bitmap]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Guid(InterfaceIds.IID_ID2D1Bitmap)]
    public readonly unsafe struct Bitmap : Bitmap.Interface, IDisposable
    {
        internal readonly ID2D1Bitmap* _handle;

        internal Bitmap(ID2D1Bitmap* handle) => _handle = handle;

        public unsafe Factory GetFactory() => Resource.From(this).GetFactory();

        public SizeF Size => _handle->GetSize().ToSizeF();

        public SizeU PixelSize => new(_handle->GetPixelSize());

        public PixelFormat PixelFormat => new(_handle->GetPixelFormat());

        public PointF Dpi
        {
            get
            {
                float x;
                float y;
                _handle->GetDpi(&x, &y);
                return new(x, y);
            }
        }

        public void CopyFromBitmap(PointU destinationPoint, Bitmap bitmap, Rectangle sourceRectangle)
        {
            var rect = sourceRectangle.ToD2D();
            _handle->CopyFromBitmap(
                (D2D_POINT_2U*)&destinationPoint,
                bitmap._handle,
                &rect).ThrowIfFailed();
        }

        public void CopyFromRenderTarget(PointU destinationPoint, IRenderTarget renderTarget, Rectangle sourceRectangle)
        {
            var rect = sourceRectangle.ToD2D();
            _handle->CopyFromRenderTarget(
                (D2D_POINT_2U*)&destinationPoint,
                ((IResource<ID2D1RenderTarget>)renderTarget).Handle,
                &rect).ThrowIfFailed();
        }

        public void CopyFromMemory(Rectangle destinationRectangle, void* sourceData, uint pitch)
        {
            var rect = destinationRectangle.ToD2D();
            _handle->CopyFromMemory(
                &rect,
                sourceData,
                pitch).ThrowIfFailed();
        }

        public void Dispose() => _handle->Release();


        public static implicit operator Image(Bitmap bitmap) => new((ID2D1Image*)bitmap._handle);

        internal interface Interface : Resource.Interface
        {
            /// <summary>
            ///  Returns the size of the bitmap in resolution independent units.
            /// </summary>
            SizeF Size { get; }

            /// <summary>
            ///  Returns the size of the bitmap in resolution dependent units, (pixels).
            /// </summary>
            SizeU PixelSize { get; }

            /// <summary>
            ///  Retrieve the format of the bitmap.
            /// </summary>
            PixelFormat PixelFormat { get; }

            /// <summary>
            ///  Return the DPI of the bitmap.
            /// </summary>
            PointF Dpi { get; }

            void CopyFromBitmap(
                PointU destinationPoint,
                Bitmap bitmap,
                Rectangle sourceRectangle);

            void CopyFromRenderTarget(
                PointU destinationPoint,
                IRenderTarget renderTarget,
                Rectangle sourceRectangle);

            void CopyFromMemory(
                Rectangle destinationRectangle,
                void* sourceData,
                uint pitch);
        }
    }
}
