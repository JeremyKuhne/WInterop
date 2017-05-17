// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.Types;

namespace WInterop.Resources.Types
{
    public class IconHandle : HandleZeroIsInvalid
    {
        public IconHandle() : base(ownsHandle: true) { }

        public IconHandle(bool ownsHandle) : base(ownsHandle) { }

        public IconHandle(IntPtr handle, bool ownsHandle = false) : base(handle, ownsHandle) { }

        public static implicit operator IconHandle(IntPtr handle) => new IconHandle(handle);

        public static implicit operator IconHandle(IconId id) => ResourceMethods.LoadIcon(id);

        protected override bool ReleaseHandle()
        {
            return ResourceMethods.Imports.DestroyIcon(handle);
        }
    }
}
