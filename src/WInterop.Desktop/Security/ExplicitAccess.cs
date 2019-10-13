// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Security.Native;

namespace WInterop.Security
{
    /// <summary>
    /// Access right
    /// </summary>
    public unsafe readonly struct ExplicitAccess
    {
        public AccessMask Permissions { get; }
        public AccessMode Mode { get; }
        public Inheritance Inheritance { get; }
        public TrusteeType TrusteeType { get; }
        public string? TrusteeName { get; }

        public ExplicitAccess(EXPLICIT_ACCESS* access)
        {
            Permissions = access->grfAccessPermissions;
            Mode = access->grfAccessMode;
            Inheritance = access->grfInheritance;
            TrusteeType = access->Trustee.TrusteeType;
            TrusteeName = access->Trustee.TrusteeName;
        }
    }
}
