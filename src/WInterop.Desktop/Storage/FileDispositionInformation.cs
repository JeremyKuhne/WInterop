// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage;

/// <summary>
///  For flagging a file handle for deletion using NtSetInformationFile. Also usable with
///  SetFileInformationByHandle and FileInfoClass.FileDispositionInfo.
/// </summary>
/// <native>[FILE_DISPOSITION_INFORMATION]</native>
public struct FileDispositionInformation
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff545765.aspx
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364221.aspx

    // NOTE: This is defined differently in MSDN, but it is really a BOOLEAN, NOT
    // a BOOL. SetFileInformationByHandle simply thunks to NtSetInformationFile.

    /// <summary>
    ///  Whether or not the file should be deleted. This is ignored if the handle was opened with the
    ///  DeleteOnClose flag.
    /// </summary>
    public ByteBoolean DeleteFile;
}