// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles;

namespace WInterop.Security
{
    public class LsaHandle : HandleZeroIsInvalid
    {
        public LsaHandle() : base(ownsHandle: true) { }

        public LsaHandle(IntPtr handle, bool ownsHandle = true) : base(ownsHandle)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            Security.LsaClose(handle);
            return true;
        }
    }
}
