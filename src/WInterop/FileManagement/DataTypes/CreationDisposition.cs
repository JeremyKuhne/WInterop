// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace WInterop.FileManagement
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
    /// <summary>
    /// Called FileMode in .NET System.IO.
    /// </summary>
    /// <remarks>
    /// FileMode.Append is a .NET construct- it is OPEN_ALWAYS with FileIOPermissionAccess.Append.
    /// </remarks>
    public enum CreationDisposition : uint
    {
        /// <summary>
        /// Create or fail if exists.
        /// </summary>
        CREATE_NEW = 1,

        /// <summary>
        /// Create or overwrite.
        /// </summary>
        CREATE_ALWAYS = 2,

        /// <summary>
        /// Opens if exists, fails otherwise.
        /// </summary>
        OPEN_EXISTING = 3,

        /// <summary>
        /// Open if exists, creates otherwise.
        /// </summary>
        OPEN_ALWAYS = 4,

        /// <summary>
        /// Opens if exists and sets the size to zero. Fails if the file does not exist.
        /// </summary>
        TRUNCATE_EXISTING = 5,
    }
}