﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Pipe information that isn't specific to the local or remote end.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff728845.aspx"/>
    /// <see cref="https://msdn.microsoft.com/en-us/library/cc232082.aspx"/>
    /// </remarks>
    public struct FILE_PIPE_INFORMATION
    {
        /// <summary>
        /// The read mode of the pipe. Attempting to change from stream to message mode
        /// is not supported (and will return STATUS_INVALID_PARAMETER).
        /// </summary>
        public PipeReadMode ReadMode;

        /// <summary>
        /// The completion mode.
        /// </summary>
        public PipeCompletionMode CompletionMode;
    }
}
