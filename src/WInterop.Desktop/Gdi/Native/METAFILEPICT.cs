// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Gdi.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct METAFILEPICT
    {
        public int mm;
        public int xExt;
        public int yExt;
        public HMETAFILE hMF;
    }
}
