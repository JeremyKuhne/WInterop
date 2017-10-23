// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DeviceManagement.Types
{
    public enum ControlCodeAccess : byte
    {
        /// <summary>
        /// Any or special access. [FILE_ANY_ACCESS]
        /// </summary>
        /// <remarks>
        /// This is also defined (in ntddk.h) as FILE_SPECIAL_ACCESS with a comment that
        /// file systems can add additional access checks for IO and FS controls that use
        /// this value.
        /// https://msdn.microsoft.com/en-us/library/windows/hardware/dn614603.aspx
        /// </remarks>
        Any = 0x00,

        /// <summary>
        /// Read access. [FILE_READ_ACCESS]
        /// </summary>
        Read = 0x01,

        /// <summary>
        /// Write access. [FILE_WRITE_ACCES]
        /// </summary>
        Write = 0x02,

        /// <summary>
        /// Both read and write access required.
        /// </summary>
        /// <remarks>
        /// Not defined in the DDK, but useful to do so as there are really only 4 states
        /// and the first "flag" is 0 (default).
        /// </remarks>
        ReadWrite = 0x03
    }
}
