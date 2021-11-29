// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security.Native;

/// <summary>
/// <see cref="https://docs.microsoft.com/en-us/windows/desktop/api/accctrl/ns-accctrl-_explicit_access_w"/>
///  [EXPLICIT_ACCESS_W]
/// </summary>
public struct EXPLICIT_ACCESS
{
    public AccessMask grfAccessPermissions;
    public AccessMode grfAccessMode;
    public Inheritance grfInheritance;
    public TRUSTEE Trustee;
}