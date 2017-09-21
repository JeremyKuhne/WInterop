// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// For flagging a file handle for deletion using SetFileInformationByHandle and FILE_INFO_BY_HANDLE_CLASS.FileDispositionInfo.
    /// </summary>
    public struct FILE_DISPOSITION_INFO
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364221.aspx

        public BOOL DeleteFile;
    }
}
