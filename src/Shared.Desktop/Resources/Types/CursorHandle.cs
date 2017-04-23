// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.Types;

namespace WInterop.Resources.Types
{
    public class CursorHandle : SafeHandleZeroIsInvalid
    {
        public CursorHandle() : base(ownsHandle: true) { }

        public CursorHandle(bool ownsHandle) : base(ownsHandle) { }

        public CursorHandle(IntPtr handle, bool ownsHandle = false) : base(handle, ownsHandle) { }

        static public implicit operator CursorHandle(IntPtr handle)
        {
            return new CursorHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            return ResourceMethods.Imports.DestroyCursor(handle);
        }
    }
}
