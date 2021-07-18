// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Storage.Native
{
    // typedef struct _ENCRYPTION_CERTIFICATE_LIST
    //  {
    //      DWORD nUsers;
    //      PENCRYPTION_CERTIFICATE* pUsers;
    //  }
    //  ENCRYPTION_CERTIFICATE_LIST, *PENCRYPTION_CERTIFICATE_LIST;

    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364030.aspx">ENCRYPTION_CERTIFICATE_LIST</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ENCRYPTION_CERTIFICATE_LIST
    {
        public uint nUsers;

        /// <summary>
        ///  Pointer to ENCRYPTION_CERTIFICATE array
        /// </summary>
        public IntPtr pUsers;
    }
}