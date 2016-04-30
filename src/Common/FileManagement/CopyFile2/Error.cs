// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.FileManagement.CopyFile2
{
    /// <summary>
    /// Message sent when an error was encountered during the copy operation.
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449413.aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct Error
    {
        COPYFILE2_COPY_PHASE CopyPhase;
        uint dwStreamNumber;
        int hrFailure;
        uint dwReserved;
        ulong uliChunkNumber;
        ulong uliStreamSize;
        ulong uliStreamBytesTransferred;
        ulong uliTotalFileSize;
        ulong uliTotalBytesTransferred;
    }
}