// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Cryptography
{
    using System.Diagnostics;

    [DebuggerDisplay("{Name}")]
    public struct SystemStoreInformation
    {
        public string Name;
        public string Key;
        public SystemStoreLocation Location;
    }
}
