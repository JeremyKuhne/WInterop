// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Globalization
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/dd319087.aspx
    /// <summary>
    /// [NLSVERSIONINFOEX]
    /// </summary>
    public struct NlsVersionInfoExtended
    {
        public uint NLSVersionInfoSize;
        public uint NLSVersion;
        public uint DefinedVersion;
        public uint EffectiveId;
        public Guid CustomVersion;
    }
}
