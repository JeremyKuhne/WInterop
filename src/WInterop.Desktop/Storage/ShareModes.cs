// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage
{
    /// <summary>
    ///  Equivalent to <see cref="System.IO.FileShare"/>.
    /// </summary>
    /// <remarks>
    ///  System.IO.FileShare contains an additional flag, <see cref="System.IO.FileShare.Inheritable"/>, that has
    ///  nothing to do with Windows. It is used to decide how to create the SECURITY_ATTRIBUTES for CreateFile.
    ///  SECURITY_ATTRIBUTES.bInheritHandle will be set to 1 if <see cref="System.IO.FileShare.Inheritable"/> is set.
    /// </remarks>
    [Flags]
    public enum ShareModes : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363874.aspx

        /// <summary>
        ///  Allow other read handles to be opened. (FILE_SHARE_READ)
        /// </summary>
        /// <remarks>
        ///  Equivalent to <see cref="System.IO.FileShare.Read"/>
        /// </remarks>
        Read = 0x00000001,

        /// <summary>
        ///  Allow other write handles to be opened. (FILE_SHARE_WRITE)
        /// </summary>
        /// <remarks>
        ///  Equivalent to <see cref="System.IO.FileShare.Write"/>
        /// </remarks>
        Write = 0x00000002,

        /// <summary>
        ///  Allow others to delete the file.
        /// </summary>
        /// <remarks>
        ///  Equivalent to <see cref="System.IO.FileShare.Delete"/>
        /// </remarks>
        Delete = 0x00000004,

        /// <summary>
        ///  Not actually defined in Windows, for convenience.
        /// </summary>
        /// <remarks>
        ///  Equivalent to <see cref="System.IO.FileShare.ReadWrite"/>
        /// </remarks>
        ReadWrite = Read | Write,

        /// <summary>
        ///  Not actually defined in Windows, for convenience.
        /// </summary>
        All = Read | Write | Delete
    }
}
