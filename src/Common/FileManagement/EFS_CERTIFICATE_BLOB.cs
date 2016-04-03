// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364015.aspx">EFS_CERTIFICATE_BLOB</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EFS_CERTIFICATE_BLOB
    {
        public uint dwCertEncodingType;
        public uint cbData;
        public IntPtr pbData;
    }
}
