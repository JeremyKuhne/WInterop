// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Which end of the pipe.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff728846.aspx"/>
    /// </remarks>
    public enum PipeEnd : uint
    {
        /// <summary>
        /// The client end of the pipe. [FILE_PIPE_CLIENT_END]
        /// </summary>
        Client = 0x0,

        /// <summary>
        /// The server end of the pipe. [FILE_PIPE_SERVER_END]
        /// </summary>
        Server = 0x1
    }
}
