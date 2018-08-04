// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security.Native
{
    /// <summary>
    /// Used to get/set the default owner for newly created objects.
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379628.aspx"/>
    /// </summary>
    public unsafe struct TOKEN_OWNER
    {
        public SID* Owner;
    }
}
