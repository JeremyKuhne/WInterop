// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Desktop.Communications.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363214.aspx
    public struct DCB
    {
        private const uint BinaryMask             = 0b0000000000000001;
        private const uint ParityMask             = 0b0000000000000010;
        private const uint CtsFlowMask            = 0b0000000000000100;
        private const uint DsrFlowMask            = 0b0000000000001000;
        private const uint DtrControlMask         = 0b0000000000110000;
        private const uint DsrSensitivityMask     = 0b0000000001000000;
        private const uint TXContinueMask         = 0b0000000010000000;
        private const uint OutMask                = 0b0000000100000000;
        private const uint InMask                 = 0b0000001000000000;
        private const uint ErrorCharMask          = 0b0000010000000000;
        private const uint NullMask               = 0b0000100000000000;
        private const uint RtsControlMask         = 0b0011000000000000;
        private const uint AbortOnErrorMask       = 0b0100000000000000;

        public uint DCBlength;
        public CommBaudRate BaudRate;
        private uint _bitFields;
        public ushort wReserved;
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
        public ushort wReserved1;

        //DWORD fBinary  :1;
        public bool fBinary
        {
            get { return this[BinaryMask]; }
            set { this[BinaryMask] = value; }
        }

        //DWORD fParity  :1;
        public bool fParity
        {
            get { return this[ParityMask]; }
            set { this[ParityMask] = value; }
        }

        //DWORD fOutxCtsFlow  :1;
        public bool fOutxCtsFlow
        {
            get { return this[CtsFlowMask]; }
            set { this[CtsFlowMask] = value; }
        }

        //DWORD fOutxDsrFlow  :1;
        public bool fOutxDsrFlow
        {
            get { return this[DsrFlowMask]; }
            set { this[DsrFlowMask] = value; }
        }

        //DWORD fDtrControl  :2;
        public DtrControl fDtrControl
        {
            get { return (DtrControl)this[DtrControlMask, 4]; }
            set { this[DtrControlMask, 4] = (uint)value; }
        }

        //DWORD fDsrSensitivity  :1;
        public bool fDsrSensitivity
        {
            get { return this[DsrSensitivityMask]; }
            set { this[DsrSensitivityMask] = value; }
        }

        //DWORD fTXContinueOnXoff  :1;
        public bool fTXContinueOnXoff
        {
            get { return this[TXContinueMask]; }
            set { this[TXContinueMask] = value; }
        }

        //DWORD fOutX  :1;
        public bool fOutX
        {
            get { return this[OutMask]; }
            set { this[OutMask] = value; }
        }

        //DWORD fInX  :1;
        public bool fInX
        {
            get { return this[InMask]; }
            set { this[InMask] = value; }
        }

        //DWORD fErrorChar  :1;
        public bool fErrorChar
        {
            get { return this[ErrorCharMask]; }
            set { this[ErrorCharMask] = value; }
        }

        //DWORD fNull  :1;
        public bool fNull
        {
            get { return this[NullMask]; }
            set { this[NullMask] = value; }
        }

        //DWORD fRtsControl  :2;
        public RtsControl fRtsControl
        {
            get { return (RtsControl)this[RtsControlMask, 12]; }
            set { this[DtrControlMask, 12] = (uint)value; }
        }

        //DWORD fAbortOnError  :1;
        public bool fAbortOnError
        {
            get { return this[AbortOnErrorMask]; }
            set { this[AbortOnErrorMask] = value; }
        }

        private bool this[uint bitMask]
        {
            get { return (_bitFields & bitMask) != 0; }
            set { if (value) _bitFields |= bitMask; else _bitFields &= ~bitMask; }
        }

        private uint this[uint bitMask, int offset]
        {
            get { return (_bitFields & bitMask) >> offset; }
            set
            {
                _bitFields = (_bitFields & ~bitMask) | (value << offset & bitMask);
            }
        }
    }
}
