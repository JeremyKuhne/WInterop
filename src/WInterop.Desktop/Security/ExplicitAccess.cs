// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security;

/// <summary>
///  Access right
/// </summary>
public readonly unsafe struct ExplicitAccess
{
    public AccessMask Permissions { get; }
    public AccessMode Mode { get; }
    public Inheritance Inheritance { get; }
    public TrusteeType TrusteeType { get; }
    public string? TrusteeName { get; }

    public ExplicitAccess(EXPLICIT_ACCESS_W* access)
    {
        Permissions = new(access->grfAccessPermissions);
        Mode = (AccessMode)access->grfAccessMode;
        Inheritance = (Inheritance)access->grfInheritance;
        TrusteeType = (TrusteeType)access->Trustee.TrusteeType;
        TrusteeName = access->Trustee.TrusteeName();
    }
}