// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Handles.Desktop
{
    public class SafeLibraryHandle : SafeHandleZeroIsInvalid
    {
        public SafeLibraryHandle() : base(ownsHandle: true)
        {
        }

        protected override bool ReleaseHandle()
        {
            DynamicLinkLibrary.DesktopNativeMethods.FreeLibrary(handle);
            handle = IntPtr.Zero;
            return true;
        }
    }
}
