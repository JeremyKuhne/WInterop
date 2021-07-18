// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Communications
{
    /// <summary>
    ///  [COMMPROP]
    /// </summary>
    /// <docs><see cref="https://docs.microsoft.com/windows/win32/api/winbase/ns-winbase-commprop"/></docs>
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