// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Handles.DataTypes
{
    /// <summary>
    /// Wrapper for an event handle
    /// </summary>
    public class EventHandle : SafeCloseHandle
    {
        public EventHandle() : base() { }

        public EventHandle(IntPtr handle, bool ownsHandle = true) : base(handle, ownsHandle)
        {
        }
    }
}
