// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Compression
{
    public enum FdiNotificationType
    {
        /// <summary>
        ///  General information about cabinet. [fdintCABINET_INFO]
        /// </summary>
        CabinetInfo,

        /// <summary>
        ///  First file in cabinet is continuation. [fdintPARTIAL_FILE]
        /// </summary>
        PartialFile,

        /// <summary>
        ///  File to be copied. [fdintCOPY_FILE]
        /// </summary>
        CopyFile,

        /// <summary>
        ///  Close the file, set relevant info. [fdintCLOSE_FILE_INFO]
        /// </summary>
        CloseFileInfo,

        /// <summary>
        ///  File continued to next cabinet. [fdintNEXT_CABINET]
        /// </summary>
        NextCabinet,

        /// <summary>
        ///  Enumeration status. [fdintENUMERATE]
        /// </summary>
        Enumerate
    }
}