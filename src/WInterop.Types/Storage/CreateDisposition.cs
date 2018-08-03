// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// File creation disposition when calling directly to NT apis.
    /// </summary>
    public enum CreateDisposition : uint
    {
        /// <summary>
        /// Default. Replace or create. Deletes existing file instead of overwriting.
        /// [FILE_SUPERSEDE]
        /// </summary>
        /// <remarks>
        /// As this potentially deletes it requires that DesiredAccess must include Delete.
        /// This has no equivalent in CreateFile.
        /// </remarks>
        Supersede = 0,

        /// <summary>
        /// Open if exists or fail if doesn't exist. Equivalent to <see cref="CreationDisposition.OpenExisting"/>.
        /// [FILE_OPEN]
        /// </summary>
        /// <remarks>
        /// TruncateExisting also uses Open and then manually truncates the file
        /// by calling NtSetInformationFile with FileAllocationInformation and an
        /// allocation size of 0.
        /// </remarks>
        Open = 1,

        /// <summary>
        /// Create if doesn't exist or fail if does exist. Equivalent to <see cref="CreationDisposition.CreateNew"/>.
        /// [FILE_CREATE]
        /// </summary>
        Create = 2,

        /// <summary>
        /// Open if exists or create if doesn't exist. Equivalent to <see cref="CreationDisposition.OpenAlways"/>.
        /// [FILE_OPEN_IF]
        /// </summary>
        OpenIf = 3,

        /// <summary>
        /// Open and overwrite if exists or fail if doesn't exist. Equivalent to <see cref="CreationDisposition.TruncateExisting"/>
        /// [FILE_OVERWRITE]
        /// </summary>
        Overwrite = 4,

        /// <summary>
        /// Open and overwrite if exists or create if doesn't exist. Equivalent to <see cref="CreationDisposition.CreateAlways"/>.
        /// [FILE_OVERWRITE_IF]
        /// </summary>
        OverwriteIf = 5
    }
}
