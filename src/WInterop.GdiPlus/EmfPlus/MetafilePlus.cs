// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Drawing.Internal;
using System.IO;
using WInterop.Com;
using WInterop.Gdi;
using WInterop.GdiPlus.Native;

namespace WInterop.GdiPlus.EmfPlus
{
    public class MetafilePlus : Image
    {
        private readonly IStream _stream;

        /// <summary>
        ///  Creates a record metafile on the given <paramref name="stream"/>.
        /// </summary>
        public MetafilePlus(
            Stream stream,
            DeviceContext deviceContext,
            EmfType emfType = EmfType.EmfPlusDual,
            string? description = null)
        {
            _stream = new DotNetStream(stream);
            _gpImage = CreateRecordMetafile(
                _stream,
                deviceContext,
                emfType,
                description: description);
        }

        /// <summary>
        ///  Creates a playback metafile for the given <paramref name="stream"/>.
        /// </summary>
        public MetafilePlus(Stream stream)
        {
            stream.Position = 0;
            _stream = new DotNetStream(stream);
            GdiPlusImports.GdipCreateMetafileFromStream(_stream, out GpMetafile metafile).ThrowIfFailed();
            _gpImage = (GpImage)metafile;
        }

        private static unsafe GpImage CreateRecordMetafile(
            IStream stream,
            Gdi.Native.HDC deviceContext,
            EmfType emfType,
            RectangleF? frame = null,
            MetafileFrameUnit frameUnit = MetafileFrameUnit.Gdi,
            string? description = null)
        {
            RectangleF frameRectangle = frame ?? default;

            fixed (char* c = description)
            {
                GdiPlusImports.GdipRecordMetafileStream(
                    stream,
                    deviceContext,
                    emfType,
                    frame.HasValue ? &frameRectangle : null,
                    frameUnit,
                    c,
                    out GpMetafile metafile).ThrowIfFailed();

                return (GpImage)metafile;
            }
        }

        public void Enumerate(Graphics graphics, EnumerateMetafilePlusCallback callback, IntPtr callbackData = default)
        {
            PointF point = default;
            GdiPlusImports.GdipEnumerateMetafileDestPoint(
                graphics,
                (GpMetafile)_gpImage,
                ref point,
                callback,
                callbackData,
                default).ThrowIfFailed();
        }

        public static implicit operator GpMetafile(MetafilePlus metafile) => (GpMetafile)metafile._gpImage;
    }
}
