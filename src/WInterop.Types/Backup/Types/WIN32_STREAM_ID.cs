// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Backup.Types
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa362667.aspx">WIN32_STREAM_ID</a> structure.
    /// See <a href="https://msdn.microsoft.com/en-us/library/dd303907.aspx">[MS-BKUP]</a> specification.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WIN32_STREAM_ID
    {
        public BackupStreamType dwStreamId;
        public StreamAttributes dwStreamAttributes;

        /// <summary>
        /// Data size in bytes
        /// </summary>
        public long Size;

        /// <summary>
        /// Stream name size, in bytes.
        /// </summary>
        public uint dwStreamNameSize;

        private TrailingString _cStreamName;
        public ReadOnlySpan<char> cStreamName => _cStreamName.GetBuffer(dwStreamNameSize);
    }
}
