// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Support.Buffers;

namespace WInterop.Storage
{
    /// <summary>
    ///  [OFSTRUCT]
    /// </summary>
    /// <docs><see cref="https://docs.microsoft.com/windows/desktop/api/winbase/ns-winbase-_ofstruct"/></docs>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct OpenFileStruct
    {
        public const int OFS_MAXPATHNAME = 128;
        public byte Bytes;
        public ByteBoolean FixedDisk;
        private readonly ushort _nErrCode;
        private readonly ushort _reserved1;
        private readonly ushort _reserved2;
        private FixedByte.Size128 _szPathName;
        public string PathName => BufferHelper.GetNullTerminatedAsciiString(_szPathName.Buffer);

        // The nErrCode is a DOS error, which maps directly to Windows errors.
        public WindowsError ErrorCode => (WindowsError)_nErrCode;
    }
}
