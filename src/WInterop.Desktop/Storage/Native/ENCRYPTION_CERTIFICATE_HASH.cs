// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Security;

namespace WInterop.Storage.Native
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/winefs/ns-winefs-_encryption_certificate_hash
    // https://msdn.microsoft.com/en-us/library/cc230500.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct ENCRYPTION_CERTIFICATE_HASH
    {
        public uint cbTotalLength;
        public SID* pUserSid;
        public EFS_HASH_BLOB* pHash;
        public char* lpDisplayInformation;
    }
}