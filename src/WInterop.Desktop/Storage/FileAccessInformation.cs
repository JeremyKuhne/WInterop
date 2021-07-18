// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/hardware/ff545733.aspx">FILE_ACCESS_INFORMATION</a> structure.
    /// </summary>
    public struct FileAccessInformation
    {
        public FileAccessRights AccessFlags;
    }
}