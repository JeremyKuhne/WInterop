// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Globalization
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/dd319087.aspx
    public struct NLSVERSIONINFOEX
    {
        public uint dwNLSVersionInfoSize;
        public uint dwNLSVersion;
        public uint dwDefinedVersion;
        public uint dwEffectiveId;
        public Guid guidCustomVersion;
    }
}
