// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Handles;

namespace WInterop.Com
{
    public class SafeComHandle : HandleZeroOrMinusOneIsInvalid
    {
        public SafeComHandle() : this(ownsHandle: true)
        {
        }

        public SafeComHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            if (!IsInvalid)
                Marshal.FreeCoTaskMem(handle);

            return true;
        }
    }
}
