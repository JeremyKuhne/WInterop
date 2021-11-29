// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.Synchronization;

namespace WInterop.Storage;

/// <summary>
///  <a href="https://docs.microsoft.com/windows/win32/api/minwinbase/nc-minwinbase-lpoverlapped_completion_routine">
///  FileIOCompletionRoutine</a> callback used by ReadFileEx, WriteFileEx, and ReadDirectoryChanges.
/// </summary>
public delegate void FileIOCompletionRoutine(
    WindowsError errorCode,
    uint numberOfBytesTransfered,
    ref OVERLAPPED overlapped);