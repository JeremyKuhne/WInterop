// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.Types;
using WInterop.Support;

namespace WInterop.VolumeManagement.Types
{
    /// <summary>
    /// Safe handle for a block of memory returned by GetEnvironmentStrings.
    /// </summary>
    public class FindVolumeHandle : HandleZeroIsInvalid
    {
        public FindVolumeHandle() : base(ownsHandle: true)
        {
        }

        protected override bool ReleaseHandle()
        {
            if (!VolumeMethods.Imports.FindVolumeClose(handle))
            {
                throw Errors.GetIoExceptionForLastError();
            }

            handle = IntPtr.Zero;
            return true;
        }
    }
}
