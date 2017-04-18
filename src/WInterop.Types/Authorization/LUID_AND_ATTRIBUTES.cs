// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Authorization.DataTypes
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/aa379263.aspx">LUID_AND_ATTRIBUTES</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct LUID_AND_ATTRIBUTES
    {
        public LUID Luid;
        public uint Attributes;
    }
}
