// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Pipe information that isn't specific to the local or remote end.
    /// </summary>
    /// <remarks><see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff728845.aspx"/></remarks>
    public struct FILE_PIPE_INFORMATION
    {
        public PipeReadMode ReadMode;
        public PipeCompletionMode CompletionMode;
    }
}
