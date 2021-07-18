// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Handles
{
    /// <summary>
    ///  Wrapper for an event handle
    /// </summary>
    public class EventHandle : CloseHandle
    {
        public EventHandle() : base() { }

        public EventHandle(IntPtr handle, bool ownsHandle = true) : base(handle, ownsHandle)
        {
        }
    }
}