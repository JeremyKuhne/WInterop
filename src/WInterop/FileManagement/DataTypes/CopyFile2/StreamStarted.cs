// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.FileManagement.DataTypes.CopyFile2
{
    /// <summary>
    /// Indicates both source and destination handles for a stream have been opened and the copy of the stream is about to be started.
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449413.aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct StreamStarted
    {
        uint dwStreamNumber;
        uint dwReserved;
        IntPtr hSourceFile;
        IntPtr hDestinationFile;
        ulong uliStreamSize;
        ulong uliTotalFileSize;
    }
}