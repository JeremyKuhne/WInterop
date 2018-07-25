// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd145060.aspx
    public enum WorldTransformMode : uint
    {
        MWT_IDENTITY       = 1,
        MWT_LEFTMULTIPLY   = 2,
        MWT_RIGHTMULTIPLY  = 3
    }
}
