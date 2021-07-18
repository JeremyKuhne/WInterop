// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Cryptography.Native
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa381414.aspx
    [StructLayout(LayoutKind.Sequential)]
    public struct CRYPT_DATA_BLOB
    {
        private readonly uint cbData;
        private readonly IntPtr pbData;
    }
}