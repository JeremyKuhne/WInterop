// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Handles
{
    public sealed class SafeFindHandle : SafeHandleZeroIsInvalid
    {
        public SafeFindHandle() : base(true) { }

        override protected bool ReleaseHandle()
        {
            return NativeMethods.FileManagement.Direct.FindClose(handle);
        }
    }
}
