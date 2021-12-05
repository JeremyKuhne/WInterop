// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security;

public readonly struct SidAndAttributes
{
    public SecurityIdentifier Sid { get; }
    public SidAttributes Attributes { get; }

    public SidAndAttributes(SecurityIdentifier sid, SidAttributes attributes)
    {
        Sid = sid;
        Attributes = attributes;
    }

    public static implicit operator SecurityIdentifier(SidAndAttributes info) => info.Sid;
}