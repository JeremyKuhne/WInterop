// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    /// <summary>
    ///  [LASTINPUTINFO]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646272.aspx
    public struct LastInputInfo
    {
        /// <summary>
        ///  Size of the struct in bytes.
        /// </summary>
        public uint Size;

        /// <summary>
        ///  Tick count when the last input was recieved.
        /// </summary>
        public uint Time;
    }
}
