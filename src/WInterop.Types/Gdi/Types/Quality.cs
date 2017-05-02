// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd183499.aspx
    public enum Quality : byte
    {
        DEFAULT_QUALITY             = 0,
        DRAFT_QUALITY               = 1,
        PROOF_QUALITY               = 2,
        NONANTIALIASED_QUALITY      = 3,
        ANTIALIASED_QUALITY         = 4,
        CLEARTYPE_QUALITY           = 5,
        CLEARTYPE_NATURAL_QUALITY   = 6
    }
}
