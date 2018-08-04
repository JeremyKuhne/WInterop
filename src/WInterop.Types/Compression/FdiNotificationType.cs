// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Compression.Types
{
    public enum FdiNotificationType
    {
        fdintCABINET_INFO,              // General information about cabinet
        fdintPARTIAL_FILE,              // First file in cabinet is continuation
        fdintCOPY_FILE,                 // File to be copied
        fdintCLOSE_FILE_INFO,           // close the file, set relevant info
        fdintNEXT_CABINET,              // File continued to next cabinet
        fdintENUMERATE                  // Enumeration status
    }
}
