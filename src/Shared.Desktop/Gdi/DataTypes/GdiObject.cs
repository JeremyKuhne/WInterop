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
    /// GDI object handle (HGDIOBJ)
    /// </summary>
    public class GdiObject : SafeHandleZeroIsInvalid
    {
        public GdiObject() : base(ownsHandle: true) { }

        public GdiObject(IntPtr handle, bool ownsHandle = true) : base(handle, ownsHandle) { }

        protected override bool ReleaseHandle()
        {
            return GdiMethods.Direct.DeleteObject(handle);
        }
    }
}
