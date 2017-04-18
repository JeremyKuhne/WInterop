// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.DataTypes;

namespace WInterop.ProcessAndThreads.DataTypes
{
    /// <summary>
    /// Safe handle for a process.
    /// </summary>
    public class SafeProcessHandle : SafeCloseHandle
    {
        public SafeProcessHandle() : base() { }

        public SafeProcessHandle(IntPtr handle, bool ownsHandle = true) : base(handle, ownsHandle)
        {
            SetHandle(handle);
        }

        static public implicit operator ProcessHandle(SafeProcessHandle handle)
        {
            return new ProcessHandle(handle.handle);
        }
    }
}
