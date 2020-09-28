// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.GdiPlus.EmfPlus
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct MetafilePlusHeader
    {
        public readonly MetafilePlusRecord Record;
        public readonly uint Version;
        public readonly uint EmfPlusFlags;
        public readonly uint LogicalDpiX;
        public readonly uint LogicalDpiY;
    }
}
