// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Handles.Types
{
    /// <summary>
    /// Wrapper for handles that need closed via CloseHandle.
    /// </summary>
    public abstract class CloseHandle : HandleZeroOrMinusOneIsInvalid
    {
        public CloseHandle() : base(ownsHandle: true) { }

        public CloseHandle(IntPtr handle, bool ownsHandle = true)
            : base(handle, ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            Support.Internal.Imports.CloseHandle(handle);
            return true;
        }
    }
}
