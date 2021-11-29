// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Communications;

/// <summary>
///  [DCB]
/// </summary>
/// <docs>https://docs.microsoft.com/windows/win32/api/winbase/ns-winbase-dcb</docs>
public struct DeviceControlBlock
{
    private const uint BinaryMask = 0b0000000000000001;
    private const uint ParityMask = 0b0000000000000010;
    private const uint CtsFlowMask = 0b0000000000000100;
    private const uint DsrFlowMask = 0b0000000000001000;
    private const uint DtrControlMask = 0b0000000000110000;
    private const uint DsrSensitivityMask = 0b0000000001000000;
    private const uint TXContinueMask = 0b0000000010000000;
    private const uint OutMask = 0b0000000100000000;
    private const uint InMask = 0b0000001000000000;
    private const uint ErrorCharMask = 0b0000010000000000;
    private const uint NullMask = 0b0000100000000000;
    private const uint RtsControlMask = 0b0011000000000000;
    private const uint AbortOnErrorMask = 0b0100000000000000;

    public uint DCBlength;
    public CommBaudRate BaudRate;
    private uint _bitFields;
    public ushort Reserved;
    public ushort XonLim;
    public ushort XoffLim;
    public byte ByteSize;
    public Parity Parity;
    public StopBits StopBits;
    public sbyte XonChar;
    public sbyte XoffChar;
    public sbyte ErrorChar;
    public sbyte EofChar;
    public sbyte EvtChar;
    public ushort Reserved1;

    // DWORD fBinary  :1;
    public bool Binary
    {
        get => this[BinaryMask];
        set => this[BinaryMask] = value;
    }

    // DWORD fParity  :1;
    public bool ParityFlag
    {
        get => this[ParityMask];
        set => this[ParityMask] = value;
    }

    // DWORD fOutxCtsFlow  :1;
    public bool OutxCtsFlow
    {
        get => this[CtsFlowMask];
        set => this[CtsFlowMask] = value;
    }

    // DWORD fOutxDsrFlow  :1;
    public bool OutxDsrFlow
    {
        get => this[DsrFlowMask];
        set => this[DsrFlowMask] = value;
    }

    // DWORD fDtrControl  :2;
    public DtrControl DtrControl
    {
        get => (DtrControl)this[DtrControlMask, 4];
        set => this[DtrControlMask, 4] = (uint)value;
    }

    // DWORD fDsrSensitivity  :1;
    public bool DsrSensitivity
    {
        get => this[DsrSensitivityMask];
        set => this[DsrSensitivityMask] = value;
    }

    // DWORD fTXContinueOnXoff  :1;
    public bool TXContinueOnXoff
    {
        get => this[TXContinueMask];
        set => this[TXContinueMask] = value;
    }

    // DWORD fOutX  :1;
    public bool OutX
    {
        get => this[OutMask];
        set => this[OutMask] = value;
    }

    // DWORD fInX  :1;
    public bool InX
    {
        get => this[InMask];
        set => this[InMask] = value;
    }

    // DWORD fErrorChar  :1;
    public bool ErrorCharFlag
    {
        get => this[ErrorCharMask];
        set => this[ErrorCharMask] = value;
    }

    // DWORD fNull  :1;
    public bool Null
    {
        get => this[NullMask];
        set => this[NullMask] = value;
    }

    // DWORD fRtsControl  :2;
    public RtsControl RtsControl
    {
        get => (RtsControl)this[RtsControlMask, 12];
        set => this[DtrControlMask, 12] = (uint)value;
    }

    // DWORD fAbortOnError  :1;
    public bool AbortOnError
    {
        get => this[AbortOnErrorMask];
        set => this[AbortOnErrorMask] = value;
    }

    private bool this[uint bitMask]
    {
        get => (_bitFields & bitMask) != 0;
        set
        {
            if (value)
            {
                _bitFields |= bitMask;
            }
            else
            {
                _bitFields &= ~bitMask;
            }
        }
    }

    private uint this[uint bitMask, int offset]
    {
        get => (_bitFields & bitMask) >> offset;
        set => _bitFields = (_bitFields & ~bitMask) | (value << offset & bitMask);
    }
}