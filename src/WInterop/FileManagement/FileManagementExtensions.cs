// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement
{
    public static class FileManagementExtensions
    {
        public static bool AreInvalid(this FileAttributes attributes)
        {
            return attributes == FileAttributes.INVALID_FILE_ATTRIBUTES;
        }
    }
}
