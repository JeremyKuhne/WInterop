// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Shell
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/shtypes/ns-shtypes-_strret
    public struct ReturnString : IDisposable
    {
        private IntPtr _pointer;


        public unsafe override string ToString()
        {
            if (_pointer == IntPtr.Zero)
                throw new ObjectDisposedException(nameof(ReturnString));

            STRRET* s = (STRRET*)_pointer.ToPointer();
            return (s->uType) switch
            {
                STRRET.STRRET_TYPE.STRRET_WSTR => new string(s->Data.pOleStr),
                STRRET.STRRET_TYPE.STRRET_CSTR => throw new NotSupportedException(),
                STRRET.STRRET_TYPE.STRRET_OFFSET => throw new NotSupportedException(),
                _ => throw new InvalidOperationException()
            };
        }

        public unsafe void Dispose()
        {
            if (_pointer == IntPtr.Zero)
                throw new ObjectDisposedException(nameof(ReturnString));

            STRRET* s = (STRRET*)_pointer.ToPointer();
            if (s->uType == STRRET.STRRET_TYPE.STRRET_WSTR)
            {
                Marshal.FreeCoTaskMem((IntPtr)s->Data.pOleStr);
                s->uType = STRRET.STRRET_TYPE.STRRET_CSTR;
                s->Data.pOleStr = null;
            }

            _pointer = IntPtr.Zero;
        }

        private struct STRRET
        {
            public STRRET_TYPE uType;
            public DataUnion Data;

            [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
            public unsafe struct DataUnion
            {
                [FieldOffset(0)]
                public char* pOleStr;
                [FieldOffset(0)]
                public uint uOffset;
                [FieldOffset(0)]
                private fixed byte _cStr[260];
            }

            public enum STRRET_TYPE : uint
            {
                STRRET_WSTR = 0,
                STRRET_OFFSET = 0x1,
                STRRET_CSTR = 0x2
            }
        }
    }
}
