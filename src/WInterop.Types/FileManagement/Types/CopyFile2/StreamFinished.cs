// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.FileManagement.Types.CopyFile2
{
    /// <summary>
    /// Indicates the copy operation for a stream have started to be completed, either successfully or due to a
    /// COPYFILE2_PROGRESS_STOP return from CopyFile2ProgressRoutine.
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449413.aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct StreamFinished
    {
        uint dwStreamNumber;
        uint dwReserved;
        IntPtr hSourceFile;
        IntPtr hDestinationFile;
        ulong uliStreamSize;
        ulong uliStreamBytesTransferred;
        ulong uliTotalFileSize;
        ulong uliTotalBytesTransferred;
    }
}