// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Desktop.Communications.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363189.aspx
    public struct COMMPROP
    {
        public ushort wPacketLength;
        public ushort wPacketVersion;
        public uint dwServiceMask;
        public uint dwReserved1;
        public uint dwMaxTxQueue;
        public uint dwMaxRxQueue;
        public SettableBaudRate dwMaxBaud;
        public ProviderSubType dwProvSubType;
        public ProviderCapabilities dwProvCapabilities;
        public SettableParams dwSettableParams;
        public SettableBaudRate dwSettableBaud;
        public SettableDataBits wSettableData;
        public SettableStopAndParityBits wSettableStopParity;
        public uint dwCurrentTxQueue;
        public uint dwCurrentRxQueue;
        public uint dwProvSpec1;
        public uint dwProvSpec2;
        public char wcProvChar;
    }
}
