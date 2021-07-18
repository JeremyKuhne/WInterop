// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Communications
{
    /// <summary>
    ///  [COMMCONFIG]
    /// </summary>
    // https://docs.microsoft.com/windows/win32/api/winbase/ns-winbase-commconfig
    public struct CommunicationsConfig
    {
        public uint Size;
        public ushort Version;
        public ushort Reserved;
        public DeviceControlBlock ControlBlock;
        public ProviderSubType ProviderSubType;
        public uint ProviderOffset;
        public uint ProviderSize;
        public char ProviderData;
    }
}