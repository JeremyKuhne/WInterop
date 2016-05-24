// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Backup.DataTypes
{
    // typedef struct _WIN32_STREAM_ID
    // {
    //     DWORD dwStreamId;
    //     DWORD dwStreamAttributes;
    //     LARGE_INTEGER Size;
    //     DWORD dwStreamNameSize;
    //     WCHAR cStreamName[ANYSIZE_ARRAY];
    // }
    // WIN32_STREAM_ID, *LPWIN32_STREAM_ID;

    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa362667.aspx">WIN32_STREAM_ID</a> structure.
    /// See <a href="https://msdn.microsoft.com/en-us/library/dd303907.aspx">[MS-BKUP]</a> specification.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct WIN32_STREAM_ID
    {
        public BackupStreamType dwStreamId;
        public StreamAttributes dwStreamAttributes;

        /// <summary>
        /// Data size in bytes
        /// </summary>
        public ulong Size;

        /// <summary>
        /// Length of the stream name in bytes
        /// </summary>
        public uint dwStreamNameSize;
    }
}
