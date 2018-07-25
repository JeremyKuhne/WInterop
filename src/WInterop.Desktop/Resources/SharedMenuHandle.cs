// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Resources
{
    public class SharedMenuHandle : MenuHandle
    {
        public SharedMenuHandle() : base(ownsHandle: false) { }

        public SharedMenuHandle(bool ownsHandle) : base(ownsHandle) { }

        public SharedMenuHandle(IntPtr handle, bool ownsHandle = false) : base(handle, ownsHandle) { }

        public static implicit operator SharedMenuHandle(IntPtr handle) => new SharedMenuHandle(handle);

        protected override bool ReleaseHandle()
        {
            // Do nothing- we only want the creation methods to have
            // the destroy call.
            return true;
        }
    }
}
