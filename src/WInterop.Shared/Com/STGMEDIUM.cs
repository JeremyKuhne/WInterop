// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi.Native;

namespace WInterop.Com
{
    public struct STGMEDIUM
    {
        public uint tymed;
        public Union Data;
        public IntPtr pUnkForRelease;

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct Union
        {
            [FieldOffset(0)]
            public HBITMAP hBitmap;
            [FieldOffset(0)]
            public IntPtr hMetaFilePict; // HMETAFILEPICT
            [FieldOffset(0)]
            public IntPtr hEnhMetaFile; // HENHMETAFILE
            [FieldOffset(0)]
            public IntPtr hGlobal; // HGLOBAL
            [FieldOffset(0)]
            public unsafe char* lpszFileName;
            [FieldOffset(0)]
            public IntPtr pstm; // IStream
            [FieldOffset(0)]
            public IntPtr pstg; // IStorage
        }
    }
}
