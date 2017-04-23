// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Support.Buffers;

namespace WInterop.Handles.DataTypes
{
    public abstract class SafeHandleZeroIsInvalid : SafeHandle, IBuffer
    {
        // Why are HANDLE return values so inconsistent?
        // https://blogs.msdn.microsoft.com/oldnewthing/20040302-00/?p=40443

        protected SafeHandleZeroIsInvalid(bool ownsHandle)
            : base(invalidHandleValue: IntPtr.Zero, ownsHandle: ownsHandle)
        {
        }

        protected SafeHandleZeroIsInvalid(IntPtr handle, bool ownsHandle = false)
            : this(ownsHandle)
        {
            SetHandle(handle);
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}
