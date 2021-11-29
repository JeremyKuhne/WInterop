// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi.Native;

namespace WInterop.Com.Native;

/// <docs>https://docs.microsoft.com/windows/win32/api/objidl/ns-objidl-ustgmedium-r1</docs>
public struct STGMEDIUM
{
    public MediumType tymed;
    public Union Data;
    public IntPtr pUnkForRelease;

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct Union
    {
        [FieldOffset(0)]
        public HBITMAP hBitmap;
        [FieldOffset(0)]
        public HMETAFILE hMetaFilePict;
        [FieldOffset(0)]
        public HENHMETAFILE hEnhMetaFile;
        [FieldOffset(0)]
        public IntPtr hGlobal; // HGLOBAL
        [FieldOffset(0)]
        public unsafe char* lpszFileName;
        [FieldOffset(0)]
        public IntPtr pstm; // IStream
        [FieldOffset(0)]
        public IntPtr pstg; // IStorage
    }

    public static unsafe void ReleaseStgMedium(STGMEDIUM* medium) => Imports.ReleaseStgMedium(medium);
}