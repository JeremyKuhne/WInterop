// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Shell
{
    /// <summary>
    ///  [KF_CATEGORY]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762512.aspx
    public enum KnownFolderCategory : uint
    {
        Virtual = 1,
        Fixed = 2,
        Common = 3,
        PerUser = 4
    }
}