// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.DataTypes;

namespace WInterop.ProcessAndThreads.DataTypes
{
    /// <summary>
    /// Safe handle for a block of memory returned by GetEnvironmentStrings.
    /// </summary>
    public class SafeEnvironmentStringsHandle : SafeHandleZeroIsInvalid
    {
        public SafeEnvironmentStringsHandle() : base(ownsHandle: true)
        {
        }

        protected override bool ReleaseHandle()
        {
            ProcessMethods.Direct.FreeEnvironmentStringsW(handle);
            handle = IntPtr.Zero;
            return true;
        }
    }
}
