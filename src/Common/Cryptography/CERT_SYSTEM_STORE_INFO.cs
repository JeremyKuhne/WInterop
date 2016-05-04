// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Cryptography
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa377568.aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct CERT_SYSTEM_STORE_INFO
    {
        public uint cbSize;
    }
}
