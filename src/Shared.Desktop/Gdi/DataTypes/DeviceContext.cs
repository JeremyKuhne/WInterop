// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.DataTypes;

namespace WInterop.Gdi.DataTypes
{
    /// <summary>
    /// DeviceContext handle (HDC)
    /// </summary>
    public class DeviceContext : SafeHandleZeroIsInvalid
    {
        public DeviceContext() : base(ownsHandle: true) { }

        public DeviceContext(IntPtr handle, bool ownsHandle = true) : base(ownsHandle)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            return GdiMethods.Direct.DeleteDC(handle);
        }
    }
}
