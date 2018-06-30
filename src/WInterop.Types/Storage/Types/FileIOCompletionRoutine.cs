// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ErrorHandling.Types;
using WInterop.Synchronization.Types;

namespace WInterop.Storage.Types
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364052.aspx">FileIOCompletionRoutine</a> callback used by
    /// ReadFileEx, WriteFileEx, and ReadDirectoryChanges.
    /// </summary>
    public delegate void FileIOCompletionRoutine(
        WindowsError dwErrorCode,
        uint dwNumberOfBytesTransfered,
        ref OVERLAPPED lpOverlapped);
}
