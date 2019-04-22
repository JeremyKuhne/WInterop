// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage.Native
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/winefs/ns-winefs-_efs_hash_blob
    // https://msdn.microsoft.com/en-us/library/cc230495.aspx
    public unsafe struct EFS_HASH_BLOB
    {
        public uint cbData;
        public byte* pbData;
    }
}
