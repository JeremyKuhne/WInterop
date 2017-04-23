// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;

namespace WInterop.Cryptography.Types
{
    [DebuggerDisplay("{SystemStoreInfo.Name} {PhysicalStoreName}")]
    public struct PhysicalStoreInformation
    {
        public SystemStoreInformation SystemStoreInformation;
        public string PhysicalStoreName;
        public string ProviderType;
        public SystemStoreLocation Location;
    }
}
