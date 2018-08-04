﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles.Types;

namespace WInterop.Ipc.Types
{
    /// <summary>
    /// Safe handle for a mailslot
    /// </summary>
    public class SafeMailslotHandle : CloseHandle
    {
        public SafeMailslotHandle() : base() { }
    }
}
