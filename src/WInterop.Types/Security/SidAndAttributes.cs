// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    public readonly struct SidAndAttributes
    {
        public SID Sid { get; }
        public SidAttributes Attributes { get; }

        public SidAndAttributes(SID sid, SidAttributes attributes)
        {
            Sid = sid;
            Attributes = attributes;
        }

        public static implicit operator SID(SidAndAttributes info) => info.Sid;
    }
}
