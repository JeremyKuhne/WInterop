// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Runtime.InteropServices;

namespace WInterop.GdiPlus.EmfPlus
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusPen
    {
        private readonly byte _data;

        public unsafe readonly MetafilePlusGraphicsVersion* Version
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return (MetafilePlusGraphicsVersion*)b;
                }
            }
        }

        public unsafe readonly uint Type
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(uint*)(b + 4);
                }
            }
        }

        public unsafe MetafilePlusPenData* PenData
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return (MetafilePlusPenData*)(b + 8);
                }
            }
        }

        public unsafe MetafilePlusBrush* BrushObject
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return (MetafilePlusBrush*)(b + GetBrushObjectOffset((MetafilePlusPen*)b));
                }
            }
        }

        public static unsafe int GetBrushObjectOffset(MetafilePlusPen* pen)
        {
            int offset = 20; // MetafilePlusObject header + MetafilePlusPen header ?
            MetafilePlusPenData* penData = (MetafilePlusPenData*)pen->PenData;

            if ((penData->PenDataFlags & PenDataFlags.PenDataTransform) != 0)
                offset += sizeof(MetafilePlusTransformMatrix);

            if ((penData->PenDataFlags & PenDataFlags.PenDataStartCap) != 0)
                offset += sizeof(uint);

            if ((penData->PenDataFlags & PenDataFlags.PenDataEndCap) != 0)
                offset += sizeof(uint);

            if ((penData->PenDataFlags & PenDataFlags.PenDataJoin) != 0)
                offset += sizeof(uint);

            if ((penData->PenDataFlags & PenDataFlags.PenDataMiterLimit) != 0)
                offset += sizeof(float);

            if ((penData->PenDataFlags & PenDataFlags.PenDataLineStyle) != 0)
                offset += sizeof(uint);

            if ((penData->PenDataFlags & PenDataFlags.PenDataDashedLineCap) != 0)
                offset += sizeof(uint);

            if ((penData->PenDataFlags & PenDataFlags.PenDataDashedLineOffset) != 0)
                offset += sizeof(uint);

            if ((penData->PenDataFlags & PenDataFlags.PenDataDashedLine) != 0)
            {
                MetafilePlusDashedLineData* dashedLine = (MetafilePlusDashedLineData*)((byte*)pen + offset);

                offset += 4; // DashedLineData offset
                offset += (int)dashedLine->DashedLineDataSize * sizeof(float);
            }

            if ((penData->PenDataFlags & PenDataFlags.PenDataNonCenter) != 0)
                offset += sizeof(uint);

            if((penData->PenDataFlags & PenDataFlags.PenDataCompoundLine) != 0)
            {
                MetafilePlusCompoundLineData* compoundLine = (MetafilePlusCompoundLineData*)((byte*)pen + offset);

                offset += 4; // CompoundLineData offset
                offset += (int)compoundLine->CompoundLineDataSize * sizeof(float);
            }

            if ((penData->PenDataFlags & PenDataFlags.PenDataCustomStartCap) != 0)
            {
                MetafilePlusCustomStartCapData* startCap = (MetafilePlusCustomStartCapData*)((byte*)pen + offset);

                offset += 4; // CustomLineCap offset
                offset += (int)startCap->CustomStartCapSize;
            }

            if ((penData->PenDataFlags & PenDataFlags.PenDataCustomEndCap) != 0)
            {
                MetafilePlusCustomEndCapData* startCap = (MetafilePlusCustomEndCapData*)((byte*)pen + offset);

                offset += 4; // CustomLineCap offset
                offset += (int)startCap->CustomEndCapSize;
            }

            return offset;
        }

        public static unsafe MetafilePlusPen* GetFromMetafilePlusObject(MetafilePlusObject* emfPlusObject)
        {
            return (MetafilePlusPen*)((byte*)emfPlusObject + (emfPlusObject->Record.Size - emfPlusObject->DataSize));   
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusPenData
    {
        private readonly byte _data;

        public unsafe readonly PenDataFlags PenDataFlags
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(PenDataFlags*)b;
                }
            }
        }

        public unsafe readonly UnitType PenUnit
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(UnitType*)(b + 4);
                }
            }
        }

        public unsafe readonly float PenWidth
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(float*)(b + 8);
                }
            }
        }

        public unsafe MetafilePlusPenOptionalData* OptionalData
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return (MetafilePlusPenOptionalData*)b;
                }
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusPenOptionalData
    {
        private readonly PenDataFlags _penDataFlags;
        private readonly byte _data;

        // TODO: Refactor this.
        private unsafe int GetOptionalDataOffset(PenDataFlags flag)
        {
            int offset = 8;

            if((_penDataFlags & flag) != 0)
            {
                if ((_penDataFlags & PenDataFlags.PenDataTransform) != 0)
                    offset += sizeof(MetafilePlusTransformMatrix);

                if (flag == PenDataFlags.PenDataStartCap)
                    return offset;

                if ((_penDataFlags & PenDataFlags.PenDataStartCap) != 0)
                    offset += sizeof(uint);

                if (flag == PenDataFlags.PenDataEndCap)
                    return offset;

                if ((_penDataFlags & PenDataFlags.PenDataEndCap) != 0)
                    offset += sizeof(uint);

                if (flag == PenDataFlags.PenDataJoin)
                    return offset;

                if ((_penDataFlags & PenDataFlags.PenDataJoin) != 0)
                    offset += sizeof(uint);

                if (flag == PenDataFlags.PenDataMiterLimit)
                    return offset;

                if ((_penDataFlags & PenDataFlags.PenDataMiterLimit) != 0)
                    offset += sizeof(float);

                if (flag == PenDataFlags.PenDataLineStyle)
                    return offset;

                if ((_penDataFlags & PenDataFlags.PenDataLineStyle) != 0)
                    offset += sizeof(uint);

                if (flag == PenDataFlags.PenDataDashedLineCap)
                    return offset;

                if ((_penDataFlags & PenDataFlags.PenDataDashedLineCap) != 0)
                    offset += sizeof(uint);

                if (flag == PenDataFlags.PenDataDashedLineOffset)
                    return offset;

                if ((_penDataFlags & PenDataFlags.PenDataDashedLineOffset) != 0)
                    offset += sizeof(uint);

                if (flag == PenDataFlags.PenDataDashedLine)
                    return offset;

                if ((_penDataFlags & PenDataFlags.PenDataDashedLine) != 0)
                {
                    MetafilePlusDashedLineData* dashedLine = (MetafilePlusDashedLineData*)((byte*)_data + offset);

                    offset += 4; // DashedLineData offset
                    offset += (int)dashedLine->DashedLineDataSize * sizeof(float);
                }

                if (flag == PenDataFlags.PenDataNonCenter)
                    return offset;

                if ((_penDataFlags & PenDataFlags.PenDataNonCenter) != 0)
                    offset += sizeof(uint);

                if (flag == PenDataFlags.PenDataCompoundLine)
                    return offset;

                if ((_penDataFlags & PenDataFlags.PenDataCompoundLine) != 0)
                {
                    MetafilePlusCompoundLineData* compoundLine = (MetafilePlusCompoundLineData*)((byte*)_data + offset);

                    offset += 4; // CompoundLineData offset
                    offset += (int)compoundLine->CompoundLineDataSize * sizeof(float);
                }

                if (flag == PenDataFlags.PenDataCustomStartCap)
                    return offset;

                if ((_penDataFlags & PenDataFlags.PenDataCustomStartCap) != 0)
                {
                    MetafilePlusCustomStartCapData* startCap = (MetafilePlusCustomStartCapData*)((byte*)_data + offset);

                    offset += 4; // CustomLineCap offset
                    offset += (int)startCap->CustomStartCapSize;
                }

                if (flag == PenDataFlags.PenDataCustomEndCap)
                    return offset;

                if ((_penDataFlags & PenDataFlags.PenDataCustomEndCap) != 0)
                {
                    MetafilePlusCustomEndCapData* startCap = (MetafilePlusCustomEndCapData*)((byte*)_data + offset);

                    offset += 4; // CustomLineCap offset
                    offset += (int)startCap->CustomEndCapSize;
                }
            }

            return offset;
        }

        public unsafe MetafilePlusTransformMatrix* TransformMatrix
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return (MetafilePlusTransformMatrix*)b;
                }
            }
        }

        public unsafe LineCapType StartCap
        {
            get
            {
                int offset = GetOptionalDataOffset(PenDataFlags.PenDataStartCap);
                fixed (byte* b = &_data)
                {
                    return *(LineCapType*)(b + offset);
                }
            }
        }

        public unsafe LineCapType EndCap
        {
            get
            {
                int offset = GetOptionalDataOffset(PenDataFlags.PenDataEndCap);
                fixed (byte* b = &_data)
                {
                    return *(LineCapType*)(b + offset);
                }
            }
        }

        public unsafe LineJoinType Join
        {
            get
            {
                int offset = GetOptionalDataOffset(PenDataFlags.PenDataJoin);
                fixed (byte* b = &_data)
                {
                    return *(LineJoinType*)(b + offset);
                }
            }
        }

        public unsafe float MiterLimit
        {
            get
            {
                int offset = GetOptionalDataOffset(PenDataFlags.PenDataMiterLimit);
                fixed (byte* b = &_data)
                {
                    return *(float*)(b + offset);
                }
            }
        }

        public unsafe LineStyle LineStyle
        {
            get
            {
                int offset = GetOptionalDataOffset(PenDataFlags.PenDataLineStyle);
                fixed (byte* b = &_data)
                {
                    return *(LineStyle*)(b + offset);
                }
            }
        }

        public unsafe DashedLineCapType DashedLineCapType
        {
            get
            {
                int offset = GetOptionalDataOffset(PenDataFlags.PenDataDashedLineCap);
                fixed (byte* b = &_data)
                {
                    return *(DashedLineCapType*)(b + offset);
                }
            }
        }

        public unsafe float DashOffset
        {
            get
            {
                int offset = GetOptionalDataOffset(PenDataFlags.PenDataDashedLineOffset);
                fixed (byte* b = &_data)
                {
                    return *(float*)(b + offset);
                }
            }
        }

        public unsafe MetafilePlusDashedLineData* DashedLineData
        {
            get
            {
                int offset = GetOptionalDataOffset(PenDataFlags.PenDataDashedLine);
                fixed (byte* b = &_data)
                {
                    return (MetafilePlusDashedLineData*)(b + offset);
                }
            }
        }

        public unsafe PenAlignment PenAlignment
        {
            get
            {
                int offset = GetOptionalDataOffset(PenDataFlags.PenDataNonCenter);
                fixed (byte* b = &_data)
                {
                    return *(PenAlignment*)(b + offset);
                }
            }
        }

        public unsafe MetafilePlusCompoundLineData* CompoundLineData
        {
            get
            {
                int offset = GetOptionalDataOffset(PenDataFlags.PenDataCompoundLine);
                fixed (byte* b = &_data)
                {
                    return (MetafilePlusCompoundLineData*)b;
                }
            }
        }

        public unsafe MetafilePlusCustomStartCapData* CustomStartCapData
        {
            get
            {
                int offset = GetOptionalDataOffset(PenDataFlags.PenDataCustomStartCap);
                fixed (byte* b = &_data)
                {
                    return (MetafilePlusCustomStartCapData*)(b + offset);
                }
            }
        }

        public unsafe MetafilePlusCustomEndCapData* CustomEndCapData
        {
            get
            {
                int offset = GetOptionalDataOffset(PenDataFlags.PenDataCustomEndCap);
                fixed (byte* b = &_data)
                {
                    return (MetafilePlusCustomEndCapData*)(b + offset);
                }
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusTransformMatrix
    {
        private readonly byte _data;

        // TODO: Implement EmfPlusTransformMatrix
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusDashedLineData
    {
        private readonly byte _data;

        public unsafe readonly uint DashedLineDataSize
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(uint*)b;
                }
            }
        }

        public unsafe readonly float DashedLineData
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(float*)(b + 4);
                }
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusCompoundLineData
    {
        private readonly byte _data;

        public unsafe readonly uint CompoundLineDataSize
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(uint*)b;
                }
            }
        }

        public unsafe readonly float CompoundLineData
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(float*)(b + 4);
                }
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusCustomStartCapData
    {
        private readonly byte _data;

        public unsafe readonly uint CustomStartCapSize
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(uint*)b;
                }
            }
        }

        public unsafe readonly MetafilePlusCustomLineCap CustomStartCap
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(MetafilePlusCustomLineCap*)(b + 4);
                }
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusCustomEndCapData
    {
        private readonly byte _data;

        public unsafe readonly uint CustomEndCapSize
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(uint*)b;
                }
            }
        }

        public unsafe readonly MetafilePlusCustomLineCap CustomEndCap
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(MetafilePlusCustomLineCap*)(b + 4);
                }
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusCustomLineCap
    {
        private readonly byte _data;

        public unsafe readonly MetafilePlusGraphicsVersion* Version
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return (MetafilePlusGraphicsVersion*)b;
                }
            }
        }

        public unsafe readonly uint Type
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(uint*)(b + 4);
                }
            }
        }

        // TODO: Implement CustomLineCapData
    }

    public enum PenDataFlags : uint
    {
        PenDataTransform = 0x0001,
        PenDataStartCap = 0x0002,
        PenDataEndCap = 0x0004,
        PenDataJoin = 0x0008,
        PenDataMiterLimit = 0x0010,
        PenDataLineStyle = 0x0020,
        PenDataDashedLineCap = 0x0040,
        PenDataDashedLineOffset = 0x0080,
        PenDataDashedLine = 0x0100,
        PenDataNonCenter = 0x0200,
        PenDataCompoundLine = 0x0400,
        PenDataCustomStartCap = 0x0800,
        PenDataCustomEndCap = 0x1000
    };

    public enum UnitType : uint
    {
        UnitTypeWorld = 0x00,
        UnitTypeDisplay = 0x01,
        UnitTypePixel = 0x02,
        UnitTypePoint = 0x03,
        UnitTypeInch = 0x04,
        UnitTypeDocument = 0x05,
        UnitTypeMillimeter = 0x06
    }

    public enum LineCapType : int
    {
        LineCapTypeFlat = 0x00000000,
        LineCapTypeSquare = 0x00000001,
        LineCapTypeRound = 0x00000002,
        LineCapTypeTriangle = 0x00000003,
        LineCapTypeNoAnchor = 0x00000010,
        LineCapTypeSquareAnchor = 0x00000011,
        LineCapTypeRoundAnchor = 0x00000012,
        LineCapTypeDiamondAnchor = 0x00000013,
        LineCapTypeArrowAnchor = 0x00000014,
        LineCapTypeAnchorMask = 0x000000F0,
        LineCapTypeCustom = 0x000000FF
    }

    public enum LineJoinType : int
    {
        LineJoinTypeMiter = 0x00000000,
        LineJoinTypeBevel = 0x00000001,
        LineJoinTypeRound = 0x00000002,
        LineJoinTypeMiterClipped = 0x00000003
    }

    public enum LineStyle : int
    {
        LineStyleSolid = 0x00000000,
        LineStyleDash = 0x00000001,
        LineStyleDot = 0x00000002,
        LineStyleDashDot = 0x00000003,
        LineStyleDashDotDot = 0x00000004,
        LineStyleCustom = 0x00000005
    }

    public enum DashedLineCapType : int
    {
        DashedLineCapTypeFlat = 0x00000000,
        DashedLineCapTypeRound = 0x00000002,
        DashedLineCapTypeTriangle = 0x00000003
    }

    public enum PenAlignment : int
    {
        PenAlignmentCenter = 0x00000000,
        PenAlignmentInset = 0x00000001,
        PenAlignmentLeft = 0x00000002,
        PenAlignmentOutset = 0x00000003,
        PenAlignmentRight = 0x00000004
    }
}
