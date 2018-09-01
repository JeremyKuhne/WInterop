// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.SystemInformation
{
    /// <summary>
    /// [EXTENDED_NAME_FORMAT]
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms724268.aspx"/>
    /// </summary>
    public enum ExtendedNameFormat : uint
    {
        Unknown = 0,
        FullyQualifiedDomainName = 1,
        SamCompatible = 2,
        Display = 3,
        UniqueId = 6,
        Canonical = 7,
        UserPrincipal = 8,
        CanonicalEx = 9,
        ServicePrincipal = 10,
        DnsDomain = 12
    }
}
