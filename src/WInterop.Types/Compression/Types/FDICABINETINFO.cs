// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Compression.Types
{
    // https://msdn.microsoft.com/en-us/library/bb432269.aspx
    public struct FDICABINETINFO
    {
        /// <summary>
        /// Total length of the cabinet file.
        /// </summary>
        public int cbCabinet;

        /// <summary>
        /// Count of folders in the cabinet.
        /// </summary>
        public ushort cFolders;

        /// <summary>
        /// Count of files in the cabinet.
        /// </summary>
        public ushort cFiles;

        /// <summary>
        /// Identifier of the cabinet set.
        /// </summary>
        public ushort setID;

        /// <summary>
        /// Cabinet number in the set. (Zero based index)
        /// </summary>
        public ushort iCabinet;

        /// <summary>
        /// True if a reserved area is present.
        /// </summary>
        public BOOL fReserve;

        /// <summary>
        /// If true has a file that is continued from the previous cabinet.
        /// </summary>
        public BOOL hasprev;

        /// <summary>
        /// If true has a file that is continues in the next cabinet.
        /// </summary>
        public BOOL hasnext;
    }
}
