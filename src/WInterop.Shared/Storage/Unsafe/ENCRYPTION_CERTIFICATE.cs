// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Storage.Unsafe
{
    //  typedef struct _ENCRYPTION_CERTIFICATE
    //  {
    //      DWORD cbTotalLength;
    //      SID* pUserSid;
    //      PEFS_CERTIFICATE_BLOB pCertBlob;
    //  }
    //  ENCRYPTION_CERTIFICATE, *PENCRYPTION_CERTIFICATE;

    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364032.aspx">ENCRYPTION_CERTIFICATE</a> structure.
    /// </summary>
    public struct ENCRYPTION_CERTIFICATE
    {
        /// <summary>
        /// Size of the struct in bytes
        /// </summary>
        public uint cbTotalLength;

        /// <summary>
        /// Pointer to SID
        /// </summary>
        public IntPtr pUserSid;

        /// <summary>
        /// Pointer to EFS_CERTIFICATE_BLOB
        /// </summary>
        public IntPtr pCertBlob;
    }
}
