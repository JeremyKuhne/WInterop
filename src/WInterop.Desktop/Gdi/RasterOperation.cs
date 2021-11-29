// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

public readonly struct RasterOperation
{
    // https://msdn.microsoft.com/en-us/library/dd145130.aspx
    // https://msdn.microsoft.com/en-us/library/dd183370.aspx

    public readonly uint Value;

    public RasterOperation(Common operation)
    {
        Value = (uint)operation;
    }

    public static implicit operator RasterOperation(Common operation) => new RasterOperation(operation);

    /// <summary>
    ///  These are the patterns that are given defines in Windows.
    /// </summary>
    public enum Common : uint
    {
        // Ternary 20 is A0

        /// <summary>
        ///  Destination pixels are replaced with the source. [SRCCOPY]
        /// </summary>
        SourceCopy = 0x00CC0020,

        /// <summary>
        ///  Destination pixels are OR'ed with the source. [SRCPAINT]
        /// </summary>
        SourcePaint = 0x00EE0086,

        /// <summary>
        ///  Destination pixels are AND'ed with the source. [SRCAND]
        /// </summary>
        SourceAnd = 0x008800C6,

        /// <summary>
        ///  Destination pixels are XOR'ed with the source. [SRCINVERT]
        /// </summary>
        SourceInvert = 0x00660046,

        /// <summary>
        ///  Destination pixels are inverted and AND'ed with the source.
        ///  [SRCERASE]
        /// </summary>
        SourceErase = 0x00440328,

        /// <summary>
        ///  Destination pixels are replaced with the inverted source. [NOTSRCCOPY]
        /// </summary>
        NotSourceCopy = 0x00330008,

        /// <summary>
        ///  Destination pixels are replaced with the inverted source AND'ed with the inverted selected brush.
        ///  [NOTSRCERASE]
        /// </summary>
        NotSourceErase = 0x001100A6,

        /// <summary>
        ///  Destination pixels are replaced with the source AND'ed with the selected brush. [MERGECOPY]
        /// </summary>
        MergeCopy = 0x00C000CA,

        /// <summary>
        ///  Destination pixels are OR'ed with the inverted source. [MERGEPAINT]
        /// </summary>
        MergePaint = 0x00BB0226,

        /// <summary>
        ///  Destination pixels are replaced with the selected brush. [PATCOPY]
        /// </summary>
        PatternCopy = 0x00F00021,

        /// <summary>
        ///  Destination pixels are OR'ed with the inverted source OR'ed with the selected brush. [PATPAINT]
        /// </summary>
        PatternPaint = 0x00FB0A09,

        /// <summary>
        ///  Destination pixels are XOR'ed with the selected brush. [PATINVERT]
        /// </summary>
        PatternInvert = 0x005A0049,

        /// <summary>
        ///  Destination pixels are inverted. [DSTINVERT]
        /// </summary>
        DestinationInvert = 0x00550009,

        /// <summary>
        ///  Destination pixels are set to black. [BLACKNESS]
        /// </summary>
        Blackness = 0x00000042,

        /// <summary>
        ///  Destination pixels are set to white. [WHITENESS]
        /// </summary>
        Whiteness = 0x00FF0062
    }

    public enum All : uint
    {
        DDx = 0x00000042,
        DPSoon = 0x00010289,
        DPSona = 0x00020C89,
        PSon = 0x000300AA,
        SDPona = 0x00040C88,
        DPon = 0x000500A9,
        PDSxnon = 0x00060865,
        PDSaon = 0x000702C5,
        SDPnaa = 0x00080F08,
        PDSxon = 0x00090245,
        DPna = 0x000A0329,
        PSDnaon = 0x000B0B2A,
        SPna = 0x000C0324,
        PDSnaon = 0x000D0B25,
        PDSonon = 0x000E08A5,
        Pn = 0x000F0001,
        PDSona = 0x00100C85,
        DSon = 0x001100A6,
        SDPxnon = 0x00120868,
        SDPaon = 0x001302C8,
        DPSxnon = 0x00140869,
        DPSaon = 0x001502C9,
        PSDPSanaxx = 0x00165CCA,
        SSPxDSxaxn = 0x00171D54,
        SPxPDxa = 0x00180D59,
        SDPSanaxn = 0x00191CC8,
        PDSPaox = 0x001A06C5,
        SDPSxaxn = 0x001B0768,
        PSDPaox = 0x001C06CA,
        DSPDxaxn = 0x001D0766,
        PDSox = 0x001E01A5,
        PDSoan = 0x001F0385,
        DPSnaa = 0x00200F09,
        SDPxon = 0x00210248,
        DSna = 0x00220326,
        SPDnaon = 0x00230B24,
        SPxDSxa = 0x00240D55,
        PDSPanaxn = 0x00251CC5,
        SDPSaox = 0x002606C8,
        SDPSxnox = 0x00271868,
        DPSxa = 0x00280369,
        PSDPSaoxxn = 0x002916CA,
        DPSana = 0x002A0CC9,
        SSPxPDxaxn = 0x002B1D58,
        SPDSoax = 0x002C0784,
        PSDnox = 0x002D060A,
        PSDPxox = 0x002E064A,
        PSDnoan = 0x002F0E2A,
        PSna = 0x0030032A,
        SDPnaon = 0x00310B28,
        SDPSoox = 0x00320688,
        Sn = 0x00330008,
        SPDSaox = 0x003406C4,
        SPDSxnox = 0x00351864,
        SDPox = 0x003601A8,
        SDPoan = 0x00370388,
        PSDPoax = 0x0038078A,
        SPDnox = 0x00390604,
        SPDSxox = 0x003A0644,
        SPDnoan = 0x003B0E24,
        PSx = 0x003C004A,
        SPDSonox = 0x003D18A4,
        SPDSnaox = 0x003E1B24,
        PSan = 0x003F00EA,
        PSDnaa = 0x00400F0A,
        DPSxon = 0x00410249,
        SDxPDxa = 0x00420D5D,
        SPDSanaxn = 0x00431CC4,
        SDna = 0x00440328,
        DPSnaon = 0x00450B29,
        DSPDaox = 0x004606C6,
        PSDPxaxn = 0x0047076A,
        SDPxa = 0x00480368,
        PDSPDaoxxn = 0x004916C5,
        DPSDoax = 0x004A0789,
        PDSnox = 0x004B0605,
        SDPana = 0x004C0CC8,
        SSPxDSxoxn = 0x004D1954,
        PDSPxox = 0x004E0645,
        PDSnoan = 0x004F0E25,
        PDna = 0x00500325,
        DSPnaon = 0x00510B26,
        DPSDaox = 0x005206C9,
        SPDSxaxn = 0x00530764,
        DPSonon = 0x005408A9,
        Dn = 0x00550009,
        DPSox = 0x005601A9,
        DPSoan = 0x00570389,
        PDSPoax = 0x00580785,
        DPSnox = 0x00590609,
        DPx = 0x005A0049,
        DPSDonox = 0x005B18A9,
        DPSDxox = 0x005C0649,
        DPSnoan = 0x005D0E29,
        DPSDnaox = 0x005E1B29,
        DPan = 0x005F00E9,
        PDSxa = 0x00600365,
        DSPDSaoxxn = 0x006116C6,
        DSPDoax = 0x00620786,
        SDPnox = 0x00630608,
        SDPSoax = 0x00640788,
        DSPnox = 0x00650606,
        DSx = 0x00660046,
        SDPSonox = 0x006718A8,
        DSPDSonoxxn = 0x006858A6,
        PDSxxn = 0x00690145,
        DPSax = 0x006A01E9,
        PSDPSoaxxn = 0x006B178A,
        SDPax = 0x006C01E8,
        PDSPDoaxxn = 0x006D1785,
        SDPSnoax = 0x006E1E28,
        PDSxnan = 0x006F0C65,
        PDSana = 0x00700CC5,
        SSDxPDxaxn = 0x00711D5C,
        SDPSxox = 0x00720648,
        SDPnoan = 0x00730E28,
        DSPDxox = 0x00740646,
        DSPnoan = 0x00750E26,
        SDPSnaox = 0x00761B28,
        DSan = 0x007700E6,
        PDSax = 0x007801E5,
        DSPDSoaxxn = 0x00791786,
        DPSDnoax = 0x007A1E29,
        SDPxnan = 0x007B0C68,
        SPDSnoax = 0x007C1E24,
        DPSxnan = 0x007D0C69,
        SPxDSxo = 0x007E0955,
        DPSaan = 0x007F03C9,
        DPSaa = 0x008003E9,
        SPxDSxon = 0x00810975,
        DPSxna = 0x00820C49,
        SPDSnoaxn = 0x00831E04,
        SDPxna = 0x00840C48,
        PDSPnoaxn = 0x00851E05,
        DSPDSoaxx = 0x008617A6,
        PDSaxn = 0x008701C5,
        DSa = 0x008800C6,
        SDPSnaoxn = 0x00891B08,
        DSPnoa = 0x008A0E06,
        DSPDxoxn = 0x008B0666,
        SDPnoa = 0x008C0E08,
        SDPSxoxn = 0x008D0668,
        SSDxPDxax = 0x008E1D7C,
        PDSanan = 0x008F0CE5,
        PDSxna = 0x00900C45,
        SDPSnoaxn = 0x00911E08,
        DPSDPoaxx = 0x009217A9,
        SPDaxn = 0x009301C4,
        PSDPSoaxx = 0x009417AA,
        DPSaxn = 0x009501C9,
        DPSxx = 0x00960169,
        PSDPSonoxx = 0x0097588A,
        SDPSonoxn = 0x00981888,
        DSxn = 0x00990066,
        DPSnax = 0x009A0709,
        SDPSoaxn = 0x009B07A8,
        SPDnax = 0x009C0704,
        DSPDoaxn = 0x009D07A6,
        DSPDSaoxx = 0x009E16E6,
        PDSxan = 0x009F0345,
        DPa = 0x00A000C9,
        PDSPnaoxn = 0x00A11B05,
        DPSnoa = 0x00A20E09,
        DPSDxoxn = 0x00A30669,
        PDSPonoxn = 0x00A41885,
        PDxn = 0x00A50065,
        DSPnax = 0x00A60706,
        PDSPoaxn = 0x00A707A5,
        DPSoa = 0x00A803A9,
        DPSoxn = 0x00A90189,
        D = 0x00AA0029,
        DPSono = 0x00AB0889,
        SPDSxax = 0x00AC0744,
        DPSDaoxn = 0x00AD06E9,
        DSPnao = 0x00AE0B06,
        DPno = 0x00AF0229,
        PDSnoa = 0x00B00E05,
        PDSPxoxn = 0x00B10665,
        SSPxDSxox = 0x00B21974,
        SDPanan = 0x00B30CE8,
        PSDnax = 0x00B4070A,
        DPSDoaxn = 0x00B507A9,
        DPSDPaoxx = 0x00B616E9,
        SDPxan = 0x00B70348,
        PSDPxax = 0x00B8074A,
        DSPDaoxn = 0x00B906E6,
        DPSnao = 0x00BA0B09,
        DSno = 0x00BB0226,
        SPDSanax = 0x00BC1CE4,
        SDxPDxan = 0x00BD0D7D,
        DPSxo = 0x00BE0269,
        DPSano = 0x00BF08C9,
        PSa = 0x00C000CA,
        SPDSnaoxn = 0x00C11B04,
        SPDSonoxn = 0x00C21884,
        PSxn = 0x00C3006A,
        SPDnoa = 0x00C40E04,
        SPDSxoxn = 0x00C50664,
        SDPnax = 0x00C60708,
        PSDPoaxn = 0x00C707AA,
        SDPoa = 0x00C803A8,
        SPDoxn = 0x00C90184,
        DPSDxax = 0x00CA0749,
        SPDSaoxn = 0x00CB06E4,
        S = 0x00CC0020,
        SDPono = 0x00CD0888,
        SDPnao = 0x00CE0B08,
        SPno = 0x00CF0224,
        PSDnoa = 0x00D00E0A,
        PSDPxoxn = 0x00D1066A,
        PDSnax = 0x00D20705,
        SPDSoaxn = 0x00D307A4,
        SSPxPDxax = 0x00D41D78,
        DPSanan = 0x00D50CE9,
        PSDPSaoxx = 0x00D616EA,
        DPSxan = 0x00D70349,
        PDSPxax = 0x00D80745,
        SDPSaoxn = 0x00D906E8,
        DPSDanax = 0x00DA1CE9,
        SPxDSxan = 0x00DB0D75,
        SPDnao = 0x00DC0B04,
        SDno = 0x00DD0228,
        SDPxo = 0x00DE0268,
        SDPano = 0x00DF08C8,
        PDSoa = 0x00E003A5,
        PDSoxn = 0x00E10185,
        DSPDxax = 0x00E20746,
        PSDPaoxn = 0x00E306EA,
        SDPSxax = 0x00E40748,
        PDSPaoxn = 0x00E506E5,
        SDPSanax = 0x00E61CE8,
        SPxPDxan = 0x00E70D79,
        SSPxDSxax = 0x00E81D74,
        DSPDSanaxxn = 0x00E95CE6,
        DPSao = 0x00EA02E9,
        DPSxno = 0x00EB0849,
        SDPao = 0x00EC02E8,
        SDPxno = 0x00ED0848,
        DSo = 0x00EE0086,
        SDPnoo = 0x00EF0A08,
        P = 0x00F00021,
        PDSono = 0x00F10885,
        PDSnao = 0x00F20B05,
        PSno = 0x00F3022A,
        PSDnao = 0x00F40B0A,
        PDno = 0x00F50225,
        PDSxo = 0x00F60265,
        PDSano = 0x00F708C5,
        PDSao = 0x00F802E5,
        PDSxno = 0x00F90845,
        DPo = 0x00FA0089,
        DPSnoo = 0x00FB0A09,
        PSo = 0x00FC008A,
        PSDnoo = 0x00FD0A0A,
        DPSoo = 0x00FE02A9,
        DDxn = 0x00FF0062
    }

    public static class Defines
    {
        // These defines (and comments) are from the Windows 3.0 DDK ROPDEFS.BLT

        // Masks for the low word of the ROP:

        public const uint EPS_OFF = 0b0000_0000_0000_0011;      // Offset within parse string
        public const uint EPS_INDEX = 0b0000_0000_0001_1100;      // Parse string index
        public const uint LogPar = 0b0000_0000_0010_0000;      // (1 indicates implied NOT as LogOp6)
        public const uint LogOp1 = 0b0000_0000_1100_0000;      // Logical Operation #1
        public const uint LogOp2 = 0b0000_0011_0000_0000;      // Logical Operation #2
        public const uint LogOp3 = 0b0000_1100_0000_0000;      // Logical Operation #3
        public const uint LogOp4 = 0b0011_0000_0000_0000;      // Logical Operation #4
        public const uint LogOp5 = 0b1100_0000_0000_0000;      // Logical Operation #5

        // The parity bit is used to encode an optional sixth logical operation which will always be a "NOT". In
        // most cases this is used to get an even number of "NOT"s so that reduction can take place (two sequential
        // trailing "NOT"s cancel each other out and thus are eliminated).

        // Each LogOp (Logical Operation) is encoded as follows:

        public const byte LogNOT = 0b00;         // NOT result
        public const byte LogXOR = 0b01;         // XOR result with next operand
        public const byte LogOR = 0b10;         // OR result with next operand
        public const byte LogAND = 0b11;         // AND result with next operand

        // The parse string is a string which contains the operands for the logical operation sequences (source,
        // destination, pattern). The logic opcodes are applied to the current result and the next element of the
        // given string (unless the LogOp is a NOT which only affects the result).
        //
        // The string is encoded as eight two-bit numbers indicating which
        // operand is to be used

        public const byte OpSpec = 0b00;        // Special Operand as noted below
        public const byte OpSrc = 0b01;        // Operand is source field
        public const byte OpDest = 0b10;        // Operand is destination field
        public const byte OpPat = 0b11;        // Operand is pattern field

        // The special operand is used for a few rops that would not fit into an RPN format.On the first occurance
        // of an OpSpec, the current result is "PUSHED", and the next operand is loaded. On the second occurance
        // of the OpSpec, the given logic operation is performed between the current result and the "PUSHED" value.

        // Define the parse strings to be allocated later.
        //
        // An example parse string for the pattern "SDPSDPSD" would be
        // "0110110110110110b"

        public const ushort ParseStr0 = 0x07AAA;      // src,pat,dest,dest,dest,dest,dest,dest
        public const ushort ParseStr1 = 0x079E7;      // src,pat,dest,src,pat,dest,src,pat
        public const ushort ParseStr2 = 0x06DB6;      // src,dest,pat,src,dest,pat,src,dest
        public const ushort ParseStr3 = 0x0AAAA;      // dest,dest,dest,dest,dest,dest,dest,dest
        public const ushort ParseStr4 = 0x0AAAA;      // dest,dest,dest,dest,dest,dest,dest,dest
        public const ushort ParseStr5 = 0x04725;      // src,spec,src,pat,spec,dest,src,src
        public const ushort ParseStr6 = 0x04739;      // src,spec,src,pat,spec,pat,dest,src
        public const ushort ParseStr7 = 0x04639;      // src,spec,src,dest,spec,pat,dest,src

        // The following equates are for certain special functions that are derived from the very first string
        // (index of SpecParseStrIndex).
        //
        // These strings will have their innerloops special cased for speed enhancements(i.e MOVSx and STOSx for
        // pattern copys and white/black fill, and MOVSx for source copy if possible)

        public const ushort PAT_COPY = 0x0021;       // P    - dest = Pattern       (0b0010_0001)
        public const ushort NOTPAT_COPY = 0x0001;       // Pn   - dest = NOT Pattern   (0b0000_0001)
        public const ushort FILL_BLACK = 0x0042;       // DDx  - dest = 0 (black)     (0b0100_0010)
        public const ushort FILL_WHITE = 0x0062;       // DDxn - dest = 1             (0b0110_0010)
        public const ushort SOURCE_COPY = 0x0020;       // S    - dest = source        (0b0010_0000)
    }
}