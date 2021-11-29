// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage.Native;

/// <summary>
///  Used to create an NTFS hard link to an existing file.
/// </summary>
/// <msdn>
///  https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntifs/ns-ntifs-_file_link_information
/// </msdn>
public struct FILE_LINK_INFORMATION
{
    public ByteBoolean ReplaceIfExists;
    public IntPtr RootDirectory;
    public uint FileNameLength;
    private char _FileName;
    public ReadOnlySpan<char> FileName => TrailingArray<char>.GetBufferInBytes(in _FileName, FileNameLength);
}