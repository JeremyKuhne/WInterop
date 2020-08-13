// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public static class WindowDefines
    {
        public const int CW_USEDEFAULT = unchecked((int)0x80000000);

        public const uint LB_ERR = unchecked((uint)-1);

        public const uint LVM_FIRST  = 0x1000;      // ListView messages
        public const uint TV_FIRST   = 0x1100;      // TreeView messages
        public const uint HDM_FIRST  = 0x1200;      // Header messages
        public const uint TCM_FIRST  = 0x1300;      // Tab control messages
        public const uint PGM_FIRST  = 0x1400;      // Pager control messages
        public const uint ECM_FIRST  = 0x1500;      // Edit control messages
        public const uint BCM_FIRST  = 0x1600;      // Button control messages
        public const uint CBM_FIRST  = 0x1700;      // Combobox control messages
        public const uint CCM_FIRST  = 0x2000;      // Common control shared messages
    }
}
