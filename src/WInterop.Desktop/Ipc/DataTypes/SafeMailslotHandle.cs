// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles.DataTypes;

namespace WInterop.Ipc.DataTypes
{
    /// <summary>
    /// Safe handle for a mailslot
    /// </summary>
    public class SafeMailslotHandle : SafeCloseHandle
    {
        public SafeMailslotHandle() : base() { }
    }
}
