// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Cryptography
{
    public static class CryptoDefines
    {
        public const uint CERT_SYSTEM_STORE_RELOCATE_FLAG = 0x80000000;
        public const uint CERT_SYSTEM_STORE_LOCATION_MASK = 0x00FF0000;
        public const int CERT_SYSTEM_STORE_LOCATION_SHIFT = 16;
    }
}
