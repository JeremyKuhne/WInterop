// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Compression
{
    /// <summary>
    ///  [FDICABINETINFO]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/bb432269.aspx
    public struct CabinetInfo
    {
        /// <summary>
        ///  Total length of the cabinet file. [cbCabinet]
        /// </summary>
        public int CountBytes;

        /// <summary>
        ///  Count of folders in the cabinet. [cFolders]
        /// </summary>
        public ushort CountFolders;

        /// <summary>
        ///  Count of files in the cabinet. [cFiles]
        /// </summary>
        public ushort CountFiles;

        /// <summary>
        ///  Identifier of the cabinet set.
        /// </summary>
        public ushort SetId;

        /// <summary>
        ///  Cabinet number in the set. (Zero based index) [iCabinet]
        /// </summary>
        public ushort Index;

        /// <summary>
        ///  True if a reserved area is present. [fReserve]
        /// </summary>
        public IntBoolean ReserveAreaPresent;

        /// <summary>
        ///  If true has a file that is continued from the previous cabinet.
        /// </summary>
        public IntBoolean HasPrevious;

        /// <summary>
        ///  If true has a file that is continues in the next cabinet.
        /// </summary>
        public IntBoolean HasNext;
    }
}
