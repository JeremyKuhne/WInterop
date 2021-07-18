// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com.Native
{
    [Flags]
    public enum DVASPECT : uint
    {
        DVASPECT_CONTENT    = 1,
        DVASPECT_THUMBNAIL  = 2,
        DVASPECT_ICON       = 4,
        DVASPECT_DOCPRINT   = 8
    }
}
