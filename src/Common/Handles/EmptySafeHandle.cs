// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Handles
{
    using System;
    using System.Runtime.InteropServices;

    public sealed class EmptySafeHandle : SafeHandle
    {
        private static readonly SafeHandle s_Instance = new EmptySafeHandle();

        private EmptySafeHandle() : base(IntPtr.Zero, true) { }

        public static SafeHandle Instance
        {
            get { return s_Instance; }
        }

        public override bool IsInvalid
        {
            get { return true; }
        }

        protected override bool ReleaseHandle()
        {
            return true;
        }
    }
}
