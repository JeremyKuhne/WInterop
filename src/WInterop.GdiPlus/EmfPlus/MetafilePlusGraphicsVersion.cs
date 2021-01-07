// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.GdiPlus.EmfPlus
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusGraphicsVersion
    {
        public const uint MetafileSignatureMask     = 0b1111_1111_1111_1111_1111_0000_0000_0000;
        public const uint GraphicsVersionMask       = 0b0000_0000_0000_0000_0000_1111_1111_1111;

        private readonly byte _data;

        public unsafe readonly uint MetafileSignature
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return (*(uint*)(b) & MetafileSignatureMask) >> 12;
                }
            }
        }

        public readonly GraphicsVersionEnum GraphicsVersion => (GraphicsVersionEnum)(_data & GraphicsVersionMask);
    }

    public enum GraphicsVersionEnum : uint
    {
        GraphicsVersion1 = 0x0001,
        GraphicsVersion1_1 = 0x0002
    }
}
