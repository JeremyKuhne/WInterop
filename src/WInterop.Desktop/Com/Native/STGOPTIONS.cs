// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Com
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe ref struct STGOPTIONS
    {
        public ushort usVersion;
        public ushort reserved;
        public uint ulSectorSize;
        public char* pwcsTemplateFile;
    }
}
