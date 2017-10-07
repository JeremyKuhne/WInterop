// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Called FileMode in .NET System.IO.
    /// </summary>
    /// <remarks>
    /// FileMode.Append is a .NET construct- it is OPEN_ALWAYS with FileIOPermissionAccess.Append.
    /// </remarks>
    public enum CreationDisposition : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx

        /// <summary>
        /// Create or fail if exists. [CREATE_NEW]
        /// </summary>
        CreateNew = 1,

        /// <summary>
        /// Create or overwrite. [CREATE_ALWAYS]
        /// </summary>
        CreateAlways = 2,

        /// <summary>
        /// Opens if exists, fails otherwise. [OPEN_EXISTING]
        /// </summary>
        OpenExisting = 3,

        /// <summary>
        /// Open if exists, creates otherwise. [OPEN_ALWAYS]
        /// </summary>
        OpenAlways = 4,

        /// <summary>
        /// Opens if exists and sets the size to zero. Fails if the file does not exist. [TRUNCATE_EXISTING]
        /// </summary>
        TruncateExisting = 5,
    }
}