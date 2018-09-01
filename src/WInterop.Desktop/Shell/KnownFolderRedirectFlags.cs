// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    /// <summary>
    /// [KF_REDIRECT_FLAGS]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762515.aspx
    [Flags]
    public enum KnownFolderRedirectFlags : uint
    {
        UserExclusive = 0x00000001,
        CopySourceDacl = 0x00000002,
        OwnerUser = 0x00000004,
        SetOwnerExplicit = 0x00000008,
        CheckOnly = 0x00000010,
        WithUI = 0x00000020,
        Unpin = 0x00000040,
        Pin = 0x00000080,
        CopyContents = 0x00000200,
        DeleteSourceContents = 0x00000400,
        ExcludeAllKnownSubfolders = 0x00000800
    }
}
