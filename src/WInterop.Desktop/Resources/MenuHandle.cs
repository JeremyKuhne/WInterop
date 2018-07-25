// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.Types;

namespace WInterop.Resources
{
    public class MenuHandle : HandleZeroOrMinusOneIsInvalid
    {
        public static MenuHandle Null = new MenuHandle(IntPtr.Zero);

        public MenuHandle() : base(ownsHandle: true) { }

        public MenuHandle(bool ownsHandle) : base(ownsHandle) { }

        public MenuHandle(IntPtr handle, bool ownsHandle = false) : base(handle, ownsHandle) { }

        public static implicit operator MenuHandle(IntPtr handle) => new MenuHandle(handle);

        protected override bool ReleaseHandle()
        {
            return ResourceMethods.Imports.DestroyMenu(handle);
        }
    }
}
