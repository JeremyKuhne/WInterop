// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Support;

namespace WInterop.Gdi.Types
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct ENUMLOGFONTEX
    {
        public const int LF_FULLFACESIZE = 64;
        public const int LF_FACESIZE = 32;

        public LOGFONT elfLogFont;
        public unsafe fixed char elfFullName[LF_FULLFACESIZE];
        public unsafe fixed char elfStyle[LF_FACESIZE];
        public unsafe fixed char elfScript[LF_FACESIZE];

        public unsafe string FullName
        {
            get
            {
                fixed (char* c = elfFullName)
                    return new string(c);
            }
            set
            {
                fixed (char* c = elfFullName)
                    Strings.StringToBuffer(value, c, LF_FULLFACESIZE - 1);
            }
        }

        public unsafe string Style
        {
            get
            {
                fixed (char* c = elfStyle)
                    return new string(c);
            }
            set
            {
                fixed (char* c = elfStyle)
                    Strings.StringToBuffer(value, c, LF_FACESIZE - 1);
            }
        }

        public unsafe string Script
        {
            get
            {
                fixed (char* c = elfScript)
                    return new string(c);
            }
            set
            {
                fixed (char* c = elfScript)
                    Strings.StringToBuffer(value, c, LF_FACESIZE - 1);
            }
        }
    }
}
