// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Communications
{
    /// <summary>
    /// [COMMPROP]
    /// </summary>
    /// <msdn><see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa363189.aspx"/></msdn>
    public struct CommunicationsProperties
    {
        public ushort PacketLength;
        public ushort PacketVersion;
        public uint ServiceMask;
        public uint Reserved1;
        public uint MaxTxQueue;
        public uint MaxRxQueue;
        public SettableBaudRate MaxBaud;
        public ProviderSubType ProviderSubType;
        public ProviderCapabilities ProviderCapabilities;
        public SettableParams SettableParams;
        public SettableBaudRate SettableBaud;
        public SettableDataBits SettableData;
        public SettableStopAndParityBits SettableStopParity;
        public uint CurrentTxQueue;
        public uint CurrentRxQueue;
        public uint ProviderSpecific1;
        public uint ProviderSpecific2;
        public char ProviderChar;
    }
}
