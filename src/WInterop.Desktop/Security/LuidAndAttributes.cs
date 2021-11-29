// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security;

/// <summary>
/// <a href="https://msdn.microsoft.com/en-us/library/aa379263.aspx">LUID_AND_ATTRIBUTES</a> structure.
///  [LUID_AND_ATTRIBUTES]
/// </summary>
public readonly struct LuidAndAttributes
{
    public readonly LUID Luid;
    public readonly PrivilegeAttributes Attributes;

    public LuidAndAttributes(LUID luid, PrivilegeAttributes attributes = default)
    {
        Luid = luid;
        Attributes = attributes;
    }

    public static implicit operator LuidAndAttributes(LUID luid) => new LuidAndAttributes(luid);
}