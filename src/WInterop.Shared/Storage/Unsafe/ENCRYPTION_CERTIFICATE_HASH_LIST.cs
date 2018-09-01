// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Storage.Unsafe
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/winefs/ns-winefs-_encryption_certificate_hash_list
    // https://msdn.microsoft.com/en-us/library/cc230501.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct ENCRYPTION_CERTIFICATE_HASH_LIST
    {
        public uint nCert_Hash;
        public ENCRYPTION_CERTIFICATE_HASH** pUsers;
    }
}
