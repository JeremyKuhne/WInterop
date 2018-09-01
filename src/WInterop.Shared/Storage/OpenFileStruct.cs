// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Support.Buffers;

namespace WInterop.Storage
{
    /// <summary>
    /// [OFSTRUCT]
    /// </summary>
    /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/desktop/api/winbase/ns-winbase-_ofstruct"/></msdn>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct OpenFileStruct
    {
        public const int OFS_MAXPATHNAME = 128;
        public byte Bytes;
        public BOOLEAN FixedDisk;
        private ushort nErrCode;
        private ushort Reserved1;
        private ushort Reserved2;
        private FixedByte.Size128 szPathName;
        public string PathName => BufferHelper.GetNullTerminatedAsciiString(szPathName.Buffer);

        // The nErrCode is a DOS error, which maps directly to Windows errors.
        public WindowsError ErrorCode => (WindowsError)nErrCode;
    }
}
