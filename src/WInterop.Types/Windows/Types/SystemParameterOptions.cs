// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    [Flags]
    public enum SystemParameterOptions : uint
    {
        SPIF_UPDATEINIFILE    = 0x0001,
        SPIF_SENDWININICHANGE = 0x0002,
        SPIF_SENDCHANGE       = SPIF_SENDWININICHANGE
    }
}
