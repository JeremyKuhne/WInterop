// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/hardware/ff545855.aspx">FILE_STANDARD_INFORMATION_EX</a> structure.
    /// </summary>
    /// <remarks>
    ///  While this isn't defined until Windows 10, it can be used interchangably with FILE_STANDARD_INFORMATION. The last two
    ///  fields simply don't have meaning. (Due to packing both versions of the struct are the same size.)
    /// </remarks>
    public readonly struct FileStandardInformationExtended
    {
        public readonly ulong AllocationSize;
        public readonly ulong EndOfFile;
        public readonly uint NumberOfLinks;
        public readonly ByteBoolean DeletePending;
        public readonly ByteBoolean Directory;
        public readonly ByteBoolean AlternateStream;
        public readonly ByteBoolean MetadataAttribute;
    }
}