// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security.Native;

/// <docs>https://docs.microsoft.com/windows/win32/api/winnt/ns-winnt-token_privileges</docs>
public struct TOKEN_PRIVILEGES
{
    public uint PrivilegeCount;

    // This is an ANYSIZE_ARRAY
    private readonly LuidAndAttributes _Privileges;

    public unsafe ReadOnlySpan<LuidAndAttributes> Privileges
    {
        get { fixed (LuidAndAttributes* p = &_Privileges) { return new ReadOnlySpan<LuidAndAttributes>(p, (int)PrivilegeCount); } }
    }
}