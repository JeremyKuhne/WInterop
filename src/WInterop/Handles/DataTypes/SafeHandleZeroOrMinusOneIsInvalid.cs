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
    public abstract class SafeHandleZeroOrMinusOneIsInvalid : SafeHandle, IBuffer
    {
        // Why are HANDLE return values so inconsistent?
        // https://blogs.msdn.microsoft.com/oldnewthing/20040302-00/?p=40443

        protected SafeHandleZeroOrMinusOneIsInvalid(bool ownsHandle)
            : base(invalidHandleValue: IntPtr.Zero, ownsHandle: ownsHandle)
        {
        }

        public override bool IsInvalid
        {
            get
            {
                return handle == IntPtr.Zero || handle == new IntPtr(-1);
            }
        }
    }
}
