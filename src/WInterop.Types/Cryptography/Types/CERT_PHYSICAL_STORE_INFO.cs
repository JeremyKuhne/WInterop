// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Cryptography.Types
{
    public struct CERT_PHYSICAL_STORE_INFO
    {
        public uint cbSize;
        public string pszOpenStoreProvider;
        public uint dwOpenEncodingType;
        public uint dwOpenFlags;
        CRYPT_DATA_BLOB OpenParameters;
        uint dwFlags;
        uint dwPriority;
    }
}
