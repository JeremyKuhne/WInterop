// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Network
{
    [Flags]
    public enum UserEnumFilter : uint
    {
        /// <summary>
        ///  [FILTER_TEMP_DUPLICATE_ACCOUNT]
        /// </summary>
        TempDuplicateAccounts = 0x0001,

        /// <summary>
        ///  [FILTER_NORMAL_ACCOUNT]
        /// </summary>
        NormalAccounts = 0x0002,

        /// <summary>
        ///  [FILTER_INTERDOMAIN_TRUST_ACCOUNT]
        /// </summary>
        InterdomainTrustAccounts = 0x0008,

        /// <summary>
        ///  [FILTER_WORKSTATION_TRUST_ACCOUNT]
        /// </summary>
        WorkstationTrustAccounts = 0x0010,

        /// <summary>
        ///  [FILTER_SERVER_TRUST_ACCOUNT]
        /// </summary>
        ServerTrustAccounts = 0x0020
    }
}