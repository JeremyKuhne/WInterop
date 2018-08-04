// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Communications
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363188.aspx
    public struct COMMCONFIG
    {
        public uint dwSize;
        public ushort wVersion;
        public ushort wReserved;
        public DCB dcb;
        public ProviderSubType dwProviderSubType;
        public uint dwProviderOffset;
        public uint dwProviderSize;
        public char wcProviderData;
    }
}
