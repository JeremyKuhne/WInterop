// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization.Types
{
    /// <summary>
    /// Specifies the type of <see cref="SID"/>.
    /// </summary>
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/aa379601.aspx
    /// </remarks>
    public enum SID_NAME_USE : uint
    {
        SidTypeUser = 1,
        SidTypeGroup,
        SidTypeDomain,
        SidTypeAlias,
        SidTypeWellKnownGroup,
        SidTypeDeletedAccount,
        SidTypeInvalid,
        SidTypeUnknown,
        SidTypeComputer,
        SidTypeLabel
    }
}
