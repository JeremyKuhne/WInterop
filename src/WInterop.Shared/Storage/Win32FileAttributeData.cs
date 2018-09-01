// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Storage
{
    /// <summary>
    /// [WIN32_FILE_ATTRIBUTE_DATA]
    /// </summary>
    /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/ns-fileapi-_win32_file_attribute_data"/></msdn>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Win32FileAttributeData
    {
        public FileAttributes FileAttributes;
        public FileTime CreationTime;
        public FileTime LastAccessTime;
        public FileTime LastWriteTime;
        public HighLowUlong FileSize;
    }
}
