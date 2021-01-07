using System;
using System.Runtime.InteropServices;

namespace WInterop.GdiPlus.EmfPlus
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusDrawLines
    {
        public const ushort CompressedDataFlag          = 0b0100_0000_0000_0000;
        public const ushort ExtraLineFlag               = 0b0010_0000_0000_0000;
        public const ushort RelativeLocationFlag        = 0b0001_0000_0000_0000;
        public const ushort ObjectIdMask                = 0b0000_0000_1111_1111;

        public readonly MetafilePlusRecord Record;

        private readonly byte _data;

        public unsafe uint Count
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(uint*)b;
                }
            }
        }

        public uint Size
        {
            get
            {
                if (RelativeLocation)
                {
                    return (Count * 0x00000002) + 0x00000010;
                }
                else if (CompressedData)
                {
                    return (Count * 0x00000004) + 0x00000010;
                }
                else
                {
                    return (Count * 0x00000008) + 0x00000010;
                }
            }
        }


        public uint DataSize
        {
            get
            {
                if (RelativeLocation)
                {
                    return (Count * 0x00000002) + 0x00000004;
                }
                else if (CompressedData)
                {
                    return (Count * 0x00000004) + 0x00000004;
                }
                else
                {
                    return (Count * 0x00000008) + 0x00000004;
                }
            }
        }

        public unsafe byte[] PointData
        {
            get
            {
                byte[] pointData = new byte[DataSize - 4];
                fixed (byte* ptr = &_data)
                {
                    for (int i = 0; 4 + i < DataSize; i++)
                    {
                        pointData[i] = ptr[4 + i];
                    }
                }

                return pointData;
            }
        }

        public bool CompressedData => (Record.Flags & CompressedDataFlag) != 0;
        public bool ExtraLine => (Record.Flags & ExtraLineFlag) != 0;
        public bool RelativeLocation => (Record.Flags & RelativeLocationFlag) != 0;
        public int ObjectId => Record.Flags & ObjectIdMask;

        public MetafilePlusPoint GetPoint(int index)
        {
            int offset = index * 4;
            MetafilePlusPoint point = new ();
            point.X = BitConverter.ToUInt16(PointData, offset);
            point.Y = BitConverter.ToUInt16(PointData, offset + 2);
            return point;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MetafilePlusPointR
    {

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MetafilePlusPoint
    {
        public ushort X;
        public ushort Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MetafilePlusPointF
    {
        public uint X;
        public uint Y;
    }
}
