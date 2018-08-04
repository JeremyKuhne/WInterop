// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Handles
{
    /// <summary>
    /// Safe handle for a Windows object manager symbolic link.
    /// </summary>
    public class SymbolicLinkObjectHandle : CloseHandle
    {
        public SymbolicLinkObjectHandle() : base() { }
    }
}
