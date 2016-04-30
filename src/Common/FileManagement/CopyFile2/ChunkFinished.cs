// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.FileManagement.CopyFile2
{
    /// <summary>
    /// Indicates the copy of a single chunk of a stream has completed.
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449413.aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct ChunkFinished
    {
        uint dwStreamNumber;
        uint dwReserved;
        IntPtr hSourceFile;
        IntPtr hDestinationFile;
        ulong uliChunkNumber;
        ulong uliChunkSize;
        ulong uliStreamSize;
        ulong uliStreamBytesTransferred;
        ulong uliTotalFileSize;
        ulong uliTotalBytesTransferred;
    }
}
