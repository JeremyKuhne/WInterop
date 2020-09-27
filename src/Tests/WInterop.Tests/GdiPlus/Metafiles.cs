// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using WInterop.Gdi;
using WInterop.Gdi.Native;
using WInterop.GdiPlus;
using WInterop.GdiPlus.EmfPlus;
using WInterop.GdiPlus.Native;
using Xunit;

namespace WInteropTests.GdiPlusTests
{
    public class Metafiles
    {
        [Fact]
        public unsafe void RecordMetafile()
        {
            GdiPlus.Initialize();

            using DeviceContext deviceContext = Gdi.GetDeviceContext();
            using Stream stream = new MemoryStream();

            using (MetafilePlus record = new MetafilePlus(stream, deviceContext))
            {
                using Graphics graphics = new Graphics(record);
                using Pen pen = new Pen(Color.Blue);
                graphics.DrawRectangle(pen, new Rectangle(10, 10, 10, 10));
            }

            stream.Length.Should().Be(676);
        }

        [Fact]
        public unsafe void GetMetafileHeader()
        {
            GdiPlus.Initialize();

            using DeviceContext deviceContext = Gdi.GetDeviceContext();
            using Stream stream = new MemoryStream();

            using (MetafilePlus record = new MetafilePlus(stream, deviceContext))
            {
                using Graphics graphics = new Graphics(record);
                using Pen pen = new Pen(Color.Blue);
                graphics.DrawRectangle(pen, new Rectangle(10, 10, 10, 10));
            }

            using (MetafilePlus playback = new MetafilePlus(stream))
            {
                using Graphics graphics = new Graphics(deviceContext);
                MetafileHeader header;
                GpStatus status = GdiPlusImports.GdipGetMetafileHeaderFromMetafile(playback, &header);
                status.Should().Be(GpStatus.Ok);
                header.Size.Should().Be(676);
                header.EmfHeader.nRecords.Should().Be(20);
            }
        }

        [Theory]
        [MemberData(nameof(RecordTypesForEmfTypes))]
        public void Enumerate_EmfType_RecordType(
            EmfType emfType,
            RecordType[] recordTypes,
            MetafileRecordType[] emfRecordTypes)
        {
            GdiPlus.Initialize();

            using DeviceContext deviceContext = Gdi.GetDeviceContext();
            using Stream stream = new MemoryStream();

            using (MetafilePlus record = new MetafilePlus(stream, deviceContext, emfType))
            {
                using Graphics graphics = new Graphics(record);
                using Pen pen = new Pen(Color.Blue);
                graphics.DrawRectangle(pen, new Rectangle(10, 10, 10, 10));
            }

            List<RecordType> types = new ();
            List<MetafileRecordType> emfTypes = new ();

            using (MetafilePlus playback = new MetafilePlus(stream))
            {
                using Graphics graphics = new Graphics(deviceContext);

                playback.Enumerate(
                    graphics,
                    (RecordType recordType,
                        uint flags,
                        uint dataSize,
                        IntPtr data,
                        IntPtr callbackData) =>
                    {
                        types.Add(recordType);
                        return true;
                    });

                var status = GdiPlusImports.GdipGetHemfFromMetafile(playback, out HENHMETAFILE hemf);

                using Metafile metafile = new Metafile(hemf);
                metafile.Enumerate((ref MetafileRecord record, nint callbackParameter) =>
                {
                    emfTypes.Add(record.RecordType);
                    return true;
                });
            }

            types.Should().BeEquivalentTo(recordTypes);
            emfTypes.Should().BeEquivalentTo(emfRecordTypes);
        }

        public static TheoryData<EmfType, RecordType[], MetafileRecordType[]> RecordTypesForEmfTypes =>
            new TheoryData<EmfType, RecordType[], MetafileRecordType[]>
            {
                {
                    EmfType.EmfOnly,
                    new RecordType[]
                    {
                        RecordType.EmfHeader,
                        RecordType.EmfSaveDC,
                        RecordType.EmfSetICMMode,
                        RecordType.EmfSetMiterLimit,
                        RecordType.EmfModifyWorldTransform,
                        RecordType.EmfExtCreatePen,
                        RecordType.EmfSelectObject,
                        RecordType.EmfSelectObject,
                        RecordType.EmfPolygon16,
                        RecordType.EmfSelectObject,
                        RecordType.EmfSelectObject,
                        RecordType.EmfModifyWorldTransform,
                        RecordType.EmfDeleteObject,
                        RecordType.EmfSetMiterLimit,
                        RecordType.EmfRestoreDC,
                        RecordType.EmfEof
                    },
                    new MetafileRecordType[]
                    {
                        MetafileRecordType.Header,
                        MetafileRecordType.SaveDC,
                        MetafileRecordType.SetICMMode,
                        MetafileRecordType.SetMiterLimit,
                        MetafileRecordType.ModifyWorldTransform,
                        MetafileRecordType.ExtCreatePen,
                        MetafileRecordType.SelectObject,
                        MetafileRecordType.SelectObject,
                        MetafileRecordType.Polygon16,
                        MetafileRecordType.SelectObject,
                        MetafileRecordType.SelectObject,
                        MetafileRecordType.ModifyWorldTransform,
                        MetafileRecordType.DeleteObject,
                        MetafileRecordType.SetMiterLimit,
                        MetafileRecordType.RestoreDC,
                        MetafileRecordType.Eof
                    }
                },
                {
                    EmfType.EmfPlusOnly,
                    new RecordType[]
                    {
                        RecordType.EmfHeader,
                        RecordType.EmfPlusHeader,
                        RecordType.EmfPlusObject,
                        RecordType.EmfPlusDrawRects,
                        RecordType.EmfPlusEndOfFile,
                        RecordType.EmfEof
                    },
                    new MetafileRecordType[]
                    {
                        MetafileRecordType.Header,
                        MetafileRecordType.GdiComment,
                        MetafileRecordType.GdiComment,
                        MetafileRecordType.SaveDC,
                        MetafileRecordType.SetICMMode,
                        MetafileRecordType.BitBlt,
                        MetafileRecordType.RestoreDC,
                        MetafileRecordType.GdiComment,
                        MetafileRecordType.Eof
                    }
                },
                {
                    EmfType.EmfPlusDual,
                    new RecordType[]
                    {
                        RecordType.EmfHeader,
                        RecordType.EmfPlusHeader,
                        RecordType.EmfPlusObject,
                        RecordType.EmfPlusDrawRects,
                        RecordType.EmfPlusEndOfFile,
                        RecordType.EmfEof
                    },
                    new MetafileRecordType[]
                    {
                        MetafileRecordType.Header,
                        MetafileRecordType.GdiComment,
                        MetafileRecordType.GdiComment,
                        MetafileRecordType.SaveDC,
                        MetafileRecordType.SetICMMode,
                        MetafileRecordType.SetMiterLimit,
                        MetafileRecordType.ModifyWorldTransform,
                        MetafileRecordType.ExtCreatePen,
                        MetafileRecordType.SelectObject,
                        MetafileRecordType.SelectObject,
                        MetafileRecordType.Polygon16,
                        MetafileRecordType.SelectObject,
                        MetafileRecordType.SelectObject,
                        MetafileRecordType.ModifyWorldTransform,
                        MetafileRecordType.DeleteObject,
                        MetafileRecordType.SetMiterLimit,
                        MetafileRecordType.BitBlt,
                        MetafileRecordType.RestoreDC,
                        MetafileRecordType.GdiComment,
                        MetafileRecordType.Eof
                    }
                },
            };
    }
}
