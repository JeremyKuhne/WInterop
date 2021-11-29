// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.GdiPlus.EmfPlus;

[StructLayout(LayoutKind.Sequential)]
public readonly ref struct MetafilePlusObject
{
    public const ushort ContinueObjectFlag = 0b1000_0000_0000_0000;
    public const ushort ObjectTypeMask = 0b0111_1111_0000_0000;
    public const ushort ObjectIdMask = 0b0000_0000_1111_1111;

    public readonly MetafilePlusRecord Record;

    private readonly byte _data;

    public uint TotalObjectSize => Continued ? Record.DataSize : 0;
    public unsafe uint DataSize
    {
        get
        {
            if (!Continued)
            {
                return Record.DataSize;
            }

            fixed (byte* b = &_data)
            {
                return *(uint*)b;
            }
        }
    }

    public MetafilePlusObjectType ObjectType => (MetafilePlusObjectType)((Record.Flags & ObjectTypeMask) >> 8);
    public int ObjectId => Record.Flags & ObjectIdMask;
    public bool Continued => (Record.Flags & ContinueObjectFlag) != 0;
}
