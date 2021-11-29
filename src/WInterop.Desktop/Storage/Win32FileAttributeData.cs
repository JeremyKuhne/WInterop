// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Storage;

/// <summary>
///  [WIN32_FILE_ATTRIBUTE_DATA]
/// </summary>
/// <msdn><see cref="https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/ns-fileapi-_win32_file_attribute_data"/></msdn>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public readonly struct Win32FileAttributeData
{
    public readonly AllFileAttributes FileAttributes;
    public readonly FileTime CreationTime;
    public readonly FileTime LastAccessTime;
    public readonly FileTime LastWriteTime;
    public readonly HighLowUlong FileSize;
}