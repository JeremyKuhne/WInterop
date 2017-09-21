// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// For flagging a file handle for deletion using NtSetInformationFile.
    /// </summary>
    public struct FILE_DISPOSITION_INFORMATION
    {
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff545765.aspx

        public BOOLEAN DeleteFile;
    }
}
