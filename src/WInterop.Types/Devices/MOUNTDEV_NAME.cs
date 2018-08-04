// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Devices
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff562256.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MOUNTDEV_NAME
    {
        /// <summary>
        /// Name length, in bytes.
        /// </summary>
        public ushort NameLength;

        private TrailingString _Name;
        public ReadOnlySpan<char> Name => _Name.GetBuffer(NameLength);
    }
}
