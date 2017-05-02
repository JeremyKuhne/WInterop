// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd183551.aspx
    public struct DESIGNVECTOR
    {
        public const uint STAMP_DESIGNVECTOR = (0x8000000 + 'd' + ('v' << 8));
        public const int MM_MAX_NUMAXES = 16;

        public uint dvReserved;
        public uint dvNumAxes;
        public unsafe fixed int dvValues[MM_MAX_NUMAXES];

        public unsafe int[] Values
        {
            get
            {
                int[] infos = new int[dvNumAxes];
                fixed (int* p = dvValues)
                {
                    for (int i = 0; i < dvNumAxes; i++)
                        infos[i] = p[i];
                }

                return infos;
            }
        }
    }
}
