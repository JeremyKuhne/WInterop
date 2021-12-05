// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using TerraFX.Interop.Windows;
using WInterop.Gdi;
using WInterop.GdiPlus.Native;
using Stream = WInterop.Com.Stream;

namespace WInterop.GdiPlus.EmfPlus;

public class MetafilePlus : Image
{
    private readonly Stream _stream;

    /// <summary>
    ///  Creates a record metafile on the given <paramref name="stream"/>.
    /// </summary>
    public MetafilePlus(
        System.IO.Stream stream,
        DeviceContext deviceContext,
        EmfType emfType = EmfType.EmfPlusDual,
        string? description = null)
    {
        _stream = Stream.FromStream(stream);
        _gpImage = CreateRecordMetafile(
            _stream,
            deviceContext,
            emfType,
            description: description);
    }

    /// <summary>
    ///  Creates a playback metafile for the given <paramref name="stream"/>.
    /// </summary>
    public MetafilePlus(System.IO.Stream stream)
    {
        stream.Position = 0;
        _stream = Stream.FromStream(stream);
        GdiPlusImports.GdipCreateMetafileFromStream(_stream, out GpMetafile metafile).ThrowIfFailed();
        _gpImage = (GpImage)metafile;
    }

    private static unsafe GpImage CreateRecordMetafile(
        Stream stream,
        HDC deviceContext,
        EmfType emfType,
        RectangleF? frame = null,
        MetafileFrameUnit frameUnit = MetafileFrameUnit.Gdi,
        string? description = null)
    {
        RectangleF frameRectangle = frame ?? default;

        fixed (char* c = description)
        {
            GdiPlusImports.GdipRecordMetafileStream(
                stream.IStream,
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

    /// <summary>
    ///  Gets the Enhanced Metafile (EMF) for this EMF+. The <see cref="MetafilePlus"/> can not be used after
    ///  calling this method.
    /// </summary>
    public Metafile GetMetafile()
    {
        GdiPlusImports.GdipGetHemfFromMetafile(this, out HENHMETAFILE hemf).ThrowIfFailed();
        return new Metafile(hemf);
    }

    public static implicit operator GpMetafile(MetafilePlus metafile) => (GpMetafile)metafile._gpImage;
}
