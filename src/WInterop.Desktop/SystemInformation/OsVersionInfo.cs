// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.SystemInformation
{
    /// <summary>
    ///  [OSVERSIONINFOEXW]
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct OsVersionInfo
    {
        /// <summary>
        ///  [dwOSVersionInfoSize]
        /// </summary>
        public uint OSVersionInfoSize;

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
        ///  [dwPlatformId]
        /// </summary>
        public uint PlatformId;

        private fixed char _szCSDVersion[128];

        /// <summary>
        ///  wServicePackMajor[]
        /// </summary>
        public ushort ServicePackMajor;

        /// <summary>
        ///  [wServicePackMinor]
        /// </summary>
        public ushort ServicePackMinor;

        /// <summary>
        ///  [wSuiteMask]
        /// </summary>
        public ushort SuiteMask;

        /// <summary>
        ///  [wProductType]
        /// </summary>
        public byte ProductType;

        /// <summary>
        ///  [wReserved]
        /// </summary>
        public byte Reserved;

        /// <summary>
        ///  [szCSDVersion]
        /// </summary>
        public unsafe Span<char> CSDVersion
        {
            get { fixed (char* c = _szCSDVersion) { return new Span<char>(c, 128); } }
        }
    }
}
