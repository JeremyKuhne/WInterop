// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles.DataTypes;

namespace WInterop.ProcessAndThreads.DataTypes
{
    /// <summary>
    /// Safe handle for a thread.
    /// </summary>
    public class SafeThreadHandle : SafeCloseHandle
    {
        public SafeThreadHandle() : base() { }
    }
}
