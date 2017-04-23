// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379595.aspx
    public struct SID_AND_ATTRIBUTES
    {
        public unsafe SID* Sid;
        public uint Attributes;
    }
}
