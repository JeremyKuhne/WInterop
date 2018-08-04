// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization
{
    /// <summary>
    /// Defines access allowed for a specific trustee identified by the specified SID.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa374847.aspx"/>
    /// </remarks>
    public struct ACCESS_ALLOWED_ACE
    {
        public ACE_HEADER Header;
        public ACCESS_MASK Mask;
        public uint SidStart;
    }
}
