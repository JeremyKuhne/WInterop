// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ErrorHandling.Types;
using WInterop.Support;

namespace WInterop.FileManagement.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365282.aspx
    public unsafe struct OFSTRUCT
    {
        public const int OFS_MAXPATHNAME = 128;
        public byte cBytes;
        public BOOLEAN fFixedDisk;
        public ushort nErrCode;
        public ushort Reserved1;
        public ushort Reserved2;
        public fixed byte szPathName[OFS_MAXPATHNAME];

        // The nErrCode is a DOS error, which maps directly to Windows errors.
        public WindowsError GetErrorCode() => (WindowsError)nErrCode;
    }
}
