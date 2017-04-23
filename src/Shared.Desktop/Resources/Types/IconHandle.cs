// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.Types;

namespace WInterop.Resources.Types
{
    public class IconHandle : SafeHandleZeroIsInvalid
    {
        public IconHandle() : base(ownsHandle: true) { }

        public IconHandle(bool ownsHandle) : base(ownsHandle) { }

        public IconHandle(IntPtr handle, bool ownsHandle = false) : base(handle, ownsHandle) { }

        static public implicit operator IconHandle(IntPtr handle) => new IconHandle(handle);

        protected override bool ReleaseHandle()
        {
            return ResourceMethods.Direct.DestroyIcon(handle);
        }
    }
}
