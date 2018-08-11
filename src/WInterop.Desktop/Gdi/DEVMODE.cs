// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/dd183565.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DEVMODE
    {
        public unsafe static ushort s_size = (ushort)sizeof(DEVMODE);

        private const int CCHDEVICENAME = 32;
        private const int CCHFORMNAME = 32;

        private FixedString.Size32 _dmDeviceName;
        public Span<char> dmDeviceName => _dmDeviceName.Buffer;
        public ushort dmSpecVersion;
        public ushort dmDriverVersion;
        public ushort dmSize;
        public ushort dmDriverExtra;
        public Fields dmFields;
        public DeviceSpecific deviceSpecific;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        private FixedString.Size32 _dmFormName;
        public Span<char> dmFormName => _dmFormName.Buffer;
        public ushort dmLogPixels;
        public uint dmBitsPerPel;
        public uint dmPelsWidth;
        public uint dmPelsHeight;
        public DisplayFlagsOrNup displayFlagsOrNup;
        public uint dmDisplayFrequency;
        public uint dmICMMethod;
        public uint dmICMIntent;
        public uint dmMediaType;
        public uint dmDitherType;
        public uint dmReserved1;
        public uint dmReserved2;
        public uint dmPanningWidth;
        public uint dmPanningHeight;


        [StructLayout(LayoutKind.Explicit)]
        public struct DisplayFlagsOrNup
        {
            [FieldOffset(0)]
            public DisplayFlags dmDisplayFlags;
            [FieldOffset(0)]
            public uint dmNup;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DeviceSpecific
        {
            [FieldOffset(0)]
            public PrinterSpecific Printer;

            [FieldOffset(0)]
            public DisplaySpecific Display;
        }

        public struct PrinterSpecific
        {
            public Orientation dmOrientation;
            public short dmPaperSize;
            public short dmPaperLength;
            public short dmPaperWidth;
            public short dmScale;
            public short dmCopies;
            public short dmDefaultSource;
            public short dmPrintQuality;
        }

        public struct DisplaySpecific
        {
            public POINTL dmPosition;
            public DisplayOrientation dmDisplayOrientation;
            public FixedOutput dmDisplayFixedOutput;
        }

        [Flags]
        public enum Fields : uint
        {
            DM_ORIENTATION = 0x00000001,
            DM_PAPERSIZE = 0x00000002,
            DM_PAPERLENGTH = 0x00000004,
            DM_PAPERWIDTH = 0x00000008,
            DM_SCALE = 0x00000010,
            DM_POSITION = 0x00000020,
            DM_NUP = 0x00000040,
            DM_DISPLAYORIENTATION = 0x00000080,
            DM_COPIES = 0x00000100,
            DM_DEFAULTSOURCE = 0x00000200,
            DM_PRINTQUALITY = 0x00000400,
            DM_COLOR = 0x00000800,
            DM_DUPLEX = 0x00001000,
            DM_YRESOLUTION = 0x00002000,
            DM_TTOPTION = 0x00004000,
            DM_COLLATE = 0x00008000,
            DM_FORMNAME = 0x00010000,
            DM_LOGPIXELS = 0x00020000,
            DM_BITSPERPEL = 0x00040000,
            DM_PELSWIDTH = 0x00080000,
            DM_PELSHEIGHT = 0x00100000,
            DM_DISPLAYFLAGS = 0x00200000,
            DM_DISPLAYFREQUENCY = 0x00400000,
            DM_ICMMETHOD = 0x00800000,
            DM_ICMINTENT = 0x01000000,
            DM_MEDIATYPE = 0x02000000,
            DM_DITHERTYPE = 0x04000000,
            DM_PANNINGWIDTH = 0x08000000,
            DM_PANNINGHEIGHT = 0x10000000,
            DM_DISPLAYFIXEDOUTPUT = 0x20000000
        }

        public enum Orientation : short
        {
            DMORIENT_PORTRAIT = 1,
            DMORIENT_LANDSCAPE = 2
        }

        public enum DisplayOrientation : uint
        {
            DMDO_DEFAULT = 0,
            DMDO_90 = 1,
            DMDO_180 = 2,
            DMDO_270 = 3
        }

        public enum FixedOutput : uint
        {
            DMDFO_DEFAULT = 0,
            DMDFO_STRETCH = 1,
            DMDFO_CENTER = 2
        }

        [Flags]
        public enum DisplayFlags
        {
            DM_INTERLACED = 0x00000002,
            DMDISPLAYFLAGS_TEXTMODE = 0x00000004
        }
    }
}
