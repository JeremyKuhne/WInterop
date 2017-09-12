// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// File creation disposition when calling directly to NT apis.
    /// </summary>
    public enum CreateDisposition : uint
    {
        /// <summary>
        /// Default. Replace or create. Deletes existing file instead of overwriting.
        /// </summary>
        /// <remarks>
        /// As this potentially deletes it requires that DesiredAccess must include Delete.
        /// This has no equivalent in CreateFile.
        /// </remarks>
        Supersede = 0,

        /// <summary>
        /// Open if exists or fail if doesn't exist. Equivalent to OpenExisting.
        /// </summary>
        /// <remarks>
        /// TruncateExisting also uses Open and then manually truncates the file
        /// by calling NtSetInformationFile with FileAllocationInformation and an
        /// allocation size of 0.
        /// </remarks>
        Open = 1,

        /// <summary>
        /// Create if doesn't exist or fail if does exist. Equivalent to CreateNew.
        /// </summary>
        Create = 2,

        /// <summary>
        /// Open if exists or create if doesn't exist. Equivalent to OpenAlways.
        /// </summary>
        OpenIf = 3,

        /// <summary>
        /// Open and overwrite if exists or fail if doesn't exist.
        /// </summary>
        /// <remarks>
        /// This has no equivalent in CreateFile.
        /// </remarks>
        Overwrite = 4,

        /// <summary>
        /// Open and overwrite if exists or create if doesn't exist. Euivalent to
        /// CreateAlways.
        /// </summary>
        OverwriteIf = 5
    }
}
