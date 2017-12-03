// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization.Types
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/aa379263.aspx">LUID_AND_ATTRIBUTES</a> structure.
    /// </summary>
    public struct LUID_AND_ATTRIBUTES
    {
        public LUID Luid;
        public uint Attributes;
    }
}
