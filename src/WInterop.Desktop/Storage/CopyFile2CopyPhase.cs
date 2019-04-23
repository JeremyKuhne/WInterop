// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// [COPYFILE2_COPY_PHASE]
    /// </summary>
    public enum CopyFile2CopyPhase
    {
        /// <summary>
        /// The copy had not yet started processing. [COPYFILE2_PHASE_NONE]
        /// </summary>
        None = 0,

        /// <summary>
        /// The source was being prepared including opening a handle to the source. This phase happens once per stream copy operation.
        /// [COPYFILE2_PHASE_PREPARE_SOURCE]
        /// </summary>
        PrepareSource = 1,

        /// <summary>
        /// The destination was being prepared including opening a handle to the destination. This phase happens once per stream copy operation.
        /// [COPYFILE2_PHASE_PREPARE_DEST]
        /// </summary>
        PrepareDestination = 2,

        /// <summary>
        /// The source file was being read. This phase happens one or more times per stream copy operation.
        /// [COPYFILE2_PHASE_READ_SOURCE]
        /// </summary>
        ReadSource = 3,

        /// <summary>
        /// The destination file was being written. This phase happens one or more times per stream copy operation.
        /// [COPYFILE2_PHASE_WRITE_DESTINATION]
        /// </summary>
        WriteDestination = 4,

        /// <summary>
        /// Both the source and destination were on the same remote server and the copy was being processed remotely.
        /// This phase happens once per stream copy operation. [COPYFILE2_PHASE_SERVER_COPY]
        /// </summary>
        ServerCopy = 5,

        /// <summary>
        /// The copy operation was processing symbolic links and/or reparse points. This phase happens once per file copy operation.
        /// [COPYFILE2_PHASE_NAMEGRAFT_COPY]
        /// </summary>
        NameGraftCopy = 6,
    }
}
