// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.InteropServices;

namespace WInterop.Gdi;

/// <summary>
///  [DEVMODE]
/// </summary>
/// <docs>https://docs.microsoft.com/windows/win32/api/wingdi/ns-wingdi-devmodew</docs>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct DeviceMode
{
    public static unsafe DeviceMode Create() => new() { Size = (ushort)sizeof(DeviceMode) };

    private const int CCHDEVICENAME = 32;
    private const int CCHFORMNAME = 32;

    private FixedString.Size32 _dmDeviceName;

    public Span<char> DeviceName => _dmDeviceName.Buffer;
    public ushort SpecVersion;
    public ushort DriverVersion;
    public ushort Size;
    public ushort DriverExtra;
    public FieldsFlags Fields;
    public DeviceSpecificUnion DeviceSpecific;
    public short Color;
    public short Duplex;
    public short YResolution;
    public short TTOption;
    public short Collate;
    private FixedString.Size32 _dmFormName;
    public Span<char> FormName => _dmFormName.Buffer;
    public ushort LogicalPixels;
    public uint BitsPerPixel;
    public uint PixelsWidth;
    public uint PixelsHeight;
    public DisplayFlagsOrNupUnion DisplayFlagsOrNup;
    public uint DisplayFrequency;
    public uint ICMMethod;
    public uint ICMIntent;
    public uint MediaType;
    public uint DitherType;
    public uint Reserved1;
    public uint Reserved2;
    public uint PanningWidth;
    public uint PanningHeight;

    [StructLayout(LayoutKind.Explicit)]
    public struct DisplayFlagsOrNupUnion
    {
        [FieldOffset(0)]
        public DisplayFlags DisplayFlags;
        [FieldOffset(0)]
        public uint Nup;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct DeviceSpecificUnion
    {
        [FieldOffset(0)]
        public PrinterSpecific Printer;

        [FieldOffset(0)]
        public DisplaySpecific Display;
    }

    public struct PrinterSpecific
    {
        public Orientation Orientation;
        public short PaperSize;
        public short PaperLength;
        public short PaperWidth;
        public short Scale;
        public short Copies;
        public short DefaultSource;
        public short PrintQuality;
    }

    public struct DisplaySpecific
    {
        public Point Position;
        public DisplayOrientation DisplayOrientation;
        public FixedOutput DisplayFixedOutput;
    }

    [Flags]
    public enum FieldsFlags : uint
    {
        Orientation = 0x00000001,
        PaperSize = 0x00000002,
        PaperLength = 0x00000004,
        PaperWidth = 0x00000008,
        Scale = 0x00000010,
        Position = 0x00000020,
        NUp = 0x00000040,
        DisplayOrientation = 0x00000080,
        Copies = 0x00000100,
        DefaultSource = 0x00000200,
        PrintQuality = 0x00000400,
        Color = 0x00000800,
        Duplex = 0x00001000,
        YResolution = 0x00002000,
        TrueTypeOption = 0x00004000,
        Collate = 0x00008000,
        FormName = 0x00010000,
        LogicalPixels = 0x00020000,
        BitsPerPixel = 0x00040000,
        PixelsWidth = 0x00080000,
        PixelsHeight = 0x00100000,
        DisplayFlags = 0x00200000,
        DisplayFrequency = 0x00400000,
        IcmMethod = 0x00800000,
        IcmIntent = 0x01000000,
        MediaType = 0x02000000,
        DitherType = 0x04000000,
        PanningWidth = 0x08000000,
        PanningHeight = 0x10000000,
        DisplayFixedOutput = 0x20000000
    }

    public enum Orientation : short
    {
        /// <summary>
        ///  [DMORIENT_PORTRAIT]
        /// </summary>
        Portrait = 1,

        /// <summary>
        ///  [DMORIENT_LANDSCAPE]
        /// </summary>
        Landscape = 2
    }

    public enum DisplayOrientation : uint
    {
        /// <summary>
        ///  [DMDO_DEFAULT]
        /// </summary>
        Default = 0,

        /// <summary>
        ///  [DMDO_90]
        /// </summary>
        Rotate90 = 1,

        /// <summary>
        ///  [DMDO_180]
        /// </summary>
        Rotate180 = 2,

        /// <summary>
        ///  [DMDO_270]
        /// </summary>
        Rotate270 = 3
    }

    public enum FixedOutput : uint
    {
        /// <summary>
        ///  [DMDFO_DEFAULT]
        /// </summary>
        Default = 0,

        /// <summary>
        ///  [DMDFO_STRETCH]
        /// </summary>
        Stretch = 1,

        /// <summary>
        ///  [DMDFO_CENTER]
        /// </summary>
        Center = 2
    }

    [Flags]
    public enum DisplayFlags
    {
        /// <summary>
        ///  [DM_INTERLACED]
        /// </summary>
        Interlaced = 0x00000002,

        /// <summary>
        ///  [DMDISPLAYFLAGS_TEXTMODE]
        /// </summary>
        TextMode = 0x00000004
    }
}