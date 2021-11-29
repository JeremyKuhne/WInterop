// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security.Native;

/// <summary>
///  <see cref="https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-security_descriptor"/>
/// </summary>
public unsafe readonly struct SECURITY_DESCRIPTOR
{
    public readonly byte Revision;
    public readonly byte Sbz1;
    public readonly SECURITY_DESCRIPTOR_CONTROL Control;

    /// <summary>
    ///  Primary owner.
    /// </summary>
    public readonly SID* Owner;

    /// <summary>
    ///  Primary group.
    /// </summary>
    public readonly SID* Group;

    /// <summary>
    ///  Audit rights, if any.
    /// </summary>
    public readonly ACL* Sacl;

    /// <summary>
    ///  Access rights, if any.
    /// </summary>
    public readonly ACL* Dacl;
}