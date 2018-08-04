// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Handles.Types
{
    public abstract class HandleZeroIsInvalid : BaseHandle
    {
        protected HandleZeroIsInvalid(bool ownsHandle)
            : base(ownsHandle)
        {
        }

        protected HandleZeroIsInvalid(IntPtr handle, bool ownsHandle = false)
            : base(handle, ownsHandle)
        {
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}
