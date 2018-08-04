﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Handles.Types
{
    /// <summary>
    /// Safe handle for a Windows object manager directory.
    /// </summary>
    public class DirectoryObjectHandle : CloseHandle
    {
        public DirectoryObjectHandle() : base() { }
    }
}