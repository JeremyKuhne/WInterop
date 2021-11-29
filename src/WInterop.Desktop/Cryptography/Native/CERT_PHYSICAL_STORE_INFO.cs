// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Cryptography.Native;

public struct CERT_PHYSICAL_STORE_INFO
{
    public uint cbSize;
    public string pszOpenStoreProvider;
    public uint dwOpenEncodingType;
    public uint dwOpenFlags;
    public CRYPT_DATA_BLOB OpenParameters;
    public uint dwFlags;
    public uint dwPriority;
}