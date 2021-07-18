// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com
{
    /// <summary>
    ///  Origin for seeking in structured storage streams.
    /// </summary>
    /// <docs>https://msdn.microsoft.com/en-us/library/windows/desktop/aa380359.aspx</docs>
    public enum StreamSeek : uint
    {
        /// <summary>
        ///  Offset is relative to the beginning of the stream. [STREAM_SEEK_SET]
        /// </summary>
        Set = 0,

        /// <summary>
        ///  Offset is relative to the current position. [STREAM_SEEK_CUR]
        /// </summary>
        Current = 1,

        /// <summary>
        ///  Offset is relative to the end of the stream. [STREAM_SEEK_END]
        /// </summary>
        End = 2
    }
}