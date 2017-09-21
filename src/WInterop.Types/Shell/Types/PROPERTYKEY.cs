// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb773381.aspx
    public struct PROPERTYKEY
    {
        public Guid fmtid;
        public uint pid;
    }
}
