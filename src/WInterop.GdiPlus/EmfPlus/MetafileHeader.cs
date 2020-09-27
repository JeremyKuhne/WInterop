// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WInterop.Gdi.Native;

namespace WInterop.GdiPlus.EmfPlus
{
    public struct MetafileHeader
    {
        public MetafileType Type;
        public uint Size;               // Size of the metafile (in bytes)
        public uint Version;            // EMF+, EMF, or WMF version
        public uint EmfPlusFlags;
        public float DpiX;
        public float DpiY;
        public int X;                   // Bounds in device units
        public int Y;
        public int Width;
        public int Height;
        public ENHMETAHEADER EmfHeader;
        public int EmfPlusHeaderSize;  // size of the EMF+ header in file
        public int LogicalDpiX;        // Logical Dpi of reference Hdc
        public int LogicalDpiY;        // usually valid only for EMF+
    }
}
