﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Handles
{
    /// <summary>
    /// Wrapper for handles that need closed via CloseHandle.
    /// </summary>
    public abstract class SafeCloseHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeCloseHandle() : base(ownsHandle: true) { }

        public SafeCloseHandle(IntPtr handle) : base(ownsHandle: true)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            NativeMethods.Handles.CloseHandle(handle);
            return true;
        }
    }
}