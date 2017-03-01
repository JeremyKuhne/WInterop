// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.DataTypes;

namespace WInterop.SecurityManagement.DataTypes
{
    public class SafeLsaHandle : SafeHandleZeroIsInvalid
    {
        public SafeLsaHandle() : base(ownsHandle: true) { }

        public SafeLsaHandle(IntPtr handle, bool ownsHandle = true) : base(ownsHandle)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            SecurityDesktopMethods.LsaClose(handle);
            return true;
        }
    }
}
