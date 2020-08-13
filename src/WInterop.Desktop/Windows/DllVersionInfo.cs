// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    /// <summary>
    ///  [DLLVERSIONINFO]
    /// </summary>
    public struct DllVersionInfo
    {
        /// <summary>
        ///  Size of the struct. [cbSize]
        /// </summary>
        public uint Size;

        /// <summary>
        ///  [dwMajorVersion]
        /// </summary>
        public uint MajorVersion;

        /// <summary>
        ///  [dwMinorVersion]
        /// </summary>
        public uint MinorVersion;

        /// <summary>
        ///  [dwBuildNumber]
        /// </summary>
        public uint BuildNumber;

        /// <summary>
        ///  [dwPlatformID]
        /// </summary>
        public uint PlatformID;
    }
}
