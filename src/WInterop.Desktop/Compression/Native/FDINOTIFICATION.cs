// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Compression.Native
{
    // https://msdn.microsoft.com/en-us/library/ff797928.aspx
    public struct FDINOTIFICATION
    {
        public int cb;
        public IntPtr psz1;
        public IntPtr psz2;
        public IntPtr psz3;
        public IntPtr pv;
        public IntPtr hf;
        public ushort date;
        public ushort time;
        public ushort attribs;
        public ushort setID;
        public ushort iCabinet;
        public ushort iFolder;
        public FdiError fdie;
    }
}