// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell;

/// <summary>
///  [KF_DEFINITION_FLAGS]
/// </summary>
// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762513.aspx
[Flags]
public enum KnownFolderDefinitionFlags : uint
{
    /// <summary>
    ///  [KFDF_LOCAL_REDIRECT_ONLY]
    /// </summary>
    LocalRedirectOnly = 0x00000002,

    /// <summary>
    ///  [KFDF_ROAMABLE]
    /// </summary>
    Roamable = 0x00000004,

    /// <summary>
    ///  [KFDF_PRECREATE]
    /// </summary>
    Precreate = 0x00000008,

    /// <summary>
    ///  [KFDF_STREAM]
    /// </summary>
    Stream = 0x00000010,

    /// <summary>
    ///  [KFDF_PUBLISHEXPANDEDPATH]
    /// </summary>
    PublicExpandPath = 0x00000020,

    /// <summary>
    ///  [KFDF_NO_REDIRECT_UI]
    /// </summary>
    NoRedirectUI = 0x00000040
}