// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Runtime.InteropServices;

namespace WInterop.GdiPlus.EmfPlus
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusBrush
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

        public unsafe readonly BrushType Type
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(BrushType*)(b + 4);
                }
            }
        }

        public unsafe readonly MetafilePlusSolidBrushData* BrushData
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return (MetafilePlusSolidBrushData*)(b + 8);
                }
            }
        }

        // TODO: Implement BrushData
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusSolidBrushData
    {
        private readonly byte _data;

        public unsafe readonly MetafilePlusARGB SolidColor
        {
            get
            {
                fixed (byte* b = &_data)
                {
                    return *(MetafilePlusARGB*)(b);
                }
            }
        }
    }

    public enum BrushType : uint
    {
        BrushTypeSolidColor = 0x00000000,
        BrushTypeHatchFill = 0x00000001,
        BrushTypeTextureFill = 0x00000002,
        BrushTypePathGradient = 0x00000003,
        BrushTypeLinearGradient = 0x00000004
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusARGB
    {
        public readonly byte Blue;
        public readonly byte Green;
        public readonly byte Red;
        public readonly byte Alpha;
    }
}
