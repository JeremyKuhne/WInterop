// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage;

/// <summary>
///  Used to set the end of file position. [FILE_END_OF_FILE_INFORMATION]
/// </summary>
/// <docs>https://docs.microsoft.com/windows-hardware/drivers/ddi/ntddk/ns-ntddk-_file_end_of_file_information</docs>
public struct FileEndOfFileInformation
{
    /// <summary>
    ///  The absolute new end of file position as a byte offset from the start of the file.
    /// </summary>
    public long EndOfFile;
}