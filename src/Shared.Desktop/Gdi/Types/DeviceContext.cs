// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.Types;

namespace WInterop.Gdi.Types
{
    /// <summary>
    /// DeviceContext handle (HDC)
    /// </summary>
    public class DeviceContext : SafeHandleZeroIsInvalid
    {
        public DeviceContext() : base(ownsHandle: true) { }

        public DeviceContext(IntPtr handle, bool ownsHandle) : base(ownsHandle)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            return GdiMethods.Imports.DeleteDC(handle);
        }
    }
}
